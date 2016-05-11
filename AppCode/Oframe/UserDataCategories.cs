using OWDARO.Models;
using OWDARO.Settings;

namespace OWDARO
{
    public static class UserDataCategories
    {
        public static UserDataCategory BillingAddressCategory
        {
            set
            {
                UserDataCategoriesHelper.SetDataCategory(value);
            }

            get
            {
                return UserDataCategoriesHelper.GetDataCategory("BillingAddressCategory");
            }
        }

        public static UserDataCategory DeliveryAddressCategory
        {
            set
            {
                UserDataCategoriesHelper.SetDataCategory(value);
            }

            get
            {
                return UserDataCategoriesHelper.GetDataCategory("DeliveryAddressCategory");
            }
        }

        public static UserDataCategory EmailCategory
        {
            set
            {
                UserDataCategoriesHelper.SetDataCategory(value);
            }

            get
            {
                return UserDataCategoriesHelper.GetDataCategory("EmailCategory");
            }
        }

        public static UserDataCategory FaxCategory
        {
            set
            {
                UserDataCategoriesHelper.SetDataCategory(value);
            }

            get
            {
                return UserDataCategoriesHelper.GetDataCategory("FaxCategory");
            }
        }

        public static UserDataCategory HomeAddressCategory
        {
            set
            {
                UserDataCategoriesHelper.SetDataCategory(value);
            }

            get
            {
                return UserDataCategoriesHelper.GetDataCategory("HomeAddressCategory");
            }
        }

        public static UserDataCategory LandlineCategory
        {
            set
            {
                UserDataCategoriesHelper.SetDataCategory(value);
            }

            get
            {
                return UserDataCategoriesHelper.GetDataCategory("LandlineCategory");
            }
        }

        public static UserDataCategory MobileCategory
        {
            set
            {
                UserDataCategoriesHelper.SetDataCategory(value);
            }

            get
            {
                return UserDataCategoriesHelper.GetDataCategory("MobileCategory");
            }
        }

        public static UserDataCategory OfficeAddressCategory
        {
            set
            {
                UserDataCategoriesHelper.SetDataCategory(value);
            }

            get
            {
                return UserDataCategoriesHelper.GetDataCategory("OfficeAddressCategory");
            }
        }

        public static UserDataCategory WebsiteCategory
        {
            set
            {
                UserDataCategoriesHelper.SetDataCategory(value);
            }

            get
            {
                return UserDataCategoriesHelper.GetDataCategory("WebsiteCategory");
            }
        }
    }
}