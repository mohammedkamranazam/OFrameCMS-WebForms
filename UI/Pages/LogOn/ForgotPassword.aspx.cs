using System;

namespace OWDARO.UI.Pages.LogOn
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("ForgotPasswordPage"));

                QAPanel.Visible = AppConfig.EnableQA;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Title = AppConfig.SiteName + ": Forgot Password";

            MasterPageFile = OWDARO.Util.Utilities.GetMainThemeFile();
        }
    }
}