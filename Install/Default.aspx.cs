using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Models;
using OWDARO.Settings;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Web.Security;

namespace OWDARO.Install
{
    public partial class Default : System.Web.UI.Page
    {
        protected bool AddApplicationRoleSettings()
        {
            var success = false;

            UserRoleSettings roleSetting;

            try
            {
                StatusLabel.Text += "<span style='color:Blue;'>Adding Application Role Settings...</span><br />";

                roleSetting = new UserRoleSettings();
                roleSetting.HideSuperAdmin = false;
                roleSetting.Login = false;
                roleSetting.MasterPage = "~/UI/Pages/MasterPages/SuperAdmin_Zice.master";
                roleSetting.Theme = AppConfig.ZiceTheme;
                roleSetting.Path = "~/UI/Pages/SuperAdmin/";
                roleSetting.RegistrationBlocked = false;
                roleSetting.Name = UserRoles.SuperAdminRole;
                roleSetting.ShowCategory = true;
                roleSetting.ShowRoles = true;
                UserRoleHelper.AddRoleSetting(roleSetting);

                roleSetting = new UserRoleSettings();
                roleSetting.HideSuperAdmin = true;
                roleSetting.Login = false;
                roleSetting.MasterPage = "~/UI/Pages/MasterPages/Admin_Zice.master";
                roleSetting.Theme = AppConfig.ZiceTheme;
                roleSetting.Path = "~/UI/Pages/Admin/";
                roleSetting.RegistrationBlocked = false;
                roleSetting.Name = UserRoles.AdminRole;
                roleSetting.ShowCategory = true;
                roleSetting.ShowRoles = true;
                UserRoleHelper.AddRoleSetting(roleSetting);

                roleSetting = new UserRoleSettings();
                roleSetting.HideSuperAdmin = true;
                roleSetting.Login = false;
                roleSetting.MasterPage = "~/UI/Pages/MasterPages/User_Main.master";
                roleSetting.Theme = AppConfig.MainTheme;
                roleSetting.Path = "~/UI/Pages/User/";
                roleSetting.RegistrationBlocked = true;
                roleSetting.Name = UserRoles.UserRole;
                roleSetting.ShowCategory = false;
                roleSetting.ShowRoles = false;
                UserRoleHelper.AddRoleSetting(roleSetting);

                roleSetting = new UserRoleSettings();
                roleSetting.HideSuperAdmin = true;
                roleSetting.Login = false;
                roleSetting.MasterPage = "~/UI/Pages/MasterPages/Inactivated_Main.master";
                roleSetting.Theme = AppConfig.MainTheme;
                roleSetting.Path = "~/UI/Pages/Inactivated/";
                roleSetting.RegistrationBlocked = true;
                roleSetting.Name = UserRoles.InactivatedRole;
                roleSetting.ShowCategory = false;
                roleSetting.ShowRoles = false;
                UserRoleHelper.AddRoleSetting(roleSetting);

                StatusLabel.Text += "<span style='color:green;'>Application Role Settings Added Successfully...</span><br />";

                success = true;
            }
            catch (Exception ex)
            {
                StatusLabel.Text += "<span style='color:Red;'>Adding Application Role Settings Failed...</span><br />" + ExceptionHelper.GetExceptionMessage(ex);
            }

            return success;
        }

        protected bool AddDefaultRoleName()
        {
            var success = false;

            try
            {
                UserRoleHelper.SetDefaultRoleName("user");

                StatusLabel.Text += "<span style='color:Green;'>Default Role Name Set Successfully...</span><br />";

                success = true;
            }
            catch
            {
                StatusLabel.Text += "<span style='color:Red;'>Setting Default Role Name Failed...</span><br />";
            }

            return success;
        }

