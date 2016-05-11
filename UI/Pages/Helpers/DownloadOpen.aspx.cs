using OWDARO.BLL.GalleryBLL;
using OWDARO.Util;
using System;

namespace ProjectJKL.UI.Pages.Helpers
{
    public partial class DownloadOpen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("DownloadOpenPage"));

                if (string.IsNullOrWhiteSpace(Request.QueryString["FileID"]))
                {
                    Response.Redirect("~/Drives.aspx");
                }

                var fileID = DataParser.LongParse(Request.QueryString["FileID"]);

                var fileQuery = FilesBL.GetObjectByID(fileID);

                Response.Redirect(fileQuery.FileURL);
            }
        }
    }
}