using System;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Controls
{
    public partial class DropDownListAdv : System.Web.UI.UserControl
    {
        public event EventHandler SelectedIndexChanged;

        public string AddDropDownCssClass
        {
            set
            {
                value = " " + value;
                DropDownList1.CssClass += value;
            }
        }

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

        public string ClientValidationFunction
        {
            get
            {
                return CustomValidator1.ClientValidationFunction;
            }

            set
            {
                CustomValidator1.ClientValidationFunction = value;
            }
        }

        public string CompareErrorMessage
        {
            set
            {
                CompareValidator1.ErrorMessage = value;
            }
        }

        public ValidationCompareOperator CompareOperator
        {
            get
            {
                return CompareValidator1.Operator;
            }

            set
            {
                CompareValidator1.Operator = value;
            }
        }

        public ValidationDataType CompareType
        {
            get
            {
                return CompareValidator1.Type;
            }

            set
            {
                CompareValidator1.Type = value;
            }
        }

        public CompareValidator CompareValidator
        {
            get
            {
                return CompareValidator1;
            }
        }

        public string ContainerID
        {
            get
            {
                return Container.ClientID;
            }
        }

        public string ControlToCompare
        {
            set
            {
                CompareValidator1.ControlToValidate = value + "$DropDownList1";
            }
        }

        public bool CultureInvariantComparison
        {
            get
            {
                return CompareValidator1.CultureInvariantValues;
            }

            set
            {
                CompareValidator1.CultureInvariantValues = value;
            }
        }

        public string CustomErrorMessage
        {
            get
            {
                return CustomValidator1.ErrorMessage;
            }

            set
            {
                CustomValidator1.ErrorMessage = value;
                CustomValidator1.Visible = true;
            }
        }

        public CustomValidator CustomValidator
        {
            get
            {
                return CustomValidator1;
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
                DropDownList1.Items.Clear();
                DropDownList1.DataSource = value;
                DropDownList.DataTextField = DataTextField;
                DropDownList.DataValueField = DataValueField;
                DropDownList1.DataBind();
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

        public string Direction
        {
            set
            {
                DropDownList1.Style.Add("direction", value);

                DIV.Attributes.Remove("class");
                LABEL.Attributes.Remove("class");

                DIV.Attributes.Add("class", value);
                LABEL.Attributes.Add("class", value);
            }
        }

        public DropDownList DropDownList
        {
            get
            {
                return DropDownList1;
            }

            set
            {
                DropDownList1 = value;
            }
        }

        public FieldWidth FieldWidth
        {
            set
            {
                switch (value)
                {
                    case FieldWidth.xxsmall:
                        AddDropDownCssClass = "xxsmall";
                        break;

                    case FieldWidth.xsmall:
                        AddDropDownCssClass = "xsmall";
                        break;

                    case FieldWidth.small:
                        AddDropDownCssClass = "small";
                        break;

                    case FieldWidth.medium:
                        AddDropDownCssClass = "medium";
                        break;

                    case FieldWidth.large:
                        AddDropDownCssClass = "large";
                        break;

                    case FieldWidth.largeXL:
                        AddDropDownCssClass = "largeXL";
                        break;

                    case FieldWidth.full:
                        AddDropDownCssClass = "full";
                        break;

                    default:
                        AddDropDownCssClass = "medium";
                        break;
                }
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

        public string OnChange
        {
            set
            {
                DropDownList1.Attributes.Add("onchange", value);
            }
        }

        public string RequiredFieldErrorMessage
        {
            get
            {
                return RequiredFieldValidator1.ErrorMessage;
            }

            set
            {
                RequiredFieldValidator1.ErrorMessage = value;
                RequiredFieldValidator1.Visible = true;
                AddDropDownCssClass = "required";
            }
        }

        public RequiredFieldValidator RequiredFieldValidator
        {
            get
            {
                return RequiredFieldValidator1;
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
                return DropDownList1.SelectedItem.Text;
            }

            set
            {
                DropDownList1.ClearSelection();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var item = DropDownList1.Items.FindByText(value);

                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        public string SelectedValue
        {
            get
            {
                return DropDownList1.SelectedItem.Value;
            }

            set
            {
                DropDownList1.ClearSelection();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var item = DropDownList1.Items.FindByValue(value);

                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
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

        public string ValidationGroup
        {
            set
            {
                DropDownList1.ValidationGroup = value;
                RequiredFieldValidator1.ValidationGroup = value;
                CustomValidator1.ValidationGroup = value;
                CompareValidator1.ValidationGroup = value;
            }
        }

        public string ValueToCompare
        {
            get
            {
                return CompareValidator1.ValueToCompare;
            }

            set
            {
                CompareValidator1.ValueToCompare = value;
            }
        }

        private void OnSelectedIndexChanged()
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void ClearSelection()
        {
            DropDownList1.ClearSelection();
        }
    }
}