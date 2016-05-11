using System;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class AudioCategoriesList : System.Web.UI.Page
    {
        protected void Page_load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LanguagesDropDownList.DataTextField = "Name";
                LanguagesDropDownList.DataValueField = "Locale";
                LanguagesDropDownList.DataSource = OWDARO.Settings.LanguageHelper.GetLanguages();
                LanguagesDropDownList.DataBind();
                LanguagesDropDownList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", ""));

                AudioCategoriesSelectComponent1.Locale = LanguagesDropDownList.SelectedValue;
            }

            LanguagesDropDownList.SelectedIndexChanged += LanguagesDropDownList_SelectedIndexChanged;
        }

        private async void LanguagesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            AudioCategoriesSelectComponent1.Locale = LanguagesDropDownList.SelectedValue;

            await AudioCategoriesSelectComponent1.LoadData();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}