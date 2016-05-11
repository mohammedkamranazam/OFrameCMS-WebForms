using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductSizesComponent : System.Web.UI.UserControl
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

        private bool IsAvailable(SC_Sizes size, List<SC_Sizes> availableSizes)
        {
            var value = false;

            if (availableSizes.Contains(size))
            {
                value = true;
            }

            return value;
        }

        private bool IsCurrent(SC_Sizes size, SC_Sizes productSize)
        {
            var isCurrent = false;

            if (productSize == null)
            {
                isCurrent = false;
                return isCurrent;
            }

            isCurrent = size == productSize;

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
            if (!IsPostBack)
            {
                var productEntity = ProductsBL.GetObjectByID(ProductID, context);

                if (productEntity != null)
                {
                    var sectionID = (int)productEntity.SectionID;
                    var categoryID = (int)productEntity.CategoryID;
                    var subCategoryID = (int)productEntity.SubCategoryID;
                    var itemNumber = productEntity.ItemNumber;
                    var colorIDQueryString = string.Empty;
                    var sizes = new List<SC_Sizes>();
                    var availableSizes = new List<SC_Sizes>();

                    var productsByGroup = (from products in context.SC_Products
                                           where products.ItemNumber == itemNumber && products.SectionID == sectionID && products.CategoryID == categoryID && products.SubCategoryID == subCategoryID && products.Hide == false
                                           select products);
                    var productsWithSize = (from products in productsByGroup
                                            where products.SizeID != null
                                            select products);

                    foreach (SC_Products productWithSize in productsWithSize)
                    {
                        if (!sizes.Contains(productWithSize.SC_Sizes))
                        {
                            sizes.Add(productWithSize.SC_Sizes);
                        }
                    }

                    if (productEntity.ColorID != null)
                    {
                        colorIDQueryString = string.Format("&ColorID={0}", productEntity.ColorID);

                        var productSizesByColor = (from products in productsByGroup
                                                   where products.ColorID == productEntity.ColorID
                                                   select products);

                        foreach (SC_Products productSizeByColor in productSizesByColor)
                        {
                            if (!availableSizes.Contains(productSizeByColor.SC_Sizes))
                            {
                                availableSizes.Add(productSizeByColor.SC_Sizes);
                            }
                        }
                    }

                    if (!sizes.Any())
                    {
                        HideTitle = true;
                    }

                    foreach (SC_Sizes size in sizes)
                    {
                        var productLink = string.Empty;

                        if (!IsCurrent(size, productEntity.SC_Sizes))
                        {
                            var sizeQueryString = string.Format("&SizeID={0}", size.SizeID);
                            var clickIDQueryString = string.Format("&ClickID=Size");
                            var url = string.Format("~/Product.aspx?ProductID={0}", ProductID);
                            productLink = string.Format("{0}{1}{2}{3}", url, sizeQueryString, colorIDQueryString, clickIDQueryString);
                        }

                        if (IsAvailable(size, availableSizes))
                        {
                            var sizeLink = new HyperLink();
                            sizeLink.CssClass = string.Format("ProductSize{0}", (IsCurrent(size, productEntity.SC_Sizes)) ? " ProductSizeSelected" : string.Empty);
                            sizeLink.Text = size.Title;
                            sizeLink.NavigateUrl = string.Format("{0}", productLink);

                            Panel1.Controls.AddAt(0, sizeLink);
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}