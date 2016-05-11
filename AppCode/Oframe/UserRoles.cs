using OWDARO.Settings;

namespace OWDARO
{
    public static class UserRoles
    {
        public static string AdminRole
        {
            get
            {
                return UserRoleHelper.GetAdminRole();
            }
        }

        public static string AnonymousRole
        {
            get
            {
                return UserRoleHelper.GetAnonymousRole();
            }
        }

        /// <summary>
        /// The Deafult Role to which a User will be registered from Registration Page
        /// </summary>
        public static string DefaultRole
        {
            get
            {
                return UserRoleHelper.GetDefaultRoleName();
            }
        }

        /// <summary>
        /// The default path which will be set for user, based on the default role, while registering from the register page
        /// </summary>
        public static string DefaultRolePath
        {
            get
            {
                return UserRoleHelper.GetDefaultRolePath();
            }
        }

        public static string InactivatedRole
        {
            get
            {
                return UserRoleHelper.GetInactivatedRole();
            }
        }

        /// <summary>
        /// Gets the Master Page based on the Role of the current logged in User
        /// </summary>
        public static string MasterPage
        {
            get
            {
                return UserRoleHelper.GetRoleMasterPage();
            }
        }

        public static string SuperAdminRole
        {
            get
            {
                return UserRoleHelper.GetSuperAdminRole();
            }
        }

        public static string UserRole
        {
            get
            {
                return UserRoleHelper.GetUserRole();
            }
        }
    }
}