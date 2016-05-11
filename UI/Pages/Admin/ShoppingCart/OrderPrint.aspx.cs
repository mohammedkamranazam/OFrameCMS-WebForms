using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.Linq;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class OrderPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["OrderNumber"]))
                {
                    var orderNumber = Request.QueryString["OrderNumber"];

                    using (var context = new ShoppingCartEntities())
                    {
                        var orderQuery = (from order in context.SC_Orders
                                          where order.OrderNumber == orderNumber
                                          select order).FirstOrDefault();

                        if (orderQuery != null)
                        {
                            OrderNumberLabel.Text = orderQuery.OrderNumber;
                            DateTimeLabel.Text = DataParser.GetDateTimeFormattedString(orderQuery.DateTime);
                            ShippingAddressLabel.Text = orderQuery.ShippingAddress;
                            BillingAddressLabel.Text = orderQuery.BillingAddress;
                            EmailIDLabel.Text = orderQuery.EmailID;
                            MobileLabel.Text = orderQuery.Mobile;
                            GridView1.DataSource = orderQuery.SC_OrderDetails;
                            GridView1.DataBind();

                            OrderTotalLabel.Text = orderQuery.OrderTotal.ToString("0.00");
                            ShippingCostLabel.Text = (orderQuery.ShipmentCost == 0) ? "Free" : orderQuery.ShipmentCost.ToString("0.00");
                            TotalAmountLabel.Text = (orderQuery.OrderTotal + orderQuery.ShipmentCost).ToString("0.00");
                        }
                    }
                }
            }
        }
    }
}