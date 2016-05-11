using System;

namespace OWDARO.UI.UserControls.Controls
{
    public partial class StatusMessageJQuery : System.Web.UI.UserControl
    {
        public string Message
        {
            set
            {
                StatusMessageLabel.Text = value;
                StatusMessagePanel.Visible = true;
            }
        }

        public StatusMessageType MessageType
        {
            set
            {
                switch (value)
                {
                    case StatusMessageType.Error:
                        StatusMessagePanel.CssClass += " error";
                        break;

                    case StatusMessageType.Info:
                        StatusMessagePanel.CssClass += " info";
                        break;

                    case StatusMessageType.Warning:
                        StatusMessagePanel.CssClass += " warning";
                        break;

                    case StatusMessageType.Success:
                        StatusMessagePanel.CssClass += " success";
                        break;

                    default:
                        StatusMessagePanel.CssClass += " info";
                        break;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                StatusMessagePanel.Visible = false;
                StatusMessagePanel.CssClass = "alertMessage";
            }
        }
    }
}