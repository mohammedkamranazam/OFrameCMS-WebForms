using System;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class UserAddPopUpComponent : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["ReturnUrl"]))
                {
                    UserAddHyperLink.NavigateUrl = string.Format("~/UI/Pages/LogOn/Register.aspx?ReturnUrl={0}", Request.QueryString["ReturnUrl"]);
                }
            }
        }
    }
}