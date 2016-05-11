using ProjectJKL.BLL.ShoppingCartBLL;
using System;

namespace ProjectJKL.UI.Pages.Open.ShoppingCart.CheckOut
{
    public partial class PaymentConfirmed : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetCheckOutThemeFile();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CartProcessingBL.CheckPaymentConfirmation();

                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("PaymentConfirmedPage"));
            }
        }
    }
}