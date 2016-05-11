using System;
using System.Web.UI.WebControls;

namespace ProjectJKL.UI.UserControls.Controls
{
    public partial class UnsafeTextBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string ContainerID
        {
            get
            {
                return Container.ClientID;
            }
        }

        public Unit EditorHeight
        {
            set
            {
                Editor1.Height = value;
            }
        }

        public string InitialValue
        {
            get
            {
                return RequiredFieldValidator1.InitialValue;
            }

            set
            {
                RequiredFieldValidator1.InitialValue = value;
                RequiredFieldValidator1.Visible = true;
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

        public string RequiredErrorMessage
        {
            get
            {
                return RequiredFieldValidator1.ErrorMessage;
            }

            set
            {
                RequiredFieldValidator1.ErrorMessage = value;
                RequiredFieldValidator1.Visible = true;
            }
        }

        public RequiredFieldValidator RequiredFieldValidator
        {
            get
            {
                return RequiredFieldValidator1;
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
                return Editor1.Text;
            }

            set
            {
                Editor1.Text = value;
            }
        }

        public string ValidationGroup
        {
            set
            {
                RequiredFieldValidator1.ValidationGroup = value;
                Editor1.ValidationGroup = value;
            }
        }
    }
}