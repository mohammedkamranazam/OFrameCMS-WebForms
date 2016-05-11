using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class RoleSettingsComponent : System.Web.UI.UserControl
    {
        public bool EnableMasterPageEditing
        {
            get
            {
                return MasterPagesDropDownList.Visible;
            }

            set
            {
                MasterPagesDropDownList.Visible = value;
            }
        }

        public string Role
        {
            set;
            get;
        }

        public UserRoleSettings RoleSetting
        {
            get
            {
                var roleSet = new UserRoleSettings();

                roleSet.Name = Role;
                roleSet.Path = GetSelectedPath();
                roleSet.HideSuperAdmin = HideSuperAdminCheckBox.Checked;
                roleSet.Login = LoginCheckBox.Checked;
                roleSet.RegistrationBlocked = RegBlockCheckBox.Checked;
                roleSet.ShowCategory = ShowCategoryCheckBox.Checked;
                roleSet.ShowRoles = ShowRolesCheckBox.Checked;

                if (EnableMasterPageEditing)
                {
                    roleSet.MasterPage = MasterPagesDropDownList.SelectedValue;
                }
                else
                {
                    roleSet.MasterPage = UserRoleHelper.GetRoleMasterPage(Role);
                }

                return roleSet;
            }

            set
            {
                SetSelectedPath(value.Path);
                MasterPagesDropDownList.SelectedValue = value.MasterPage;
                HideSuperAdminCheckBox.Checked = value.HideSuperAdmin;
                LoginCheckBox.Checked = value.Login;
                RegBlockCheckBox.Checked = value.RegistrationBlocked;
                ShowCategoryCheckBox.Checked = value.ShowCategory;
                ShowRolesCheckBox.Checked = value.ShowRoles;
            }
        }

        private string GetSelectedPath()
        {
            if (TreeView1.SelectedNode == null)
            {
                return PathLabel.Text;
            }

            var length = HttpRuntime.AppDomainAppPath.Length;

            var path = TreeView1.SelectedNode.Value.Substring(length);

            path = path.Replace('\\', '/');

            path = "~/" + path;

            return path;
        }

        private void Initialize()
        {
            var dir = new DirectoryInfo(Server.MapPath("~/UI/Pages/MasterPages"));

            var masterPages = dir.GetFiles("*.Master", SearchOption.AllDirectories);

            foreach (FileInfo masterPage in masterPages)
            {
                MasterPagesDropDownList.Items.Add(new ListItem(masterPage.Name, String.Format("~/UI/Pages/MasterPages/{0}", masterPage.Name)));
            }

            Utilities.BuildTree(TreeView1, string.Empty, true);
        }

        private void SetSelectedPath(string path)
        {
            PathLabel.Text = path;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initialize();
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            PathLabel.Text = GetSelectedPath();
        }
    }
}