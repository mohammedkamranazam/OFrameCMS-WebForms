using Nemiro.OAuth;
using Nemiro.OAuth.Clients;
using OWDARO;
using OWDARO.Util;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProjectJKL
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;

            if (app != null && app.Context != null)
            {
                app.Context.Response.Headers.Remove("Server");

                if (app.Context.Request.Url.LocalPath.EndsWith("/"))
                {
                    app.Context.RewritePath(string.Concat(app.Context.Request.Url.LocalPath, "Default"));
                }
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ErrorLogger.LogError(Server.GetLastError());
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            if (AppConfig.EnableOAuthRegistration)
            {
                if (!string.IsNullOrWhiteSpace(AppConfig.FacebookAPIKey) || !string.IsNullOrWhiteSpace(AppConfig.FacebookSecretKey))
                {
                    OAuthManager.RegisterClient
                               (new FacebookClient(AppConfig.FacebookAPIKey, AppConfig.FacebookSecretKey));
                }

                if (!string.IsNullOrWhiteSpace(AppConfig.GoogleAPIKey) || !string.IsNullOrWhiteSpace(AppConfig.GoogleSecretKey))
                {
                    OAuthManager.RegisterClient
                               (new GoogleClient(AppConfig.GoogleAPIKey, AppConfig.GoogleSecretKey));
                }

                if (!string.IsNullOrWhiteSpace(AppConfig.TwitterAPIKey) || !string.IsNullOrWhiteSpace(AppConfig.TwitterSecretKey))
                {
                    OAuthManager.RegisterClient
                               (new TwitterClient(AppConfig.TwitterAPIKey, AppConfig.TwitterSecretKey));
                }
            }

            //if (!string.IsNullOrWhiteSpace(AppConfig.MAITelemetricKey))
            //{
            //    Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration.Active.InstrumentationKey = AppConfig.MAITelemetricKey;
            //}
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }
    }
}