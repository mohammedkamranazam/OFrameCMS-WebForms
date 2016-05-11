using OWDARO.BLL.OFrameBLL;
using OWDARO.Util;
using System;
using System.Web.Security;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class LoginComponent : System.Web.UI.UserControl
    {
        public string BoxTitle
        {
            set
            {
                BoxTitleLiteral.Text = value;
            }
        }

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

        public string HeaderTitle
        {
            set
            {
                HeaderTitleLiteral.Text = value;
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

        public string ValidationGroup
        {
            set
            {
                UsernameTextBox.ValidationGroup = value;
                PasswordTextBox.ValidationGroup = value;
                FormToolbar1.ValidationGroup = value;
            }
        }

        private void FormToolbar1_CustomClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                LogOn();
            }
        }

        private void LogOn()
        {
            var username = UsernameTextBox.Text;
            var password = PasswordTextBox.Text;
            var rememberMe = RememberMeCheckBox.Checked;

            var user = Membership.GetUser(username);

            if (user != null)
            {
                if (user.IsApproved)
                {
                    if (Membership.ValidateUser(username, password))
                    {
                        ActionOnLogin.Login(username, rememberMe, UseRedirect, UseCustomRedirect, CustomRedirectURL);
                    }
                    else
                    {
                        StatusMessageLabel.MessageType = StatusMessageType.Error;
                        StatusMessageLabel.Message = "incorrect username or password";
                    }
                }
                else
                {
                    StatusMessageLabel.MessageType = StatusMessageType.Warning;
                    StatusMessageLabel.Message = "login disabled";
                }
            }
            else
            {
                StatusMessageLabel.MessageType = StatusMessageType.Error;
                StatusMessageLabel.Message = "incorrect username or password";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FormToolbar1.CustomClick += new EventHandler(FormToolbar1_CustomClick);

            if (!IsPostBack)
            {
                if ((string.IsNullOrWhiteSpace(AppConfig.FacebookAPIKey) || string.IsNullOrWhiteSpace(AppConfig.FacebookSecretKey)) && (string.IsNullOrWhiteSpace(AppConfig.GoogleAPIKey) || string.IsNullOrWhiteSpace(AppConfig.GoogleSecretKey)) && (string.IsNullOrWhiteSpace(AppConfig.TwitterAPIKey) || string.IsNullOrWhiteSpace(AppConfig.TwitterSecretKey)))
                {
                    OAuthComponentDIV.Visible = false;
                }

                OAuthComponentDIV.Visible = AppConfig.EnableOAuthRegistration;
            }
        }
    }
}