using OWDARO.BLL.GalleryBLL;
using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class VideoSetComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                var videoSetID = DataParser.IntParse(Request.QueryString["VideoSetID"]);

                var videoSetQuery = await VideoSetBL.GetObjectByIDAsync(videoSetID, context);

                if (videoSetQuery != null)
                {
                    string locale = videoSetQuery.Locale;
                    string direction = LanguageHelper.GetLocaleDirection(locale);

                    TitleLiteral.Text = videoSetQuery.Title;
                    TitleH1.Style.Add("direction", direction);

                    LoadMoreButton.Text = LanguageHelper.GetKey("LoadMore", locale);

                    var seoEntity = new SEO();
                    seoEntity.Title = videoSetQuery.Title;
                    seoEntity.Description = videoSetQuery.Description;
                    seoEntity.Keywords = videoSetQuery.Tags;

                    Utilities.SetPageSEO(Page, seoEntity);

                    await LoadSetVideos(context, videoSetQuery, direction, locale);
                }
                else
                {
                    Response.Redirect("~/VideoCategories.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        private async Task LoadSetVideos(GalleryEntities context, GY_VideoSet videoSet, string direction, string locale)
        {
            const string emptyPanelTag = "<div class='EmptyVideoSet'><img class='EmptyVideoSetImage' src='{0}' /></div>";
            const string panelTag = "<div class='VideoSetPanel'>{0}<div class='Clear'></div></div>";

            StringBuilder sb = new StringBuilder();

            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            CurrentCountHiddenField.Value = toFetchCount.ToString();

            var setVideos = (from set in videoSet.GY_Videos
                             where set.Hide == false && set.Locale == locale
                             select set);

            if (setVideos.Any())
            {
                foreach (GY_Videos video in setVideos.OrderByDescending(c => c.AddedOn).Take(toFetchCount))
                {
                    sb.Append(await VideosBL.GetVideoHTML(video, direction, context, Page));
                }

                VideosLiteral.Text = string.Format(panelTag, sb);
            }
            else
            {
                VideosLiteral.Text = string.Format(emptyPanelTag, ResolveClientUrl(AppConfig.EmptyPanelImage));
            }

            UpdateLoadMoreControls(toFetchCount, setVideos.Count(), locale);
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
            using (var context = new GalleryEntities())
            {
                var videoSetID = DataParser.IntParse(Request.QueryString["VideoSetID"]);

                var videoSetQuery = await VideoSetBL.GetObjectByIDAsync(videoSetID, context);

                string locale = videoSetQuery.Locale;
                string direction = LanguageHelper.GetLocaleDirection(locale);

                await LoadSetVideos(context, videoSetQuery, direction, locale);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["VideoSetID"]))
                {
                    this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                    {
                        await LoadData();
                    }));
                }
                else
                {
                    Response.Redirect("~/VideoCategories.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }
}