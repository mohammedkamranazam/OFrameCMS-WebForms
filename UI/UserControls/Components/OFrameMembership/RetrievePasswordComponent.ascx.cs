using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.ILL;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Web.Security;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class RetrievePasswordComponent : System.Web.UI.UserControl
    {
        private async void FormToolbar1_CustomClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var userEmail = EmailIDTextBox.Text;

                using (var context = new MembershipEntities())
                {
                    var userEntity = await UserBL.IfUserExistsFromEmailAsync(userEmail, context);

                    if (userEntity != null)
                    {
                        var user = Membership.GetUser(userEntity.Username);

                        if (user != null)
                        {
                            var securityAnswer = userEntity.SecurityAnswer;

                            var newPassword = user.ResetPassword(securityAnswer);

                            var templateBody = MailHelper.GetEmailTemplateFromFile(AppConfig.EmailTemplate1);

                            var places = new EmailPlaceHolder();

                            places.PlaceHolder1 = Utilities.DateTimeNow().ToString();
                            places.PlaceHolder2 = "Your New Password Is: ";
                            places.PlaceHolder3 = newPassword;

                            var emailBody = MailHelper.GenerateEmailBody(places, templateBody);

                            try
                            {
                                MailHelper.Send(AppConfig.WebsiteMainEmail, userEmail, "Account Password", emailBody);

                                StatusMessage.MessageType = StatusMessageType.Success;
                                StatusMessage.Message = "Password Successfully Sent To Email";
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError(ex);
                                StatusMessage.MessageType = StatusMessageType.Error;
                                StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                            }
                        }
                        else
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = "Email ID Without Login Account";
                        }
                    }
                    else
                    {
                        StatusMessage.MessageType = StatusMessageType.Info;
                        StatusMessage.Message = "Email ID Not In Use";
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EmailIDTextBox.ValidationExpression = Validator.EmailValidationExpression;
                EmailIDTextBox.ValidationErrorMessage = Validator.EmailValidationErrorMessage;
            }

            FormToolbar1.CustomClick += new EventHandler(FormToolbar1_CustomClick);
        }
    }
}