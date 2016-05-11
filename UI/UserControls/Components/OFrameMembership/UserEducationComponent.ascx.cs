using OWDARO.BLL.MembershipBLL;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class UserEducationComponent : System.Web.UI.UserControl
    {
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

        public string Institute
        {
            get
            {
                return InstituteTextBox.Text;
            }

            set
            {
                InstituteTextBox.Text = value;
            }
        }

        public string QualificationType
        {
            get
            {
                return QualificationTypeDropDownList.SelectedValue;
            }

            set
            {
                QualificationTypeDropDownList.SelectedValue = value;
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

        public string Stream
        {
            get
            {
                return StreamTextBox.Text;
            }

            set
            {
                StreamTextBox.Text = value;
            }
        }

        public string ValidationGroup
        {
            set
            {
                InstituteTextBox.ValidationGroup = value;
                StreamTextBox.ValidationGroup = value;
                StartDateTextBox.ValidationGroup = value;
                EndDateTextBox.ValidationGroup = value;
                QualificationTypeDropDownList.ValidationGroup = value;
            }
        }

        private async Task LoadData()
        {
            await EducationQualificationTypeBL.PopulateEducationQualificationTypesListAsync(QualificationTypeDropDownList);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));

                InstituteTextBox.LabelText = "Institute";
                InstituteTextBox.SmallLabelText = string.Empty;
                InstituteTextBox.RequiredErrorMessage = "institute name is required";

                StreamTextBox.LabelText = "Stream";
                StreamTextBox.SmallLabelText = string.Empty;
                StreamTextBox.RequiredErrorMessage = "stream is required";

                QualificationTypeDropDownList.LabelText = "Qualification Type";
                QualificationTypeDropDownList.SmallLabelText = "add your different addresses based upon type";
                QualificationTypeDropDownList.RequiredFieldErrorMessage = "please select the qualification type";

                StartDateTextBox.LabelText = "Start Date";
                StartDateTextBox.SmallLabelText = string.Empty;
                StartDateTextBox.SmallLabelText = "date format: " + Validator.DateParseExpression;
                StartDateTextBox.DateFormat = Validator.DateParseExpression;
                StartDateTextBox.Format = Validator.DateParseExpression;
                StartDateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                StartDateTextBox.ValidationExpression = Validator.CalendarValidationExpression;

                EndDateTextBox.LabelText = "End Date";
                EndDateTextBox.SmallLabelText = string.Empty;
                EndDateTextBox.SmallLabelText = "date format: " + Validator.DateParseExpression;
                EndDateTextBox.DateFormat = Validator.DateParseExpression;
                EndDateTextBox.Format = Validator.DateParseExpression;
                EndDateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                EndDateTextBox.ValidationExpression = Validator.CalendarValidationExpression;
            }
        }
    }
}