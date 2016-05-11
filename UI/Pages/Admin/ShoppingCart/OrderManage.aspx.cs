using OWDARO;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class OrderManage : System.Web.UI.Page
    {
        private void SetCancelledStatus(bool status)
        {
            if (status)
            {
                if (!IsCancelledButton.CssClass.NullableContains(" btn-inverse"))
                {
                    IsCancelledButton.CssClass += " btn-inverse";
                    IsCancelledButton.Text = "Set Order Not Cancelled";
                }
            }
            else
            {
                if (IsCancelledButton.CssClass.NullableContains(" btn-inverse"))
                {
                    var inverseStyleLength = (" btn-inverse").Length;
                    var currentStyleLength = IsCancelledButton.CssClass.Length;
                    var currentStyle = IsCancelledButton.CssClass;
                    var newStyle = currentStyle.Remove(currentStyleLength - inverseStyleLength);
                    IsCancelledButton.CssClass = newStyle;
                    IsCancelledButton.Text = "Set Order Cancelled";
                }
            }

            CancelledStatusLabel.Text = (status) ? "Yes" : "No";
        }

        private void SetCompletedStatus(bool status)
        {
            if (status)
            {
                if (!IsCompletedButton.CssClass.NullableContains(" btn-inverse"))
                {
                    IsCompletedButton.CssClass += " btn-inverse";
                    IsCompletedButton.Text = "Set Order Not Completed";
                }
            }
            else
            {
                if (IsCompletedButton.CssClass.NullableContains(" btn-inverse"))
                {
                    var inverseStyleLength = (" btn-inverse").Length;
                    var currentStyleLength = IsCompletedButton.CssClass.Length;
                    var currentStyle = IsCompletedButton.CssClass;
                    var newStyle = currentStyle.Remove(currentStyleLength - inverseStyleLength);
                    IsCompletedButton.CssClass = newStyle;
                    IsCompletedButton.Text = "Set Order Completed";
                }
            }

            CompletedStatusLabel.Text = (status) ? "Yes" : "No";
        }

        private void SetDispatchedStatus(bool status)
        {
            if (status)
            {
                if (!IsDispatchedButton.CssClass.NullableContains(" btn-inverse"))
                {
                    IsDispatchedButton.CssClass += " btn-inverse";
                    IsDispatchedButton.Text = "Set Order Not Dispatched";
                }
            }
            else
            {
                if (IsDispatchedButton.CssClass.NullableContains(" btn-inverse"))
                {
                    var inverseStyleLength = (" btn-inverse").Length;
                    var currentStyleLength = IsDispatchedButton.CssClass.Length;
                    var currentStyle = IsDispatchedButton.CssClass;
                    var newStyle = currentStyle.Remove(currentStyleLength - inverseStyleLength);
                    IsDispatchedButton.CssClass = newStyle;
                    IsDispatchedButton.Text = "Set Order Dispatched";
                }
            }

            DispatchedStatusLabel.Text = (status) ? "Yes" : "No";
        }

        private void SetFailedStatus(bool status)
        {
            FailedStatusLabel.Text = (status) ? "Yes" : "No";
        }

        private void SetPaidStatus(bool status)
        {
            if (status)
            {
                if (!IsPaidButton.CssClass.NullableContains(" btn-inverse"))
                {
                    IsPaidButton.CssClass += " btn-inverse";
                    IsPaidButton.Text = "Set Amount Not Paid";
                }
            }
            else
            {
                if (IsPaidButton.CssClass.NullableContains(" btn-inverse"))
                {
                    var inverseStyleLength = (" btn-inverse").Length;
                    var currentStyleLength = IsPaidButton.CssClass.Length;
                    var currentStyle = IsPaidButton.CssClass;
                    var newStyle = currentStyle.Remove(currentStyleLength - inverseStyleLength);
                    IsPaidButton.CssClass = newStyle;
                    IsPaidButton.Text = "Set Amount Paid";
                }
            }

            PaidStatusLabel.Text = (status) ? "Yes" : "No";
        }

        private void SetRefundStatus(bool status)
        {
            if (status)
            {
                if (!IsRefundButton.CssClass.NullableContains(" btn-inverse"))
                {
                    IsRefundButton.CssClass += " btn-inverse";
                    IsRefundButton.Text = "Set Payment Not Refunded";
                }
            }
            else
            {
                if (IsRefundButton.CssClass.NullableContains(" btn-inverse"))
                {
                    var inverseStyleLength = (" btn-inverse").Length;
                    var currentStyleLength = IsRefundButton.CssClass.Length;
                    var currentStyle = IsRefundButton.CssClass;
                    var newStyle = currentStyle.Remove(currentStyleLength - inverseStyleLength);
                    IsRefundButton.CssClass = newStyle;
                    IsRefundButton.Text = "Set Payment Refunded";
                }
            }

            RefundStatusLabel.Text = (status) ? "Yes" : "No";
        }

        private void SetReturnedStatus(bool status)
        {
            if (status)
            {
                if (!IsReturnedButton.CssClass.NullableContains(" btn-inverse"))
                {
                    IsReturnedButton.CssClass += " btn-inverse";
                    IsReturnedButton.Text = "Set Order Not Returned";
                }
            }
            else
            {
                if (IsReturnedButton.CssClass.NullableContains(" btn-inverse"))
                {
                    var inverseStyleLength = (" btn-inverse").Length;
                    var currentStyleLength = IsReturnedButton.CssClass.Length;
                    var currentStyle = IsReturnedButton.CssClass;
                    var newStyle = currentStyle.Remove(currentStyleLength - inverseStyleLength);
                    IsReturnedButton.CssClass = newStyle;
                    IsReturnedButton.Text = "Set Order Returned";
                }
            }

            ReturnedStatusLabel.Text = (status) ? "Yes" : "No";
        }

        protected void IsCancelledButton_Click(object sender, EventArgs e)
        {
            var orderNumber = Request.QueryString["OrderNumber"];

            using (var context = new ShoppingCartEntities())
            {
                var orderQuery = OrdersBL.GetObjectByID(orderNumber, context);

                orderQuery.IsCancelled = !orderQuery.IsCancelled;
                orderQuery.IsCompleted = !orderQuery.IsCancelled;

                if (orderQuery.IsCancelled)
                {
                    CartProcessingBL.RollBackOrderQuantity(orderNumber, context);
                }
                else
                {
                    if (CartProcessingBL.HasOrderedProductsGoneOutOfStock(orderQuery, context))
                    {
                        StatusStatusMessage.MessageType = StatusMessageType.Warning;
                        StatusStatusMessage.Message = "Cannot Process The Order As Some Of The Products Have Gone Out Of Stock";
                        return;
                    }
                    else
                    {
                        CartProcessingBL.ProcessOrderQuantity(orderQuery);
                    }
                }

                try
                {
                    context.SaveChanges();
                    StatusStatusMessage.MessageType = StatusMessageType.Success;
                    StatusStatusMessage.Message = "Status Successfully Updated";
                    SetCancelledStatus(orderQuery.IsCancelled);
                    SetCompletedStatus(orderQuery.IsCompleted);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusStatusMessage.MessageType = StatusMessageType.Error;
                    StatusStatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }
            }
        }

        protected void IsCompletedButton_Click(object sender, EventArgs e)
        {
            var orderNumber = Request.QueryString["OrderNumber"];

            using (var context = new ShoppingCartEntities())
            {
                var orderQuery = OrdersBL.GetObjectByID(orderNumber, context);

                orderQuery.IsCompleted = !orderQuery.IsCompleted;
                orderQuery.IsCancelled = !orderQuery.IsCompleted;

                try
                {
                    context.SaveChanges();
                    StatusStatusMessage.MessageType = StatusMessageType.Success;
                    StatusStatusMessage.Message = "Status Successfully Updated";
                    SetCompletedStatus(orderQuery.IsCompleted);
                    SetCancelledStatus(orderQuery.IsCancelled);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusStatusMessage.MessageType = StatusMessageType.Error;
                    StatusStatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }
            }
        }

        protected void IsDispatchedButton_Click(object sender, EventArgs e)
        {
            var orderNumber = Request.QueryString["OrderNumber"];

            using (var context = new ShoppingCartEntities())
            {
                var orderQuery = OrdersBL.GetObjectByID(orderNumber, context);

                orderQuery.IsDispatched = !orderQuery.IsDispatched;
                orderQuery.IsReturned = !orderQuery.IsDispatched;

                try
                {
                    context.SaveChanges();
                    StatusStatusMessage.MessageType = StatusMessageType.Success;
                    StatusStatusMessage.Message = "Status Successfully Updated";
                    SetDispatchedStatus(orderQuery.IsDispatched);
                    SetReturnedStatus(orderQuery.IsReturned);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusStatusMessage.MessageType = StatusMessageType.Error;
                    StatusStatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }
            }
        }

        protected void IsPaidButton_Click(object sender, EventArgs e)
        {
            var orderNumber = Request.QueryString["OrderNumber"];

            using (var context = new ShoppingCartEntities())
            {
                var orderQuery = OrdersBL.GetObjectByID(orderNumber, context);

                orderQuery.IsPaid = !orderQuery.IsPaid;
                orderQuery.IsRefund = !orderQuery.IsPaid;

                try
                {
                    context.SaveChanges();
                    StatusStatusMessage.MessageType = StatusMessageType.Success;
                    StatusStatusMessage.Message = "Status Successfully Updated";
                    SetPaidStatus(orderQuery.IsPaid);
                    SetRefundStatus(orderQuery.IsRefund);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusStatusMessage.MessageType = StatusMessageType.Error;
                    StatusStatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }
            }
        }

        protected void IsRefundButton_Click(object sender, EventArgs e)
        {
            var orderNumber = Request.QueryString["OrderNumber"];

            using (var context = new ShoppingCartEntities())
            {
                var orderQuery = OrdersBL.GetObjectByID(orderNumber, context);

                orderQuery.IsRefund = !orderQuery.IsRefund;
                orderQuery.IsPaid = !orderQuery.IsRefund;

                try
                {
                    context.SaveChanges();
                    StatusStatusMessage.MessageType = StatusMessageType.Success;
                    StatusStatusMessage.Message = "Status Successfully Updated";
                    SetRefundStatus(orderQuery.IsRefund);
                    SetPaidStatus(orderQuery.IsPaid);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusStatusMessage.MessageType = StatusMessageType.Error;
                    StatusStatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }
            }
        }

        protected void IsReturnedButton_Click(object sender, EventArgs e)
        {
            var orderNumber = Request.QueryString["OrderNumber"];

            using (var context = new ShoppingCartEntities())
            {
                var orderQuery = OrdersBL.GetObjectByID(orderNumber, context);

                orderQuery.IsReturned = !orderQuery.IsReturned;
                orderQuery.IsDispatched = !orderQuery.IsReturned;

                if (orderQuery.IsReturned)
                {
                    CartProcessingBL.RollBackOrderQuantity(orderNumber, context);
                }
                else
                {
                    if (CartProcessingBL.HasOrderedProductsGoneOutOfStock(orderQuery, context))
                    {
                        StatusStatusMessage.MessageType = StatusMessageType.Warning;
                        StatusStatusMessage.Message = "Cannot Dispatch The Order As Some Of The Products Have Gone Out Of Stock";
                        return;
                    }
                    else
                    {
                        CartProcessingBL.ProcessOrderQuantity(orderQuery);
                    }
                }

                try
                {
                    context.SaveChanges();
                    StatusStatusMessage.MessageType = StatusMessageType.Success;
                    StatusStatusMessage.Message = "Status Successfully Updated";
                    SetReturnedStatus(orderQuery.IsReturned);
                    SetDispatchedStatus(orderQuery.IsDispatched);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusStatusMessage.MessageType = StatusMessageType.Error;
                    StatusStatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["OrderNumber"]))
                {
                    var orderNumber = Request.QueryString["OrderNumber"];

                    using (var context = new ShoppingCartEntities())
                    {
                        var orderQuery = OrdersBL.GetObjectByID(orderNumber, context);

                        if (orderQuery != null)
                        {
                            OrderNumberLabel.Text = orderQuery.OrderNumber;
                            DateTimeLabel.Text = DataParser.GetDateTimeFormattedString(orderQuery.DateTime);
                            ShippingAddressLabel.Text = orderQuery.ShippingAddress;
                            BillingAddressLabel.Text = orderQuery.BillingAddress;
                            EmailIDLabel.Text = string.Format("<a href='mailto:{0}'>{1}</a>", orderQuery.EmailID, orderQuery.EmailID);
                            MobileLabel.Text = orderQuery.Mobile;
                            OrderTotalLabel.Text = string.Format("Rs. {0:0.00}/-", orderQuery.OrderTotal);
                            OrderPrintHyperLink.NavigateUrl = String.Format("OrderPrint.aspx?OrderNumber={0}", Request.QueryString["OrderNumber"]);

                            SetPaidStatus(orderQuery.IsPaid);
                            SetCancelledStatus(orderQuery.IsCancelled);
                            SetCompletedStatus(orderQuery.IsCompleted);
                            SetFailedStatus(orderQuery.IsFailed);
                            SetRefundStatus(orderQuery.IsRefund);
                            SetDispatchedStatus(orderQuery.IsDispatched);
                            SetReturnedStatus(orderQuery.IsReturned);

                            if (!orderQuery.IsFailed && !orderQuery.IsCancelled && !orderQuery.IsCompleted && !orderQuery.IsConfirmed && !orderQuery.IsDispatched && !orderQuery.IsPaid && !orderQuery.IsRefund && !orderQuery.IsReturned)
                            {
                                IsPaidButton.Enabled = IsRefundButton.Enabled = IsCancelledButton.Enabled = IsCompletedButton.Enabled = IsDispatchedButton.Enabled = IsReturnedButton.Enabled = false;
                            }
                        }
                        else
                        {
                            Response.Redirect("OrdersList.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("OrdersList.aspx");
                }
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}