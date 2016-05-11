using Nemiro.OAuth;
using OWDARO;
using OWDARO.BLL.MembershipBLL;
using OWDARO.BLL.OFrameBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Web.Security;

namespace ProjectJKL.UI.Pages.LogOn
{
    public partial class OAuthRegister : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetMainThemeFile();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var result = OAuthWeb.VerifyAuthorization();

            if (result.IsSuccessfully)
            {
                RegisterationHandler(result.UserInfo);
            }
            else
            {
                Response.Redirect("~/UI/Pages/LogOn/", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void RegisterationHandler(UserInfo userInfo)
        {
            using (var context = new MembershipEntities())
            {
                var user = UserBL.GetUserFromEmail(userInfo.Email, context);

                if (user != null)
                {
                    ActionOnLogin.Login(user.Email, true, true, false, string.Empty);
                }
                else
                {
                    SigUpUser(userInfo, context);
                }
            }
        }

        private void SigUpUser(UserInfo userInfo, MembershipEntities context)
        {
            var roleName = UserRoles.DefaultRole;
            var roleRootFolder = UserRoles.DefaultRolePath;

            var userEntity = new MS_Users();

            userEntity.Username = userInfo.Email;
            userEntity.Name = userInfo.FullName;
            userEntity.Email = userInfo.Email;
            userEntity.ProfilePic = userInfo.Userpic;
            userEntity.UserRole = roleName;
            userEntity.SecurityQuestion = Guid.NewGuid().ToString();
            userEntity.SecurityAnswer = Guid.NewGuid().ToString();
            userEntity.UserCategoryID = UserCategoryHelper.GetDefaultCategoryID();
            userEntity.DateOfBirth = userInfo.Birthday;

            if (CreateUserLogin(userEntity.Username, PasswordHelper.Generate(10), userEntity.Email, userEntity.SecurityQuestion, userEntity.SecurityAnswer, true))
            {
                if (AddUserToRole(userEntity.Username, roleName))
                {
                    if (UserBL.Add(userEntity, context))
                    {
                        ActionOnLogin.Login(userEntity.Email, true, true, false, string.Empty);
                    }
                    else
                    {
                        DeleteUserFromRole(userEntity.Username, roleName);
                        DeleteUserLogin(userEntity.Username);
                    }
                }
                else
                {
                    DeleteUserLogin(userEntity.Username);
                }
            }
        }

        private bool DeleteUserLogin(string username)
        {
            var success = false;

            try
            {
                Membership.DeleteUser(username, true);
                success = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                success = false;
            }

            return success;
        }

        private bool DeleteUserFromRole(string username, string role)
        {
            var success = false;

            try
            {
                Roles.RemoveUserFromRole(username, role);
                success = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                success = false;
            }

            return success;
        }

        private bool AddUserToRole(string username, string role)
        {
            var success = false;

            try
            {
                Roles.AddUserToRole(username, role);
                success = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                success = false;
            }

            return success;
        }

        private bool CreateUserLogin(string username, string password, string email, string question, string answer, bool isApproved)
        {
            var success = false;

            MembershipCreateStatus creationStatus;

            Membership.CreateUser(username, password, email, question, answer, isApproved, out creationStatus);

            switch (creationStatus)
            {
                case MembershipCreateStatus.Success:
                    success = true;
                    break;

                case MembershipCreateStatus.InvalidPassword:
                    success = false;
                    break;

                case MembershipCreateStatus.InvalidEmail:
                    success = false;
                    break;

                case MembershipCreateStatus.UserRejected:
                    success = false;
                    break;

                case MembershipCreateStatus.DuplicateEmail:
                    success = false;
                    break;

                default:
                    success = false;
                    break;
            }

            return success;
        }
    }
}