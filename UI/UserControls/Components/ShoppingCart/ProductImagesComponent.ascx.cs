using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Linq;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductImagesComponent : System.Web.UI.UserControl
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
                ProductDetailsImageImage.ImageUrl = ProductImagesBL.GetProductFirstImageThumb(ProductID, DatabaseContext);
                ProductDetailsImageImage.Attributes.Add("data-zoom-image", ResolveClientUrl(ProductImagesBL.GetProductFirstImage(ProductID, DatabaseContext)));

                var productImagesQuery = (from productImages in DatabaseContext.SC_ProductImages
                                          where productImages.ProductID == ProductID
                                          select productImages);

                ImagesRepeater.DataSource = productImagesQuery.ToList();
                ImagesRepeater.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}