using OWDARO.Helpers;
using OWDARO.ILL;
using OWDARO.Settings;
using OWDARO.Util;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Others
{
    public partial class ContactUsQueryFormComponent : UserControl
    {
        private void FormToolbar1_CustomClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var AdminMessage = string.Empty;

                AdminMessage += "<div style='padding:20px;'>" +
                                "<p><strong>Name: </strong><span>" + NameTextBox.Text + "</span></p>" +
                                "<p><strong>Email: </strong><span>" + EmailTextBox.Text + "</span></p>" +
                                "<p><strong>Mobile/Phone: </strong><span>" + PhoneTextBox.Text + "</span></p>" +
                                "<p><strong>City: </strong><span>" + CityTextBox.Text + "</span></p>" +
                                "<p><strong>State: </strong><span>" + StateTextBox.Text + "</span></p>" +
                                "<p><strong>Country: </strong><span>" + CountryDropDownList.Country.Value + "</span></p>" +
                                "<p><strong>Comments: </strong><span>" + MessageTextBox.Text + "</span></p>" +
                                "</div>";

                var templateBody = MailHelper.GetEmailTemplateFromFile(AppConfig.EmailTemplate1);

                var adminPlaces = new EmailPlaceHolder();

                adminPlaces.PlaceHolder1 = Utilities.DateTimeNow().ToString();
                adminPlaces.PlaceHolder2 = AdminMessage;

                var adminEmailBody = MailHelper.GenerateEmailBody(adminPlaces, templateBody);

                try
                {
                    var t = Task.Factory.StartNew(() => MailHelper.Send(AppConfig.WebsiteMainEmail, AppConfig.WebsiteAdminEmail, "Quick Contact", adminEmailBody));

                    t.Wait();

                    StatusMessage.Message = "Your message is received by us. Thank you for contacting us.";
                    StatusMessage.MessageType = StatusMessageType.Success;
                }
                catch (Exception ex)
                {
                    var t1 = Task.Factory.StartNew(() => ErrorLogger.LogError(ex));

                    t1.Wait();

                    StatusMessage.Message = "There was an error while sending your message. Please try again after some time" + ExceptionHelper.GetExceptionMessage(ex);
                    StatusMessage.MessageType = StatusMessageType.Error;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EmailTextBox.ValidationErrorMessage = Validator.EmailValidationErrorMessage;
                EmailTextBox.ValidationExpression = Validator.EmailValidationExpression;

                PhoneTextBox.ValidationErrorMessage = Validator.LandlineValidationErrorMessage;
                PhoneTextBox.ValidationExpression = Validator.LandlineValidationExpression;

                string locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
                string direction = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureDirectionCookieKey);

                NameTextBox.Direction = direction;
                EmailTextBox.Direction = direction;
                PhoneTextBox.Direction = direction;
                CityTextBox.Direction = direction;
                StateTextBox.Direction = direction;
                MessageTextBox.Direction = direction;
                CountryDropDownList.Direction = direction;
                FormToolbar1.Direction = direction;

                NameTextBox.RequiredErrorMessage = ValidationHelper.GetMessage("NameIsRequired", locale);
                EmailTextBox.RequiredErrorMessage = ValidationHelper.GetMessage("EmailIsRequired", locale);
                PhoneTextBox.RequiredErrorMessage = ValidationHelper.GetMessage("MobileIsRequired", locale);
                CityTextBox.RequiredErrorMessage = ValidationHelper.GetMessage("CityIsRequired", locale);
                StateTextBox.RequiredErrorMessage = ValidationHelper.GetMessage("StateIsRequired", locale);
                CountryDropDownList.RequiredFieldErrorMessage = ValidationHelper.GetMessage("DropDownIsRequired", locale);
                MessageTextBox.RequiredErrorMessage = ValidationHelper.GetMessage("CommentIsRequired", locale);

                FormToolbar1.CustomButtonText = LanguageHelper.GetKey("Send", locale);
                NameTextBox.LabelText = LanguageHelper.GetKey("Name", locale);
                EmailTextBox.LabelText = LanguageHelper.GetKey("Email", locale);
                PhoneTextBox.LabelText = string.Format("{0}/{1}", LanguageHelper.GetKey("Mobile", locale), LanguageHelper.GetKey("Landline", locale));
                CityTextBox.LabelText = LanguageHelper.GetKey("City", locale);
                StateTextBox.LabelText = LanguageHelper.GetKey("State", locale);
                CountryDropDownList.LabelText = LanguageHelper.GetKey("Country", locale);
                MessageTextBox.LabelText = LanguageHelper.GetKey("Comment", locale);
            }

            FormToolbar1.CustomClick += FormToolbar1_CustomClick;
        }
    }
}