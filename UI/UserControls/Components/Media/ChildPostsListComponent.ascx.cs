using OWDARO.BLL.MediaBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Media
{
    public partial class ChildPostsListComponent : System.Web.UI.UserControl
    {
        public string Title
        {
            set
            {
                TitleHeaderLiteral.Text = value;
            }
        }

        private string GenerateListItemsHTML(string itemHTML, IQueryable<ME_Posts> childPosts, bool generateGoBack, int parentPostID, string locale)
        {
            StringBuilder sb = new StringBuilder();

            if (childPosts.Any())
            {
                if (generateGoBack)
                {
                    var goBackAnchorURL = string.Format("~/Post.aspx?PostID={0}", parentPostID);
                    var goBackHTML = string.Format(itemHTML, ResolveClientUrl(goBackAnchorURL), string.Format("<span class='GoBackSpan'></span> {0}", LanguageHelper.GetKey("MainPostText", locale)));
                    sb.Append(goBackHTML);
                }

                foreach (ME_Posts childPost in childPosts)
                {
                    var postAnchorURL = string.Format("~/Post.aspx?PostID={0}", childPost.PostID);
                    sb.Append(string.Format(itemHTML, ResolveClientUrl(postAnchorURL), childPost.Title));
                }
            }
            else
            {
                Visible = false;
            }

            return sb.ToString();
        }

        private async Task LoadData()
        {
            var postID = DataParser.IntParse(Request.QueryString["PostID"]);

            using (var context = new MediaEntities())
            {
                var postQuery = await PostsBL.GetObjectByIDAsync(postID, context);

                if (postQuery != null)
                {
                    var locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
                    var direction = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureDirectionCookieKey);

                    var itemHTML = "<li><a href='{0}'>{1}</a></li>";
                    var generateGoBack = false;

                    if (postQuery.ParentPostID != null)
                    {
                        postID = (int)postQuery.ParentPostID;

                        generateGoBack = true;
                    }

                    ChildPostsListComponentUL.Style.Add("direction", direction);
                    TitleHeader.Style.Add("direction", direction);
                    Title = LanguageHelper.GetKey("ChildPostsListComponentTitleHeader", locale);

                    var childPostsQuery = (from set in context.ME_Posts
                                           where set.Hide == false && set.ParentPostID == postID && set.Locale == locale
                                           select set);

                    ItemsLiteral.Text = GenerateListItemsHTML(itemHTML, childPostsQuery, generateGoBack, postID, locale);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["PostID"]))
                {
                    Visible = false;
                }
                else
                {
                    this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                    {
                        await LoadData();
                    }));
                }
            }
        }
    }
}