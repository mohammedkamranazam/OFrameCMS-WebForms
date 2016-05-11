using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductReviewsComponent : System.Web.UI.UserControl
    {
        public ShoppingCartEntities DatabaseContext
        {
            get;
            set;
        }

        public int ProductID
        {
            get
            {
                return (ViewState["ProductID"] == null) ? 0 : DataParser.IntParse(ViewState["ProductID"].ToString());
            }

            set
            {
                ViewState["ProductID"] = value.ToString();

                Render();
            }
        }

        private void Render()
        {
            if (DatabaseContext == null)
            {
                using (var context = new ShoppingCartEntities())
                {
                    Render(context);
                }
            }
            else
            {
                Render(DatabaseContext);
            }
        }

        private void Render(ShoppingCartEntities context)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}