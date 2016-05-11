using System;

namespace OWDARO.UI.UserControls.Controls
{
    public partial class FormToolbar : System.Web.UI.UserControl
    {
        public event EventHandler Cancel;

        public event EventHandler CustomClick;

        public event EventHandler CustomPopupClick;

        public event EventHandler Delete;

        public event EventHandler Edit;

        public event EventHandler Save;

        public event EventHandler SaveGoBack;

        public event EventHandler Update;

        public bool CustomButtonCausesValidation
        {
            set
            {
                CustomButton.CausesValidation = value;
            }
        }

        public bool CustomPopUpButtonCausesValidation
        {
            set
            {
                CustomPopupButton.CausesValidation = value;
            }
        }

        public bool CenterButtons
        {
            set
            {
                if (value)
                {
                    DIV.Style.Add("margin", "0px");
                    DIV.Style.Add("text-align", "center");
                }
                else
                {
                    DIV.Style.Clear();
                }
            }
        }

        public string CancelButtonCssClass
        {
            set
            {
                CancelButton.CssClass = value;
            }
        }

        public string CustomButtonCssClass
        {
            set
            {
                CustomButton.CssClass = value;
            }
        }

        public string SaveButtonCssClass
        {
            set
            {
                SaveButton.CssClass = value;
            }
        }

        public string SaveButtonText
        {
            set
            {
                SaveButton.Text = value;
            }
        }

        public string CustomButtonText
        {
            set
            {
                CustomButton.Text = value;
            }
        }

        public string CustomPopupButtonCssClass
        {
            set
            {
                CustomPopupButton.CssClass = value;
            }
        }

        public string CustomPopupButtonText
        {
            set
            {
                CustomPopupButton.Text = value;
            }
        }

        public string CustomPopupCancelButtonText
        {
            set
            {
                CustomPopupCancelButton.Text = value;
            }
        }

        public string CustomPopupMessage
        {
            set
            {
                CustomPopupMessageLabel.Text = value;
            }
        }

        public string CustomPopupOKButtonText
        {
            set
            {
                CustomPopupOKButton.Text = value;
            }
        }

        public string CustomPopupTitle
        {
            set
            {
                CustomPopupTitleLabel.Text = value;
            }
        }

        public string DeleteButtonCssClass
        {
            set
            {
                DeleteButton.CssClass = value;
            }
        }

        public string DeletePopupMessage
        {
            set
            {
                DeletePopupMessageLabel.Text = value;
            }
        }

        public string DeletePopupTitle
        {
            set
            {
                DeletePopupTitleLabel.Text = value;
            }
        }

        public string EditButtonCssClass
        {
            set
            {
                EditButton.CssClass = value;
            }
        }

        public string UpdateButtonCssClass
        {
            set
            {
                UpdateButton.CssClass = value;
            }
        }

        public string UpdateButtonText
        {
            set
            {
                UpdateButton.Text = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether to hide the toolbar and not disable its features. Helpful to continue showing messages.
        /// </summary>
        /// <value>
        /// <c>true</c> if [hide toolbar]; otherwise, <c>false</c>.
        /// </value>
        public bool HideToolbar
        {
            set
            {
                if (value)
                {
                    ToolbarPanel.Attributes.Add("style", "display:none;");
                }
                else
                {
                    ToolbarPanel.Attributes.Add("style", "display:block;");
                }
            }
        }

        public string MessagePopupDismissButtonText
        {
            set
            {
                MessagePopupDismissButton.Text = value;
            }
        }

        public string MessagePopupMessage
        {
            set
            {
                MessagePopupMessageLabel.Text = value;
            }
        }

        public string MessagePopupOnCancelScript
        {
            set
            {
                MessageButton_ModalPopupExtender.OnCancelScript = value;
            }
        }

        public string MessagePopupSubmitButtonText
        {
            set
            {
                MessagePopupSubmitButton.Text = value;
            }
        }

        public string MessagePopupTitle
        {
            set
            {
                MessagePopupTitleLabel.Text = value;
            }
        }

        public string RedirectToAbsoluteUrl
        {
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    MessagePopupOnCancelScript = "window.location = '" + value + "'";
                }
            }
        }

