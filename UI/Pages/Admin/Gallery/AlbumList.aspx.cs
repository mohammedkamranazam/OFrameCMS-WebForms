using OWDARO.Settings;
using System;
using System.Web.UI.WebControls;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class AlbumList : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }

        protected void Page_load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LanguagesDropDownList.DataTextField = "Name";
                LanguagesDropDownList.DataValueField = "Locale";
                LanguagesDropDownList.DataSource = LanguageHelper.GetLanguages();
                LanguagesDropDownList.DataBind();
                LanguagesDropDownList.Items.Insert(0, new ListItem("All", ""));
            }
        }
    }
}