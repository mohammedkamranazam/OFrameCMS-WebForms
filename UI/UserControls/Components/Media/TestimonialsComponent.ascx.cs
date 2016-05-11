using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Media
{
    public partial class TestimonialsComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            using (var context = new MediaEntities())
            {
                var testimonialsQuery = await (from set in context.ME_Testimonials
                                               select set).ToListAsync();

                Repeater1.DataSource = testimonialsQuery;
                Repeater1.DataBind();

                if (!testimonialsQuery.Any())
                {
                    this.Visible = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }
        }
    }
}