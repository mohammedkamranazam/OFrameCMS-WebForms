using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class DrivesComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            const string emptyPanelTag = "<div class='EmptyDrives'><img class='EmptyDrivesImage' src='{0}' /></div>";
            const string drivesPanel = "<div class='DrivesComponent'>{0}<div class='Clear'></div></div>";

            StringBuilder sb = new StringBuilder();

            var locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
            var direction = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureDirectionCookieKey);

            TitleH1.Style.Add("direction", direction);
            TitleLiteral.Text = LanguageHelper.GetKey("DrivesTitle", locale);

            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            CurrentCountHiddenField.Value = toFetchCount.ToString();

            using (var context = new GalleryEntities())
            {
                var drivesQuery = await (from set in context.GY_Drives
                                         where set.Hide == false && set.Locale == locale
                                         select set).ToListAsync();

                if (drivesQuery.Any())
                {
                    foreach (var drive in drivesQuery.OrderByDescending(c => c.DriveID).Take(toFetchCount))
                    {
                        sb.Append(DrivesBL.GetDrivesHTML(drive, direction, Page));
                    }

                    DrivesLiteral.Text = string.Format(drivesPanel, sb);
                }
                else
                {
                    DrivesLiteral.Text = string.Format(emptyPanelTag, ResolveClientUrl(AppConfig.EmptyPanelImage));
                }

                UpdateLoadMoreControls(toFetchCount, drivesQuery.Count(), locale);
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("DrivesPage"));

                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }
        }
    }
}