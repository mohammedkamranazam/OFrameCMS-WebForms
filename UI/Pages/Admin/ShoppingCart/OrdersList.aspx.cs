using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class OrdersList : System.Web.UI.Page
    {
        private IQueryable<SC_Orders> GetOrdersQuery(ShoppingCartEntities context)
        {
            switch (FilterTypeDropDownList.SelectedValue)
            {
                case "Cancelled":
                    return (from set in context.SC_Orders
                            where set.IsCancelled
                            select set).OrderBy(c => c.DateTime);

                case "Completed":
                    return (from set in context.SC_Orders
                            where set.IsCompleted
                            select set).OrderBy(c => c.DateTime);

                case "Paid":
                    return (from set in context.SC_Orders
                            where set.IsPaid
                            select set).OrderBy(c => c.DateTime);

                case "Failed":
                    return (from set in context.SC_Orders
                            where set.IsFailed
                            select set).OrderBy(c => c.DateTime);

                case "Refund":
                    return (from set in context.SC_Orders
                            where set.IsRefund
                            select set).OrderBy(c => c.DateTime);

                case "Dispatched":
                    return (from set in context.SC_Orders
                            where set.IsDispatched
                            select set).OrderBy(c => c.DateTime);

                case "Returned":
                    return (from set in context.SC_Orders
                            where set.IsReturned
                            select set).OrderBy(c => c.DateTime);

                case "Pending":
                    return (from set in context.SC_Orders
                            where set.IsCompleted == false && set.IsCancelled == false
                            select set).OrderBy(c => c.DateTime);

                default:
                    return (from set in context.SC_Orders
                            select set).OrderBy(c => c.DateTime);
            }
        }

        private IQueryable<SC_Orders> GetSearchQuery(ShoppingCartEntities context)
        {
            var searchTerm = SearchTermTextBox.Text;

            return (from set in GetOrdersQuery(context)
                    where set.OrderNumber.Contains(searchTerm) ||
                        set.BillingAddress.Contains(searchTerm) ||
                        set.EmailID.Contains(searchTerm) ||
                        set.Mobile.Contains(searchTerm) ||
                        set.ShippingAddress.Contains(searchTerm)
                    select set).OrderBy(c => c.DateTime);
        }

        protected void FilterTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (string.IsNullOrWhiteSpace(SearchTermTextBox.Text))
                {
                    GridView1.DataSource = GetOrdersQuery(context).ToList();
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = GetSearchQuery(context).ToList();
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (string.IsNullOrWhiteSpace(SearchTermTextBox.Text))
                {
                    GridView1.DataSource = GetOrdersQuery(context).ToList();
                    GridView1.PageIndex = e.NewPageIndex;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = GetSearchQuery(context).ToList();
                    GridView1.PageIndex = e.NewPageIndex;
                    GridView1.DataBind();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var context = new ShoppingCartEntities())
                {
                    GridView1.DataSource = GetOrdersQuery(context).ToList();
                    GridView1.DataBind();
                }
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                GridView1.DataSource = GetSearchQuery(context).ToList();
                GridView1.DataBind();
            }
        }

        public string GetOrderItemStyle(string orderNumber)
        {
            using (var context = new ShoppingCartEntities())
            {
                var orderQuery = OrdersBL.GetObjectByID(orderNumber, context);

                if (orderQuery.IsFailed)
                {
                    return "Failed";
                }

                if (orderQuery.IsCancelled)
                {
                    return "Cancelled";
                }

                if (orderQuery.IsCompleted)
                {
                    return "Completed";
                }
            }

            return string.Empty;
        }
    }
}