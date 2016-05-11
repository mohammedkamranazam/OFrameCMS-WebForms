using OWDARO.Models;
using System;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class CategorySettingsComponent : System.Web.UI.UserControl
    {
        public string BoxTitle
        {
            set
            {
                BoxTitleLiteral.Text = value;
            }
        }

        public int CategoryId
        {
            get;
            set;
        }

        public string CategoryName
        {
            get;
            set;
        }

        public UserCategorySettings CategorySettings
        {
            get
            {
                return GetCategorySettings();
            }

            set
            {
                SetCategorySettings(value);
            }
        }

        protected UserCategorySettings GetCategorySettings()
        {
            var catSet = new UserCategorySettings();

            catSet.CategoryID = CategoryId;
            catSet.Name = CategoryName;
            catSet.ShowAddress = AddressAUSCheckBox.Checked;
            catSet.ShowBillingAddress = BillingAddressAUSCheckBox.Checked;
            catSet.ShowEducation = EducationAUSCheckBox.Checked;
            catSet.ShowWork = WorkAUSCheckBox.Checked;
            catSet.ShowDateOfBirth = DOBAUSCheckBox.Checked;
            catSet.ShowDeliveryAddress = DeliveryAddressAUSCheckBox.Checked;
            catSet.ShowFax = FaxAUSCheckBox.Checked;
            catSet.ShowGender = GenderAUSCheckBox.Checked;
            catSet.ShowLandline = LandlineAUSCheckBox.Checked;
            catSet.ShowMobile = MobileAUSCheckBox.Checked;
            catSet.ShowWebsite = WebsiteAUSCheckBox.Checked;

            return catSet;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SetCategorySettings(UserCategorySettings catSet)
        {
            CategoryId = catSet.CategoryID;
            CategoryName = catSet.Name;
            AddressAUSCheckBox.Checked = catSet.ShowAddress;
            BillingAddressAUSCheckBox.Checked = catSet.ShowBillingAddress;
            DeliveryAddressAUSCheckBox.Checked = catSet.ShowDeliveryAddress;
            EducationAUSCheckBox.Checked = catSet.ShowEducation;
            WorkAUSCheckBox.Checked = catSet.ShowWork;
            DOBAUSCheckBox.Checked = catSet.ShowDateOfBirth;
            FaxAUSCheckBox.Checked = catSet.ShowFax;
            GenderAUSCheckBox.Checked = catSet.ShowGender;
            LandlineAUSCheckBox.Checked = catSet.ShowLandline;
            MobileAUSCheckBox.Checked = catSet.ShowMobile;
            WebsiteAUSCheckBox.Checked = catSet.ShowWebsite;
        }
    }
}