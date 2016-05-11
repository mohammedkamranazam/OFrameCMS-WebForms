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
    public partial class UserWorkDetailsComponent : System.Web.UI.UserControl
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
                var userWork = new MS_UserWorks();

                userWork.Username = Username;
                userWork.City = UserWorkComponent.City;
                userWork.Country = UserWorkComponent.Country;
                userWork.Description = UserWorkComponent.Description;
                userWork.EndDate = DataParser.NullableDateTimeParse(UserWorkComponent.EndDate);
                userWork.Organization = UserWorkComponent.Organization;
                userWork.Position = UserWorkComponent.Position;
                userWork.StartDate = DataParser.NullableDateTimeParse(UserWorkComponent.StartDate);
                userWork.WorkHere = UserWorkComponent.WorkHere;

                if (UserWorkComponent.WorkHere)
                {
                    userWork.EndDate = null;
                }

                try
                {
                    using (var context = new MembershipEntities())
                    {
                        context.MS_UserWorks.Add(userWork);

                        await context.SaveChangesAsync();

                        await SetGridView(context);

                        UserWorkComponent.WorkHere = false;

                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;
                    }
                }
                catch (Exception ex)
                {
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }
            }
        }

        private async Task SetGridView(MembershipEntities context)
        {
            var query = await (from works in context.MS_UserWorks
                               where works.Username == Username
                               select works).ToListAsync();

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
                    await UserWorkBL.DeleteUserWorkAsync(dataID, context);

                    await SetGridView(context);

                    StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
                    StatusMessage.MessageType = StatusMessageType.Success;
                }
                catch (Exception ex)
                {
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