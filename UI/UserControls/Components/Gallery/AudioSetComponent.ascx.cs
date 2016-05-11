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
    public partial class AudioSetComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                var audioSetID = DataParser.IntParse(Request.QueryString["AudioSetID"]);

                var audioSetQuery = await AudioSetBL.GetObjectByIDAsync(audioSetID, context);

                if (audioSetQuery != null)
                {
                    string locale = audioSetQuery.Locale;
                    string direction = LanguageHelper.GetLocaleDirection(locale);

                    TitleLiteral.Text = audioSetQuery.Title;
                    TitleH1.Style.Add("direction", direction);

                    LoadMoreButton.Text = LanguageHelper.GetKey("LoadMore", locale);

                    var seoEntity = new SEO();
                    seoEntity.Title = audioSetQuery.Title;
                    seoEntity.Description = audioSetQuery.Description;
                    seoEntity.Keywords = audioSetQuery.Tags;

                    Utilities.SetPageSEO(Page, seoEntity);

                    await LoadSetAudios(context, audioSetQuery, direction, locale);
                }
                else
                {
                    Response.Redirect("~/AudioCategories.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        private async Task LoadSetAudios(GalleryEntities context, GY_AudioSet audioSet, string direction, string locale)
        {
            const string emptyPanelTag = "<div class='EmptyAudioSet'><img class='EmptyAudioSetImage' src='{0}' /></div>";
            const string panelTag = "<div class='AudioSetPanel'>{0}<div class='Clear'></div></div>";

            StringBuilder sb = new StringBuilder();

            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            CurrentCountHiddenField.Value = toFetchCount.ToString();

            var setAudios = (from set in audioSet.GY_Audios
                             where set.Hide == false && set.Locale == locale
                             select set);

            if (setAudios.Any())
            {
                foreach (GY_Audios audio in setAudios.OrderBy(c => c.Title).Take(toFetchCount))
                {
                    sb.Append(await AudiosBL.GetAudioHTML(audio, direction, Page, context));
                }

                AudiosLiteral.Text = string.Format(panelTag, sb);
            }
            else
            {
                AudiosLiteral.Text = string.Format(emptyPanelTag, ResolveClientUrl(AppConfig.EmptyPanelImage));
            }

            UpdateLoadMoreControls(toFetchCount, setAudios.Count(), locale);
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
                var audioSetID = DataParser.IntParse(Request.QueryString["AudioSetID"]);

                var audioSetQuery = await AudioSetBL.GetObjectByIDAsync(audioSetID, context);

                string locale = audioSetQuery.Locale;
                string direction = LanguageHelper.GetLocaleDirection(locale);

                await LoadSetAudios(context, audioSetQuery, direction, locale);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["AudioSetID"]))
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