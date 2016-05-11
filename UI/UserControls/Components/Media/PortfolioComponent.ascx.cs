using OWDARO.BLL.MediaBLL;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Text;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Media
{
    public partial class PortfolioComponent : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string GetCategories(int portfolioID)
        {
            StringBuilder sb = new StringBuilder();

            using (var context = new MediaEntities())
            {
                var categories = PortfoliosBL.GetObjectByID(portfolioID, context).ME_PortfolioCategories;

                foreach (var category in categories)
                {
                    sb.Append(string.Format("{0}, ", category.ME_ProjectCategories.Title));
                }
            }

            if (sb.Length > 0)
            {
                var lastCommaIndex = sb.ToString().LastIndexOf(',');

                sb = sb.Remove(lastCommaIndex, 1);
            }

            return sb.ToString();
        }
    }
}