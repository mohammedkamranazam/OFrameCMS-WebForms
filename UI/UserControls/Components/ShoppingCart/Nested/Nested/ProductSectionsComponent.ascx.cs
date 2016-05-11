using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.Linq;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductSectionsComponent : System.Web.UI.UserControl
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
                return DataParser.BoolParse(CategoriesVisibleHiddenField.Value);
            }

            set
            {
                CategoriesVisibleHiddenField.Value = value.ToString();
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

        public string SectionAnchroCssClass
        {
            get
            {
                return SectionAnchorCssClassHiddenField.Value;
            }

            set
            {
                SectionAnchorCssClassHiddenField.Value = value;
            }
        }

        public string SectionLICssClass
        {
            get
            {
                return SectionLICssClassHiddenField.Value;
            }

            set
            {
                SectionLICssClassHiddenField.Value = value;
            }
        }

        public string SectionLISelectedCssClass
        {
            get
            {
                return SectionLISelectedCssClassHiddenField.Value;
            }

            set
            {
                SectionLISelectedCssClassHiddenField.Value = value;
            }
        }

        public string SectionsULCssClass
        {
            get
            {
                return SectionsULCssClassHiddenField.Value;
            }

            set
            {
                SectionsULCssClassHiddenField.Value = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var context = new ShoppingCartEntities())
                {
                    var sectionsQuery = (from sections in context.SC_Sections
                                         where sections.Hide == false
                                         select sections);

                    SectionsRepeater.DataSource = sectionsQuery.ToList();
                    SectionsRepeater.DataBind();
                }
            }
        }
    }
}