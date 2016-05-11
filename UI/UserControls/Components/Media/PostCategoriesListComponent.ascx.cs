using OWDARO.BLL.MediaBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Media
{
    public partial class PostCategoriesListComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            using (var context = new MediaEntities())
            {
                var locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
                var direction = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureDirectionCookieKey);

                StringBuilder sb = new StringBuilder();

                List<ME_PostCategories> postCategories = new List<ME_PostCategories>();

                if (!string.IsNullOrWhiteSpace(Request.QueryString["PostCategoryID"]))
                {
                    int postCategoryID = Request.QueryString["PostCategoryID"].IntParse();

                    postCategories = await (from set in context.ME_PostCategories
                                            where set.Hide == false && set.ParentPostCategoryID == postCategoryID
                                            select set).ToListAsync();

                    if (postCategories.Any())
                    {
                        locale = postCategories.FirstOrDefault().Locale;
                        direction = LanguageHelper.GetLocaleDirection(locale);
                    }
                }
                else
                {
                    postCategories = await (from set in context.ME_PostCategories
                                            where set.Hide == false && set.ParentPostCategoryID == null && set.Locale == locale
                                            select set).ToListAsync();
                }

                sb.Append(GetPostCategoriesItems(postCategories, direction));

                ItemsLiteral.Text = sb.ToString();

                await BuildGoBackMenuItem(context, locale, direction);
            }
        }

        private async Task BuildGoBackMenuItem(MediaEntities context, string locale, string direction)
        {
            string aTag = "<a href='{1}' class='GoBackAnchor' style='direction:{2};'>{0}</a>";
            string goBackSpan = string.Format("<span class='GoBackSpan'></span> {0}", LanguageHelper.GetKey("GoBackText", locale));
            var goBackHTML = string.Empty;
            var goBackURL = "~/Posts.aspx{0}";

            if (!string.IsNullOrWhiteSpace(Request.QueryString["PostCategoryID"]))
            {
                int postCategoryID = Request.QueryString["PostCategoryID"].IntParse();

                var postCategoryQuery = await PostCategoriesBL.GetObjectByIDAsync(postCategoryID, context);

                if (postCategoryQuery != null)
                {
                    int? parentPostCategoryID = postCategoryQuery.ParentPostCategoryID;

                    if (parentPostCategoryID == null)
                    {
                        GoBackLiteral.Text = string.Format(aTag, goBackSpan, ResolveClientUrl(string.Format(goBackURL, string.Empty)), direction);
                    }
                    else
                    {
                        GoBackLiteral.Text = string.Format(aTag, goBackSpan, ResolveClientUrl(string.Format(goBackURL, string.Format("?PostCategoryID={0}", parentPostCategoryID))), direction);
                    }
                }
                else
                {
                    GoBackLiteral.Text = string.Empty;
                }
            }
            else
            {
                GoBackLiteral.Text = string.Empty;
            }
        }

        public string GetPostCategoriesItems(ICollection<ME_PostCategories> categories, string direction)
        {
            StringBuilder sb = new StringBuilder();
            const string ulTag = "<ul class='PostCategoriesListComponent MultiLevelAccordianMenu' style='direction:{1};'>{0}</ul>";
            const string liTag = "<li><a href='{0}' class='{3}'>{1}</a>{2}</li>";
            const string aTagURL = "~/Posts.aspx?PostCategoryID={0}";

            if (categories.Any())
            {
                foreach (var category in categories)
                {
                    sb.Append(string.Format(liTag, ResolveClientUrl(string.Format(aTagURL, category.PostCategoryID)), category.Title,
                         GetPostCategoriesItems(category.ME_ChildPostCategories, direction), direction));
                }

                return string.Format(ulTag, sb, direction);
            }
            else
            {
                return string.Empty;
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