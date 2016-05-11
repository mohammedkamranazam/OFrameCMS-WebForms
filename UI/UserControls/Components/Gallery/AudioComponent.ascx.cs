using OWDARO.BLL.GalleryBLL;
using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class AudioComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                var audioID = DataParser.IntParse(Request.QueryString["AudioID"]);

                var audioQuery = await AudiosBL.GetObjectByIDAsync(audioID, context);

                if (audioQuery != null)
                {
                    var locale = audioQuery.Locale;
                    var direction = LanguageHelper.GetLocaleDirection(locale);

                    DescriptionParaTag.Style.Add("direction", direction);
                    TitleH1.Style.Add("direction", direction);

                    var seoEntity = new SEO();
                    seoEntity.Title = audioQuery.Title;
                    seoEntity.Description = audioQuery.Description;
                    seoEntity.Keywords = audioQuery.Tags;

                    Utilities.SetPageSEO(Page, seoEntity);

                    TitleLiteral.Text = audioQuery.Title;
                    DescriptionLiteral.Text = audioQuery.Description;
                    TakenOnLiteral.Text = DataParser.GetDateFormattedString(audioQuery.TakenOn);
                    LikesCountLiteral.Text = audioQuery.LikesCount.ToString();
                    DislikesCountLiteral.Text = audioQuery.DislikesCount.ToString();

                    AudioLiteral.Text = await AudiosBL.GetAudioControlHTML(audioQuery, context, Page);

                    if (audioQuery.FileID != null)
                    {
                        AudioDownloadLink.NavigateUrl = string.Format("~/UI/Pages/Helpers/DownloadGet.aspx?FileID={0}", audioQuery.FileID);
                        AudioDownloadLink.Visible = true;
                    }
                    else
                    {
                        AudioDownloadLink.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("~/AudioCategories.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
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