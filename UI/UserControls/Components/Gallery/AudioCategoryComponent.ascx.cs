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
    public partial class AudioCategoryComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            var audioCategoryID = DataParser.IntParse(Request.QueryString["AudioCategoryID"]);

            using (var context = new GalleryEntities())
            {
                var audioCategoryQuery = await AudioCategoriesBL.GetObjectByIDAsync(audioCategoryID, context);

                if (audioCategoryQuery != null)
                {
                    TitleH1.Style.Add("direction", LanguageHelper.GetLocaleDirection(audioCategoryQuery.Locale));
                    TitleLiteral.Text = audioCategoryQuery.Title;

                    Tab1Literal.Text = LanguageHelper.GetKey("Audios", audioCategoryQuery.Locale);
                    Tab2Literal.Text = LanguageHelper.GetKey("Playlists", audioCategoryQuery.Locale);

                    CategoryAudioSetsLoadMoreButton.Text = LanguageHelper.GetKey("LoadMore", audioCategoryQuery.Locale);
                    CategoryAudiosLoadMoreButton.Text = LanguageHelper.GetKey("LoadMore", audioCategoryQuery.Locale);

                    var seoEntity = new SEO();
                    seoEntity.Title = audioCategoryQuery.Title;
                    seoEntity.Description = audioCategoryQuery.Description;
                    seoEntity.Keywords = audioCategoryQuery.Description;

                    Utilities.SetPageSEO(Page, seoEntity);

                    var locale = audioCategoryQuery.Locale;
                    var direction = LanguageHelper.GetLocaleDirection(locale);

                    await LoadCategoryAudios(context, audioCategoryQuery, direction, locale);

                    LoadCategoryAudioSets(audioCategoryQuery, direction, locale);
                }
                else
                {
                    Response.Redirect("~/AudioCategories.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        private async Task LoadCategoryAudios(GalleryEntities context, GY_AudioCategories audioCategory, string direction, string locale)
        {
            const string emptyPanelTag = "<div class='CategoryAudiosEmptyPanel'><img class='EmptyCategoryAudiosImage' src='{0}' /></div>";
            const string panelTag = "<div class='CategoryAudiosPanel'>{0}<div class='Clear'></div></div>";

            StringBuilder sb = new StringBuilder();

            var currentCount = DataParser.IntParse(CategoryAudiosCurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            CategoryAudiosCurrentCountHiddenField.Value = toFetchCount.ToString();

            List<GY_Audios> audiosList = new List<GY_Audios>();

            audiosList.AddRange(audioCategory.GY_Audios);
            audiosList.AddRange(GetAudiosRecursively(audioCategory));

            var categoryAudios = (from set in audiosList.AsQueryable()
                                  where set.Hide == false && set.Locale == locale
                                  select set).Distinct().ToList();

            if (categoryAudios.Any())
            {
                foreach (GY_Audios audio in categoryAudios.OrderBy(c => c.Title).Take(toFetchCount))
                {
                    sb.Append(await AudiosBL.GetAudioHTML(audio, direction, Page, context));
                }

                AudiosLiteral.Text = string.Format(panelTag, sb);
            }
            else
            {
                AudiosLiteral.Text = string.Format(emptyPanelTag, ResolveClientUrl(AppConfig.EmptyPanelImage));
            }

            UpdateCategoryAudiosLoadMoreControls(toFetchCount, categoryAudios.Count(), locale);
        }

        public List<GY_Audios> GetAudiosRecursively(GY_AudioCategories audioCategory)
        {
            List<GY_Audios> audios = new List<GY_Audios>();

            foreach (GY_AudioCategories category in audioCategory.GY_ChildAudioCategories)
            {
                audios.AddRange(category.GY_Audios);

                if (category.GY_ChildAudioCategories.Any())
                {
                    audios.AddRange(GetAudiosRecursively(category));
                }
            }

            return audios;
        }

        private void LoadCategoryAudioSets(GY_AudioCategories audioCategory, string direction, string locale)
        {
            const string emptyPanelTag = "<div class='CategoryAudioSetsEmptyPanel'><img class='EmptyCategoryAudioSetsImage' src='{0}' /></div>";
            const string panelTag = "<div class='CategoryAudioSetsPanel'>{0}<div class='Clear'></div></div>";
            const string tags = "<div class='AudioSetBlock' style='direction:{0};'><a href='{1}'><img src='{2}' alt='{3}' /><h2{6} title='{4}'>{3}</h2><div class='SetToolbar'><span>{5}</span></div></a></div>";

            StringBuilder sb = new StringBuilder();

            string totalAudios = LanguageHelper.GetKey("TotalAudios", locale);

            var currentCount = DataParser.IntParse(CategoryAudioSetsCurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            CategoryAudioSetsCurrentCountHiddenField.Value = toFetchCount.ToString();

            List<GY_AudioSet> audioSetList = new List<GY_AudioSet>();

            audioSetList.AddRange(audioCategory.GY_AudioSet);
            audioSetList.AddRange(GetAudioSetRecursively(audioCategory));

            var audioSetQuery = (from set in audioSetList.AsQueryable()
                                 where set.Hide == false && set.Locale == locale
                                 select set).Distinct().ToList();

            if (audioSetQuery.Any())
            {
                foreach (GY_AudioSet audioSet in audioSetQuery.OrderByDescending(c => c.AddedOn).Take(toFetchCount))
                {
                    int count = audioSet.GY_Audios.Count();

                    sb.Append(string.Format(tags,
                        direction,
                        ResolveClientUrl(string.Format("~/AudioSet.aspx?AudioSetID={0}", audioSet.AudioSetID)),
                        ResolveClientUrl(Utilities.GetImageThumbURL(audioSet.ImageID)),
                        audioSet.Title,
                        audioSet.Description,
                        string.Format("{1}{0}", count, totalAudios),
                        (!string.IsNullOrWhiteSpace(audioSet.Description)) ? " class='tooltip'" : string.Empty));
                }

                AudioSetLiteral.Text = string.Format(panelTag, sb);
            }
            else
            {
                AudioSetLiteral.Text = string.Format(emptyPanelTag, ResolveClientUrl(AppConfig.EmptyPanelImage));
            }

            UpdateCategoryAudioSetLoadMoreControls(toFetchCount, audioSetQuery.Count(), locale);
        }

        public List<GY_AudioSet> GetAudioSetRecursively(GY_AudioCategories audioCategory)
        {
            List<GY_AudioSet> sets = new List<GY_AudioSet>();

            foreach (GY_AudioCategories category in audioCategory.GY_ChildAudioCategories)
            {
                sets.AddRange(category.GY_AudioSet);

                if (category.GY_ChildAudioCategories.Any())
                {
                    sets.AddRange(GetAudioSetRecursively(category));
                }
            }

            return sets;
        }

        private void UpdateCategoryAudioSetLoadMoreControls(int toFetchCount, int totalItemsCount, string locale)
        {
            if (totalItemsCount == 0)
            {
                CategoryAudioSetsLoadMoreButton.Visible = false;
            }

            if (toFetchCount >= totalItemsCount)
            {
                CategoryAudioSetsLoadMoreButton.Enabled = false;
                CategoryAudioSetsLoadMoreButton.CssClass = "LoadMoreButtonDisabled";
                CategoryAudioSetsLoadMoreButton.Text = LanguageHelper.GetKey("NoMoreItemsToDisplay", locale);
            }
        }

        private void UpdateCategoryAudiosLoadMoreControls(int toFetchCount, int totalItemsCount, string locale)
        {
            if (totalItemsCount == 0)
            {
                CategoryAudiosLoadMoreButton.Visible = false;
            }

            if (toFetchCount >= totalItemsCount)
            {
                CategoryAudiosLoadMoreButton.Enabled = false;
                CategoryAudiosLoadMoreButton.CssClass = "LoadMoreButtonDisabled";
                CategoryAudiosLoadMoreButton.Text = LanguageHelper.GetKey("NoMoreItemsToDisplay", locale);
            }
        }

        protected async void CategoryAudioSetsLoadMoreButton_Click(object sender, EventArgs e)
        {
            var audioCategoryID = DataParser.IntParse(Request.QueryString["AudioCategoryID"]);

            using (var context = new GalleryEntities())
            {
                var audioCategoryQuery = await AudioCategoriesBL.GetObjectByIDAsync(audioCategoryID, context);

                var locale = audioCategoryQuery.Locale;
                var direction = LanguageHelper.GetLocaleDirection(locale);

                LoadCategoryAudioSets(audioCategoryQuery, direction, locale);
            }
        }

        protected async void CategoryAudiosLoadMoreButton_Click(object sender, EventArgs e)
        {
            var audioCategoryID = DataParser.IntParse(Request.QueryString["AudioCategoryID"]);

            using (var context = new GalleryEntities())
            {
                var audioCategoryQuery = await AudioCategoriesBL.GetObjectByIDAsync(audioCategoryID, context);

                var locale = audioCategoryQuery.Locale;
                var direction = LanguageHelper.GetLocaleDirection(locale);

                await LoadCategoryAudios(context, audioCategoryQuery, direction, locale);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["AudioCategoryID"]))
                {
                    this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                    {
                        await LoadData();
                    }));
                }
                else
                {
                    Response.Redirect("~/AudioCategories.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }
}