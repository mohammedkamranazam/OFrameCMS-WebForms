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
    public partial class RelatedAudiosComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                var audioID = DataParser.IntParse(Request.QueryString["AudioID"]);

                var audioQuery = await AudiosBL.GetObjectByIDAsync(audioID, context);

                if (audioQuery != null)
                {
                    string locale = audioQuery.Locale;
                    string direction = LanguageHelper.GetLocaleDirection(locale);

                    TitleLiteral.Text = LanguageHelper.GetKey("RelatedAudios", locale);
                    TitleH1.Style.Add("direction", direction);

                    LoadMoreButton.Text = LanguageHelper.GetKey("LoadMore", locale);

                    await LoadAudios(context, audioQuery, direction, locale);
                }
                else
                {
                    Response.Redirect("~/AudioCategories.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        private async Task LoadAudios(GalleryEntities context, GY_Audios audioQuery, string direction, string locale)
        {
            const string panelTag = "<div class='RelatedAudiosPanel'>{0}<div class='Clear'></div></div>";

            StringBuilder sb = new StringBuilder();

            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            CurrentCountHiddenField.Value = toFetchCount.ToString();

            var relatedAudiosQuery = (from set in audioQuery.GY_AudioCategories.GY_Audios
                                      where set.AudioID != audioQuery.AudioID && set.Hide == false && set.Locale == locale
                                      select set).ToList();

            if (relatedAudiosQuery.Any())
            {
                foreach (GY_Audios audio in relatedAudiosQuery.OrderBy(c => c.Title).Take(toFetchCount))
                {
                    sb.Append(await AudiosBL.GetAudioHTML(audio, direction, Page, context));
                }

                AudiosLiteral.Text = string.Format(panelTag, sb);
            }
            else
            {
                this.Visible = false;
            }

            UpdateLoadMoreControls(toFetchCount, relatedAudiosQuery.Count(), locale);
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
                var audioID = DataParser.IntParse(Request.QueryString["AudioID"]);

                var audioQuery = await AudiosBL.GetObjectByIDAsync(audioID, context);

                string locale = audioQuery.Locale;
                string direction = LanguageHelper.GetLocaleDirection(locale);

                await LoadAudios(context, audioQuery, direction, locale);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["AudioID"]))
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