using OWDARO.BLL.MediaBLL;
using OWDARO.Settings;
using OWDARO.Util;
using System;

namespace ProjectJKL
{
    public partial class AboutUs : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetMainThemeFile();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utilities.SetPageSEO(Page, SEOHelper.GetPageSEO("AboutUsPage"));
                Utilities.SetPageCache(PageCacheHelper.GetCache("AboutUsPage"));

                var postQuery = PostsBL.GetObjectByID(DataParser.IntParse(KeywordsHelper.GetKeywordValue("AboutUsPostID")));

                if (postQuery != null)
                {
                    PostEmbedComponent1.PostContent = postQuery.PostContent;
                    AboutUsTitleLiteral.Text = postQuery.Title;
                }
            }
        }
    }
}