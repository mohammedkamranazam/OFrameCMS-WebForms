using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using System;
using System.Transactions;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace OWDARO.UI.Pages.SuperAdmin
{
    public partial class RoleManage : System.Web.UI.Page
    {
        private void Formtoolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void Formtoolbar1_Delete(object sender, EventArgs e)
        {
            var role = RolesDropDownList.SelectedValue;

            if (role == UserRoles.AnonymousRole)
            {
                StatusMessage.MessageType = StatusMessageType.Warning;
                StatusMessage.Message = "Cannot delete this role. It is default to the system.";
                return;
            }

            using (var scope = new TransactionScope())
            {
                try
                {
                    Roles.DeleteRole(role, true);

                    UserRoleHelper.DeleteRoleSetting(role);

                    RolesDropDownList.Items.Remove(role);

                    SetRoleSettings(RolesDropDownList.SelectedItem.Value);

                    StatusMessage.MessageType = StatusMessageType.Success;
                    StatusMessage.Message = "role successfully deleted";
                }
                catch (Exception ex)
                {
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }

                scope.Complete();
            }
        }

        private void Formtoolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                RoleSettingsComponent1.Role = RolesDropDownList.SelectedValue;

                try
                {
                    UserRoleHelper.SetRoleSetting(RoleSettingsComponent1.RoleSetting);

                    StatusMessage.MessageType = StatusMessageType.Success;
                    StatusMessage.Message = "successfully saved";
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
                Page.Title = String.Format("Cockpit: {0}: {1}", AppConfig.SiteName, "Manage Roles");

                RolesDropDownList.DataSource = Roles.GetAllRoles();

                RolesDropDownList.AddSelect();
                RolesDropDownList.Items.Add(new ListItem(UserRoles.AnonymousRole));
            }

            RolesDropDownList.SelectedIndexChanged += RolesDropDownList_SelectedIndexChanged;
            Formtoolbar1.Save += Formtoolbar1_Save;
            Formtoolbar1.Delete += Formtoolbar1_Delete;
            Formtoolbar1.Cancel += Formtoolbar1_Cancel;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }

        protected void RolesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RolesDropDownList.SelectedValue == "-1")
            {
                return;
            }

            RoleSettingsComponent1.EnableMasterPageEditing = !(RolesDropDownList.SelectedValue == UserRoles.AnonymousRole);

            SetRoleSettings(RolesDropDownList.SelectedValue);
        }

        protected void SetRoleSettings(string role)
        {
            RoleSettingsComponent1.RoleSetting = UserRoleHelper.GetRoleSetting(role);
        }
    }
}