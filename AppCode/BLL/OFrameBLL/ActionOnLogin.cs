using OWDARO.BLL.MembershipBLL;
using OWDARO.Util;
using ProjectJKL.BLL.ShoppingCartBLL;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace OWDARO.BLL.OFrameBLL
{
    public static class ActionOnLogin
    {
        public static void ExecuteAtLogin(string username)
        {
            Utilities.ClearOldPerformanceKeys();

            if (AppConfig.ShoppingCartEnabled)
            {
                var execute = CartProcessingBL.MoveFromTempCartToUserCartAsync(username);

                Task.WhenAll(execute);
            }
        }

        public static void Login(string username, bool rememberMe)
        {
            Login(username, rememberMe, true, false, string.Empty);
        }

        public static void Login(string username, bool rememberMe, bool useRedirect, bool useCustomRedirect, string customRedirectURL)
        {
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            FormsAuthentication.SetAuthCookie(username, rememberMe);

            ExecuteAtLogin(username);

            RedirectToUrl(username, useRedirect, useCustomRedirect, customRedirectURL);
        }

        public static void RedirectToUrl(string username, bool useRedirect, bool useCustomRedirect, string customRedirectURL)
        {
            if (useRedirect)
            {
                if (useCustomRedirect)
                {
                    HttpContext.Current.Response.Redirect(customRedirectURL, false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    if (AppConfig.UseLoginReturnURL)
                    {
                        if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["ReturnUrl"]))
                        {
                            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.QueryString["ReturnUrl"], false);
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect(UserBL.GetRootFolder(username), false);
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect(UserBL.GetRootFolder(username), false);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri, false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
    }
}