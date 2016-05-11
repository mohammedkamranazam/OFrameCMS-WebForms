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
    public partial class VideoCategoriesComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                // var sectionID = DataParser.IntParse(Request.QueryString["VideoSectionID"]);

                // var sectionQuery = await VideoSectionsBL.GetObjectByIDAsync(sectionID, context);

                //if(sectionQuery != null)
                //{
                string locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
                string direction = LanguageHelper.GetLocaleDirection(locale);

                TitleH1.Style.Add("direction", direction);
                TitleLiteral.Text = LanguageHelper.GetKey("VideoCategoriesTitle", locale);

                LoadMoreButton.Text = LanguageHelper.GetKey("LoadMore", locale);

                //var seoEntity = new SEO();
                //seoEntity.Title = sectionQuery.Title;
                //seoEntity.Description = sectionQuery.Description;
                //seoEntity.Keywords = sectionQuery.Description;

                //Utilities.SetPageSEO(Page, seoEntity);

                await LoadVideoCategories(context, locale, direction);
                //}
                //else
                //{
                //    Response.Redirect("~/VideoCategories.aspx", false);
                //    Context.ApplicationInstance.CompleteRequest();
                //}
            }
        }

        private async Task LoadVideoCategories(GalleryEntities context, string locale, string direction)
        {
            const string emptyPanelTag = "<div class='EmptyVideoCategories'><img class='EmptyVideoCategoriesImage' src='{0}' /></div>";
            const string panelTag = "<div class='VideoCategoriesPanel'>{0}<div class='Clear'></div></div>";
            const string tag = "<div class='VideoCategoriesItem' style='direction:{0};'><div class='DescriptionDiv'><h2 class='Title'><a href='{1}' tooltip='{2}'>{2}</a></h2><p class='Description'>{3}</p></div><div class='InfoDiv'><span class='VideoCount'>{5}<span class='Count'>{6}</span></span><a class='OpenVideocategory' href='{1}'>{4}</a><div class='Clear'></div></div></div>";
            StringBuilder sb = new StringBuilder();

            string open = LanguageHelper.GetKey("Open", locale);
            string totalVideos = LanguageHelper.GetKey("TotalVideos", locale);

            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;

            CurrentCountHiddenField.Value = toFetchCount.ToString();

            var videoCategories = await (from set in context.GY_VideoCategories
                                         where set.Hide == false && set.Locale == locale && set.ParentVideoCategoryID == null
                                         select set).ToListAsync();

            if (videoCategories.Any())
            {
                foreach (var videoCategory in videoCategories.OrderByDescending(c => c.VideoCategoryID).Take(toFetchCount))
                {
                    string src = ResolveClientUrl(string.Format("~/VideoCategory.aspx?VideoCategoryID={0}", videoCategory.VideoCategoryID));

                    int totalVideosCount = 0;
                    totalVideosCount = totalVideosCount + videoCategory.GY_Videos.Count();
                    totalVideosCount = totalVideosCount + GetVideosCountRecursively(videoCategory);

                    sb.Append(string.Format(tag, direction, src, videoCategory.Title, videoCategory.Description, open, totalVideos, totalVideosCount));
                }

                VideoCategoriesLiteral.Text = string.Format(panelTag, sb);
            }
            else
            {
                VideoCategoriesLiteral.Text = string.Format(emptyPanelTag, ResolveClientUrl(AppConfig.EmptyPanelImage));
            }

            //DataList1.DataSource = videoCategoriesQuery.OrderByDescending(c => c.VideoCategoryID).Take(toFetchCount);
            //DataList1.DataBind();

            UpdateLoadMoreControls(toFetchCount, videoCategories.Count(), locale);
        }

        public int GetVideosCountRecursively(GY_VideoCategories videoCategory)
        {
            int count = 0;

            foreach (GY_VideoCategories category in videoCategory.GY_ChildVideoCategories)
            {
                count = count + category.GY_Videos.Count();

                if (category.GY_ChildVideoCategories.Any())
                {
                    count = count + GetVideosCountRecursively(category);
                }
            }

            return count;
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
                //var sectionID = DataParser.IntParse(Request.QueryString["VideoSectionID"]);

                //var sectionQuery = await VideoSectionsBL.GetObjectByIDAsync(sectionID, context);

                string locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
                string direction = LanguageHelper.GetLocaleDirection(locale);

                await LoadVideoCategories(context, locale, direction);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if(!string.IsNullOrWhiteSpace(Request.QueryString["VideoSectionID"]))
                //{
                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
                //}
                //else
                //{
                //    Response.Redirect("~/VideoCategories.aspx", false);
                //    Context.ApplicationInstance.CompleteRequest();
                //}
            }
        }
    }
}