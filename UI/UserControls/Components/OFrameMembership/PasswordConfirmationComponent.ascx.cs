using System;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class PasswordConfirmationComponent : System.Web.UI.UserControl
    {
        public string Password
        {
            get
            {
                return NewPasswordTextBox.Text;
            }

            set
            {
                NewPasswordTextBox.Text = value;
            }
        }

        public string ValidationGroup
        {
            set
            {
                NewPasswordTextBox.ValidationGroup = value;
                ConfirmPasswordTextBox.ValidationGroup = value;
                RequiredFieldValidator2.ValidationGroup = value;
                RequiredFieldValidator3.ValidationGroup = value;
                PasswordStrengthRegularExpressionValidator.ValidationGroup = value;
                CompareValidator1.ValidationGroup = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PasswordStrengthRegularExpressionValidator.ValidationExpression = Validator.PasswordValidationExpression;
                PasswordStrengthRegularExpressionValidator.ErrorMessage = Validator.PasswordValidationErrorMessage;
            }
        }
    }
}