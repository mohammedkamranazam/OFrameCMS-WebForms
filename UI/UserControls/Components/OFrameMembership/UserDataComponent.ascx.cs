using System;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class UserDataComponent : System.Web.UI.UserControl
    {
        private string validationGroup;

        public string DataTextBoxLabelText
        {
            set
            {
                DataTextBox.LabelText = value;
            }
        }

        public int DataTextBoxMaxLength
        {
            set
            {
                DataTextBox.MaxLength = value;
            }
        }

        public string DataTextBoxRequiredErrorMessage
        {
            set
            {
                DataTextBox.RequiredErrorMessage = value;
            }
        }

        public string DataTextBoxSmallLabelText
        {
            set
            {
                DataTextBox.SmallLabelText = value;
            }
        }

        public string DataTextBoxText
        {
            get
            {
                return DataTextBox.Text;
            }

            set
            {
                DataTextBox.Text = value;
            }
        }

        public string DataTextBoxValidationErrorMessage
        {
            set
            {
                DataTextBox.ValidationErrorMessage = value;
            }
        }

        public string DataTextBoxValidationExpression
        {
            set
            {
                DataTextBox.ValidationExpression = value;
            }
        }

        public string ValidationGroup
        {
            get
            {
                return validationGroup;
            }

            set
            {
                DataTextBox.ValidationGroup = value;
                validationGroup = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}