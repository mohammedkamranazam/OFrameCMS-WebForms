using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using System;
using System.Web.Security;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class LoginStatus : System.Web.UI.UserControl
    {
        public string CssClass
        {
            set
            {
                LoginStatusUnorderedList.Attributes.Add("class", value);
            }
        }

        private void Initialize()
        {
            string locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);

            LoginHyperLink.Text = LanguageHelper.GetKey("Login", locale);
            LogoutLinkButton.Text = LanguageHelper.GetKey("LogOut", locale);
            AccountHyperLink.Text = LanguageHelper.GetKey("Account", locale);
            ContactHyperLink.Text = LanguageHelper.GetKey("ChangeContact", locale);
            ProfileHyperLink.Text = LanguageHelper.GetKey("ChangeProfile", locale);
            PasswordHyperLink.Text = LanguageHelper.GetKey("ChangePassword", locale);
            QAHyperLink.Text = LanguageHelper.GetKey("ChnageSecurityQA", locale);

            var user = Membership.GetUser();

            if (user == null)
            {
                InitializeLoggedOutView();
            }
            else
            {
                LoginHyperLink.Visible = false;

                AccountHyperLink.NavigateUrl = UserBL.GetRootFolder(user.UserName);

                SecQALI.Visible = QAHyperLink.Visible = AppConfig.EnableQA;
                AccountLI.Visible = AccountHyperLink.Visible = true;
                ContactLI.Visible = ContactHyperLink.Visible = true;
                ProfileLI.Visible = ProfileHyperLink.Visible = true;
                PasswordLI.Visible = PasswordHyperLink.Visible = true;
                LogoutLinkButton.Visible = true;
            }
        }

        private void InitializeLoggedOutView()
        {
            LoginHyperLink.NavigateUrl = string.Format("~/UI/Pages/LogOn/Default.aspx?ReturnUrl={0}", Request.Path);
            AccountLI.Visible = AccountHyperLink.Visible = false;
            ContactLI.Visible = ContactHyperLink.Visible = false;
            ProfileLI.Visible = ProfileHyperLink.Visible = false;
            PasswordLI.Visible = PasswordHyperLink.Visible = false;
            SecQALI.Visible = QAHyperLink.Visible = false;
            LogoutLinkButton.Visible = false;
        }

        protected void LogoutLinkButton_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();

            LoginHyperLink.Visible = true;

            InitializeLoggedOutView();

            Response.Redirect(FormsAuthentication.LoginUrl);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initialize();
            }
        }
    }
}