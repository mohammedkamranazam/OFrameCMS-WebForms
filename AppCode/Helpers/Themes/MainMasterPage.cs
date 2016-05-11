using System;
using System.Web.UI;

namespace OWDARO.Themes
{
    public class MainMasterPage : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LogoImage.ImageUrl = AppConfig.LogoRelativeURL;
                LogoImage.AlternateText = AppConfig.SiteName;

                HeaderTitleLiteral.Text = AppConfig.HeaderTitle;
                HeaderTagLineLiteral.Text = AppConfig.HeaderTagLine;
            }
        }

        protected global::System.Web.UI.WebControls.ContentPlaceHolder head;

        protected global::System.Web.UI.HtmlControls.HtmlForm form1;

        protected global::AjaxControlToolkit.ToolkitScriptManager ScriptManager1;

        protected global::System.Web.UI.WebControls.ContentPlaceHolder SliderPlaceHolder;

        protected global::System.Web.UI.WebControls.ContentPlaceHolder ContentPlaceHolder1;

        protected global::System.Web.UI.WebControls.ContentPlaceHolder BottomPlaceHolder;

        protected global::System.Web.UI.WebControls.Image LogoImage;

        protected global::System.Web.UI.WebControls.Literal HeaderTagLineLiteral;

        protected global::System.Web.UI.WebControls.Literal HeaderTitleLiteral;

        protected global::System.Web.UI.UpdateProgress UpdateProgress1;

        protected global::AjaxControlToolkit.AlwaysVisibleControlExtender UpdateProgress1_AlwaysVisibleControlExtender;
    }
}