using System;

namespace ProjectJKL
{
    public partial class Portfolio : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetMainThemeFile();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("PortfolioPage"));
                OWDARO.Util.Utilities.SetPageCache(OWDARO.Settings.PageCacheHelper.GetCache("PortfolioPage"));
            }
        }
    }
}