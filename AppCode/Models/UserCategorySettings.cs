using System;

namespace OWDARO.Models
{
    [Serializable]
    public class UserCategorySettings
    {
        public int CategoryID
        {
            get;
            set;
        }

        public bool Locked
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public bool ShowAddress
        {
            get;
            set;
        }

        public bool ShowBillingAddress
        {
            get;
            set;
        }

        public bool ShowDateOfBirth
        {
            get;
            set;
        }

        public bool ShowDeliveryAddress
        {
            get;
            set;
        }

        public bool ShowEducation
        {
            get;
            set;
        }

        public bool ShowFax
        {
            get;
            set;
        }

        public bool ShowGender
        {
            get;
            set;
        }

        public bool ShowLandline
        {
            get;
            set;
        }

        public bool ShowMobile
        {
            get;
            set;
        }

        public bool ShowWebsite
        {
            get;
            set;
        }

        public bool ShowWork
        {
            get;
            set;
        }
    }
}