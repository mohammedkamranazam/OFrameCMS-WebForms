using OWDARO.BLL.MediaBLL;
using OWDARO.Settings;
using OWDARO.Util;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Others
{
    public partial class ContactUsDetailsComponent : System.Web.UI.UserControl
    {
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

        private async Task LoadData()
        {
            var postQuery = await PostsBL.GetObjectByIDAsync(DataParser.IntParse(KeywordsHelper.GetKeywordValue("ContactUsPostID")));

            if (postQuery != null)
            {
                string locale = postQuery.Locale;
                string direction = LanguageHelper.GetLocaleDirection(locale);

                TitleH1.Style.Add("direction", direction);
                TitleLiteral.Text = postQuery.Title;

                PostEmbedComponent1.PostContent = postQuery.PostContent;
            }
        }
    }
}