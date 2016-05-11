using System;

namespace OWDARO.UI.UserControls.Components.Utility
{
    public partial class WebCamComponent : System.Web.UI.UserControl
    {
        public string ImageBase64
        {
            get
            {
                return WebCamAppImage64StrHiddenField.Value;
            }

            set
            {
                WebCamAppImage64StrHiddenField.Value = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}