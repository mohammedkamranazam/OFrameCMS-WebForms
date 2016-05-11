using System;
using System.Web.UI.WebControls;

namespace ProjectJKL.UI.UserControls.Controls
{
    public partial class LabelAdv : System.Web.UI.UserControl
    {
        public string ContainerID
        {
            get
            {
                return Container.ClientID;
            }
        }

        public string Direction
        {
            set
            {
                LiteralParaTag.Style.Add("direction", value);

                DIV.Attributes.Remove("class");
                LABEL.Attributes.Remove("class");

                DIV.Attributes.Add("class", value);
                LABEL.Attributes.Add("class", value);
            }
        }

        public string HelpLabelText
        {
            get
            {
                return HelpLabel1.Text;
            }

            set
            {
                HelpLabel1.Text = value;
            }
        }

        public string LabelText
        {
            get
            {
                return Label1.Text;
            }

            set
            {
                Label1.Text = value;
            }
        }

        public LiteralMode Mode
        {
            get
            {
                return Literal1.Mode;
            }

            set
            {
                Literal1.Mode = value;
            }
        }

        public string SmallLabelText
        {
            get
            {
                return SmallLabel1.Text;
            }

            set
            {
                SmallLabel1.Text = value;
            }
        }

        public string Text
        {
            get
            {
                return Literal1.Text;
            }

            set
            {
                Literal1.Text = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}