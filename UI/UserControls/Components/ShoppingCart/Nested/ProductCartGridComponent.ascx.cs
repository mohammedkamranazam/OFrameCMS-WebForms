using OWDARO.Helpers;
using OWDARO.OEventArgs;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductCartGridComponent : System.Web.UI.UserControl
    {
        public event ItemCountChangedEventHandler ItemCountChanged;

        public event EventHandler ShowPopUp;

        private void OnItemCountChanged(int itemCount)
        {
            if (ItemCountChanged != null)
            {
                var args = new ItemCountEventArgs(itemCount);

                ItemCountChanged(this, args);
            }
        }

        private void OnShowPopUp()
        {
            if (ShowPopUp != null)
            {
                ShowPopUp(this, EventArgs.Empty);
            }
        }

        protected void CartRefreshButton_Click(object sender, EventArgs e)
        {
            InitializeCart();

            OnShowPopUp();
        }

        protected void GridView1_RowCancellingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;

            InitializeCart();

            OnShowPopUp();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var CartIDHiddenField = GridView1.Rows[e.RowIndex].FindControl("CartIDHiddenField") as HiddenField;

            var cartID = DataParser.IntParse(CartIDHiddenField.Value);

            var user = Membership.GetUser();

            using (var context = new ShoppingCartEntities())
            {
                if (user == null)
                {
                    try
                    {
                        TempCartBL.Delete(TempCartBL.GetObjectbyID(cartID, context), context);
                        StatusMessage.Message = "item removed successfully";
                        StatusMessage.MessageType = StatusMessageType.Success;
                    }
                    catch (Exception ex)
                    {
                        StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                        StatusMessage.MessageType = StatusMessageType.Error;
                        ErrorLogger.LogError(ex);
                    }
                }
                else
                {
                    try
                    {
                        UserCartBL.Delete(UserCartBL.GetObjectbyID(cartID, context), context);
                        StatusMessage.Message = "item removed successfully";
                        StatusMessage.MessageType = StatusMessageType.Success;
                    }
                    catch (Exception ex)
                    {
                        StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                        StatusMessage.MessageType = StatusMessageType.Error;
                        ErrorLogger.LogError(ex);
                    }
                }

                InitializeCart(user, context);

                OnShowPopUp();
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

            InitializeCart();

            OnShowPopUp();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Page.IsValid)
            {
                var ProductIDHiddenField = GridView1.Rows[e.RowIndex].FindControl("ProductIDHiddenField") as HiddenField;
                var EditQuantityTextBox = GridView1.Rows[e.RowIndex].FindControl("EditQuantityTextBox") as TextBox;

                var productID = int.Parse(ProductIDHiddenField.Value);
                var quantity = float.Parse(EditQuantityTextBox.Text);

                using (var context = new ShoppingCartEntities())
                {
                    var productEntity = ProductsBL.GetObjectByID(productID, context);

                    if (quantity <= 0 || quantity < (double)productEntity.MinOQ || quantity > (double)productEntity.MaxOQ || quantity > (double)productEntity.AvailableQuantity)
                    {
                        StatusMessage.MessageType = StatusMessageType.Warning;
                        StatusMessage.Message = "Invalid Quantity Entered";

                        return;
                    }

                    var user = Membership.GetUser();

                    if (user == null)
                    {
                        var tempCart = new SC_TempCart();
                        tempCart.Quantity = quantity;
                        tempCart.ProductID = productID;
                        tempCart.AnonymousUserID = MembershipHelper.GetAnonymousID();

                        try
                        {
                            CartProcessingBL.UpdateTempCartFromCart(tempCart, context);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);
                            StatusMessage.MessageType = StatusMessageType.Error;
                            StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                        }
                    }
                    else
                    {
                        var userCart = new SC_UserCart();
                        userCart.Quantity = quantity;
                        userCart.ProductID = productID;
                        userCart.Username = user.UserName;

                        try
                        {
                            CartProcessingBL.UpdateUserCartFromCart(userCart, context);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);
                            StatusMessage.MessageType = StatusMessageType.Error;
                            StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                        }
                    }

                    GridView1.EditIndex = -1;

                    InitializeCart(user, context);

                    OnShowPopUp();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var context = new ShoppingCartEntities())
                {
                    var user = Membership.GetUser();

                    if (!string.IsNullOrWhiteSpace(Request.QueryString["ProductID"]) && !string.IsNullOrWhiteSpace(Request.QueryString["InCart"]))
                    {
                        AddToCart(user, context);
                    }

                    InitializeCart(user, context);
                }
            }
        }

        public void AddToCart()
        {
            var user = Membership.GetUser();

            AddToCart(user);
        }

        public void AddToCart(MembershipUser user)
        {
            using (var context = new ShoppingCartEntities())
            {
                AddToCart(user, context);
            }
        }

        public void AddToCart(MembershipUser user, ShoppingCartEntities context)
        {
            var productID = DataParser.IntParse(Request.QueryString["ProductID"]);

            var productEntity = ProductsBL.GetObjectByID(productID, context);

            if (productEntity == null)
            {
                Response.Redirect("~/Products.aspx", false);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

            if (productEntity.PreOderFlag)
            {
                StatusMessage.MessageType = StatusMessageType.Info;
                StatusMessage.Message = "Item is not available for purchase. Please pre order it from Item's page.";
                return;
            }

            if (productEntity.Hide)
            {
                StatusMessage.MessageType = StatusMessageType.Warning;
                StatusMessage.Message = "Item is not available for purchase.";
                return;
            }

            var minOQ = (double)productEntity.MinOQ;
            var availQty = (double)productEntity.AvailableQuantity;
            double qtyToAdd = 1;

            if (availQty <= 0)
            {
                StatusMessage.MessageType = StatusMessageType.Info;
                StatusMessage.Message = "This item is out of stock. Kindly subscribe for stock availability notification.";
                return;
            }

            if (minOQ <= 0)
            {
                qtyToAdd = 1;
            }
            else
            {
                if (minOQ > availQty)
                {
                    qtyToAdd = availQty;
                }
                else
                {
                    qtyToAdd = minOQ;
                }
            }
            if (user == null)
            {
                var tc = new SC_TempCart();
                tc.AnonymousUserID = MembershipHelper.GetAnonymousID();
                tc.ProductID = productEntity.ProductID;
                tc.Quantity = qtyToAdd;

                if (!CartProcessingBL.IfProductInTempCart(tc, context))
                {
                    try
                    {
                        CartProcessingBL.AddToTempCart(tc, context);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        StatusMessage.MessageType = StatusMessageType.Error;
                        StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                    }
                }
            }
            else
            {
                var uc = new SC_UserCart();
                uc.Username = user.UserName;
                uc.ProductID = productEntity.ProductID;
                uc.Quantity = qtyToAdd;

                if (!CartProcessingBL.IfProductInUserCart(uc, context))
                {
                    try
                    {
                        CartProcessingBL.AddToUserCart(uc, context);
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

        public void InitializeCart()
        {
            var user = Membership.GetUser();

            InitializeCart(user);
        }

        public void InitializeCart(MembershipUser user)
        {
            using (var context = new ShoppingCartEntities())
            {
                InitializeCart(user, context);
            }
        }

        public void InitializeCart(MembershipUser user, ShoppingCartEntities context)
        {
            if (user == null)
            {
                CartProcessingBL.InitializeTempCartGridView(GridView1);
            }
            else
            {
                CartProcessingBL.InitializeUserCartGridView(GridView1, user);
            }

            var cartCount = CartProcessingBL.GetCartCount(user, context);

            if (cartCount == 0 || CartProcessingBL.DoesCartHaveOutOfStockItems(user, context))
            {
                CheckOutLink.Enabled = false;
                CheckOutLink.CssClass = "CheckOutLinkDisabled";
            }
            else
            {
                CheckOutLink.Enabled = true;
                CheckOutLink.CssClass = "CheckOutLink";
            }

            if (cartCount == 0)
            {
                CartHeaderPanel.Visible = false;
                SubTotalAmountDiv.Visible = false;
                DiscountAmountDiv.Visible = false;
                PromoCodeDiscountAmountDiv.Visible = false;
                TotalAmountDiv.Visible = false;
            }
            else
            {
                CartHeaderPanel.Visible = true;

                var subTotalAmount = CartProcessingBL.GetCartSubTotal(user, context);
                var totalAmount = CartProcessingBL.GetCartTotal(user, context);
                var promocodeDiscountAmount = 0d;
                var discountAmount = subTotalAmount - totalAmount;

                TotalAmountDiv.Visible = true;

                if (subTotalAmount == 0)
                {
                    SubTotalAmountDiv.Visible = false;
                }
                else
                {
                    SubTotalAmountDiv.Visible = true;
                }

                if (discountAmount == 0)
                {
                    DiscountAmountDiv.Visible = false;
                }
                else
                {
                    DiscountAmountDiv.Visible = true;
                }

                if (promocodeDiscountAmount == 0)
                {
                    PromoCodeDiscountAmountDiv.Visible = false;
                }
                else
                {
                    PromoCodeDiscountAmountDiv.Visible = true;
                }

                CartSubTotalAmountLabel.Text = string.Format("{0:0.00}", subTotalAmount);
                CartSubTotalAmountTextLabel.Text = string.Format("Sub Total: Rs.");

                CartDiscountAmountLabel.Text = string.Format("{0:0.00}", discountAmount);
                CartDiscountAmountTextLabel.Text = string.Format("Discount Amount: Rs.");

                CartPromoCodeDiscountAmountLabel.Text = string.Format("{0:0.00}", promocodeDiscountAmount);
                CartPromoCodeDiscountAmountTextLabel.Text = string.Format("Promo Code Discount: Rs.");

                CartTotalAmountLabel.Text = string.Format("{0:0.00}", totalAmount);
                CartTotalAmountTextLabel.Text = string.Format("Amount Payable: Rs.");
            }

            OnItemCountChanged(cartCount);
        }
    }
}