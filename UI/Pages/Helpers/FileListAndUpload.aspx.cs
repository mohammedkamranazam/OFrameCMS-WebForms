using System;

namespace ProjectJKL.UI.Pages.Helpers
{
    public partial class FileListAndUpload : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetPopUpThemeFile();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("FileListAndUploadPage"));

                if (!string.IsNullOrWhiteSpace(Request.QueryString["Locale"]))
                {
                    FilesSelectComponent1.Locale = Request.QueryString["Locale"];
                }
            }
        }
    }
}