using OWDARO.BLL.MediaBLL;
using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Media
{
    public partial class PostComponent : UserControl
    {
        public int PostID
        {
            get;
            set;
        }

        private async Task LoadData()
        {
            using (var context = new MediaEntities())
            {
                var postEntity = await PostsBL.GetObjectByIDAsync(PostID, context);

                if (postEntity != null && !postEntity.Hide)
                {
                    var seoEntity = new SEO();
                    seoEntity.Title = postEntity.Title;
                    seoEntity.Description = postEntity.SubTitle;
                    seoEntity.Keywords = postEntity.Tags;

                    Utilities.SetPageSEO(Page, seoEntity);

                    PostDetailsDiv.Style.Add("direction", LanguageHelper.GetLocaleDirection(postEntity.Locale));

                    TitleLiteral.Text = postEntity.Title;
                    SubTitleLiteral.Text = postEntity.SubTitle;
                    AddedOnLiteral.Text = DataParser.GetDateTimeFormattedString(postEntity.AddedOn);
                    AuthorHyperLink.Text = postEntity.Username;
                    AuthorHyperLink.NavigateUrl = string.Format("~/UserDetails.aspx?Username={0}", postEntity.Username);
                    CategoryHyperLink.Text = postEntity.ME_PostCategories.Title;
                    CategoryHyperLink.NavigateUrl = string.Format("~/Posts.aspx?PostCategoryID={0}", postEntity.PostCategoryID);
                    ImageLiteral.Text = Utilities.GetFocusPointImage(postEntity.ImageID, "ImageFrame", postEntity.Title, "PostImage", Page);
                    TagsLiteral.Text = Utilities.GetTagsHTML(postEntity.Tags, Page);
                    PostEmbedComponent.PostContent = postEntity.PostContent;
                }
                else
                {
                    Response.Redirect("~/Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["PostID"]))
                {
                    Response.Redirect("~/Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                PostID = Request.QueryString["PostID"].IntParse();

                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }
        }
    }
}