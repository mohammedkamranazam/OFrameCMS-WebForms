using System;

namespace ProjectJKL.UI.Pages.LogOn
{
    public partial class DeOAuthregistration : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetMainThemeFile();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}