using OWDARO.BLL.MediaBLL;
using OWDARO.Helpers;
using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Linq;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Media
{
    public partial class PostsComponent : UserControl
    {
        public string Title
        {
            get
            {
                return TitleLiteral.Text;
            }

            set
            {
                TitleLiteral.Text = value;
            }
        }

        private IQueryable<ME_Posts> GetAllPosts(string locale, MediaEntities context)
        {
            return (from set in context.ME_Posts
                    where set.Hide == false && set.Locale == locale
                    select set);
        }

        private void InitializeCategoryPageHeader(int categoryID, MediaEntities context)
        {
            var categoryQuery = PostCategoriesBL.GetObjectByID(categoryID, context);

            if (categoryQuery != null)
            {
                TitleLiteral.Text = categoryQuery.Title;

                var seoEntity = new SEO();
                seoEntity.Title = categoryQuery.Title;
                seoEntity.Description = categoryQuery.Description;
                seoEntity.Keywords = categoryQuery.Description;

                Utilities.SetPageSEO(Page, seoEntity);
            }
        }

        private void PopulateAllPosts(string locale)
        {
            using (var context = new MediaEntities())
            {
                PopulateAllPosts(locale, context);
            }
        }

        private void PopulateAllPosts(string locale, MediaEntities context)
        {
            ListView1.DataSource = GetAllPosts(locale, context).ToList();
            ListView1.DataBind();
        }

        private void PopulateCategoriesPosts(string locale, int categoryID, MediaEntities context)
        {
            ListView1.DataSource = GetAllPosts(locale, context).Where(p => p.PostCategoryID == categoryID).ToList();
            ListView1.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
                var direction = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureDirectionCookieKey);

                HeaderTitle.Style.Add("direction", direction);

                Title = LanguageHelper.GetKey("PostsComponentHeaderTitle", locale);

                Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("AllPostsPage"));

                using (var context = new MediaEntities())
                {
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["PostCategoryID"]))
                    {
                        var categoryID = DataParser.IntParse(Request.QueryString["PostCategoryID"]);

                        InitializeCategoryPageHeader(categoryID, context);

                        PopulateCategoriesPosts(locale, categoryID, context);
                    }
                    else
                    {
                        PopulateAllPosts(locale);
                    }
                }
            }
        }
    }
}