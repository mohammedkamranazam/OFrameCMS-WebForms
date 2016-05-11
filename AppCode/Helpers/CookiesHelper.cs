using System;
using System.Web;

namespace OWDARO.Helpers
{
    public static class CookiesHelper
    {
        public static string GetCookie(string key)
        {
            var cookie = HttpContext.Current.Request.Cookies[key];
            return (cookie == null) ? null : cookie.Value;
        }

        public static void SetCookie(string key, string value, DateTime expires)
        {
            var cookie = new HttpCookie(key, value);
            cookie.Expires = expires;
            cookie.HttpOnly = true;
            HttpContext.Current.Response.AppendCookie(cookie);
        }
    }
}