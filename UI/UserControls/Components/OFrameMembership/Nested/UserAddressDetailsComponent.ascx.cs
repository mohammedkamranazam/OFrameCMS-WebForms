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
    public partial class UserAddressDetailsComponent : System.Web.UI.UserControl
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
                var userAddress = new MS_UserAdresses();
                userAddress.AddressCategory = CategoryDropDownList.SelectedValue;
                userAddress.City = AddressComponent.City;
                userAddress.Country = AddressComponent.Country;
                userAddress.State = AddressComponent.State;
                userAddress.StreetName = AddressComponent.Street;
                userAddress.Username = Username;
                userAddress.ZipCode = AddressComponent.ZipCode;

                using (var context = new MembershipEntities())
                {
                    context.MS_UserAdresses.Add(userAddress);

                    try
                    {
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
            var query = await (from adresses in context.MS_UserAdresses
                               where adresses.Username == Username
                               select adresses).ToListAsync();

            GridView.DataSource = query.OrderBy(c => c.AddressCategory);
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
                    await UserAddressBL.DeleteUserAddressAsync(dataID, context);

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

                CategoryDropDownList.Items.Add(new ListItem(UserDataCategories.HomeAddressCategory.Value));
                CategoryDropDownList.Items.Add(new ListItem(UserDataCategories.DeliveryAddressCategory.Value));
                CategoryDropDownList.Items.Add(new ListItem(UserDataCategories.BillingAddressCategory.Value));
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