        public string RedirectToRelativeUrl
        {
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    MessagePopupOnCancelScript = "window.location = '" + ResolveClientUrl(value) + "'";
                }
            }
        }

        public string SaveGoBackButtonCssClass
        {
            set
            {
                SaveGoBackButton.CssClass = value;
            }
        }

        public bool ShowCancel
        {
            get
            {
                return CancelButton.Visible;
            }

            set
            {
                CancelButton.Visible = value;
            }
        }

        public bool ShowCustom
        {
            get
            {
                return CustomButton.Visible;
            }

            set
            {
                CustomButton.Visible = value;
            }
        }

        public bool ShowCustomPopupButton
        {
            get
            {
                return CustomPopupButton.Visible;
            }

            set
            {
                CustomPopupButton.Visible = value;
            }
        }

        public bool ShowDelete
        {
            get
            {
                return DeleteButton.Visible;
            }

            set
            {
                DeleteButton.Visible = value;
            }
        }

        public bool ShowEdit
        {
            get
            {
                return EditButton.Visible;
            }

            set
            {
                EditButton.Visible = value;
            }
        }

        public bool ShowSave
        {
            get
            {
                return SaveButton.Visible;
            }

            set
            {
                SaveButton.Visible = value;
            }
        }

        public bool ShowSaveGoBack
        {
            get
            {
                return SaveGoBackButton.Visible;
            }

            set
            {
                SaveGoBackButton.Visible = value;
            }
        }

        public bool ShowUpdate
        {
            get
            {
                return UpdateButton.Visible;
            }

            set
            {
                UpdateButton.Visible = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to completely hide the toolbar and disable its features. Not helpful to continue showing messages.
        /// </summary>
        /// <value>
        /// <c>true</c> if [show toolbar]; otherwise, <c>false</c>.
        /// </value>
        public bool ToolabrDisabled
        {
            get
            {
                return !ToolbarPanel.Visible;
            }

            set
            {
                ToolbarPanel.Visible = !value;
            }
        }

        public string ValidationGroup
        {
            set
            {
                SaveButton.ValidationGroup = value;
                SaveGoBackButton.ValidationGroup = value;
                CancelButton.ValidationGroup = value;
                DeleteButton.ValidationGroup = value;
                EditButton.ValidationGroup = value;
                UpdateButton.ValidationGroup = value;
                CustomButton.ValidationGroup = value;
                CustomPopupButton.ValidationGroup = value;
            }
        }

        private void OnCancel()
        {
            if (Cancel != null)
            {
                Cancel(this, EventArgs.Empty);
            }
        }

        private void OnCustomClick()
        {
            if (CustomClick != null)
            {
                CustomClick(this, EventArgs.Empty);
            }
        }

        private void OnCustomPopupClick()
        {
            if (CustomPopupClick != null)
            {
                CustomPopupClick(this, EventArgs.Empty);
            }
        }

        private void OnDelete()
        {
            if (Delete != null)
            {
                Delete(this, EventArgs.Empty);
            }
        }

        private void OnEdit()
        {
            if (Edit != null)
            {
                Edit(this, EventArgs.Empty);
            }
        }

        private void OnSave()
        {
            if (Save != null)
            {
                Save(this, EventArgs.Empty);
            }
        }

        private void OnSaveGoBack()
        {
            if (SaveGoBack != null)
            {
                SaveGoBack(this, EventArgs.Empty);
            }
        }

        private void OnUpdate()
        {
            if (Update != null)
            {
                Update(this, EventArgs.Empty);
            }
        }

        public string Direction
        {
            set
            {
                DIV.Attributes.Remove("class");
                DIV.Attributes.Add("class", value);
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            OnCancel();
        }

        protected void CustomButton_Click(object sender, EventArgs e)
        {
            OnCustomClick();
        }

        protected void CustomPopupButton_Click(object sender, EventArgs e)
        {
            OnCustomPopupClick();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PopupsPanel.Visible = true;
            CustomPopupPanel.Visible = true;
            DeletePopupPanel.Visible = true;
            MessagePopupPanel.Visible = true;
            FalsePanel.Visible = false;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            OnSave();
        }

        protected void SaveGoBackButton_Click(object sender, EventArgs e)
        {
            OnSaveGoBack();
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            OnUpdate();
        }

        public void ShowCustomPopup()
        {
            CustomPopupButton_ModalPopupExtender.Show();
        }

        public void ShowCustomPopup(string title, string message)
        {
            CustomPopupTitleLabel.Text = title;
            CustomPopupMessageLabel.Text = message;
            CustomPopupButton_ModalPopupExtender.Show();
        }

        public void ShowDeletePopup()
        {
            DeleteButton_ModalPopupExtender.Show();
        }

        public void ShowDeletePopup(string title, string message)
        {
            DeletePopupTitleLabel.Text = title;
            DeletePopupMessageLabel.Text = message;
            DeleteButton_ModalPopupExtender.Show();
        }

        public void ShowMessagePopup()
        {
            MessageButton_ModalPopupExtender.Show();
        }

        public void ShowMessagePopup(string title, string message)
        {
            MessagePopupTitleLabel.Text = title;
            MessagePopupMessageLabel.Text = message;
            MessageButton_ModalPopupExtender.Show();
        }
    }
}