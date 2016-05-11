using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.Linq;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductIconsComponent : System.Web.UI.UserControl
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
                var productIconsQuery = (from productIcons in context.SC_ProductIcons
                                         where productIcons.ProductID == ProductID
                                         select productIcons);
                IconsRepeater.DataSource = productIconsQuery.ToList();
                IconsRepeater.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}