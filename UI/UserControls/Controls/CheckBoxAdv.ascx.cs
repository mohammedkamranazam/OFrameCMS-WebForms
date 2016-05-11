using System;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Controls
{
    public partial class CheckBoxAdv : System.Web.UI.UserControl
    {
        public event EventHandler CheckedChanged;

        public bool AutoPostBack
        {
            set
            {
                CheckBox1.AutoPostBack = value;
            }
        }

        public CheckBox CheckBox
        {
            get
            {
                return CheckBox1;
            }

            set
            {
                CheckBox1 = value;
            }
        }

        public string CheckBoxCssClass
        {
            set
            {
                CheckBox1.Attributes.Add("class", value);
            }
        }

        public Unit CheckBoxHeight
        {
            set
            {
                CheckBox1.Height = value;
            }

            get
            {
                return CheckBox1.Height;
            }
        }

        public bool Checked
        {
            get
            {
                return CheckBox1.Checked;
            }

            set
            {
                CheckBox1.Checked = value;
            }
        }

        public string CheckedImageOverUrl
        {
            set
            {
                Extender1.CheckedImageOverUrl = value;
            }
        }

        public string CheckedImageUrl
        {
            set
            {
                Extender1.CheckedImageUrl = value;
            }
        }

        public string ContainerID
        {
            get
            {
                return Container.ClientID;
            }
        }

        public string DisabledCheckedImageUrl
        {
            set
            {
                Extender1.DisabledCheckedImageUrl = value;
            }
        }

        public string DisabledUncheckedImageUrl
        {
            set
            {
                Extender1.DisabledUncheckedImageUrl = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return CheckBox1.Enabled;
            }

            set
            {
                CheckBox1.Enabled = value;
            }
        }

        public Unit Height
        {
            get
            {
                return CheckBox1.Height;
            }

            set
            {
                CheckBox1.Height = value;
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

        public int ImageHeight
        {
            set
            {
                Extender1.ImageHeight = value;
            }
        }

        public int ImageWidth
        {
            set
            {
                Extender1.ImageWidth = value;
            }
        }

        public Literal Label
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

        public string OnClick
        {
            set
            {
                CheckBox1.Attributes.Add("onclick", value);
            }
        }

        public Literal SmallLabel
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
                return CheckBox1.Text;
            }

            set
            {
                CheckBox1.Text = value;
            }
        }

        public string UncheckedImageOverUrl
        {
            set
            {
                Extender1.UncheckedImageOverUrl = value;
            }
        }

        public string UncheckedImageUrl
        {
            set
            {
                Extender1.UncheckedImageUrl = value;
            }
        }

        private void OnCheckedChanged()
        {
            if (CheckedChanged != null)
            {
                CheckedChanged(this, EventArgs.Empty);
            }
        }

        public string Direction
        {
            set
            {
                CheckBox1.Style.Add("direction", value);

                DIV.Attributes.Remove("class");
                LABEL.Attributes.Remove("class");

                DIV.Attributes.Add("class", value);
                LABEL.Attributes.Add("class", value);
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Checked = CheckBox1.Checked;

            OnCheckedChanged();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}