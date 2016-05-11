using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.Util;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace ProjectJKL.UI.UserControls.Components.Others
{
    public partial class FileSelectorComponent : System.Web.UI.UserControl
    {
        public string Locale
        {
            set
            {
                _LocaleHiddenField__.Value = value;
            }

            get
            {
                return _LocaleHiddenField__.Value;
            }
        }

        public long? FileID
        {
            get
            {
                return _FileIDHiddenField__.Value.NullableIntParse();
            }

            set
            {
                _FileIDHiddenField__.Value = value.ToString();
            }
        }

        private async Task LoadData()
        {
            if (FileID != null)
            {
                var fileQuery = await FilesBL.GetObjectByIDAsync((long)FileID);

                _FileSelectorImage__.ImageUrl = Utilities.GetImageThumbURL(fileQuery.ImageID);
                _TitleLabel__.Text = fileQuery.Title;
            }
            else
            {
                _FileSelectorImage__.ImageUrl = Utilities.GetImageThumbURL((int?)null);
                _TitleLabel__.Text = "No File Selected";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _NoImagePathHiddenField__.Value = string.Format("/Themes/{0}/Graphics/nullImage.png", AppConfig.ZiceTheme);
            }

            this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
            {
                await LoadData();
            }));
        }
    }
}