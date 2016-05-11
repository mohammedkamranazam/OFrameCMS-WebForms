using Nemiro.OAuth;
using OWDARO;
using OWDARO.Settings;
using OWDARO.Util;
using System;

namespace ProjectJKL.UI.UserControls.Components.OFrameMembership.Nested
{
    public partial class OAuthComponent : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AppConfig.EnableOAuthRegistration)
                {
                    TitleLiteral.Text = LanguageHelper.GetKey("OAuthComponentTitle");
                    string returnURL = Utilities.GetAbsoluteURL("~/UI/Pages/LogOn/OAuthRegister.aspx");

                    if (!string.IsNullOrWhiteSpace(AppConfig.FacebookAPIKey) && !string.IsNullOrWhiteSpace(AppConfig.FacebookSecretKey))
                    {
                        try
                        {
                            FacebookHyperLink.Visible = true;
                            FacebookHyperLink.NavigateUrl = OAuthWeb.GetAuthorizationUrl("facebook", returnURL);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(AppConfig.GoogleAPIKey) && !string.IsNullOrWhiteSpace(AppConfig.GoogleSecretKey))
                    {
                        try
                        {
                            GoogleHyperLink.Visible = true;
                            GoogleHyperLink.NavigateUrl = OAuthWeb.GetAuthorizationUrl("Google", returnURL);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(AppConfig.TwitterAPIKey) && !string.IsNullOrWhiteSpace(AppConfig.TwitterSecretKey))
                    {
                        try
                        {
                            TwitterHyperLink.NavigateUrl = OAuthWeb.GetAuthorizationUrl("Twitter", returnURL);
                            TwitterHyperLink.Visible = true;
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);
                        }
                    }
                }
            }
        }
    }
}