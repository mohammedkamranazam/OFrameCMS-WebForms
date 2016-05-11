using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.Linq;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductCategoriesComponent : System.Web.UI.UserControl
    {
        public string CategoriesULCssClass
        {
            get
            {
                return CategoriesULCssClassHiddenField.Value;
            }

            set
            {
                CategoriesULCssClassHiddenField.Value = value;
            }
        }

        public bool CategoriesVisible
        {
            get
            {
                return CategoriesRepeater.Visible;
            }

            set
            {
                CategoriesRepeater.Visible = value;
                SubCategoriesVisible = value;
                CategoriesUL.Visible = value;
            }
        }

        public string CategoryAnchroCssClass
        {
            get
            {
                return CategoryAnchorCssClassHiddenField.Value;
            }

            set
            {
                CategoryAnchorCssClassHiddenField.Value = value;
            }
        }

        public string CategoryLICssClass
        {
            get
            {
                return CategoryLICssClassHiddenField.Value;
            }

            set
            {
                CategoryLICssClassHiddenField.Value = value;
            }
        }

        public string CategoryLISelectedCssClass
        {
            get
            {
                return CategoryLISelectedCssClassHiddenField.Value;
            }

            set
            {
                CategoryLISelectedCssClassHiddenField.Value = value;
            }
        }

        public int SectionID
        {
            get
            {
                return (ViewState["SectionID"] == null) ? 0 : DataParser.IntParse(ViewState["SectionID"].ToString());
            }

            set
            {
                ViewState["SectionID"] = value.ToString();
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
                return DataParser.BoolParse(SubCategoriesVisibleHiddenField.Value);
            }

            set
            {
                SubCategoriesVisibleHiddenField.Value = value.ToString();
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
                var categoriesQuery = (from categories in context.SC_Categories
                                       where categories.SectionID == SectionID && categories.Hide == false
                                       select categories);

                CategoriesRepeater.DataSource = categoriesQuery.ToList();
                CategoriesRepeater.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Render();
        }
    }
}