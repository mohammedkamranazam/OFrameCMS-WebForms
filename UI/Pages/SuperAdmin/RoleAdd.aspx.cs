using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using System;
using System.Transactions;
using System.Web.Security;

namespace OWDARO.UI.Pages.SuperAdmin
{
    public partial class RoleAdd : System.Web.UI.Page
    {
        private void Formtoolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (Roles.RoleExists(RoleNameTextBox.Text))
                {
                    StatusMessage.MessageType = StatusMessageType.Info;
                    StatusMessage.Message = "role already exists";
                    return;
                }

                try
                {
                    using (var scope = new TransactionScope())
                    {
                        Roles.CreateRole(RoleNameTextBox.Text);

                        RoleSettingsComponent1.Role = RoleNameTextBox.Text;
                        UserRoleHelper.AddRoleSetting(RoleSettingsComponent1.RoleSetting);

                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = "role successfully added";

                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = String.Format("Cockpit: {0}: {1}", AppConfig.SiteName, "Add Role");
            }

            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += Formtoolbar1_Cancel;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}