        protected bool AddUser()
        {
            var success = false;

            var user = new MS_Users();
            user.ProfilePic = AppConfig.MaleAvatar;
            user.UserCategoryID = null;
            user.UserRole = UserRoles.SuperAdminRole;
            user.Email = AppConfig.WebsiteMainEmail;
            user.Name = "Super Administrator";
            user.SecurityAnswer = Guid.NewGuid().ToString();
            user.SecurityQuestion = Guid.NewGuid().ToString();
            user.Username = "master";
            var password = "123456";

            StatusLabel.Text += "<span style='color:Blue;'>Adding User Profile...</span><br />";

            try
            {
                success = UserBL.Add(user);
            }
            catch (Exception ex)
            {
                StatusLabel.Text += "<span style='color:Red;'>User Profile Addition Failed...</span><br />" + ExceptionHelper.GetExceptionMessage(ex);
            }

            if (success)
            {
                StatusLabel.Text += "<span style='color:green;'>User Profile Added Successfully...</span><br />";

                StatusLabel.Text += "<span style='color:Blue;'>Creating User Login...</span><br />";

                MembershipCreateStatus creationStatus;

                Membership.CreateUser(user.Username, password, user.Email, user.SecurityQuestion, user.SecurityAnswer, true, out creationStatus);

                switch (creationStatus)
                {
                    case MembershipCreateStatus.Success:
                        StatusLabel.Text += "<span style='color:green;'>User Login Created Successfully...</span><br />";
                        StatusLabel.Text += "<span style='color:Blue;'>Adding User To Application Role...</span><br />";
                        Roles.AddUserToRole(user.Username, UserRoles.SuperAdminRole);
                        StatusLabel.Text += "<span style='color:green;'>User Added To Application Role Successfully...</span><br />";
                        success = true;
                        break;

                    case MembershipCreateStatus.DuplicateUserName:
                        StatusLabel.Text = "Duplicate Username";
                        StatusLabel.Text += "<span style='color:Red;'>Duplicate Username...</span><br />";
                        success = false;
                        break;

                    case MembershipCreateStatus.InvalidUserName:
                        StatusLabel.Text = "Invalid Username";
                        StatusLabel.Text += "<span style='color:Red;'>Invalid Username...</span><br />";
                        success = false;
                        break;

                    case MembershipCreateStatus.UserRejected:
                        StatusLabel.Text = "User Rejected";
                        StatusLabel.Text += "<span style='color:Red;'>User Rejected...</span><br />";
                        success = false;
                        break;

                    case MembershipCreateStatus.DuplicateEmail:
                        StatusLabel.Text = "Duplicate Email";
                        StatusLabel.Text += "<span style='color:Red;'>Duplicate Email ID...</span><br />";
                        success = false;
                        break;

                    case MembershipCreateStatus.InvalidEmail:
                        StatusLabel.Text = "Invalid Email";
                        StatusLabel.Text += "<span style='color:Red;'>Invalid Email ID...</span><br />";
                        success = false;
                        break;

                    case MembershipCreateStatus.InvalidPassword:
                        StatusLabel.Text = "Invalid Password";
                        StatusLabel.Text += "<span style='color:Red;'>Invalid Password...</span><br />";
                        success = false;
                        break;

                    default:
                        StatusLabel.Text = "Unsuccessful";
                        StatusLabel.Text += "<span style='color:Red;'>User Login Creation Failed...</span><br />";
                        success = false;
                        break;
                }

                if (!success)
                {
                    StatusLabel.Text += "<span style='color:Red;'>Deleting User Profile...</span><br />";

                    UserBL.Delete(user.Username);

                    StatusLabel.Text += "<span style='color:Red;'>User Profile Deleted Successfully...</span><br />";
                }
            }
            else
            {
                StatusLabel.Text += "<span style='color:Red;'>User Profile Addition Failed...</span><br />";
            }

            return success;
        }

        protected bool CreateApplicationRoles()
        {
            var success = false;

            try
            {
                StatusLabel.Text += "<span style='color:Blue;'>Creating Application Roles...</span><br />";

                Roles.CreateRole(UserRoles.SuperAdminRole);
                Roles.CreateRole(UserRoles.AdminRole);
                Roles.CreateRole(UserRoles.UserRole);
                Roles.CreateRole(UserRoles.InactivatedRole);

                StatusLabel.Text += "<span style='color:green;'>Application Roles Created Successfully...</span><br />";

                success = true;
            }
            catch (Exception ex)
            {
                StatusLabel.Text += "<span style='color:Red;'>Creating Application Roles Failed...</span><br />" + ExceptionHelper.GetExceptionMessage(ex);
            }

            return success;
        }

        protected bool CreateDatabase()
        {
            return true;
        }

        protected bool CreateDatabaseUser()
        {
            return true;
        }

        protected bool DeleteApplicationRoles()
        {
            var success = false;

            try
            {
                StatusLabel.Text += "<span style='color:Red;'>Deleting Application Roles...</span><br />";

                Roles.DeleteRole(UserRoles.SuperAdminRole);
                Roles.DeleteRole(UserRoles.AdminRole);
                Roles.DeleteRole(UserRoles.UserRole);
                Roles.DeleteRole(UserRoles.InactivatedRole);

                StatusLabel.Text += "<span style='color:Red;'>Application Roles Deleted Successfully...</span><br />";

                success = true;
            }
            catch (Exception ex)
            {
                StatusLabel.Text += "<span style='color:Red;'>Deleting Application Roles Failed...</span><br />" + ExceptionHelper.GetExceptionMessage(ex);
            }

            return success;
        }

        protected bool DeleteApplicationRoleSettings()
        {
            var success = false;

            try
            {
                StatusLabel.Text += "<span style='color:Red;'>Deleting Application Role Settings...</span><br />";

                UserRoleHelper.DeleteRoleSetting(UserRoles.SuperAdminRole);
                UserRoleHelper.DeleteRoleSetting(UserRoles.AdminRole);
                UserRoleHelper.DeleteRoleSetting(UserRoles.UserRole);
                UserRoleHelper.DeleteRoleSetting(UserRoles.InactivatedRole);

                StatusLabel.Text += "<span style='color:Red;'>Application Role Settings Deleted Successfully...</span><br />";

                success = true;
            }
            catch (Exception ex)
            {
                StatusLabel.Text += "<span style='color:Red;'>Deleting Application Role Settings Failed...</span><br />" + ExceptionHelper.GetExceptionMessage(ex);
            }

            return success;
        }

