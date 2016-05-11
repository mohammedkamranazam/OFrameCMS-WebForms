using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace OWDARO.UI.Pages.SuperAdmin
{
    public partial class AccessManager : System.Web.UI.Page
    {
        private void AddRule()
        {
            var access = new PathAccess();
            access.Path = DirectoryLbl.Text;

            if (AllowRadioButton.Checked)
            {
                access.AccessType = "allow";
            }
            else
            {
                access.AccessType = "deny";
            }

            if (RolesRadioButton.Checked)
            {
                access.AccessLevel = "roles";
                access.AccessTo = RolesDropDown.SelectedItem.Value;
            }
            else
            {
                if (UserRadioButton.Checked)
                {
                    access.AccessLevel = "users";
                    access.AccessTo = UserDropDown.SelectedItem.Value;
                }
                else
                {
                    if (AllUsersRadioButton.Checked)
                    {
                        access.AccessLevel = "users";
                        access.AccessTo = "*";
                    }
                    else
                    {
                        if (AnonymousRadioButton.Checked)
                        {
                            access.AccessLevel = "users";
                            access.AccessTo = "?";
                        }
                    }
                }
            }
            if (PositionRadioButton.Checked)
            {
                WebConfigAccessHelper.AddRuleAt(access, DataParser.IntParse(RuleCountDropDown.SelectedItem.Value) - 1);
            }
            else
            {
                if (AppendRadioButton.Checked)
                {
                    WebConfigAccessHelper.AddRule(access, false);
                }
                else
                {
                    if (PrependRadioButton.Checked)
                    {
                        WebConfigAccessHelper.AddRule(access, true);
                    }
                }
            }
            BindRulesToGridView();
        }

        private void BindRulesToGridView()
        {
            GridView1.DataSource = WebConfigAccessHelper.GetRulesForPath(DirectoryLbl.Text);
            GridView1.DataBind();
            XMLTextBox.Text = WebConfigAccessHelper.GetPathLocationXML(DirectoryLbl.Text);

            var ruleCount = GridView1.Rows.Count;

            RuleCountDropDown.Items.Clear();

            for (var x = 1; x <= Math.Max(ruleCount, 1); x++)
            {
                RuleCountDropDown.Items.Add(new ListItem(x.ToString(), x.ToString()));
            }
        }

        protected void AddRuleButton_Click(object sender, EventArgs e)
        {
            WebConfigAccessHelper.CreatePathLocation(DirectoryLbl.Text);

            AddRule();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var AccessTypeHiddenField = GridView1.Rows[e.RowIndex].FindControl("AccessTypeHiddenField") as HiddenField;
            var AccessLevelHiddenField = GridView1.Rows[e.RowIndex].FindControl("AccessLevelHiddenField") as HiddenField;
            var AccessToHiddenField = GridView1.Rows[e.RowIndex].FindControl("AccessToHiddenField") as HiddenField;

            var access = new PathAccess();
            access.AccessType = AccessTypeHiddenField.Value;
            access.AccessLevel = AccessLevelHiddenField.Value;
            access.AccessTo = AccessToHiddenField.Value;
            access.Path = DirectoryLbl.Text;

            WebConfigAccessHelper.RemoveRule(access);

            BindRulesToGridView();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = String.Format("Cockpit: {0}: {1}", AppConfig.SiteName, "Access Manager");

                var separators = new char[] { ';' };
                var patterns = AppConfig.AccessManagerSearchPatters.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                Utilities.BuildTree(TreeView1, string.Empty, patterns);

                RolesDropDown.DataSource = Roles.GetAllRoles();
                RolesDropDown.DataBind();

                UserDropDown.DataSource = Membership.GetAllUsers();
                UserDropDown.DataBind();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            var length = HttpRuntime.AppDomainAppPath.Length;

            var path = TreeView1.SelectedNode.Value.Substring(length);

            path = path.Replace('\\', '/');

            DirectoryLbl.Text = path;

            BindRulesToGridView();
        }
    }
}