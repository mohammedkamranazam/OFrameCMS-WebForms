using OWDARO.OEventArgs;
using OWDARO.Settings;
using System;

namespace OWDARO.UI.UserControls.Controls.Nested
{
    public partial class LocalizationDropDownList : System.Web.UI.UserControl
    {
        public event LanguageDirectionChangedEventHandler LanguageDirectionChanged;

        public string AddDropDownCssClass
        {
            set
            {
                LanguagesDropDownList.AddDropDownCssClass = value;
            }
        }

        public bool AutoPostBack
        {
            get
            {
                return LanguagesDropDownList.AutoPostBack;
            }

            set
            {
                LanguagesDropDownList.AutoPostBack = value;
            }
        }

        public string ContainerID
        {
            get
            {
                return LanguagesDropDownList.ContainerID;
            }
        }

        public FieldWidth FieldWidth
        {
            set
            {
                LanguagesDropDownList.FieldWidth = value;
            }
        }

        public string LabelText
        {
            get
            {
                return LanguagesDropDownList.LabelText;
            }

            set
            {
                LanguagesDropDownList.LabelText = value;
            }
        }

        public string Locale
        {
            get
            {
                return LanguageLocaleHiddenField.Value;
            }

            set
            {
                LanguagesDropDownList.DataTextField = "Name";
                LanguagesDropDownList.DataValueField = "Locale";
                LanguagesDropDownList.DataSource = LanguageHelper.GetLanguages();
                LanguagesDropDownList.DataBind();
                LanguagesDropDownList.SelectedValue = value;

                LanguageLocaleHiddenField.Value = value;
            }
        }

        public string OnChange
        {
            set
            {
                LanguagesDropDownList.OnChange = value;
            }
        }

        public string SmallLabelText
        {
            get
            {
                return LanguagesDropDownList.SmallLabelText;
            }

            set
            {
                LanguagesDropDownList.SmallLabelText = value;
            }
        }

        public string ValidationGroup
        {
            set
            {
                LanguagesDropDownList.ValidationGroup = value;
            }
        }

        private void LanguagesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Locale = LanguagesDropDownList.SelectedValue;

            var direction = LanguageHelper.GetLocaleDirection(Locale);

            OnLanguageDirectionChanged(direction);
        }

        private void OnLanguageDirectionChanged(string direction)
        {
            if (LanguageDirectionChanged != null)
            {
                LanguageDirectionChanged(this, new LanguageDirectionChangedEventArgs(direction));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LanguagesDropDownList.SelectedIndexChanged += LanguagesDropDownList_SelectedIndexChanged;
        }
    }
}