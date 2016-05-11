using System;

namespace ProjectJKL.UI.Pages.Open.ShoppingCart.CheckOut
{
    public partial class PaymentGateway : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetCheckOutThemeFile();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("PaymentGatewayPage"));
            }

            Response.Redirect(string.Format("~/UI/Pages/Open/ShoppingCart/CheckOut/PaymentConfirmed.aspx?OrderNumber={0}&PaymentDone={1}", Request.QueryString["OrderNumber"], true));
        }
    }
}