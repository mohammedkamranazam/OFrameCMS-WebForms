using OWDARO.Util;
using System;

namespace ProjectJKL.UI.UserControls.Components.Others
{
    public partial class ImageSelectorComponent : System.Web.UI.UserControl
    {
        public int? ImageID
        {
            get
            {
                return _ImageIDHiddenField__.Value.NullableIntParse();
            }

            set
            {
                _ImageIDHiddenField__.Value = value.ToString();
            }
        }

        public string StoragePath
        {
            get
            {
                return StoragePathHiddenField.Value;
            }

            set
            {
                StoragePathHiddenField.Value = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _ImageSelectorImage__.ImageUrl = Utilities.GetImageThumbURL(ImageID);
        }
    }
}