using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.Linq;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductSubCategoriesComponent : System.Web.UI.UserControl
    {
        public int CategoryID
        {
            get
            {
                return (ViewState["CategoryID"] == null) ? 0 : DataParser.IntParse(ViewState["CategoryID"].ToString());
            }

            set
            {
                ViewState["CategoryID"] = value.ToString();
            }
        }

        public string SubCategoriesULCssClass
        {
            get
            {
                return SubCategoriesULCssClassHiddenField.Value;
            }

            set
            {
                SubCategoriesULCssClassHiddenField.Value = value;
            }
        }

        public bool SubCategoriesVisible
        {
            get
            {
                return SubCategoriesRepeater.Visible;
            }

            set
            {
                SubCategoriesRepeater.Visible = value;
                SubCategoriesUL.Visible = value;
            }
        }

        public string SubCategoryAnchroCssClass
        {
            get
            {
                return SubCategoryAnchorCssClassHiddenField.Value;
            }

            set
            {
                SubCategoryAnchorCssClassHiddenField.Value = value;
            }
        }

        public string SubCategoryLICssClass
        {
            get
            {
                return SubCategoryLICssClassHiddenField.Value;
            }

            set
            {
                SubCategoryLICssClassHiddenField.Value = value;
            }
        }

        public string SubCategoryLISelectedCssClass
        {
            get
            {
                return SubCategoryLISelectedCssClassHiddenField.Value;
            }

            set
            {
                SubCategoryLISelectedCssClassHiddenField.Value = value;
            }
        }

        private void Render()
        {
            if (!Visible)
            {
                return;
            }

            using (var context = new ShoppingCartEntities())
            {
                Render(context);
            }
        }

        private void Render(ShoppingCartEntities context)
        {
            if (!Visible)
            {
                return;
            }

            if (!IsPostBack)
            {
                var subCategoriesQuery = (from subCategories in context.SC_SubCategories
                                          where subCategories.CategoryID == CategoryID && subCategories.Hide == false
                                          select subCategories);

                SubCategoriesRepeater.DataSource = subCategoriesQuery.ToList();
                SubCategoriesRepeater.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Render();
        }
    }
}