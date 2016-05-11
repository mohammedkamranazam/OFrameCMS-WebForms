using AjaxControlToolkit;
using OWDARO.Util;
using System;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Controls
{
    public partial class Slider : System.Web.UI.UserControl
    {
        public string ContainerID
        {
            get
            {
                return Container.ClientID;
            }
        }

        public int Decimals
        {
            get
            {
                return TextBox1_SliderExtender.Decimals;
            }

            set
            {
                TextBox1_SliderExtender.Decimals = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return TextBox1_SliderExtender.Enabled;
            }

            set
            {
                TextBox1_SliderExtender.Enabled = value;
            }
        }

        public string HandleCssClass
        {
            get
            {
                return TextBox1_SliderExtender.HandleCssClass;
            }

            set
            {
                TextBox1_SliderExtender.HandleCssClass = value;
            }
        }

        public string HandleImageURL
        {
            get
            {
                return TextBox1_SliderExtender.HandleImageUrl;
            }

            set
            {
                TextBox1_SliderExtender.HandleImageUrl = value;
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

        public int Length
        {
            get
            {
                return TextBox1_SliderExtender.Length;
            }

            set
            {
                TextBox1_SliderExtender.Length = value;
            }
        }

        public double Maximum
        {
            get
            {
                return TextBox1_SliderExtender.Maximum;
            }

            set
            {
                TextBox1_SliderExtender.Maximum = value;
            }
        }

        public double Minimum
        {
            get
            {
                return TextBox1_SliderExtender.Minimum;
            }

            set
            {
                TextBox1_SliderExtender.Minimum = value;
            }
        }

        public SliderOrientation Orientation
        {
            get
            {
                return TextBox1_SliderExtender.Orientation;
            }

            set
            {
                TextBox1_SliderExtender.Orientation = value;
            }
        }

        public string RailCssClass
        {
            get
            {
                return TextBox1_SliderExtender.RailCssClass;
            }

            set
            {
                TextBox1_SliderExtender.RailCssClass = value;
            }
        }

        public bool RaiseChangeOnlyOnMouseUp
        {
            get
            {
                return TextBox1_SliderExtender.RaiseChangeOnlyOnMouseUp;
            }

            set
            {
                TextBox1_SliderExtender.RaiseChangeOnlyOnMouseUp = value;
            }
        }

        public string ScriptPath
        {
            get
            {
                return TextBox1_SliderExtender.ScriptPath;
            }

            set
            {
                TextBox1_SliderExtender.ScriptPath = value;
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

        public int Steps
        {
            get
            {
                return TextBox1_SliderExtender.Steps;
            }

            set
            {
                TextBox1_SliderExtender.Steps = value;
            }
        }

        public string TooltipText
        {
            get
            {
                return TextBox1_SliderExtender.TooltipText;
            }

            set
            {
                TextBox1_SliderExtender.TooltipText = value;
            }
        }

        public ValidateRequestMode SliderValidateRequestMode
        {
            get
            {
                return TextBox1_SliderExtender.ValidateRequestMode;
            }

            set
            {
                TextBox1_SliderExtender.ValidateRequestMode = value;
            }
        }

        public double Value
        {
            get
            {
                return TextBox1.Text.DoubleParse();
            }

            set
            {
                TextBox1.Text = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}