using OWDARO.BLL.MembershipBLL;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class CheckOutStep2Component : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EmailIDTextBox.ValidationExpression = Validator.EmailValidationExpression;
                EmailIDTextBox.ValidationErrorMessage = Validator.EmailValidationErrorMessage;
                EmailIDTextBox.PopUpListBox.Width = new Unit("300px");

                MobileTextBox.ValidationExpression = Validator.MobileValidationExpression;
                MobileTextBox.ValidationErrorMessage = Validator.MobileValidationErrorMessage;
                MobileTextBox.PopUpListBox.Width = new Unit("200px");

                MembershipUser user = Membership.GetUser();

                using (var context = new ShoppingCartEntities())
                {
                    if (CartProcessingBL.DoesCartHaveOutOfStockItems(user, context))
                    {
                        Response.Redirect("~/Cart.aspx");
                    }

                    OrderNumberHiddenField.Value = CartProcessingBL.GetNewOrderNumber(context);
                }

                if (user != null)
                {
                    DeliveryAddressSelectionComponent.Title = LanguageHelper.GetKey("CheckoutPageDeliveryAddressListHoverMenuText");
                    DeliveryAddressSelectionComponent.Username = user.UserName;
                    DeliveryAddressSelectionComponent.AddressCategory = UserDataCategories.DeliveryAddressCategory.Value;

                    BillingAddressSelectionComponent.Title = LanguageHelper.GetKey("CheckoutPageBillingAddressListHoverMenuText");
                    BillingAddressSelectionComponent.Username = user.UserName;
                    BillingAddressSelectionComponent.AddressCategory = UserDataCategories.BillingAddressCategory.Value;

                    using (var context = new MembershipEntities())
                    {
                        var userEmailsQuery = (from userDatas in context.MS_UsersData where userDatas.UsersDataCategory == UserDataCategories.EmailCategory.Value select userDatas);

                        EmailIDTextBox.Text = UserBL.GetUserByUsername(user.UserName, context).Email;

                        EmailIDTextBox.PopUpListBox.DataSource = userEmailsQuery.ToList();
                        EmailIDTextBox.PopUpListBox.DataTextField = "UserData";
                        EmailIDTextBox.PopUpListBox.DataValueField = "UserData";
                        EmailIDTextBox.PopUpListBox.DataBind();

                        EmailIDTextBox.PopUpListBox.Items.Insert(0, new ListItem("Cancel", string.Empty));
                        EmailIDTextBox.PopUpListBox.Items.Add(EmailIDTextBox.Text);

                        var userMobilesQuery = (from userDatas in context.MS_UsersData where userDatas.UsersDataCategory == UserDataCategories.MobileCategory.Value select userDatas);

                        if (userMobilesQuery.Any())
                        {
                            MobileTextBox.PopUpListBox.DataSource = userMobilesQuery.ToList();
                            MobileTextBox.PopUpListBox.DataTextField = "UserData";
                            MobileTextBox.PopUpListBox.DataValueField = "UserData";
                            MobileTextBox.PopUpListBox.DataBind();

                            MobileTextBox.PopUpListBox.Items.Insert(0, new ListItem("Cancel", string.Empty));
                        }
                        else
                        {
                            MobileTextBox.EnablePopUp = false;
                        }
                    }

                    //IsUser==true
                }
                else if (string.IsNullOrWhiteSpace(Request.QueryString["GuestEmailID"]))
                {
                    Response.Redirect("~/UI/Pages/Open/ShoppingCart/CheckOut/CheckOutStep1.aspx");
                }
                else
                {
                    DeliveryAddressSelectionComponent.Visible = false;
                    BillingAddressSelectionComponent.Visible = false;
                    SaveThisBillingAddressCheckBox.Visible = false;
                    SaveThisDeliveryAddressCheckBox.Visible = false;

                    EmailIDTextBox.EnablePopUp = false;
                    EmailIDTextBox.ReadOnly = true;
                    EmailIDTextBox.Text = Request.QueryString["GuestEmailID"];

                    MobileTextBox.EnablePopUp = false;

                    //IsGuest==true
                }
            }

            DeliveryAddressSelectionComponent.UserAddressSelected += DeliveryAddressSelectionComponent_UserAddressSelected;
            BillingAddressSelectionComponent.UserAddressSelected += BillingAddressSelectionComponent_UserAddressSelected;
        }

        protected void BillingAddressSelectionComponent_UserAddressSelected(object sender, OWDARO.OEventArgs.UserAddressSelectionEventArgs e)
        {
            BillingAddressComponent.Street = e.StreetName;
            BillingAddressComponent.City = e.City;
            BillingAddressComponent.ZipCode = e.ZipCode;
            BillingAddressComponent.State = e.State;
            BillingAddressComponent.Country = e.Country;
        }

        protected void DeliveryAddressSelectionComponent_UserAddressSelected(object sender, OWDARO.OEventArgs.UserAddressSelectionEventArgs e)
        {
            DeliveryAddressComponent.Street = e.StreetName;
            DeliveryAddressComponent.City = e.City;
            DeliveryAddressComponent.ZipCode = e.ZipCode;
            DeliveryAddressComponent.State = e.State;
            DeliveryAddressComponent.Country = e.Country;
        }

        protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
        {
            if (UseForBillingCheckBox.Checked && Wizard1.ActiveStepIndex == 1)
            {
                Wizard1.ActiveStepIndex = 2;
            }

            if (Wizard1.ActiveStepIndex == 3)
            {
                MS_UserAdresses deliveryAddress = GetDeliveryAddress();
                MS_UserAdresses billingAddress = GetBillingAddress();

                OrderNumberLabel.Text = OrderNumberHiddenField.Value;
                DateTimeLabel.Text = string.Format("{0}", DataParser.GetDateTimeFormattedString(Utilities.DateTimeNow()));
                ShippingAddressLabel.Text = string.Format("{0}<br />{1}-{2},<br />{3}, {4}", deliveryAddress.StreetName, deliveryAddress.City, deliveryAddress.ZipCode, deliveryAddress.State, deliveryAddress.Country);
                BillingAddressLabel.Text = string.Format("{0}<br />{1}-{2},<br />{3}, {4}", billingAddress.StreetName, billingAddress.City, billingAddress.ZipCode, billingAddress.State, billingAddress.Country);
                EmailIDLabel.Text = EmailIDTextBox.Text;
                MobileLabel.Text = MobileTextBox.Text;

                using (var context = new ShoppingCartEntities())
                {
                    MembershipUser user = Membership.GetUser();

                    int cartCount = CartProcessingBL.GetCartCount(user, context);

                    if (cartCount <= 0)
                    {
                        Response.Redirect("~/Cart.aspx");
                    }

                    if (CartProcessingBL.DoesCartHaveOutOfStockItems(user, context))
                    {
                        StatusMessage.MessageType = StatusMessageType.Warning;
                        StatusMessage.Message = "Some of the items in the cart have gone out of stock. Please remove such items for successfully checking out.";

                        DisableFinishButton();
                        DisableBackButton();
                    }

                    double orderTotal = CartProcessingBL.GetCartTotal(user, context);
                    OrderTotalLabel.Text = orderTotal.ToString("0.00");

                    double shipmentCost = CartProcessingBL.CalculateShipmentCost(GetDeliveryAddress());
                    ShippingCostLabel.Text = (shipmentCost == 0) ? "Free" : shipmentCost.ToString("0.00");

                    double totalAmount = orderTotal + shipmentCost;
                    TotalAmountLabel.Text = totalAmount.ToString("0.00");

                    InitializeGrid(user);
                }
            }
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (Page.IsValid)
            {
                MembershipUser user = Membership.GetUser();

                MS_UserAdresses deliveryAddress = GetDeliveryAddress();
                MS_UserAdresses billingAddress = GetBillingAddress();

                using (var context = new ShoppingCartEntities())
                {
                    if (CartProcessingBL.DoesCartHaveOutOfStockItems(user, context))
                    {
                        StatusMessage.MessageType = StatusMessageType.Warning;
                        StatusMessage.Message = "Some of the items in the cart have gone out of stock. Please remove such items for successfully checking out.";

                        DisableFinishButton();
                        DisableBackButton();

                        InitializeGrid(user);
                    }
                    else
                    {
                        if (LockQuantity(context, user))
                        {
                            CreateOrder(user, deliveryAddress, billingAddress, context);
                        }
                        else
                        {
                            StatusMessage.MessageType = StatusMessageType.Error;
                            StatusMessage.Message = "There was an error while processing your order.";
                        }
                    }
                }
            }
        }

        protected void Wizard1_CancelButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Cart.aspx");
        }

        private bool LockQuantity(ShoppingCartEntities context, MembershipUser user)
        {
            if (user == null)
            {
                return CartProcessingBL.LockTempCartItems(context);
            }
            else
            {
                return CartProcessingBL.LockUserCartItems(user, context);
            }
        }

        private void ProceedToPaymentGateway(ShoppingCartEntities context, SC_Orders orderEntity)
        {
            orderEntity.ReceiptHTML = Utilities.GetHTML(RecieptPanel, string.Empty);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
            }

            Response.Redirect(String.Format("~/UI/Pages/Open/ShoppingCart/CheckOut/PaymentGateway.aspx?OrderNumber={0}", OrderNumberHiddenField.Value));
        }

        private void CreateOrder(MembershipUser user, MS_UserAdresses deliveryAddress, MS_UserAdresses billingAddress, ShoppingCartEntities context)
        {
            SC_Orders orderEntity = new SC_Orders();
            orderEntity.DateTime = Utilities.DateTimeNow();
            orderEntity.EmailID = EmailIDTextBox.Text;
            orderEntity.Mobile = MobileTextBox.Text;
            orderEntity.IsDispatched = false;
            orderEntity.IsFailed = false;
            orderEntity.IsRefund = false;
            orderEntity.IsReturned = false;
            orderEntity.IsCancelled = false;
            orderEntity.IsCompleted = false;
            orderEntity.IsConfirmed = true;
            orderEntity.IsPaid = false;
            orderEntity.ShipmentCost = CartProcessingBL.CalculateShipmentCost(deliveryAddress);
            orderEntity.OrderTotal = CartProcessingBL.GetCartTotal(user, context);
            orderEntity.OrderNumber = OrderNumberHiddenField.Value;
            orderEntity.BillingAddress = string.Format("{0}<br />{1}-{2},<br />{3}, {4}", billingAddress.StreetName, billingAddress.City, billingAddress.ZipCode, billingAddress.State, billingAddress.Country);
            orderEntity.ShippingAddress = string.Format("{0}<br />{1}-{2},<br />{3}, {4}", deliveryAddress.StreetName, deliveryAddress.City, deliveryAddress.ZipCode, deliveryAddress.State, deliveryAddress.Country);

            if (OrdersBL.Add(orderEntity, context))
            {
                if (CartProcessingBL.AddOrderDetails(OrderNumberHiddenField.Value, user, context))
                {
                    if (user != null)
                    {
                        if (CartProcessingBL.AddToUserOrders(OrderNumberHiddenField.Value, user, context))
                        {
                            SaveAddress(user, deliveryAddress, billingAddress);

                            ProceedToPaymentGateway(context, orderEntity);
                        }
                        else
                        {
                            StatusMessage.MessageType = StatusMessageType.Error;
                            StatusMessage.Message = "An error occurred while taking your order as a registered user";

                            //TODO ROLL BACK ORDER
                        }
                    }
                    else
                    {
                        if (CartProcessingBL.AddToGuestOrders(OrderNumberHiddenField.Value, EmailIDTextBox.Text, MobileTextBox.Text, context))
                        {
                            ProceedToPaymentGateway(context, orderEntity);
                        }
                        else
                        {
                            StatusMessage.MessageType = StatusMessageType.Error;
                            StatusMessage.Message = "An error occurred while taking your order as a guest";

                            //TODO ROLL BACK ORDER
                        }
                    }
                }
                else
                {
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = "An error occurred while taking your order details";

                    //TODO ROLL BACK ORDER
                }
            }
            else
            {
                StatusMessage.MessageType = StatusMessageType.Error;
                StatusMessage.Message = "An error occurred while taking your order";

                //TODO ROLL BACK ORDER
            }
        }

        private void SaveAddress(MembershipUser user, MS_UserAdresses deliveryAddress, MS_UserAdresses billingAddress)
        {
            if (user != null)
            {
                using (var context = new MembershipEntities())
                {
                    if (SaveThisDeliveryAddressCheckBox.Checked)
                    {
                        deliveryAddress.Username = user.UserName;
                        context.MS_UserAdresses.Add(deliveryAddress);
                    }

                    if (SaveThisBillingAddressCheckBox.Checked && !UseForBillingCheckBox.Checked)
                    {
                        billingAddress.Username = user.UserName;
                        context.MS_UserAdresses.Add(billingAddress);
                    }

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                    }
                }
            }
        }

        private MS_UserAdresses GetDeliveryAddress()
        {
            MS_UserAdresses deliveryAddress = new MS_UserAdresses();
            deliveryAddress.AddressCategory = UserDataCategories.DeliveryAddressCategory.Value;
            deliveryAddress.City = DeliveryAddressComponent.City;
            deliveryAddress.Country = DeliveryAddressComponent.Country;
            deliveryAddress.State = DeliveryAddressComponent.State;
            deliveryAddress.StreetName = DeliveryAddressComponent.Street;
            deliveryAddress.ZipCode = DeliveryAddressComponent.ZipCode;

            return deliveryAddress;
        }

        private MS_UserAdresses GetBillingAddress()
        {
            MS_UserAdresses billingAddress = new MS_UserAdresses();
            billingAddress.AddressCategory = UserDataCategories.BillingAddressCategory.Value;
            billingAddress.City = BillingAddressComponent.City;
            billingAddress.Country = BillingAddressComponent.Country;
            billingAddress.State = BillingAddressComponent.State;
            billingAddress.StreetName = BillingAddressComponent.Street;
            billingAddress.ZipCode = BillingAddressComponent.ZipCode;

            if (UseForBillingCheckBox.Checked)
            {
                billingAddress = GetDeliveryAddress();
            }

            return billingAddress;
        }

        private void InitializeGrid(MembershipUser user)
        {
            if (user == null)
            {
                CartProcessingBL.InitializeTempCartGridView(GridView1);
            }
            else
            {
                CartProcessingBL.InitializeUserCartGridView(GridView1, user);
            }
        }

        private void DisableFinishButton()
        {
            Button finishButton = Wizard1.FindControl("FinishNavigationTemplateContainerID").FindControl("FinishButton") as Button;

            finishButton.Enabled = false;
            finishButton.Visible = false;
        }

        private void DisableBackButton()
        {
            Button backButton = Wizard1.FindControl("FinishNavigationTemplateContainerID").FindControl("FinishPreviousButton") as Button;

            backButton.Enabled = false;
            backButton.Visible = false;
        }
    }
}