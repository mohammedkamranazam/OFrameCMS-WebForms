using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class UserDataDetailsComponent : System.Web.UI.UserControl
    {
        private string validationGroup;

        public string BoxIcon
        {
            set
            {
                var classes = "ico color " + value;
                BoxTitleIcon.Attributes.Add("class", classes);
            }
        }

        public string BoxTitle
        {
            set
            {
                BoxTitleLiteral.Text = value;
            }
        }

        public bool ComponentVisible
        {
            get
            {
                return (ViewState["ComponentVisible"] == null) ? false : DataParser.BoolParse(ViewState["ComponentVisible"].ToString());
            }

            set
            {
                ViewState["ComponentVisible"] = value;
            }
        }

        public string DataCategory
        {
            get
            {
                return (ViewState["DataCategory"] == null) ? string.Empty : ViewState["DataCategory"].ToString();
            }

            set
            {
                ViewState["DataCategory"] = value;
            }
        }

        public string DataTextBoxLabelText
        {
            set
            {
                UserDataComponent.DataTextBoxLabelText = value;
            }
        }

        public int DataTextBoxMaxLength
        {
            set
            {
                UserDataComponent.DataTextBoxMaxLength = value;
            }
        }

        public string DataTextBoxRequiredErrorMessage
        {
            set
            {
                UserDataComponent.DataTextBoxRequiredErrorMessage = value;
            }
        }

        public string DataTextBoxSmallLabelText
        {
            set
            {
                UserDataComponent.DataTextBoxSmallLabelText = value;
            }
        }

        public string DataTextBoxValidationErrorMessage
        {
            set
            {
                UserDataComponent.DataTextBoxValidationErrorMessage = value;
            }
        }

        public string DataTextBoxValidationExpression
        {
            set
            {
                UserDataComponent.DataTextBoxValidationExpression = value;
            }
        }

        public string GridViewHyperLinkFormatString
        {
            get
            {
                return (ViewState["GridViewHyperLinkFormatString"] == null) ? string.Empty : ViewState["GridViewHyperLinkFormatString"].ToString();
            }

            set
            {
                ViewState["GridViewHyperLinkFormatString"] = value;
            }
        }

        public string GridViewLiteralText
        {
            get
            {
                return (ViewState["GridViewLiteralText"] == null) ? string.Empty : ViewState["GridViewLiteralText"].ToString();
            }

            set
            {
                ViewState["GridViewLiteralText"] = value;
            }
        }

        public string HeaderIcon
        {
            set
            {
                var classes = "ico color " + value;
                HeaderTitleIcon.Attributes.Add("class", classes);
            }
        }

        public string HeaderTitle
        {
            set
            {
                HeaderTitleLiteral.Text = value;
            }
        }

        public string ToolBarCustomButtonText
        {
            set
            {
                FormToolbar1.CustomButtonText = value;
            }
        }

        public string Username
        {
            get
            {
                return (ViewState["Username"] == null) ? string.Empty : ViewState["Username"].ToString();
            }

            set
            {
                ViewState["Username"] = value;
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
                validationGroup = value;
                UserDataComponent.ValidationGroup = value;
                FormToolbar1.ValidationGroup = value;
            }
        }

        private async void FormToolbar1_CustomClick(object sender, EventArgs e)
        {
            Page.Validate(ValidationGroup);

            if (Page.IsValid)
            {
                using (var context = new MembershipEntities())
                {
                    var userData = new MS_UsersData();

                    userData.UsersDataCategory = DataCategory;
                    userData.Username = Username;
                    userData.UserData = UserDataComponent.DataTextBoxText;

                    try
                    {
                        await UsersDataBL.AddUsersDataAsync(userData, context);

                        await SetGridView(context);

                        StatusMessage.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;
                        StatusMessage.MessageType = StatusMessageType.Success;
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                        StatusMessage.MessageType = StatusMessageType.Error;
                    }
                }
            }
        }

        private async Task SetGridView(MembershipEntities context)
        {
            GridView.DataSource = await UsersDataBL.GetUsersDataCollectionAsync(Username, DataCategory, context);
            GridView.DataBind();
        }

        protected async void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var UserDataIDHiddenField = GridView.Rows[e.RowIndex].FindControl("UserDataIDHiddenField") as HiddenField;

            var UserDataID = DataParser.IntParse(UserDataIDHiddenField.Value);

            using (var context = new MembershipEntities())
            {
                try
                {
                    await UsersDataBL.DeleteUserDataAsync(UserDataID, context);

                    await SetGridView(context);

                    StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
                    StatusMessage.MessageType = StatusMessageType.Success;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                    StatusMessage.MessageType = StatusMessageType.Error;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ComponentVisible)
            {
                return;
            }

            if (!IsPostBack)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await SetGridView();
                }));
            }

            FormToolbar1.CustomClick += new EventHandler(FormToolbar1_CustomClick);
        }

        protected async Task SetGridView()
        {
            using (var context = new MembershipEntities())
            {
                await SetGridView(context);
            }
        }
    }
}