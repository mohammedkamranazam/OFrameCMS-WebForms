using OWDARO.BLL.MediaBLL;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Media
{
    public partial class RelatedPostsComponent : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["PostID"]))
                {
                    this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                    {
                        await LoadData();
                    }));
                }
            }
        }

        public int Count
        {
            get;
            set;
        }

        public async Task LoadData()
        {
            int postID = Request.QueryString["PostID"].IntParse();

            using (var context = new MediaEntities())
            {
                var postQuery = await PostsBL.GetObjectByIDAsync(postID, context);

                string locale = postQuery.Locale;
                string direction = LanguageHelper.GetLocaleDirection(locale);

                TitleH1.Style.Add("direction", direction);
                TitleLiteral.Text = LanguageHelper.GetKey("RelatedPosts", locale);

                int postCategoryID = postQuery.PostCategoryID;

                var relatedPosts = await (from set in context.ME_Posts
                                          where set.PostCategoryID == postCategoryID &&
                                          set.Hide == false &&
                                          set.ImageID != null &&
                                          set.PostID != postID &&
                                          set.Locale == locale
                                          select set).OrderByDescending(c => c.AddedOn).Take(20).ToListAsync();

                Repeater1.DataSource = relatedPosts;
                Repeater1.DataBind();

                if (!relatedPosts.Any())
                {
                    this.Visible = false;
                }
            }
        }
    }
}