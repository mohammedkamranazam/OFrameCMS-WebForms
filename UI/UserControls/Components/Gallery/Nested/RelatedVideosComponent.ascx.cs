using OWDARO.BLL.GalleryBLL;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Gallery.Nested
{
    public partial class RelatedVideosComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                var videoID = DataParser.IntParse(Request.QueryString["VideoID"]);

                var videoQuery = await VideosBL.GetObjectByIDAsync(videoID, context);

                if (videoQuery != null)
                {
                    string locale = videoQuery.Locale;
                    string direction = LanguageHelper.GetLocaleDirection(locale);

                    TitleLiteral.Text = LanguageHelper.GetKey("RelatedVideos", locale);
                    TitleH1.Style.Add("direction", direction);

                    LoadMoreButton.Text = LanguageHelper.GetKey("LoadMore", locale);

                    await LoadVideos(context, videoQuery, direction, locale);
                }
                else
                {
                    Response.Redirect("~/VideoCategories.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        private async Task LoadVideos(GalleryEntities context, GY_Videos videoQuery, string direction, string locale)
        {
            const string panelTag = "<div class='RelatedVideosPanel'>{0}<div class='Clear'></div></div>";

            StringBuilder sb = new StringBuilder();

            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            CurrentCountHiddenField.Value = toFetchCount.ToString();

            var relatedVideosQuery = (from set in videoQuery.GY_VideoCategories.GY_Videos
                                      where set.VideoID != videoQuery.VideoID && set.Hide == false && set.Locale == locale
                                      select set).ToList();

            if (relatedVideosQuery.Any())
            {
                foreach (GY_Videos video in relatedVideosQuery.OrderBy(c => c.Title).Take(toFetchCount))
                {
                    sb.Append(await VideosBL.GetVideoHTML(video, direction, context, Page));
                }

                VideosLiteral.Text = string.Format(panelTag, sb);
            }
            else
            {
                this.Visible = false;
            }

            UpdateLoadMoreControls(toFetchCount, relatedVideosQuery.Count(), locale);
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
                var videoID = DataParser.IntParse(Request.QueryString["VideoID"]);

                var videoQuery = await VideosBL.GetObjectByIDAsync(videoID, context);

                string locale = videoQuery.Locale;
                string direction = LanguageHelper.GetLocaleDirection(locale);

                await LoadVideos(context, videoQuery, direction, locale);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["VideoID"]))
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