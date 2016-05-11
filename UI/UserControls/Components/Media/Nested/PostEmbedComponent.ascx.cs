using OWDARO.BLL.MediaBLL;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.Media.Nested
{
    public partial class PostEmbedComponent : System.Web.UI.UserControl
    {


        public string PostContent
        {
            get
            {
                return PostContentTextBox.Text;
            }

            set
            {
                PostContentTextBox.Text = value;
            }
        }

        public int? PostID
        {
            get
            {
                return PostIDHiddenField.Value.NullableIntParse();
            }

            set
            {
                PostIDHiddenField.Value = value.ToString();
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

        private async Task LoadData()
        {
            if (PostID != null)
            {
                var post = await PostsBL.GetObjectByIDAsync((int)PostID);

                if (post != null)
                {
                    PostContent = post.PostContent;
                }
            }

            using (var context = new GalleryEntities())
            {
                PostEmbedLiteral.Text = await Utilities.GetHyperHTMLAsync(StringHelper.RemoveTruncator(PostContent), context, Page);

                PostContent = string.Empty;
            }
        }
    }
}