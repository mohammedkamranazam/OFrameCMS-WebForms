using OWDARO;
using System;

namespace ProjectJKL.UI.Pages.Inactivated
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = String.Format("{1} | {0}", AppConfig.SiteName, "Inactivated User");
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;

            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetMainThemeFile();
        }
    }
}