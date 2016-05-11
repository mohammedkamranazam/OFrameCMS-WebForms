using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.Linq;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductHighlightsComponent : System.Web.UI.UserControl
    {
        public int Count
        {
            get
            {
                return (ViewState["Count"] == null) ? -1 : DataParser.IntParse(ViewState["Count"].ToString());
            }

            set
            {
                ViewState["Count"] = value.ToString();
            }
        }

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

                if (ShowHighlights)
                {
                    Render();
                }
            }
        }

        public bool ShowHighlights
        {
            get
            {
                return HighlightsRepeater.Visible;
            }

            set
            {
                HighlightsRepeater.Visible = value;
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
                var highlightsQuery = (from highlights in context.SC_Highlights
                                       where highlights.ProductID == ProductID
                                       select highlights);

                if (Count == -1)
                {
                    HighlightsRepeater.DataSource = highlightsQuery.ToList();
                }
                else
                {
                    HighlightsRepeater.DataSource = highlightsQuery.Take(Count).ToList();
                }

                HighlightsRepeater.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}