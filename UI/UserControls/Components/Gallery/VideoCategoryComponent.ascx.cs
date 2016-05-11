using OWDARO.BLL.GalleryBLL;
using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class VideoCategoryComponent : System.Web.UI.UserControl
    {
        private async Task LoadCategoryVideos(GalleryEntities context, GY_VideoCategories videoCategory, string direction, string locale)
        {
            const string emptyPanelTag = "<div class='CategoryVideosEmptyPanel'><img class='EmptyCategoryVideosImage' src='{0}' /></div>";
            const string panelTag = "<div class='CategoryVideosPanel'>{0}<div class='Clear'></div></div>";

            StringBuilder sb = new StringBuilder();

            var currentCount = DataParser.IntParse(CategoryVideosCurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            CategoryVideosCurrentCountHiddenField.Value = toFetchCount.ToString();

            //DataParser.IntParse(Request.QueryString["VideoCategoryID"]);

            List<GY_Videos> videosList = new List<GY_Videos>();

            videosList.AddRange(videoCategory.GY_Videos);
            videosList.AddRange(GetVideosRecursively(videoCategory));

            var categoryVideosQuery = (from set in videosList.AsQueryable()
                                       where set.Hide == false && set.Locale == locale
                                       select set).Distinct().ToList();

            if (categoryVideosQuery.Any())
            {
                foreach (GY_Videos video in categoryVideosQuery.OrderByDescending(c => c.AddedOn).Take(toFetchCount))
                {
                    sb.Append(await VideosBL.GetVideoHTML(video, direction, context, Page));
                }

                VideosLiteral.Text = string.Format(panelTag, sb);
            }
            else
            {
                VideosLiteral.Text = string.Format(emptyPanelTag, ResolveClientUrl(AppConfig.EmptyPanelImage));
            }

            UpdateCategoryVideosLoadMoreControls(toFetchCount, categoryVideosQuery.Count(), locale);
        }

        public List<GY_Videos> GetVideosRecursively(GY_VideoCategories videoCategory)
        {
            List<GY_Videos> videos = new List<GY_Videos>();

            foreach (GY_VideoCategories category in videoCategory.GY_ChildVideoCategories)
            {
                videos.AddRange(category.GY_Videos);

                if (category.GY_ChildVideoCategories.Any())
                {
                    videos.AddRange(GetVideosRecursively(category));
                }
            }

            return videos;
        }

        private void LoadCategoryVideoSets(GY_VideoCategories videoCategory, string direction, string locale)
        {
            const string emptyPanelTag = "<div class='CategoryVideoSetsEmptyPanel'><img class='EmptyCategoryVideoSetsImage' src='{0}' /></div>";
            const string panelTag = "<div class='CategoryVideoSetsPanel'>{0}<div class='Clear'></div></div>";
            const string tags = "<div class='VideoSetBlock' style='direction:{4};'><a href='{0}'><img src='{2}' alt='{1}' /><span class='VideoCount'>{5}</span><h2{6} title='{3}'>{1}</h2></a></div>";
            StringBuilder sb = new StringBuilder();

            string totalVideos = LanguageHelper.GetKey("TotalVideos", locale);

            var currentCount = DataParser.IntParse(CategoryVideoSetsCurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            CategoryVideoSetsCurrentCountHiddenField.Value = toFetchCount.ToString();

            List<GY_VideoSet> videoSetList = new List<GY_VideoSet>();

            videoSetList.AddRange(videoCategory.GY_VideoSet);
            videoSetList.AddRange(GetVideoSetRecursively(videoCategory));

            var videoSetQuery = (from set in videoSetList.AsQueryable()
                                 where set.Hide == false && set.Locale == locale
                                 select set).Distinct().ToList();

            if (videoSetQuery.Any())
            {
                foreach (GY_VideoSet videoSet in videoSetQuery.OrderByDescending(c => c.AddedOn).Take(toFetchCount))
                {
                    int count = videoSet.GY_Videos.Count();

                    sb.Append(string.Format(tags, ResolveClientUrl(string.Format("~/VideoSet.aspx?VideoSetID={0}", videoSet.VideoSetID)),
                        videoSet.Title, ResolveClientUrl(Utilities.GetImageThumbURL(videoSet.ImageID)),
                        videoSet.Description, direction, string.Format("{1}{0}", count, totalVideos),
                        (!string.IsNullOrWhiteSpace(videoSet.Description)) ? " class='tooltip'" : string.Empty));
                }

                VideoSetLiteral.Text = string.Format(panelTag, sb);
            }
            else
            {
                VideoSetLiteral.Text = string.Format(emptyPanelTag, ResolveClientUrl(AppConfig.EmptyPanelImage));
            }

            UpdateCategoryVideoSetLoadMoreControls(toFetchCount, videoSetQuery.Count(), locale);
        }

        public List<GY_VideoSet> GetVideoSetRecursively(GY_VideoCategories videoCategory)
        {
            List<GY_VideoSet> sets = new List<GY_VideoSet>();

            foreach (GY_VideoCategories category in videoCategory.GY_ChildVideoCategories)
            {
                sets.AddRange(category.GY_VideoSet);

                if (category.GY_ChildVideoCategories.Any())
                {
                    sets.AddRange(GetVideoSetRecursively(category));
                }
            }

            return sets;
        }

        private async Task LoadData()
        {
            var videoCategoryID = DataParser.IntParse(Request.QueryString["VideoCategoryID"]);

            using (var context = new GalleryEntities())
            {
                var videoCategoryQuery = await VideoCategoriesBL.GetObjectByIDAsync(videoCategoryID, context);

                if (videoCategoryQuery != null)
                {
                    TitleH1.Style.Add("direction", LanguageHelper.GetLocaleDirection(videoCategoryQuery.Locale));
                    TitleLiteral.Text = videoCategoryQuery.Title;

                    Tab1Literal.Text = LanguageHelper.GetKey("Videos", videoCategoryQuery.Locale);
                    Tab2Literal.Text = LanguageHelper.GetKey("Playlists", videoCategoryQuery.Locale);

                    CategoryVideoSetsLoadMoreButton.Text = LanguageHelper.GetKey("LoadMore", videoCategoryQuery.Locale);
                    CategoryVideosLoadMoreButton.Text = LanguageHelper.GetKey("LoadMore", videoCategoryQuery.Locale);

                    var seoEntity = new SEO();
                    seoEntity.Title = videoCategoryQuery.Title;
                    seoEntity.Description = videoCategoryQuery.Description;
                    seoEntity.Keywords = videoCategoryQuery.Description;

                    Utilities.SetPageSEO(Page, seoEntity);

                    var locale = videoCategoryQuery.Locale;
                    var direction = LanguageHelper.GetLocaleDirection(locale);

                    await LoadCategoryVideos(context, videoCategoryQuery, direction, locale);

                    LoadCategoryVideoSets(videoCategoryQuery, direction, locale);
                }
                else
                {
                    Response.Redirect("~/VideoCategories.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        private void UpdateCategoryVideoSetLoadMoreControls(int toFetchCount, int totalItemsCount, string locale)
        {
            if (totalItemsCount == 0)
            {
                CategoryVideoSetsLoadMoreButton.Visible = false;
            }

            if (toFetchCount >= totalItemsCount)
            {
                CategoryVideoSetsLoadMoreButton.Enabled = false;
                CategoryVideoSetsLoadMoreButton.CssClass = "LoadMoreButtonDisabled";
                CategoryVideoSetsLoadMoreButton.Text = LanguageHelper.GetKey("NoMoreItemsToDisplay", locale);
            }
        }

        private void UpdateCategoryVideosLoadMoreControls(int toFetchCount, int totalItemsCount, string locale)
        {
            if (totalItemsCount == 0)
            {
                CategoryVideosLoadMoreButton.Visible = false;
            }

            if (toFetchCount >= totalItemsCount)
            {
                CategoryVideosLoadMoreButton.Enabled = false;
                CategoryVideosLoadMoreButton.CssClass = "LoadMoreButtonDisabled";
                CategoryVideosLoadMoreButton.Text = LanguageHelper.GetKey("NoMoreItemsToDisplay", locale);
            }
        }

        protected async void CategoryVideoSetsLoadMoreButton_Click(object sender, EventArgs e)
        {
            var videoCategoryID = DataParser.IntParse(Request.QueryString["VideoCategoryID"]);

            using (var context = new GalleryEntities())
            {
                var videoCategoryQuery = await VideoCategoriesBL.GetObjectByIDAsync(videoCategoryID, context);

                var locale = videoCategoryQuery.Locale;
                var direction = LanguageHelper.GetLocaleDirection(locale);

                LoadCategoryVideoSets(videoCategoryQuery, direction, locale);
            }
        }

        protected async void CategoryVideosLoadMoreButton_Click(object sender, EventArgs e)
        {
            var videoCategoryID = DataParser.IntParse(Request.QueryString["VideoCategoryID"]);

            using (var context = new GalleryEntities())
            {
                var videoCategoryQuery = await VideoCategoriesBL.GetObjectByIDAsync(videoCategoryID, context);

                var locale = videoCategoryQuery.Locale;
                var direction = LanguageHelper.GetLocaleDirection(locale);

                await LoadCategoryVideos(context, videoCategoryQuery, direction, locale);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["VideoCategoryID"]))
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