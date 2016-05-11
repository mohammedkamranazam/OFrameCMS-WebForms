using OWDARO.Models;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Linq;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductDetailsComponent : System.Web.UI.UserControl
    {
        private int GetProductByColorID()
        {
            using (var context = new ShoppingCartEntities())
            {
                var id = 0;

                var productID = DataParser.IntParse(Request.QueryString["ProductID"]);
                var colorID = DataParser.IntParse(Request.QueryString["ColorID"]);

                var productQuery = ProductsBL.GetObjectByID(productID, context);

                if (productQuery != null)
                {
                    var sectionID = (int)productQuery.SectionID;
                    var categoryID = (int)productQuery.CategoryID;
                    var subCategoryID = (int)productQuery.SubCategoryID;
                    var itemNumber = productQuery.ItemNumber;

                    var productsQuery = (from products in context.SC_Products
                                         where products.SectionID == sectionID && products.CategoryID == categoryID && products.SubCategoryID == subCategoryID && products.ItemNumber == itemNumber && products.ColorID == colorID && products.Hide == false
                                         select products);

                    if (productsQuery.Any())
                    {
                        id = productsQuery.FirstOrDefault().ProductID;
                    }
                }

                return id;
            }
        }

        private int GetProductByColorIDAndSizeID()
        {
            using (var context = new ShoppingCartEntities())
            {
                var id = 0;

                var productID = DataParser.IntParse(Request.QueryString["ProductID"]);
                var colorID = DataParser.IntParse(Request.QueryString["ColorID"]);
                var sizeID = DataParser.IntParse(Request.QueryString["SizeID"]);

                var productQuery = ProductsBL.GetObjectByID(productID, context);

                if (productQuery != null)
                {
                    var sectionID = (int)productQuery.SectionID;
                    var categoryID = (int)productQuery.CategoryID;
                    var subCategoryID = (int)productQuery.SubCategoryID;
                    var itemNumber = productQuery.ItemNumber;

                    var productsQuery = (from products in context.SC_Products
                                         where products.SectionID == sectionID && products.CategoryID == categoryID && products.SubCategoryID == subCategoryID && products.ItemNumber == itemNumber && products.ColorID == colorID && products.SizeID == sizeID && products.Hide == false
                                         select products);

                    if (productsQuery.Any())
                    {
                        id = productsQuery.FirstOrDefault().ProductID;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(Request.QueryString["ClickID"]))
                        {
                            var clickID = Request.QueryString["ClickID"];

                            switch (clickID)
                            {
                                case "Color":
                                    var productsColorQuery = (from products in context.SC_Products
                                                              where products.SectionID == sectionID && products.CategoryID == categoryID && products.SubCategoryID == subCategoryID && products.ItemNumber == itemNumber && products.ColorID == colorID && products.Hide == false
                                                              select products);
                                    if (productsColorQuery.Any())
                                    {
                                        id = productsColorQuery.FirstOrDefault().ProductID;
                                    }
                                    break;

                                case "Size":
                                    var productsSizeQuery = (from products in context.SC_Products
                                                             where products.SectionID == sectionID && products.CategoryID == categoryID && products.SubCategoryID == subCategoryID && products.ItemNumber == itemNumber && products.SizeID == sizeID && products.Hide == false
                                                             select products);
                                    if (productsSizeQuery.Any())
                                    {
                                        id = productsSizeQuery.FirstOrDefault().ProductID;
                                    }
                                    break;
                            }
                        }
                    }
                }

                return id;
            }
        }

        private int GetProductBySizeID()
        {
            using (var context = new ShoppingCartEntities())
            {
                var id = 0;

                var productID = DataParser.IntParse(Request.QueryString["ProductID"]);
                var sizeID = DataParser.IntParse(Request.QueryString["SizeID"]);

                var productQuery = ProductsBL.GetObjectByID(productID, context);

                if (productQuery != null)
                {
                    var sectionID = (int)productQuery.SectionID;
                    var categoryID = (int)productQuery.CategoryID;
                    var subCategoryID = (int)productQuery.SubCategoryID;
                    var itemNumber = productQuery.ItemNumber;

                    var productsQuery = (from products in context.SC_Products
                                         where products.SectionID == sectionID && products.CategoryID == categoryID && products.SubCategoryID == subCategoryID && products.ItemNumber == itemNumber && products.SizeID == sizeID && products.Hide == false
                                         select products);

                    if (productsQuery.Any())
                    {
                        id = productsQuery.FirstOrDefault().ProductID;
                    }
                }

                return id;
            }
        }

        private void SetProductDetails(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                var productQuery = ProductsBL.GetObjectByID(productID, context);

                if (productQuery != null && !productQuery.Hide)
                {
                    var seoEntity = new SEO();
                    seoEntity.Title = productQuery.Title;
                    seoEntity.Description = productQuery.Details;
                    seoEntity.Keywords = productQuery.Tags;

                    Utilities.SetPageSEO(Page, seoEntity);

                    TitleLiteral.Text = productQuery.Title;
                    SubTitleLiteral.Text = productQuery.SubTitle;
                    LikesLiteral.Text = (string.IsNullOrWhiteSpace(productQuery.Likes.ToString())) ? "0" : productQuery.Likes.ToString();
                    Rating1.CurrentRating = RatingsBL.CurrentRating(productID, context);
                    DescriptionLiteral.Text = productQuery.Description;
                    ReviewsCountLiteral.Text = string.Format("{0} Reviews", ReviewsBL.GetReviewsCount(productID, context));
                    PriceLiteral.Text = ProductsBL.GetPriceText(productID, context);
                    DiscountLiteral.Text = ProductsBL.GetHasDiscountText(productID, context);

                    if (!string.IsNullOrWhiteSpace(productQuery.PriceDescription))
                    {
                        PriceDescriptionDiv.Visible = true;
                        PriceDescriptionLiteral.Text = productQuery.PriceDescription;
                    }
                    else
                    {
                        PriceDescriptionDiv.Visible = false;
                    }

                    if (!string.IsNullOrWhiteSpace(productQuery.SpecialOffer))
                    {
                        SpecialOfferDiv.Visible = true;
                        SpecialOfferLiteral.Text = productQuery.SpecialOffer;
                    }
                    else
                    {
                        SpecialOfferDiv.Visible = false;
                    }

                    if (productQuery.AvailabilityTypeID != null)
                    {
                        var availabilityTypeQuery = AvailabilityTypesBL.GetObjectByID((int)productQuery.AvailabilityTypeID, context);

                        if (availabilityTypeQuery != null)
                        {
                            AvailabilityTypeLiteral.Text = availabilityTypeQuery.Title;
                            AvailabilityTypeDiv.Style.Add("color", availabilityTypeQuery.ColorName);
                            if (!string.IsNullOrWhiteSpace(availabilityTypeQuery.Description))
                            {
                                AvailabilityTypeDescriptionDiv.Visible = true;
                                AvailabilityDescriptionLiteral.Text = availabilityTypeQuery.Description;
                            }
                            else
                            {
                                AvailabilityTypeDescriptionDiv.Visible = false;
                            }
                        }
                        else
                        {
                            AvailabilityDiv.Visible = false;
                        }
                    }
                    else
                    {
                        AvailabilityDiv.Visible = false;
                    }

                    if (productQuery.BrandID != null)
                    {
                        var brandQuery = BrandsBL.GetObjectByID((int)productQuery.BrandID, context);

                        if (brandQuery != null)
                        {
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(productQuery.Details))
                    {
                        DetailsDiv.Visible = true;
                        DetailsLiteral.Text = productQuery.Details;
                    }
                    else
                    {
                        DetailsDiv.Visible = false;
                    }

                    ProductImagesComponent1.DatabaseContext = context;
                    ProductImagesComponent1.ProductID = productID;
                    ProductIconsComponent1.DatabaseContext = context;
                    ProductIconsComponent1.ProductID = productID;
                    ProductColorsComponent1.DatabaseContext = context;
                    ProductColorsComponent1.ProductID = productID;
                    ProductSizesComponent1.DatabaseContext = context;
                    ProductSizesComponent1.ProductID = productID;
                    ProductHighlightsComponent1.DatabaseContext = context;
                    ProductHighlightsComponent1.ProductID = productID;
                    HotItemImage.Visible = ProductsBL.IsHotItem((int)productQuery.SoldOutCount);
                    NewArrivalImage.Visible = ProductsBL.IsNewProduct((DateTime)productQuery.UploadedOn);
                    OutOfStockSpan.Visible = ProductsBL.IsOutOfStock(productID, context);
                    InStockSpan.Visible = productQuery.AvailableQuantity > 0;
                }
                else
                {
                    Response.Redirect("~/Products.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pID = 0;

                if (!string.IsNullOrWhiteSpace(Request.QueryString["ProductID"]) && !string.IsNullOrWhiteSpace(Request.QueryString["ColorID"]) && !string.IsNullOrWhiteSpace(Request.QueryString["SizeID"]))
                {
                    pID = GetProductByColorIDAndSizeID();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["ProductID"]) && !string.IsNullOrWhiteSpace(Request.QueryString["ColorID"]))
                    {
                        pID = GetProductByColorID();
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(Request.QueryString["ProductID"]) && !string.IsNullOrWhiteSpace(Request.QueryString["SizeID"]))
                        {
                            pID = GetProductBySizeID();
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(Request.QueryString["ProductID"]))
                            {
                                var productID = DataParser.IntParse(Request.QueryString["ProductID"]);
                                SetProductDetails(productID);
                            }
                            else
                            {
                                Response.Redirect("~/Products.aspx");
                            }
                        }
                    }
                }
                if (pID != 0)
                {
                    Response.Redirect(string.Format("~/Product.aspx?ProductID={0}", pID));
                }
            }
        }
    }
}