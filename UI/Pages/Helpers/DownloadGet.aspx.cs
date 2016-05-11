using OWDARO.BLL.GalleryBLL;
using OWDARO.Util;
using System;

namespace ProjectJKL.UI.Pages.Helpers
{
    public partial class DownloadGet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("DownloadGetPage"));

                if (string.IsNullOrWhiteSpace(Request.QueryString["FileID"]))
                {
                    Response.Redirect("~/Drives.aspx");
                }

                var fileID = DataParser.LongParse(Request.QueryString["FileID"]);

                var fileQuery = FilesBL.GetObjectByID(fileID);

                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", fileQuery.FileName));
                Response.TransmitFile(Server.MapPath(fileQuery.FileURL));
                Response.End();
            }
        }
    }
}