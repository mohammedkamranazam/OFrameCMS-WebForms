using OWDARO;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using System;
using System.IO;
using System.Net;
using System.Web;

namespace ProjectJKL.UI.Pages.Helpers
{
    public partial class Download : System.Web.UI.Page
    {
        private void app_EndRequest(object sender, EventArgs e)
        {
        }

        private void DownloadDocument(int downloadID)
        {
        }

        private void DownloadFile(string fullFileName)
        {
            var server = ServerHelper.GetDefaultServer();

            if (server.IsHttp)
            {
                var url = server.Domain + Path.Combine(server.RootDirectory, server.Path) + fullFileName;

                Response.Redirect(url);
            }
            else
            {
                var guid = Guid.NewGuid().ToString() + "_";
                var tempDownloadName = guid + fullFileName;

                var remoteFileDirectory = server.Path;
                var localDownloadDirectory = Server.MapPath(LocalStorages.Temp);
                var localFullFilePath = Path.Combine(localDownloadDirectory, tempDownloadName);

                FtpHelper.Download(localDownloadDirectory, remoteFileDirectory, fullFileName, tempDownloadName, server.Username, server.Password, server.IP);

                var request = new WebClient();

                var response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + fullFileName);

                var data = request.DownloadData(localFullFilePath);
                response.BinaryWrite(data);
                response.End();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("DownloadPage"));

                if (!string.IsNullOrWhiteSpace(Request.QueryString["DownloadID"]))
                {
                    var downloadID = DataParser.IntParse(Request.QueryString["DownloadID"]);

                    DownloadDocument(downloadID);
                }
            }

            var app = new HttpApplication();
            app.EndRequest += app_EndRequest;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}