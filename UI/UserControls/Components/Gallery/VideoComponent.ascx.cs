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
    public partial class VideoComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                var videoID = DataParser.IntParse(Request.QueryString["VideoID"]);

                var videoQuery = await VideosBL.GetObjectByIDAsync(videoID, context);

                if (videoQuery != null)
                {
                    var locale = videoQuery.Locale;
                    var direction = LanguageHelper.GetLocaleDirection(locale);

                    var seoEntity = new SEO();
                    seoEntity.Title = videoQuery.Title;
                    seoEntity.Description = videoQuery.Description;
                    seoEntity.Keywords = videoQuery.Tags;

                    Utilities.SetPageSEO(Page, seoEntity);

                    DescriptionParaTag.Style.Add("direction", direction);
                    TitleH1.Style.Add("direction", direction);

                    TitleLiteral.Text = videoQuery.Title;
                    DescriptionLiteral.Text = videoQuery.Description;
                    TakenOnLiteral.Text = DataParser.GetDateFormattedString(videoQuery.TakenOn);
                    LikesCountLiteral.Text = videoQuery.LikesCount.ToString();
                    DislikesCountLiteral.Text = videoQuery.DislikesCount.ToString();

                    VideoLiteral.Text = await VideosBL.GetVideoControlHTML(videoQuery, context, Page);

                    if (videoQuery.FileID != null)
                    {
                        VideoDownloadLink.NavigateUrl = string.Format("~/UI/Pages/Helpers/DownloadGet.aspx?FileID={0}", videoQuery.FileID);
                        VideoDownloadLink.Visible = true;
                    }
                    else
                    {
                        VideoDownloadLink.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("~/VideoCategories.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
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