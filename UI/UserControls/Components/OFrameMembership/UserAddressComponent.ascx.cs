using System;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class UserAddressComponent : System.Web.UI.UserControl
    {
        public string City
        {
            get
            {
                return CityTextBox.Text;
            }

            set
            {
                CityTextBox.Text = value;
            }
        }

        public string CityLabelText
        {
            set
            {
                CityTextBox.LabelText = value;
            }
        }

        public int CityMaxLength
        {
            set
            {
                CityTextBox.MaxLength = value;
            }
        }

        public string CityRequiredErrorMessage
        {
            set
            {
                CityTextBox.RequiredErrorMessage = value;
            }
        }

        public string CitySmallLabelText
        {
            set
            {
                CityTextBox.SmallLabelText = value;
            }
        }

        public string Country
        {
            get
            {
                return CountryDropDownList.DropDownList.SelectedValue;
            }

            set
            {
                CountryDropDownList.DropDownList.SelectedValue = value;
            }
        }

        public string CountryLabelText
        {
            set
            {
                CountryDropDownList.LabelText = value;
            }
        }

        public string CountrySmallLabelText
        {
            set
            {
                CountryDropDownList.SmallLabelText = value;
            }
        }

        public string State
        {
            get
            {
                return StateTextBox.Text;
            }

            set
            {
                StateTextBox.Text = value;
            }
        }

        public string StateLabelText
        {
            set
            {
                StateTextBox.LabelText = value;
            }
        }

        public int StateMaxLength
        {
            set
            {
                StateTextBox.MaxLength = value;
            }
        }

        public string StateRequiredErrorMessage
        {
            set
            {
                StateTextBox.RequiredErrorMessage = value;
            }
        }

        public string StateSmallLabelText
        {
            set
            {
                StateTextBox.SmallLabelText = value;
            }
        }

        public string Street
        {
            get
            {
                return StreetTextBox.Text;
            }

            set
            {
                StreetTextBox.Text = value;
            }
        }

        public string StreetLabelText
        {
            set
            {
                StreetTextBox.LabelText = value;
            }
        }

        public int StreetMaxLength
        {
            set
            {
                StreetTextBox.MaxLength = value;
            }
        }

        public string StreetRequiredErrorMessage
        {
            set
            {
                StreetTextBox.RequiredErrorMessage = value;
            }
        }

        public string StreetSmallLabelText
        {
            set
            {
                StreetTextBox.SmallLabelText = value;
            }
        }

        public string ValidationGroup
        {
            set
            {
                StreetTextBox.ValidationGroup = value;
                CityTextBox.ValidationGroup = value;
                ZipCodeTextBox.ValidationGroup = value;
                StateTextBox.ValidationGroup = value;
                CountryDropDownList.ValidationGroup = value;
            }
        }

        public string ZipCode
        {
            get
            {
                return ZipCodeTextBox.Text;
            }

            set
            {
                ZipCodeTextBox.Text = value;
            }
        }

        public string ZipCodeLabelText
        {
            set
            {
                ZipCodeTextBox.LabelText = value;
            }
        }

        public int ZipCodeMaxLength
        {
            set
            {
                ZipCodeTextBox.MaxLength = value;
            }
        }

        public string ZipCodeRequiredErrorMessage
        {
            set
            {
                ZipCodeTextBox.RequiredErrorMessage = value;
            }
        }

        public string ZipCodeSmallLabelText
        {
            set
            {
                ZipCodeTextBox.SmallLabelText = value;
            }
        }

        public string ZipCodeValidationErrorMessage
        {
            set
            {
                ZipCodeTextBox.ValidationErrorMessage = value;
            }
        }

        public string ZipCodeValidationExpression
        {
            set
            {
                ZipCodeTextBox.ValidationExpression = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ZipCodeTextBox.ValidationErrorMessage = Validator.ZipCodeValidationErrorMessage;
                ZipCodeTextBox.ValidationExpression = Validator.ZipCodeValidationExpression;
            }
        }
    }
}