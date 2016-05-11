using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class DownloadsComponent : UserControl
    {
        private string ArrangeBy()
        {
            return String.Format("{0} {1}", SortByHiddenField.Value, OrderByHiddenField.Value);
        }

        private string BuildFiles(GY_Drives driveQuery, long? folderID, string locale, string direction, GalleryEntities context)
        {
            StringBuilder sb = new StringBuilder();

            var filesQuery = (from set in driveQuery.GY_Files
                              where set.Hide == false && set.Locale == locale && set.FolderID == folderID
                              select set).AsQueryable<GY_Files>().OrderBy(ArrangeBy()).ToList();

            filesQuery.ForEach(file =>
            {
                sb.Append(FilesBL.GetFileHTML(file, direction, Page));
            });

            return sb.ToString();
        }

        private string BuildFolders(GY_Drives driveQuery, long? folderID, string locale, string direction)
        {
            StringBuilder sb = new StringBuilder();

            var foldersQuery = (from set in driveQuery.GY_Folders
                                where set.Hide == false && set.Locale == locale && set.ParentFolderID == folderID
                                select set).ToList();

            foldersQuery.ForEach(folder =>
            {
                sb.Append(FoldersBL.GetFolderHTML(folder, direction, Page));
            });

            return sb.ToString();
        }

        private async Task LoadData()
        {
            var driveID = DataParser.IntParse(Request.QueryString["DriveID"]);
            var folderID = DataParser.NullableIntParse(Request.QueryString["FolderID"]);

            using (var context = new GalleryEntities())
            {
                var driveQuery = await DrivesBL.GetObjectByIDAsync(driveID, context);

                if (driveQuery != null)
                {
                    var seoEntity = new SEO();
                    seoEntity.Title = driveQuery.Title;
                    seoEntity.Description = driveQuery.Description;
                    seoEntity.Keywords = driveQuery.Description;

                    SortByDropDownList.Enabled = true;
                    OrderByDropDownList.Enabled = true;

                    Utilities.SetPageSEO(Page, seoEntity);

                    StringBuilder sb = new StringBuilder();
                    const string emptyPanel = "<div class='EmptyDownloads'><img class='EmptyDownloadsImage' src='{0}' /></div>";
                    const string downloadsPanel = "<div class='DownloadsPanel'>{0}<div class='Clear'></div></div>";

                    string locale = driveQuery.Locale;
                    string direction = LanguageHelper.GetLocaleDirection(locale);

                    var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
                    var toFetchCount = currentCount + 20;
                    CurrentCountHiddenField.Value = toFetchCount.ToString();

                    TitleH1.Style.Add("direction", direction);
                    TitleLiteral.Text = string.Format("{0} {1}", driveQuery.Title, LanguageHelper.GetKey("DownloadsTitle", locale));

                    sb.Append(BuildFolders(driveQuery, folderID, locale, direction));

                    sb.Append(BuildFiles(driveQuery, folderID, locale, direction, context));

                    DownloadsLiteral.Text = string.Format(downloadsPanel, sb);

                    if (string.IsNullOrWhiteSpace(sb.ToString()))
                    {
                        DownloadsLiteral.Text = string.Format(emptyPanel, ResolveClientUrl(AppConfig.EmptyPanelImage));

                        SortByDropDownList.Enabled = false;
                        OrderByDropDownList.Enabled = false;
                    }

                    //UpdateLoadMoreControls(toFetchCount, driveQuery.GY_Folders.Count(), locale);
                }
            }
        }

        private void UpdateLoadMoreControls(int toFetchCount, int totalItemsCount, string locale)
        {
            if (totalItemsCount == 0)
            {
                LoadMoreButton.Visible = false;
            }

            if (toFetchCount >= totalItemsCount)
            {
                LoadMoreButton.Enabled = false;
                LoadMoreButton.CssClass = "LoadMoreButtonDisabled";
                LoadMoreButton.Text = LanguageHelper.GetKey("NoMoreItemsToDisplay", locale);
            }
        }

        protected async void LoadMoreButton_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        protected async void OrderByDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CookiesHelper.SetCookie("__DownloadsOrderBy_", OrderByDropDownList.SelectedValue, Utilities.DateTimeNow().AddDays(1));
            OrderByHiddenField.Value = OrderByDropDownList.SelectedValue;

            await LoadData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["DriveID"]))
                {
                    var now = Utilities.DateTimeNow();

                    SortByDropDownList.Items.Add(new ListItem("Date", "AddedOn"));
                    SortByDropDownList.Items.Add(new ListItem("Name", "Title"));
                    SortByDropDownList.Items.Add(new ListItem("Size", "Size"));
                    SortByDropDownList.Items.Add(new ListItem("Type", "FileTypeID"));

                    OrderByDropDownList.Items.Add(new ListItem("Ascending", "ASC"));
                    OrderByDropDownList.Items.Add(new ListItem("Descending", "DESC"));

                    if (string.IsNullOrWhiteSpace(CookiesHelper.GetCookie("__DownloadsSortBy_")))
                    {
                        SortByHiddenField.Value = SortByDropDownList.SelectedValue;
                        CookiesHelper.SetCookie("__DownloadsSortBy_", SortByDropDownList.SelectedValue, now.AddDays(1));
                    }
                    else
                    {
                        SortByHiddenField.Value = CookiesHelper.GetCookie("__DownloadsSortBy_");
                        SortByDropDownList.Items.FindByValue(SortByHiddenField.Value).Selected = true;
                    }

                    if (string.IsNullOrWhiteSpace(CookiesHelper.GetCookie("__DownloadsOrderBy_")))
                    {
                        OrderByHiddenField.Value = OrderByDropDownList.SelectedValue;
                        CookiesHelper.SetCookie("__DownloadsOrderBy_", OrderByDropDownList.SelectedValue, now.AddDays(1));
                    }
                    else
                    {
                        OrderByHiddenField.Value = CookiesHelper.GetCookie("__DownloadsOrderBy_");
                        OrderByDropDownList.Items.FindByValue(OrderByHiddenField.Value).Selected = true;
                    }

                    this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                    {
                        await LoadData();
                    }));
                }
                else
                {
                    Response.Redirect("~/Drives.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected async void SortByDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CookiesHelper.SetCookie("__DownloadsSortBy_", SortByDropDownList.SelectedValue, Utilities.DateTimeNow().AddDays(1));
            SortByHiddenField.Value = SortByDropDownList.SelectedValue;

            await LoadData();
        }
    }
}