using OWDARO.Settings;
using System;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Controls
{
    public partial class CountryDropDown : System.Web.UI.UserControl
    {
        public event EventHandler SelectedIndexChanged;

        public bool AutoPostBack
        {
            get
            {
                return DropDownList1.AutoPostBack;
            }

            set
            {
                DropDownList1.AutoPostBack = value;
            }
        }

        public ListItem Country
        {
            get
            {
                return DropDownList1.SelectedItem;
            }

            set
            {
                DropDownList1.SelectedValue = value.Value;
            }
        }

        public object DataSource
        {
            get
            {
                return DropDownList1.DataSource;
            }

            set
            {
                DropDownList1.DataSource = value;
            }
        }

        public string DataTextField
        {
            get
            {
                return DropDownList1.DataTextField;
            }

            set
            {
                DropDownList1.DataTextField = value;
            }
        }

        public string DataTextFormatString
        {
            get
            {
                return DropDownList1.DataTextFormatString;
            }

            set
            {
                DropDownList1.DataTextFormatString = value;
            }
        }

        public string DataValueField
        {
            get
            {
                return DropDownList1.DataValueField;
            }

            set
            {
                DropDownList1.DataValueField = value;
            }
        }

        public DropDownList DropDownList
        {
            get
            {
                return DropDownList1.DropDownList;
            }

            set
            {
                DropDownList1.DropDownList = value;
            }
        }

        public string InitialValue
        {
            get
            {
                return DropDownList1.InitialValue;
            }

            set
            {
                DropDownList1.InitialValue = value;
            }
        }

        public ListItemCollection Items
        {
            get
            {
                return DropDownList1.Items;
            }
        }

        public Literal Label
        {
            get
            {
                return DropDownList1.Label;
            }

            set
            {
                DropDownList1.Label = value;
            }
        }

        public string LabelText
        {
            get
            {
                return DropDownList1.LabelText;
            }

            set
            {
                DropDownList1.LabelText = value;
            }
        }

        public string RequiredFieldErrorMessage
        {
            get
            {
                return DropDownList1.RequiredFieldErrorMessage;
            }

            set
            {
                DropDownList1.RequiredFieldErrorMessage = value;
            }
        }

        public ListItem SelectedItem
        {
            get
            {
                return DropDownList1.SelectedItem;
            }
        }

        public string SelectedText
        {
            get
            {
                return DropDownList1.SelectedText;
            }

            set
            {
                DropDownList1.SelectedText = value;
            }
        }

        public string SelectedValue
        {
            get
            {
                return DropDownList1.SelectedValue;
            }

            set
            {
                DropDownList1.SelectedValue = value;
            }
        }

        public Literal SmallLabel
        {
            get
            {
                return DropDownList1.SmallLabel;
            }

            set
            {
                DropDownList1.SmallLabel = value;
            }
        }

        public string SmallLabelText
        {
            get
            {
                return DropDownList1.SmallLabelText;
            }

            set
            {
                DropDownList1.SmallLabelText = value;
            }
        }

        public string ValidationGroup
        {
            set
            {
                DropDownList1.ValidationGroup = value;
            }
        }

        private void Initialize()
        {
            var separators = new char[] { ';' };
            var countries = KeywordsHelper.GetKeywordValue("Countries").Split(separators, StringSplitOptions.RemoveEmptyEntries);
            DropDownList1.DataSource = countries;

            if (!string.IsNullOrWhiteSpace(SelectedText))
            {
                DropDownList1.SelectedText = SelectedText;
            }

            if (!string.IsNullOrWhiteSpace(SelectedValue))
            {
                DropDownList1.SelectedValue = SelectedValue;
            }
        }

        private void OnSelectedIndexChanged()
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        public string Direction
        {
            set
            {
                DropDownList1.Direction = value;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && DataSource == null)
            {
                Initialize();
            }

            DropDownList1.SelectedIndexChanged += new EventHandler(DropDownList1_SelectedIndexChanged);
        }
    }
}