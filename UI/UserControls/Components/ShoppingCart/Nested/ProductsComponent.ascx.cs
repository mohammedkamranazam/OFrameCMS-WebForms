using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductsComponent : System.Web.UI.UserControl
    {
        public int RepeatColumns
        {
            set
            {
                ProductsRepeater.RepeatColumns = value;
            }
        }

        private IEnumerable<SC_Products> GetProductsWithStock(IQueryable<SC_Products> products, ShoppingCartEntities context)
        {
            var productsWithStock = new List<SC_Products>();

            foreach (SC_Products product in (from product in products
                                             select product))
            {
                productsWithStock.Add(ProductsBL.SwitchShowInCart(product, context));
            }

            return productsWithStock.OrderByDescending(c => c.SoldOutCount).AsEnumerable<SC_Products>();
        }

        private void LoadProducts(ShoppingCartEntities context)
        {
            if (string.IsNullOrWhiteSpace(Request.QueryString["SectionID"]) && string.IsNullOrWhiteSpace(Request.QueryString["CategoryID"]) && string.IsNullOrWhiteSpace(Request.QueryString["SubCategoryID"]))
            {
                SelectProductsAll(context);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["SectionID"]) && (string.IsNullOrWhiteSpace(Request.QueryString["CategoryID"]) && string.IsNullOrWhiteSpace(Request.QueryString["SubCategoryID"])))
                {
                    SelectProductsBySection(context);
                }
                else
                {
                    if ((string.IsNullOrWhiteSpace(Request.QueryString["SectionID"]) && string.IsNullOrWhiteSpace(Request.QueryString["SubCategoryID"])) && !string.IsNullOrWhiteSpace(Request.QueryString["CategoryID"]))
                    {
                        SelectProductsByCategory(context);
                    }
                    else
                    {
                        if ((string.IsNullOrWhiteSpace(Request.QueryString["SectionID"]) && string.IsNullOrWhiteSpace(Request.QueryString["CategoryID"])) && !string.IsNullOrWhiteSpace(Request.QueryString["SubCategoryID"]))
                        {
                            SelectProductsBySubCategory(context);
                        }
                    }
                }
            }
        }

        private void SearchProductsAll(ShoppingCartEntities context)
        {
            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            var searchTerm = SearchTextBox.Text;

            var searchTermIsNumber = Regex.IsMatch(searchTerm, @"^\d+$");
            double searchTermPrice = 0;

            if (searchTermIsNumber)
            {
                searchTermPrice = DataParser.DoubleParse(searchTerm);
            }

            var productsQuery = (from set in context.SC_Products
                                 where
                                     set.Hide == false
                                     && (
                                     set.Title.Contains(searchTerm) ||
                                     set.SubTitle.Contains(searchTerm) ||
                                     set.Description.Contains(searchTerm) ||
                                     set.Details.Contains(searchTerm) ||
                                     set.Tags.Contains(searchTerm) ||
                                     set.SC_Brands.Title.Contains(searchTerm) ||
                                     set.ItemNumber.Contains(searchTerm) ||
                                     set.Price <= searchTermPrice
                                     ) &&
                                     !(set.SC_Sections.Hide == true || set.SC_Categories.Hide == true || set.SC_SubCategories.Hide == true)
                                 select set);

            ProductsRepeater.DataSource = productsQuery.OrderByDescending(c => c.SoldOutCount).Distinct().Take(toFetchCount).ToList();
            ProductsRepeater.DataBind();

            UpdateLoadMoreControls(toFetchCount, productsQuery.Count());
        }

        private void SelectProductsAll(ShoppingCartEntities context)
        {
            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;

            TitleLiteral.Text = "All Products";
            Page.Title = string.Format("{1} | {0}", AppConfig.SiteName, "All Products");

            var productsQuery = (from products in context.SC_Products
                                 where
                                     products.Hide == false &&
                                     products.ShowInCart == true &&
                                     !(products.SC_Sections.Hide == true || products.SC_Categories.Hide == true || products.SC_SubCategories.Hide == true)
                                 select products);

            var productsQueryTop = productsQuery.Take(toFetchCount);

            ProductsRepeater.DataSource = GetProductsWithStock(productsQueryTop, context);
            ProductsRepeater.DataBind();

            UpdateLoadMoreControls(toFetchCount, productsQuery.Count());
        }

        private void SelectProductsByCategory(ShoppingCartEntities context)
        {
            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            var categoryID = DataParser.IntParse(Request.QueryString["CategoryID"]);

            var categoryQuery = CategoriesBL.GetObjectByID(categoryID, context);

            if (categoryQuery != null)
            {
                TitleLiteral.Text = categoryQuery.Title;
                Page.Title = string.Format("{1} | {0}", AppConfig.SiteName, categoryQuery.Title);
                Page.MetaDescription = categoryQuery.Description;
                Page.MetaKeywords = categoryQuery.Title;
            }

            var productsQuery = (from products in context.SC_Products
                                 where
                                     products.CategoryID == categoryID &&
                                     products.Hide == false && products.ShowInCart == true &&
                                     !(products.SC_Sections.Hide == true || products.SC_Categories.Hide == true || products.SC_SubCategories.Hide == true)
                                 select products);

            var productsQueryTop = productsQuery.Take(toFetchCount);

            ProductsRepeater.DataSource = GetProductsWithStock(productsQueryTop, context);
            ProductsRepeater.DataBind();

            UpdateLoadMoreControls(toFetchCount, productsQuery.Count());
        }

        private void SelectProductsBySection(ShoppingCartEntities context)
        {
            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            var sectionID = DataParser.IntParse(Request.QueryString["SectionID"]);

            var sectionQuery = SectionsBL.GetObjectByID(sectionID, context);

            if (sectionQuery != null)
            {
                TitleLiteral.Text = sectionQuery.Title;

                var seoEntity = new SEO();
                seoEntity.Title = sectionQuery.Title;
                seoEntity.Description = sectionQuery.Description;
                seoEntity.Keywords = sectionQuery.AlternateText;

                Utilities.SetPageSEO(Page, seoEntity);
            }

            var productsQuery = (from products in context.SC_Products
                                 where
                                     products.SectionID == sectionID &&
                                     products.Hide == false &&
                                     products.ShowInCart == true &&
                                     !(products.SC_Sections.Hide == true || products.SC_Categories.Hide == true || products.SC_SubCategories.Hide == true)
                                 select products);

            var productsQueryTop = productsQuery.Take(toFetchCount);

            ProductsRepeater.DataSource = GetProductsWithStock(productsQueryTop, context);
            ProductsRepeater.DataBind();

            UpdateLoadMoreControls(toFetchCount, productsQuery.Count());
        }

        private void SelectProductsBySubCategory(ShoppingCartEntities context)
        {
            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            var subCategoryID = DataParser.IntParse(Request.QueryString["SubCategoryID"]);

            var subCategoryQuery = SubCategoriesBL.GetObjectByID(subCategoryID, context);

            if (subCategoryQuery != null)
            {
                TitleLiteral.Text = subCategoryQuery.Title;
                Page.Title = string.Format("{1} | {0}", AppConfig.SiteName, subCategoryQuery.Title);
                Page.MetaDescription = subCategoryQuery.Description;
                Page.MetaKeywords = subCategoryQuery.Title;
            }

            var productsQuery = (from products in context.SC_Products
                                 where
                                 products.SubCategoryID == subCategoryID &&
                                 products.Hide == false && products.ShowInCart == true &&
                                 !(products.SC_Sections.Hide == true || products.SC_Categories.Hide == true || products.SC_SubCategories.Hide == true)
                                 select products);

            var productsQueryTop = productsQuery.Take(toFetchCount);

            ProductsRepeater.DataSource = GetProductsWithStock(productsQueryTop, context);
            ProductsRepeater.DataBind();

            UpdateLoadMoreControls(toFetchCount, productsQuery.Count());
        }

        private void SetVisibility()
        {
            if (ProductsRepeater.Items.Count > 0)
            {
                ProductsPanel.Visible = true;
                EmptyDataPanel.Visible = false;
            }
            else
            {
                ProductsPanel.Visible = false;
                EmptyDataPanel.Visible = true;
            }
        }

        private void UpdateLoadMoreControls(int toFetchCount, int totalProductsCount)
        {
            CurrentCountHiddenField.Value = toFetchCount.ToString();

            if (totalProductsCount == 0)
            {
                LoadMoreButton.Visible = false;
            }

            if (toFetchCount >= totalProductsCount)
            {
                LoadMoreButton.Enabled = false;
                LoadMoreButton.CssClass = "LoadMoreButtonDisabled";
                LoadMoreButton.Text = LanguageHelper.GetKey("NoMoreItemsToDisplay");
            }

            SetVisibility();
        }

        protected void LoadMoreButton_Click(object sender, EventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                LoadProducts(context);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var context = new ShoppingCartEntities())
                {
                    LoadProducts(context);

                    if (!string.IsNullOrWhiteSpace(Request.QueryString["SearchTerm"]))
                    {
                        SearchTextBox.Text = Request.QueryString["SearchTerm"];
                        SearchProductsAll(context);
                    }
                }
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                SearchProductsAll(context);
            }
        }
    }
}