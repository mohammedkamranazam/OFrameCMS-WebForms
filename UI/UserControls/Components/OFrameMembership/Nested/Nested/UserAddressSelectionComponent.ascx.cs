using OWDARO.OEventArgs;
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
    public partial class UserAddressSelectionComponent : System.Web.UI.UserControl
    {
        public event UserAddressSelectionEventHandler UserAddressSelected;

        public string AddressCategory
        {
            get
            {
                return (ViewState["AddressCategory"] == null) ? string.Empty : ViewState["AddressCategory"].ToString();
            }

            set
            {
                ViewState["AddressCategory"] = value;
            }
        }

        public string Title
        {
            set
            {
                TitleLiteral.Text = value;
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

        private async Task LoadData()
        {
            using (var context = new MembershipEntities())
            {
                var addressQuery = await (from set in context.MS_UserAdresses
                                          where set.Username == Username && set.Hide == false && set.AddressCategory == AddressCategory
                                          select set).ToListAsync();

                if (addressQuery.Any())
                {
                    Repeater1.DataSource = addressQuery;
                    Repeater1.DataBind();
                }
                else
                {
                    Component.Visible = false;
                }
            }
        }

        private void OnUserAddressSelected(string streetName, string city, string zipCode, string state, string country)
        {
            if (UserAddressSelected != null)
            {
                var args = new UserAddressSelectionEventArgs(streetName, city, zipCode, state, country);

                UserAddressSelected(this, args);
            }
        }

        protected async void AddressSelectButton_Command(object sender, CommandEventArgs e)
        {
            var addressID = DataParser.IntParse(e.CommandArgument.ToString());

            using (var context = new MembershipEntities())
            {
                var addressQuery = await (from addresses in context.MS_UserAdresses
                                          where addresses.AddressID == addressID
                                          select addresses).FirstOrDefaultAsync();

                if (addressQuery != null)
                {
                    OnUserAddressSelected(addressQuery.StreetName, addressQuery.City, addressQuery.ZipCode, addressQuery.State, addressQuery.Country);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }
        }
    }
}