using OWDARO.Util;
using System;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class PhotoList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["AlbumId"]))
                {
                    //LanguagesDropDownList.DataTextField = "Name";
                    //LanguagesDropDownList.DataValueField = "Locale";
                    //LanguagesDropDownList.DataSource = OWDARO.Settings.LanguageHelper.GetLanguages();
                    //LanguagesDropDownList.DataBind();
                    //LanguagesDropDownList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", ""));

                    var albumID = DataParser.IntParse(Request.QueryString["AlbumId"]);

                    PhotoAddHyperLink.NavigateUrl = String.Format("~/UI/Pages/Admin/Gallery/PhotoAdd.aspx?AlbumId={0}", albumID);
                    AlbumManageHyperLink.NavigateUrl = String.Format("~/UI/Pages/Admin/Gallery/AlbumManage.aspx?AlbumId={0}", albumID);
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/AlbumList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}