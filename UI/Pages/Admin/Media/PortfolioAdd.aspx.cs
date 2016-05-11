using OWDARO;
using OWDARO.BLL.MediaBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Linq;

namespace ProjectJKL.UI.Pages.Admin.Media
{
    public partial class PortfolioAdd : System.Web.UI.Page
    {
        private void Add(MediaEntities context, ME_Portfolios entity)
        {
            try
            {
                PortfoliosBL.Add(entity, context);
                StatusMessage.MessageType = StatusMessageType.Success;
                StatusMessage.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                StatusMessage.MessageType = StatusMessageType.Error;
                StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
            }
        }

        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new MediaEntities())
                {
                    var entity = new ME_Portfolios();

                    entity.Title = TitleTextBox.Text;
                    entity.Description = DescriptionEditor.Text;
                    entity.Date = DataParser.NullableDateTimeParse(DateTextBox.Text);
                    entity.ClientID = ClientIDDropDownList.GetSelectedValue();
                    entity.URL = URLTextBox.Text;
                    entity.ImageID = ImageSelectorComponent1.ImageID;

                    Add(context, entity);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var context = new MediaEntities())
                {
                    var clientsQuery = (from set in context.ME_Clients
                                        select set);

                    ClientIDDropDownList.DataSource = clientsQuery.ToList();
                    ClientIDDropDownList.DataTextField = "Title";
                    ClientIDDropDownList.DataValueField = "ClientID";
                    ClientIDDropDownList.DataBind();
                    ClientIDDropDownList.AddSelect();
                }

                ImageSelectorComponent1.StoragePath = LocalStorages.Others;
            }

            DateTextBox.DateFormat = Validator.DateParseExpression;
            DateTextBox.Format = Validator.DateParseExpression;
            DateTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
            DateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
            DateTextBox.ValidationExpression = Validator.CalendarValidationExpression;

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}