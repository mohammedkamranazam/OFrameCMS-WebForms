using AjaxControlToolkit;
using System;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Controls
{
    public partial class TextBoxAdv : System.Web.UI.UserControl
    {
        public event EventHandler TextChanged;

        // private properties...
        private Literal Label
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

        private Literal SmallLabel
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

        private TextBox TextBox
        {
            get
            {
                return TextBox1;
            }

            set
            {
                TextBox1 = value;
            }
        }

        public string AddTextBoxCssClass
        {
            set
            {
                value = " " + value;
                TextBox1.CssClass += value;
            }
        }

        public string AddWatermarkCssClass
        {
            set
            {
                value = " " + value;
                WaterMarkExtenderPanel.CssClass += value;
            }
        }

        public AutoCompleteType AutoCompleteType
        {
            set
            {
                TextBox1.AutoCompleteType = value;
            }
        }

        public bool AutoPostBack
        {
            get
            {
                return TextBox1.AutoPostBack;
            }

            set
            {
                TextBox1.AutoPostBack = value;
            }
        }

        public CalendarDefaultView CalendarDefaultView
        {
            get
            {
                return CalendarExtender1.DefaultView;
            }

            set
            {
                CalendarExtender1.DefaultView = value;
                CalendarExtenderPanel.Visible = true;
            }
        }

        public string CalendarExtenderCssClass
        {
            set
            {
                CalendarExtender1.CssClass = value;
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

        public string CommitProperty
        {
            set
            {
                PopupControlExtender1.CommitProperty = value;
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
                CompareValidator1.ControlToValidate = value + "$TextBox1";
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

        public bool CultureInvariantRange
        {
            get
            {
                return RangeValidator1.CultureInvariantValues;
            }

            set
            {
                RangeValidator1.CultureInvariantValues = value;
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

        public string DateFormat
        {
            get
            {
                return CalendarExtender1.TodaysDateFormat;
            }

            set
            {
                CalendarExtender1.TodaysDateFormat = value;
                CalendarExtenderPanel.Visible = true;
            }
        }

        public string Direction
        {
            set
            {
                TextBox1.Style.Add("direction", value);

                DIV.Attributes.Remove("class");
                LABEL.Attributes.Remove("class");

                DIV.Attributes.Add("class", value);
                LABEL.Attributes.Add("class", value);
            }
        }

        public bool Enabled
        {
            get
            {
                return TextBox1.Enabled;
            }

            set
            {
                TextBox1.Enabled = value;
            }
        }

        public bool EnablePopUp
        {
            set
            {
                if (value)
                {
                    PopupControlExtender1.Enabled = true;
                    PopUpControlExtenderPanel.Visible = true;
                    PopUpPanel.Visible = true;
                }
                else
                {
                    PopupControlExtender1.Enabled = false;
                    PopUpControlExtenderPanel.Visible = false;
                    PopUpPanel.Visible = false;
                }
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return CalendarExtender1.EndDate;
            }

            set
            {
                CalendarExtender1.EndDate = value;
                CalendarExtenderPanel.Visible = true;
            }
        }

        public FieldWidth FieldWidth
        {
            set
            {
                switch (value)
                {
                    case FieldWidth.xxsmall:
                        TextBox1.CssClass = "xxsmall";
                        break;

                    case FieldWidth.xsmall:
                        TextBox1.CssClass = "xsmall";
                        break;

                    case FieldWidth.small:
                        TextBox1.CssClass = "small";
                        break;

                    case FieldWidth.medium:
                        TextBox1.CssClass = "medium";
                        break;

                    case FieldWidth.large:
                        TextBox1.CssClass = "large";
                        break;

                    case FieldWidth.largeXL:
                        TextBox1.CssClass = "largeXL";
                        break;

                    case FieldWidth.full:
                        TextBox1.CssClass = "full";
                        break;

                    default:
                        TextBox1.CssClass = "full";
                        break;
                }
            }
        }

        public FilterModes FilterMode
        {
            get
            {
                return FilteredTextBoxExtender1.FilterMode;
            }

            set
            {
                FilteredTextBoxExtender1.FilterMode = value;
                FilteredTextBoxExtenderPanel.Visible = true;
            }
        }

        public FilterTypes FilterType
        {
            get
            {
                return FilteredTextBoxExtender1.FilterType;
            }

            set
            {
                FilteredTextBoxExtender1.FilterType = value;
                FilteredTextBoxExtenderPanel.Visible = true;
            }
        }

        public string Format
        {
            get
            {
                return CalendarExtender1.Format;
            }

            set
            {
                CalendarExtender1.Format = value;
                CalendarExtenderPanel.Visible = true;
            }
        }

        public string HelpLabelText
        {
            get
            {
                return HelpLabelLiteral.Text;
            }

            set
            {
                HelpLabelLiteral.Text = value;
                HelpLabelSpan.Visible = true;
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

        public string InvalidChars
        {
            get
            {
                return FilteredTextBoxExtender1.InvalidChars;
            }

            set
            {
                FilteredTextBoxExtender1.InvalidChars = value;
                FilteredTextBoxExtenderPanel.Visible = true;
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

        public MaskedEditValidator MaskedEditValidator
        {
            get
            {
                return MaskedEditValidator;
            }
        }

        public int MaxLength
        {
            set
            {
                TextBox1.Attributes.Add("maxlength", value.ToString());
            }
        }

        public string MaxValue
        {
            get
            {
                return RangeValidator1.MaximumValue;
            }

            set
            {
                RangeValidator1.MaximumValue = value;
                RangeValidator1.Visible = true;
            }
        }

        public bool MEAcceptAMPM
        {
            set
            {
                TextBox1_MaskedEditExtender.AcceptAMPM = value;
            }
        }

        public MaskedEditShowSymbol MEAcceptNegative
        {
            set
            {
                TextBox1_MaskedEditExtender.AcceptNegative = value;
            }
        }

        public bool MEAutoComplete
        {
            set
            {
                TextBox1_MaskedEditExtender.AutoComplete = value;
            }
        }

        public string MEAutoCompleteValue
        {
            set
            {
                TextBox1_MaskedEditExtender.AutoCompleteValue = value;
            }
        }

        public int MECentury
        {
            set
            {
                TextBox1_MaskedEditExtender.Century = value;
            }
        }

        public bool MEClearMaskOnLostFocus
        {
            set
            {
                TextBox1_MaskedEditExtender.ClearMaskOnLostFocus = value;
            }
        }

        public bool MEClearTextOnInvalid
        {
            set
            {
                TextBox1_MaskedEditExtender.ClearTextOnInvalid = value;
            }
        }

        public string MECultureName
        {
            set
            {
                TextBox1_MaskedEditExtender.CultureName = value;
            }
        }

        public MaskedEditShowSymbol MEDisplayMoney
        {
            set
            {
                TextBox1_MaskedEditExtender.DisplayMoney = value;
            }
        }

        public string MEErrorTooltipCssClass
        {
            set
            {
                TextBox1_MaskedEditExtender.ErrorTooltipCssClass = value;
            }
        }

        public bool MEErrorTooltipEnabled
        {
            set
            {
                TextBox1_MaskedEditExtender.ErrorTooltipEnabled = value;
            }
        }

        public string MEFiltered
        {
            set
            {
                TextBox1_MaskedEditExtender.Filtered = value;
            }
        }

        public MaskedEditInputDirection MEInputDirection
        {
            set
            {
                TextBox1_MaskedEditExtender.InputDirection = value;
            }
        }

        public string MEMask
        {
            set
            {
                TextBox1_MaskedEditExtender.Mask = value;
                MaskedEditExtenderPanel.Visible = true;
            }
        }

        public MaskedEditType MEMaskType
        {
            set
            {
                TextBox1_MaskedEditExtender.MaskType = value;
            }
        }

        public string MEOnBlurCssNegative
        {
            set
            {
                TextBox1_MaskedEditExtender.OnBlurCssNegative = value;
            }
        }

        public string MEOnFocusCssClass
        {
            set
            {
                TextBox1_MaskedEditExtender.OnFocusCssClass = value;
            }
        }

        public string MEOnFocusCssNegative
        {
            set
            {
                TextBox1_MaskedEditExtender.OnFocusCssNegative = value;
            }
        }

        public string MEOnInvalidCssClass
        {
            set
            {
                TextBox1_MaskedEditExtender.OnInvalidCssClass = value;
            }
        }

        public string MEPromptCharacter
        {
            set
            {
                TextBox1_MaskedEditExtender.PromptCharacter = value;
            }
        }

        public MaskedEditUserDateFormat MEUserDateFormat
        {
            set
            {
                TextBox1_MaskedEditExtender.UserDateFormat = value;
            }
        }

        public MaskedEditUserTimeFormat MEUserTimeFormat
        {
            set
            {
                TextBox1_MaskedEditExtender.UserTimeFormat = value;
            }
        }

        public string MEVEmptyValueBlurredText
        {
            set
            {
                MaskedEditValidator1.EmptyValueBlurredText = value;
            }
        }

        public string MEVEmptyValueMessage
        {
            set
            {
                MaskedEditValidator1.EmptyValueMessage = value;
            }
        }

        public string MEVInvalidValueBlurredMessage
        {
            set
            {
                MaskedEditValidator1.InvalidValueBlurredMessage = value;
            }
        }

        public string MEVInvalidValueMessage
        {
            set
            {
                MaskedEditValidator1.InvalidValueMessage = value;
            }
        }

        public bool MEVIsValidEmpty
        {
            get
            {
                return MaskedEditValidator1.IsValidEmpty;
            }

            set
            {
                MaskedEditValidator1.IsValidEmpty = value;
            }
        }

        public string MEVMaximumValue
        {
            get
            {
                return MaskedEditValidator1.MaximumValue;
            }

            set
            {
                MaskedEditValidator1.MaximumValue = value;
            }
        }

        public string MEVMaximumValueBlurredMessage
        {
            set
            {
                MaskedEditValidator1.MaximumValueBlurredMessage = value;
            }
        }

        public string MEVMaximumValueMessage
        {
            set
            {
                MaskedEditValidator1.MaximumValueMessage = value;
            }
        }

        public string MEVMinimumValue
        {
            get
            {
                return MaskedEditValidator1.MinimumValue;
            }

            set
            {
                MaskedEditValidator1.MinimumValue = value;
            }
        }

        public string MEVMinimumValueBlurredText
        {
            set
            {
                MaskedEditValidator1.MinimumValueBlurredText = value;
            }
        }

        public string MEVMinimumValueMessage
        {
            set
            {
                MaskedEditValidator1.MinimumValueMessage = value;
            }
        }

        public string MEVTooltipMessage
        {
            set
            {
                MaskedEditValidator1.TooltipMessage = value;
            }
        }

        public bool MEVVisible
        {
            set
            {
                MaskedEditValidator1.Visible = value;
            }
        }

        public string MinValue
        {
            get
            {
                return RangeValidator1.MinimumValue;
            }

            set
            {
                RangeValidator1.MinimumValue = value;
                RangeValidator1.Visible = true;
            }
        }

        public ListBox PopUpListBox
        {
            get
            {
                return PopUpListBox1;
            }

            set
            {
                PopUpListBox1 = value;
            }
        }

        public PopupControlPopupPosition PopUpPosition
        {
            get
            {
                return PopupControlExtender1.Position;
            }

            set
            {
                PopupControlExtender1.Position = value;
            }
        }

        public string RangeErrorMessage
        {
            get
            {
                return RangeValidator1.ErrorMessage;
            }

            set
            {
                RangeValidator1.ErrorMessage = value;
                RangeValidator1.Visible = true;
            }
        }

        public ValidationDataType RangeType
        {
            get
            {
                return RangeValidator1.Type;
            }

            set
            {
                RangeValidator1.Type = value;
                RangeValidator1.Visible = true;
            }
        }

        public RangeValidator RangeValidator
        {
            get
            {
                return RangeValidator1;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return TextBox1.ReadOnly;
            }

            set
            {
                TextBox1.ReadOnly = value;
            }
        }

        public RegularExpressionValidator RegularExpressionValidator
        {
            get
            {
                return RegularExpressionValidator1;
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
                AddTextBoxCssClass = "required";
            }
        }

        public RequiredFieldValidator RequiredFieldValidator
        {
            get
            {
                return RequiredFieldValidator1;
            }
        }

        public DateTime? SelectedDate
        {
            get
            {
                return CalendarExtender1.SelectedDate;
            }

            set
            {
                CalendarExtender1.SelectedDate = value;
                CalendarExtenderPanel.Visible = true;
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

        public DateTime? StartDate
        {
            get
            {
                return CalendarExtender1.StartDate;
            }

            set
            {
                CalendarExtender1.StartDate = value;
                CalendarExtenderPanel.Visible = true;
            }
        }

        public string Text
        {
            get
            {
                return TextBox1.Text;
            }

            set
            {
                TextBox1.Text = value;
            }
        }

        public string TextBoxCssClass
        {
            set
            {
                TextBox1.CssClass = value;
            }
        }

        public TextBoxMode TextMode
        {
            get
            {
                return TextBox1.TextMode;
            }

            set
            {
                TextBox1.TextMode = value;
            }
        }

        public string Tip
        {
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    TextBox1.Attributes.Add("title", value);
                }
            }
        }

        public TipPosition TipPosition
        {
            set
            {
                switch (value)
                {
                    case TipPosition.Left:
                        TipSpan.Attributes.Add("class", "etip");
                        break;

                    case TipPosition.Right:
                        TipSpan.Attributes.Add("class", "wtip");
                        break;
                }
            }
        }

        public string ValidationErrorMessage
        {
            get
            {
                return RegularExpressionValidator1.ErrorMessage;
            }

            set
            {
                RegularExpressionValidator1.ErrorMessage = value;
                RegularExpressionValidator1.Visible = true;
            }
        }

        public string ValidationExpression
        {
            get
            {
                return RegularExpressionValidator1.ValidationExpression;
            }

            set
            {
                RegularExpressionValidator1.ValidationExpression = value;
                RegularExpressionValidator1.Visible = true;
            }
        }

        public string ValidationGroup
        {
            set
            {
                TextBox1.ValidationGroup = value;
                RegularExpressionValidator1.ValidationGroup = value;
                RequiredFieldValidator1.ValidationGroup = value;
                CompareValidator1.ValidationGroup = value;
                RangeValidator1.ValidationGroup = value;
                CustomValidator1.ValidationGroup = value;
                MaskedEditValidator1.ValidationGroup = value;
            }
        }

        public string ValidChars
        {
            get
            {
                return FilteredTextBoxExtender1.ValidChars;
            }

            set
            {
                FilteredTextBoxExtender1.ValidChars = value;
                FilteredTextBoxExtenderPanel.Visible = true;
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

        public string WatermarkCssClass
        {
            set
            {
                WaterMarkExtenderPanel.CssClass = value;
            }
        }

        public string WatermarkText
        {
            get
            {
                return TextBoxWatermarkExtender1.WatermarkText;
            }

            set
            {
                TextBoxWatermarkExtender1.WatermarkText = value;
                WaterMarkExtenderPanel.Visible = true;
            }
        }

        private void OnTextChanged()
        {
            if (TextChanged != null)
            {
                TextChanged(this, EventArgs.Empty);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void PopUpListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PopUpListBox.SelectedItem.Value))
            {
                PopupControlExtender1.Commit(PopUpListBox.SelectedItem.Value);
            }
            else
            {
                PopupControlExtender1.Cancel();
            }

            PopUpListBox.ClearSelection();
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged();
        }
    }
}