        protected bool DeleteDatabase()
        {
            return true;
        }

        protected bool DeleteDatabaseUser()
        {
            return true;
        }

        protected bool DeleteDefaultRoleName()
        {
            var success = false;

            try
            {
                StatusLabel.Text += "<span style='color:Red;'>Deleting Default Role Name...</span><br />";

                UserRoleHelper.SetDefaultRoleName(string.Empty);

                StatusLabel.Text += "<span style='color:Red;'>Default Role Name Deleted Successfully...</span><br />";

                success = true;
            }
            catch (Exception ex)
            {
                StatusLabel.Text += "<span style='color:Red;'>Delete Default Role Name Failed...</span><br />" + ExceptionHelper.GetExceptionMessage(ex);
            }

            return success;
        }

        protected void InstallButton_Click(object sender, EventArgs e)
        {
            if (!AppConfig.AppInstalled)
            {
                StatusLabel.Text = string.Empty;
                StatusLabel.Text += "<span style='color:Blue;'>Application Installation Started...</span><br />";
                if (CreateDatabase())
                {
                    if (CreateDatabaseUser())
                    {
                        if (InstallDatabaseTables())
                        {
                            if (CreateApplicationRoles())
                            {
                                if (AddApplicationRoleSettings())
                                {
                                    if (AddDefaultRoleName())
                                    {
                                        if (AddUser())
                                        {
                                            StatusLabel.Text += "<span style='color:green;'>Installation Completed Successfully...</span><br />";
                                            AppConfig.AppInstalled = true;
                                        }
                                        else
                                        {
                                            RollBack();
                                        }
                                    }
                                    else
                                    {
                                        RollBack();
                                    }
                                }
                                else
                                {
                                    RollBack();
                                }
                            }
                            else
                            {
                                RollBack();
                            }
                        }
                        else
                        {
                            RollBack();
                        }
                    }
                    else
                    {

                        RollBack();
                    }
                }
                else
                {
                    StatusLabel.Text += "<span style='color:Red;'>Application Installation Failed...</span><br />";
                }
            }
            else
            {
                StatusLabel.Text += "<span style='color:Orange;'>Application Already Installed...</span><br />";
            }

            StatusLabel.Text += "<hr style='margin:5px; color:#333333; height:1px; width:100%; background:#333333;' />";
        }

        private void RollBack()
        {
            StatusLabel.Text += "<span style='color:Red;'>Rolling Back...</span><br />";

            DeleteUsers();
            DeleteDefaultRoleName();
            DeleteApplicationRoleSettings();
            DeleteApplicationRoles();
            UnInstallDatabaseTables();
            DeleteDatabaseUser();
            DeleteDatabase();

            StatusLabel.Text += "<span style='color:Red;'>Roll Back Finished Successfully...</span><br />";

        }

        protected bool DeleteUsers()
        {
            var success = false;

            try
            {
                StatusLabel.Text += "<span style='color:Red;'>Deleting Users...</span><br />";

                Membership.DeleteUser("master", true);

                StatusLabel.Text += "<span style='color:Red;'>Users Deleted Successfully...</span><br />";

                success = true;
            }
            catch (Exception ex)
            {
                StatusLabel.Text += "<span style='color:Red;'>Deleting Users Failed...</span><br />" + ExceptionHelper.GetExceptionMessage(ex);
            }

            return success;
        }

        protected bool InstallDatabaseTables()
        {
            var success = false;

            try
            {
                StatusLabel.Text += "<span style='color:Blue;'>Installing Database Tables...</span><br />";

                System.Web.Management.SqlServices.Install(ServerTextBox.Text, UserNameTextBox.Text, PasswordTextBox.Text, DataBaseTextBox.Text, System.Web.Management.SqlFeatures.All);

                StatusLabel.Text += "<span style='color:green;'>Database Tables Installed Successfully...</span><br />";

                success = true;
            }
            catch (Exception ex)
            {
                StatusLabel.Text += "<span style='color:Red;'>Database Tables Installation Failed...</span><br />" + ExceptionHelper.GetExceptionMessage(ex);
            }

            return success;
        }

        protected bool UnInstallDatabaseTables()
        {
            var success = false;

            try
            {
                StatusLabel.Text += "<span style='color:red;'>Uinstalling Database Tables...</span><br />";

                System.Web.Management.SqlServices.Uninstall(ServerTextBox.Text, UserNameTextBox.Text, PasswordTextBox.Text, DataBaseTextBox.Text, System.Web.Management.SqlFeatures.All);

                StatusLabel.Text += "<span style='color:red;'>Database Tables Uinstalled Successfully...</span><br />";

                success = true;
            }
            catch (Exception ex)
            {
                StatusLabel.Text += "<span style='color:Red;'>Database Tables Uninstallation Failed...</span><br />" + ExceptionHelper.GetExceptionMessage(ex);
            }

            return success;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("InstallPage"));

                if (!AppConfig.AppInstalled)
                {
                    FormPanel.Visible = true;
                }
                else
                {
                    FormPanel.Visible = false;
                }
            }
        }
    }
}