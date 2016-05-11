using OWDARO.BLL.MembershipBLL;
using System;
using System.Web.Security;

namespace OWDARO.UI.Pages.Common
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder());
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var user = Membership.GetUser();

                if (user != null)
                {
                    if (user.ChangePassword(CurrentPasswordTextBox.Text, PasswordConfirmationComponent.Password))
                    {
                        StatusLabel.MessageType = StatusMessageType.Success;
                        StatusLabel.Message = "password successfully changed";
                    }
                    else
                    {
                        StatusLabel.MessageType = StatusMessageType.Error;
                        StatusLabel.Message = "incorrect password";
                    }
                }
                else
                {
                    StatusLabel.MessageType = StatusMessageType.Warning;
                    StatusLabel.Message = "invalid logged in user";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("ChangePasswordPage"));
            }

            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += new EventHandler(FormToolbar1_Cancel);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Title = AppConfig.SiteName + ": Change Password";
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetRoleThemeFile();
        }
    }
}