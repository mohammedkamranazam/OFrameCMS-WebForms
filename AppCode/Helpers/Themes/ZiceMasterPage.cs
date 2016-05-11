using OWDARO.BLL.MembershipBLL;
using OWDARO.Util;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Web.Security;
using System.Web.UI;

namespace OWDARO.Themes
{
    public class ZiceMasterPage : MasterPage
    {
        private void AddBranding()
        {
            //   var iframeHTML = "<div style=\"padding:25px 0px 0px 0px; float:left; margin:0px 0px 20px 0px;\"><iframe src=\"http://owdaro.com/ad-frame.aspx?id=cockpit\" width=\"300\" height=\"100\" frameborder=\"0\" scrolling=\"no\"></iframe></div>";
            //  TopColumn.Controls.AddAt(0, new LiteralControl(iframeHTML));

            var owdaroFooterHtml = String.Format("<div style=\"visibility: visible; z-index: 1; border-top: 1px solid #eee; display: inline-block; opacity: 1; font-size: 10px; color: #999999; font-family: Verdana; margin: 20px 0px 10px 0px; padding: 10px 0px 20px 0px; position: static; top: 0; left: 0; bottom: 0; right: 0;\">&copy; Copyright {0} <span class=\"tip\"><a href=\"http://owdaro.com\" title=\"www.owdaro.com\">OWDARO</a></span> | {1}</div>", Utilities.DateTimeNow().Year, GetProductVersionInfo());

            BrandingLiteral.Text = owdaroFooterHtml;
        }

        private string GetProductVersionInfo()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return string.Format("<span class='ProductName'>{0}</span><span class='Version'> | Version <a href='{2}'>{1}</a></span>",
                fileVersionInfo.ProductName,
                fileVersionInfo.ProductVersion,
                string.Format("http://owdaro.com/products.aspx?cockpit={0}",
                fileVersionInfo.ProductVersion));
        }

        private string GetProfileImageURL()
        {
            var imageURL = AppConfig.MaleAvatar;

            var user = Membership.GetUser();

            if (user != null)
            {
                imageURL = UserBL.GetUserByUsername(user.UserName).ProfilePic;
            }

            return imageURL;
        }

        private void SetAvatarImage()
        {
            var keyValue = string.Empty;
            var performanceKey = Constants.Keys.AvatarPathPerformanceKey;

            Func<string> fnc = new Func<string>(GetProfileImageURL);

            var args = new object[] { };

            Utilities.GetPerformance<string>(PerformanceMode.Session, performanceKey, out keyValue, fnc, args);

            ProfileAvatarImage.ImageUrl = keyValue;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SecurityQAHyperLink.Visible = AppConfig.EnableQA;
            }

            AddBranding();
            SetAvatarImage();
        }

        protected global::System.Web.UI.HtmlControls.HtmlTitle Title;

        protected global::System.Web.UI.WebControls.ContentPlaceHolder head;

        protected global::System.Web.UI.HtmlControls.HtmlForm form1;

        protected global::AjaxControlToolkit.ToolkitScriptManager ScriptManager1;

        protected global::System.Web.UI.WebControls.TextBox Zice_Admin_text_search;

        protected global::System.Web.UI.WebControls.Image ProfileAvatarImage;

        protected global::System.Web.UI.WebControls.LoginName LoginName1;

        protected global::System.Web.UI.WebControls.Image GearImage;

        protected global::AjaxControlToolkit.HoverMenuExtender GearImage_HoverMenuExtender;

        protected global::System.Web.UI.WebControls.ContentPlaceHolder ContentPlaceHolder1;

        protected global::System.Web.UI.WebControls.Literal BrandingLiteral;

        protected global::System.Web.UI.HtmlControls.HtmlGenericControl TopColumn;

        protected global::System.Web.UI.WebControls.ContentPlaceHolder ContentPlaceHolder2;

        protected global::System.Web.UI.WebControls.ContentPlaceHolder ContentPlaceHolder3;

        protected global::System.Web.UI.UpdateProgress UpdateProgress1;

        protected global::AjaxControlToolkit.AlwaysVisibleControlExtender UpdateProgress1_AlwaysVisibleControlExtender;

        protected global::System.Web.UI.WebControls.Panel ProfileOptionsPanel;

        protected global::System.Web.UI.WebControls.HyperLink HyperLink1;

        protected global::System.Web.UI.WebControls.HyperLink HyperLink2;

        protected global::System.Web.UI.WebControls.HyperLink HyperLink3;

        protected global::System.Web.UI.WebControls.HyperLink SecurityQAHyperLink;
    }
}