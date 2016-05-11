using System;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Controls
{
    public partial class RadioButtonAdv : System.Web.UI.UserControl
    {
        public bool Checked
        {
            get
            {
                return RadioButton1.Checked;
            }

            set
            {
                RadioButton1.Checked = value;
            }
        }

        public string ContainerID
        {
            get
            {
                return Container.ClientID;
            }
        }

        public string GroupName
        {
            get
            {
                return RadioButton1.GroupName;
            }

            set
            {
                RadioButton1.GroupName = value;
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

        public Label Label
        {
            get
            {
                return Label1;
            }

            set
            {
                Label1 = value;
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

        public RadioButton RadioButton
        {
            get
            {
                return RadioButton1;
            }

            set
            {
                RadioButton1 = value;
            }
        }

        public Label SmallLabel
        {
            get
            {
                return SmallLabel1;
            }

            set
            {
                SmallLabel1 = value;
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
                return RadioButton1.Text;
            }

            set
            {
                RadioButton1.Text = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}