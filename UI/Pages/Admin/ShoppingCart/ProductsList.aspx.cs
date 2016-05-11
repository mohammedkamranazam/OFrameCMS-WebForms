using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.UI.WebControls;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class ProductsList : System.Web.UI.Page
    {
        private IQueryable<SC_Products> GetProductsQuery(ShoppingCartEntities context)
        {
            var subCategoryID = DataParser.IntParse(SubCategoriesDropDownList.SelectedValue);

            return (from products in context.SC_Products
                    where products.SubCategoryID == subCategoryID
                    select products);
        }

        private IQueryable<SC_Products> GetSearchQuery(ShoppingCartEntities context)
        {
            var searchTerm = SearchTermTextBox.Text;

            return (from products in context.SC_Products
                    where
                        (products.Title.Contains(searchTerm) ||
                        products.SubTitle.Contains(searchTerm) ||
                        products.Description.Contains(searchTerm) ||
                        products.Details.Contains(searchTerm) ||
                        products.Tags.Contains(searchTerm) ||
                        products.SC_Brands.Title.Contains(searchTerm) ||
                        products.ItemNumber.Contains(searchTerm)) &&
                        !(products.SC_Sections.Hide ||
                        products.SC_Categories.Hide ||
                        products.SC_SubCategories.Hide)
                    select products);
        }

        private string GetSortDirection(string column)
        {
            var sortDirection = "DESC";

            var sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    var lastDirection = ViewState["SortDirection"] as string;

                    if ((lastDirection != null) && (lastDirection == "DESC"))
                    {
                        sortDirection = "ASC";
                    }
                }
            }

            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        private void InitializeCategories(ShoppingCartEntities context)
        {
            var sectionID = DataParser.IntParse(SectionsDropDownList.SelectedValue);

            var categoriesQuery = (from categories in context.SC_Categories
                                   where categories.Hide == false && categories.SectionID == sectionID
                                   select categories);
            CategoriesDropDownList.DataTextField = "Title";
            CategoriesDropDownList.DataValueField = "CategoryID";
            CategoriesDropDownList.DataSource = categoriesQuery.ToList();
            CategoriesDropDownList.DataBind();
        }

        private void InitializeProducts(ShoppingCartEntities context)
        {
            GridView1.DataSource = GetProductsQuery(context).ToList();
            GridView1.DataBind();

            SearchTermTextBox.Text = string.Empty;
        }

        private void InitializeSections(ShoppingCartEntities context)
        {
            var sectionsQuery = (from sections in context.SC_Sections
                                 where sections.Hide == false
                                 select sections);
            SectionsDropDownList.DataTextField = "Title";
            SectionsDropDownList.DataValueField = "SectionID";
            SectionsDropDownList.DataSource = sectionsQuery.ToList();
            SectionsDropDownList.DataBind();
        }

        private void InitializeSubCategories(ShoppingCartEntities context)
        {
            var categoryID = DataParser.IntParse(CategoriesDropDownList.SelectedValue);

            var subCategoriesQuery = (from subCategories in context.SC_SubCategories
                                      where subCategories.Hide == false && subCategories.CategoryID == categoryID
                                      select subCategories);
            SubCategoriesDropDownList.DataTextField = "Title";
            SubCategoriesDropDownList.DataValueField = "SubCategoryID";
            SubCategoriesDropDownList.DataSource = subCategoriesQuery.ToList();
            SubCategoriesDropDownList.DataBind();

            if (SubCategoriesDropDownList.Items.Count == 0)
            {
                SubCategoriesDropDownList.Items.Insert(0, new ListItem("No Sub Categories Defined", "-1"));
            }
        }

        protected void CategoriesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                InitializeSubCategories(context);
                InitializeProducts(context);
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (string.IsNullOrWhiteSpace(SearchTermTextBox.Text))
                {
                    GridView1.DataSource = GetProductsQuery(context).ToList();
                    GridView1.PageIndex = e.NewPageIndex;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = GetSearchQuery(context).ToList();
                    GridView1.PageIndex = e.NewPageIndex;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (string.IsNullOrWhiteSpace(SearchTermTextBox.Text))
                {
                    GridView1.DataSource = GetProductsQuery(context).OrderBy(e.SortExpression + " " + GetSortDirection(e.SortExpression)).ToList();
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = GetSearchQuery(context).OrderBy(e.SortExpression + " " + GetSortDirection(e.SortExpression)).ToList();
                    GridView1.DataBind();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var context = new ShoppingCartEntities())
                {
                    InitializeSections(context);

                    InitializeCategories(context);

                    InitializeSubCategories(context);

                    InitializeProducts(context);
                }
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                GridView1.DataSource = GetSearchQuery(context).ToList();
                GridView1.DataBind();
            }
        }

        protected void SectionsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                InitializeCategories(context);
                InitializeSubCategories(context);
                InitializeProducts(context);
            }
        }

        protected void SubCategoriesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                InitializeProducts(context);
            }
        }
    }
}