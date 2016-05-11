using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductColorsComponent : System.Web.UI.UserControl
    {
        public ShoppingCartEntities DatabaseContext
        {
            get;
            set;
        }

        public bool HideTitle
        {
            set
            {
                Title.Visible = !value;
            }
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

        private string GetColorToolTipString(IQueryable<SC_Products> productsByColor, ShoppingCartEntities context)
        {
            var tip = string.Empty;

            if (productsByColor.Any())
            {
                var sizes = new List<SC_Sizes>();

                foreach (SC_Products productByColor in productsByColor)
                {
                    if (!sizes.Contains(productByColor.SC_Sizes) && productByColor.SizeID != null)
                    {
                        sizes.Add(productByColor.SC_Sizes);
                    }
                }

                var count = sizes.Count;

                if (count > 0)
                {
                    tip = string.Format("<br />Size: ");
                }

                var item = 0;
                foreach (SC_Sizes size in sizes)
                {
                    tip += size.Title;
                    item++;

                    if (item < count)
                    {
                        tip += ", ";
                    }
                }
            }

            return tip;
        }

        private string GetRollOverImageByColor(SC_Products firstProductByColor, ShoppingCartEntities context)
        {
            var imageURL = AppConfig.NoImage;

            if (firstProductByColor != null)
            {
                imageURL = ProductImagesBL.GetProductFirstImageThumb(firstProductByColor.ProductID, context);
            }

            return imageURL;
        }

        private bool IsCurrent(int colorID, int? productColorID)
        {
            var isCurrent = false;

            if (productColorID == null)
            {
                isCurrent = false;
                return isCurrent;
            }

            isCurrent = colorID == productColorID;

            return isCurrent;
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
            var productEntity = ProductsBL.GetObjectByID(ProductID, context);

            if (productEntity != null)
            {
                var sectionID = (int)productEntity.SectionID;
                var categoryID = (int)productEntity.CategoryID;
                var subCategoryID = (int)productEntity.SubCategoryID;
                var itemNumber = productEntity.ItemNumber;
                var sizeIDQueryString = string.Empty;
                var colors = new List<SC_Colors>();

                var productsByGroup = (from products in context.SC_Products
                                       where products.ItemNumber == itemNumber && products.SectionID == sectionID && products.CategoryID == categoryID && products.SubCategoryID == subCategoryID && products.Hide == false
                                       select products);
                var productsWithColor = (from products in productsByGroup
                                         where products.ColorID != null
                                         select products);

                if (productEntity.SizeID != null)
                {
                    sizeIDQueryString = string.Format("&SizeID={0}", productEntity.SizeID);
                }

                foreach (SC_Products productWithColor in productsWithColor)
                {
                    if (!colors.Contains(productWithColor.SC_Colors))
                    {
                        colors.Add(productWithColor.SC_Colors);
                    }
                }

                if (!colors.Any())
                {
                    HideTitle = true;
                }

                foreach (SC_Colors color in colors)
                {
                    var productsByColor = (from products in productsByGroup
                                           where products.ColorID == color.ColorID
                                           select products);
                    var firstProductByColor = productsByColor.FirstOrDefault();

                    var productLink = string.Empty;
                    var onHoverString = string.Format("OnAction({0}, '{1}');", ProductID, ResolveClientUrl(GetRollOverImageByColor(firstProductByColor, context)));
                    var onHoverOutString = string.Format("OnAction({0}, '{1}');", ProductID, ResolveClientUrl(ProductImagesBL.GetProductFirstImageThumb(ProductID, context)));

                    if (!IsCurrent(color.ColorID, productEntity.ColorID))
                    {
                        var colorQueryString = string.Format("&ColorID={0}", color.ColorID);
                        var clickIDQueryString = string.Format("&ClickID=Color");
                        var url = string.Format("~/Product.aspx?ProductID={0}", ProductID);
                        productLink = string.Format("{0}{1}{2}{3}", url, colorQueryString, sizeIDQueryString, clickIDQueryString);
                    }

                    var colorLink = new HyperLink();
                    colorLink.CssClass = string.Format("ProductColor{0}", (IsCurrent(color.ColorID, productEntity.ColorID)) ? " ProductColorSelected" : string.Empty);
                    colorLink.Text = color.Title;
                    colorLink.NavigateUrl = string.Format("{0}", productLink);

                    var tipLabel = new Label();
                    tipLabel.CssClass = "SpanTip";
                    tipLabel.Text = string.Format("Color: {0}{1}", color.Title, GetColorToolTipString(productsByColor, context));

                    var colorImage = new Image();
                    colorImage.AlternateText = color.Title;
                    colorImage.ImageUrl = color.ImageURL;
                    colorImage.Attributes.Add("onmouseover", onHoverString);
                    colorImage.Attributes.Add("onmouseout", onHoverOutString);

                    colorLink.Controls.Add(colorImage);
                    colorLink.Controls.Add(tipLabel);
                    Panel1.Controls.AddAt(0, colorLink);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}