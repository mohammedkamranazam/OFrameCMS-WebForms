using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using System;
using System.Web;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Others.Nested
{
    public partial class LocalizationSwitchComponent : UserControl
    {
        protected void LanguagesDropDownList_SelectedIndexChanged1(object sender, EventArgs e)
        {
            var now = Utilities.DateTimeNow();

            CookiesHelper.SetCookie(Constants.Keys.CurrentCultureCookieKey, LanguagesDropDownList.SelectedValue, now.AddDays(1));

            var direction = LanguageHelper.GetLocaleDirection(LanguagesDropDownList.SelectedValue);

            CookiesHelper.SetCookie(Constants.Keys.CurrentCultureDirectionCookieKey, direction, now.AddDays(1));

            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Set("Lang", LanguagesDropDownList.SelectedValue);
            var url = Request.Url.AbsolutePath;
            var updatedQueryString = String.Format("?{0}", nameValues);
            Response.Redirect(url + updatedQueryString, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool isMultilingual = AppConfig.IsSiteMultiLingual;
                this.Visible = isMultilingual;

                var locale = AppConfig.DefaultLocale;
                var now = Utilities.DateTimeNow();

                if (!string.IsNullOrWhiteSpace(Request.QueryString["Lang"]) && isMultilingual)
                {
                    locale = Request.QueryString["Lang"];
                }
                else
                {
                    var currentLocale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);

                    if (!string.IsNullOrWhiteSpace(currentLocale))
                    {
                        locale = currentLocale;
                    }
                }

                LanguagesDropDownList.DataTextField = "Name";
                LanguagesDropDownList.DataValueField = "Locale";
                LanguagesDropDownList.DataSource = LanguageHelper.GetLanguages();
                LanguagesDropDownList.DataBind();
                LanguagesDropDownList.SelectedValue = locale;

                CookiesHelper.SetCookie(Constants.Keys.CurrentCultureCookieKey, locale, now.AddDays(1));

                CookiesHelper.SetCookie(Constants.Keys.CurrentCultureDirectionCookieKey, LanguageHelper.GetLocaleDirection(locale), now.AddDays(1));
            }
        }
    }
}