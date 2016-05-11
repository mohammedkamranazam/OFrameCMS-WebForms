using OWDARO.BLL.MediaBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;

namespace OWDARO.UI.UserControls.Components.Media
{
    public partial class SubscribeComponent : System.Web.UI.UserControl
    {
        public string ButtonCssClass
        {
            set
            {
                Subscribe.CssClass = value;
            }
        }

        public string ButtonAddCssClass
        {
            set
            {
                Subscribe.CssClass += " " + value;
            }
        }

        public bool EmailVisible
        {
            get
            {
                return EmailTextBox.Visible;
            }

            set
            {
                EmailTextBoxRequiredFieldValidator.Visible = value;
                EmailTextBox.Visible = value;
            }
        }

        public bool MobileVisible
        {
            get
            {
                return MobileTextBox.Visible;
            }

            set
            {
                MobileTextBoxRequiredFieldValidator.Visible = value;
                MobileTextBox.Visible = value;
            }
        }

        public bool NameVisible
        {
            get
            {
                return NameTextBox.Visible;
            }

            set
            {
                NameTetBoxRequiredFieldValidator.Visible = value;
                NameTextBox.Visible = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
                string direction = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureDirectionCookieKey);

                SubscribeForm.Style.Add("direction", direction);

                NameTextBox.Attributes.Add("placeholder", LanguageHelper.GetKey("Name", locale));
                EmailTextBox.Attributes.Add("placeholder", LanguageHelper.GetKey("Email", locale));
                MobileTextBox.Attributes.Add("placeholder", string.Format("{0}/{1}", LanguageHelper.GetKey("Mobile", locale), LanguageHelper.GetKey("Landline", locale)));

                MobileTextBoxRequiredFieldValidator.ErrorMessage = ValidationHelper.GetMessage("MobileIsRequired", locale);
                EmailTextBoxRequiredFieldValidator.ErrorMessage = ValidationHelper.GetMessage("EmailIsRequired", locale);
                NameTetBoxRequiredFieldValidator.ErrorMessage = ValidationHelper.GetMessage("NameIsRequired", locale);

                Subscribe.Text = LanguageHelper.GetKey("Subscribe", locale);
            }
        }

        protected async void Subscribe_Click(object sender, EventArgs e)
        {
            string locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);

            if (Page.IsValid && StringHelper.IsEmail(EmailTextBox.Text))
            {
                using (var context = new MediaEntities())
                {
                    if (await SubscriptionsBL.ExistsAsync(EmailTextBox.Text, context))
                    {
                        StatusMessage.MessageType = StatusMessageType.Warning;
                        StatusMessage.Message = LanguageHelper.GetKey("YouAreAlreadySubscribed", locale);
                        return;
                    }

                    var entity = new ME_Subscriptions();
                    entity.Email = EmailTextBox.Text;
                    entity.Name = NameTextBox.Text;
                    entity.Mobile = MobileTextBox.Text;
                    entity.IsSubscribed = true;

                    try
                    {
                        await SubscriptionsBL.AddAsync(entity, context);

                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = LanguageHelper.GetKey("YouHaveBeenSubscribedSuccessfully", locale);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        StatusMessage.MessageType = StatusMessageType.Error;
                        StatusMessage.Message = LanguageHelper.GetKey("ErrorWhileSubscribing", locale);
                    }
                }
            }
            else
            {
                StatusMessage.MessageType = StatusMessageType.Warning;
                StatusMessage.Message = ValidationHelper.GetMessage("InvalidFormData", locale);
            }
        }
    }
}