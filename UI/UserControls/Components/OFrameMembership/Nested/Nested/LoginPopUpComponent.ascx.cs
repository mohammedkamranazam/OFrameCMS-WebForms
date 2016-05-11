using OWDARO.Util;
using System;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class LoginPopUpComponent : System.Web.UI.UserControl
    {
        public string CustomRedirectURL
        {
            get
            {
                return CustomRedirectURLHiddenField.Value;
            }

            set
            {
                CustomRedirectURLHiddenField.Value = value;
            }
        }

        public bool UseCustomRedirect
        {
            get
            {
                return DataParser.BoolParse(UseCustomRedirectHiddenField.Value);
            }

            set
            {
                UseCustomRedirectHiddenField.Value = value.ToString();
            }
        }

        public bool UseRedirect
        {
            get
            {
                return DataParser.BoolParse(UseRedirectHiddenField.Value);
            }

            set
            {
                UseRedirectHiddenField.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoginComponent1.UseRedirect = UseRedirect;
                LoginComponent1.UseCustomRedirect = UseCustomRedirect;
                LoginComponent1.CustomRedirectURL = CustomRedirectURL;
            }
        }
    }
}