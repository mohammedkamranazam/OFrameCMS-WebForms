using OWDARO;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Linq;

namespace ProjectJKL.UI.Pages.Open.ShoppingCart.CheckOut
{
    public partial class OrderConfirmed : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetCheckOutThemeFile();
        }

        private void FormToolbar1_CustomClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("OrderConfirmedPage"));

                if (!string.IsNullOrWhiteSpace(Request.QueryString["OrderNumber"]) && !string.IsNullOrWhiteSpace(Request.QueryString["Success"]))
                {
                    var success = DataParser.BoolParse(Request.QueryString["Success"]);
                    var orderNumber = Request.QueryString["OrderNumber"];

                    if (success)
                    {
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

                                try
                                {
                                    CartProcessingBL.SendEmails(orderQuery.EmailID, orderQuery.ReceiptHTML, orderNumber);

                                    StatusMessageLabel.MessageType = StatusMessageType.Success;
                                    StatusMessageLabel.Message = "We have successfully confirmed your order.";
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogger.LogError(ex);

                                    StatusMessageLabel.MessageType = StatusMessageType.Warning;
                                    StatusMessageLabel.Message = "<span style='color:black;'>We have successfully confirmed your order.</span><br />" +
                                        "However there was an issue while sending you an email containing your receipt. " +
                                        "Please contact our support team and mention your problem along with the order number.<br /><br />" + ExceptionHelper.GetExceptionMessage(ex);
                                }
                            }
                        }

                        FailurePanel.Visible = false;
                    }
                    else
                    {
                        SuccessPanel.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }

            FormToolbar1.CustomClick += FormToolbar1_CustomClick;
        }
    }
}