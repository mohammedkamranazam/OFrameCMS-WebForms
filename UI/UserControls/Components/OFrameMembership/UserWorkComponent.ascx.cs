using System;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class UserWorkComponent : System.Web.UI.UserControl
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

        public string Country
        {
            get
            {
                return CountryDropDownList.SelectedValue;
            }

            set
            {
                CountryDropDownList.SelectedValue = value;
            }
        }

        public string Description
        {
            get
            {
                return DescriptionTextBox.Text;
            }

            set
            {
                DescriptionTextBox.Text = value;
            }
        }

        public string EndDate
        {
            get
            {
                return EndDateTextBox.Text;
            }

            set
            {
                EndDateTextBox.Text = value;
            }
        }

        public string Organization
        {
            get
            {
                return OrganizationTextBox.Text;
            }

            set
            {
                OrganizationTextBox.Text = value;
            }
        }

        public string Position
        {
            get
            {
                return PositionTextBox.Text;
            }

            set
            {
                PositionTextBox.Text = value;
            }
        }

        public string StartDate
        {
            get
            {
                return StartDateTextBox.Text;
            }

            set
            {
                StartDateTextBox.Text = value;
            }
        }

        public string ValidationGroup
        {
            set
            {
                OrganizationTextBox.ValidationGroup = value;
                PositionTextBox.ValidationGroup = value;
                CityTextBox.ValidationGroup = value;
                CountryDropDownList.ValidationGroup = value;
                DescriptionTextBox.ValidationGroup = value;
                StartDateTextBox.ValidationGroup = value;
                EndDateTextBox.ValidationGroup = value;
            }
        }

        public bool WorkHere
        {
            get
            {
                return WorkHereCheckBox.Checked;
            }

            set
            {
                WorkHereCheckBox.Checked = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OrganizationTextBox.LabelText = "Company";
                OrganizationTextBox.SmallLabelText = "company or organization where you work";
                OrganizationTextBox.RequiredErrorMessage = "company name is required";

                PositionTextBox.LabelText = "Position";
                PositionTextBox.SmallLabelText = "work position or role";
                PositionTextBox.RequiredErrorMessage = "work position is required";

                CityTextBox.LabelText = "City";
                CityTextBox.SmallLabelText = "name of the city where your office is";

                CountryDropDownList.LabelText = "Country";
                CountryDropDownList.SmallLabelText = "country name where your office is";

                DescriptionTextBox.LabelText = "Description";

                StartDateTextBox.LabelText = "Joining Date";
                StartDateTextBox.SmallLabelText = "company joining date";

                StartDateTextBox.SmallLabelText = "date format: " + Validator.DateParseExpression;
                StartDateTextBox.Format = Validator.DateParseExpression;
                StartDateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                StartDateTextBox.ValidationExpression = Validator.CalendarValidationExpression;

                EndDateTextBox.LabelText = "Left On";
                EndDateTextBox.SmallLabelText = "company leaving date";

                EndDateTextBox.SmallLabelText = "date format: " + Validator.DateParseExpression;
                EndDateTextBox.Format = Validator.DateParseExpression;
                EndDateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                EndDateTextBox.ValidationExpression = Validator.CalendarValidationExpression;

                WorkHereCheckBox.LabelText = "Currently Work Here";
                WorkHereCheckBox.SmallLabelText = "check this if you currently work here";
            }
        }
    }
}