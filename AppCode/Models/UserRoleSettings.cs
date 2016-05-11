using System;

namespace OWDARO.Models
{
    [Serializable]
    public class UserRoleSettings
    {
        public bool HideSuperAdmin
        {
            get;
            set;
        }

        public bool Locked
        {
            get;
            set;
        }

        public bool Login
        {
            get;
            set;
        }

        public string MasterPage
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public bool RegistrationBlocked
        {
            get;
            set;
        }

        public bool ShowCategory
        {
            get;
            set;
        }

        public bool ShowRoles
        {
            get;
            set;
        }

        public string Theme
        {
            get;
            set;
        }
    }
}