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
    public partial class AudioCategoriesComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                string locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
                string direction = LanguageHelper.GetLocaleDirection(locale);

                TitleH1.Style.Add("direction", direction);
                TitleLiteral.Text = LanguageHelper.GetKey("AudioCategoriesTitle", locale);

                LoadMoreButton.Text = LanguageHelper.GetKey("LoadMore", locale);

                //var seoEntity = new SEO();
                //seoEntity.Title = sectionQuery.Title;
                //seoEntity.Description = sectionQuery.Description;
                //seoEntity.Keywords = sectionQuery.Description;

                //Utilities.SetPageSEO(Page, seoEntity);

                await LoadAudioCategories(locale, direction, context);
            }
        }

        private async Task LoadAudioCategories(string locale, string direction, GalleryEntities context)
        {
            const string emptyPanelTag = "<div class='EmptyAudioCategories'><img class='EmptyAudioCategoriesImage' src='{0}' /></div>";
            const string panelTag = "<div class='AudioCategoriesPanel'>{0}<div class='Clear'></div></div>";
            const string tag = "<div class='AudioCategoriesItem' style='direction:{0};'><div class='DescriptionDiv'><h2 class='Title'><a href='{1}' tooltip='{2}'>{2}</a></h2><p class='Description'>{3}</p></div><div class='InfoDiv'><span class='AudioCount'>{5}<span class='Count'>{6}</span></span><a class='OpenAudiocategory' href='{1}'>{4}</a><div class='Clear'></div></div></div>";
            StringBuilder sb = new StringBuilder();

            string open = LanguageHelper.GetKey("Open", locale);
            string totalAudios = LanguageHelper.GetKey("TotalAudios", locale);

            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;

            CurrentCountHiddenField.Value = toFetchCount.ToString();

            var audioCategories = await (from set in context.GY_AudioCategories
                                         where set.Hide == false && set.Locale == locale && set.ParentAudioCategoryID == null
                                         select set).ToListAsync();

            if (audioCategories.Any())
            {
                foreach (var audioCategory in audioCategories.OrderByDescending(c => c.AudioCategoryID).Take(toFetchCount))
                {
                    int totalAudiosCount = 0;
                    totalAudiosCount = totalAudiosCount + audioCategory.GY_Audios.Count();
                    totalAudiosCount = totalAudiosCount + GetAudiosCountRecursively(audioCategory);

                    string src = ResolveClientUrl(string.Format("~/AudioCategory.aspx?AudioCategoryID={0}", audioCategory.AudioCategoryID));

                    sb.Append(string.Format(tag, direction, src, audioCategory.Title, audioCategory.Description, open, totalAudios, totalAudiosCount));
                }

                AudioCategoriesLiteral.Text = string.Format(panelTag, sb);
            }
            else
            {
                AudioCategoriesLiteral.Text = string.Format(emptyPanelTag, ResolveClientUrl(AppConfig.EmptyPanelImage));
            }

            //DataList1.DataSource = audioCategoriesQuery.OrderByDescending(c => c.AudioCategoryID).Take(toFetchCount);
            //DataList1.DataBind();

            UpdateLoadMoreControls(toFetchCount, audioCategories.Count(), locale);
        }

        public int GetAudiosCountRecursively(GY_AudioCategories audioCategory)
        {
            int count = 0;

            foreach (GY_AudioCategories category in audioCategory.GY_ChildAudioCategories)
            {
                count = count + category.GY_Audios.Count();

                if (category.GY_ChildAudioCategories.Any())
                {
                    count = count + GetAudiosCountRecursively(category);
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
                string locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
                string direction = LanguageHelper.GetLocaleDirection(locale);

                await LoadAudioCategories(locale, direction, context);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }
        }
    }
}