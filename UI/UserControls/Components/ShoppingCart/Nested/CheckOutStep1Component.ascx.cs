using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Web.Security;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class CheckOutStep1Component : System.Web.UI.UserControl
    {
        private void GuestBuyToolbar_CustomClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect(string.Format("~/UI/Pages/Open/ShoppingCart/CheckOut/CheckOutStep2.aspx?GuestEmailID={0}", GuestEmailIDTextBox.Text));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GuestEmailIDTextBox.ValidationErrorMessage = Validator.EmailValidationErrorMessage;
                GuestEmailIDTextBox.ValidationExpression = Validator.EmailValidationExpression;

                if (CartProcessingBL.GetCartCount() <= 0)
                {
                    Response.Redirect("~/Cart.aspx");
                }

                var user = Membership.GetUser();

                var checkOutStep2Url = "~/UI/Pages/Open/ShoppingCart/CheckOut/CheckOutStep2.aspx";

                if (user != null)
                {
                    Response.Redirect(checkOutStep2Url);
                }
                else
                {
                    if (!AppConfig.AllowGuestBuy)
                    {
                        Response.Redirect(string.Format("~/UI/Pages/LogOn/Default.aspx?ReturnUrl={0}", checkOutStep2Url));
                    }
                }
            }

            GuestBuyToolbar.CustomClick += GuestBuyToolbar_CustomClick;
        }
    }
}