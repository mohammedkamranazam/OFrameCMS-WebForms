using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class UserEducationDetailsComponent : System.Web.UI.UserControl
    {
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

        private async void FormToolbar1_CustomClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new MembershipEntities())
                {
                    var userEducation = new MS_UserEducations();
                    userEducation.EducationQualificationTypeID = DataParser.IntParse(EducationComponent.QualificationType);
                    userEducation.EndDate = DataParser.NullableDateTimeParse(EducationComponent.EndDate);
                    userEducation.InstituteName = EducationComponent.Institute;
                    userEducation.StartDate = DataParser.NullableDateTimeParse(EducationComponent.StartDate);
                    userEducation.Stream = EducationComponent.Stream;
                    userEducation.Username = Username;

                    try
                    {
                        context.MS_UserEducations.Add(userEducation);

                        await context.SaveChangesAsync();

                        await SetGridView(context);

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
            }
        }

        private async Task SetGridView(MembershipEntities context)
        {
            var query = await (from educations in context.MS_UserEducations
                               where educations.Username == Username
                               select educations).ToListAsync();

            GridView.DataSource = query.OrderByDescending(c => c.EndDate);
            GridView.DataBind();
        }

        protected async void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var IDHiddenField = GridView.Rows[e.RowIndex].FindControl("IDHiddenField") as HiddenField;

            var dataID = DataParser.IntParse(IDHiddenField.Value);

            using (var context = new MembershipEntities())
            {
                try
                {
                    await UserEducationBL.DeleteUserEducationAsync(dataID, context);

                    await SetGridView(context);

                    StatusMessage.MessageType = StatusMessageType.Success;
                    StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
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