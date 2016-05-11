using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.ILL;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class SecurityQAComponent : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void QAWizard_CancelButtonClick(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected async void QAWizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (Page.IsValid)
            {
                var user = Membership.GetUser(UsernameTextBox.Text);

                if (user != null)
                {
                    using (var context = new MembershipEntities())
                    {
                        var userEntity = await UserBL.GetUserByUsernameAsync(user.UserName, context);

                        if (userEntity.SecurityAnswer == SecurityAnswerTextBox.Text)
                        {
                            try
                            {
                                var newPassword = user.ResetPassword(SecurityAnswerTextBox.Text);

                                var templateBody = MailHelper.GetEmailTemplateFromFile(AppConfig.EmailTemplate1);

                                var places = new EmailPlaceHolder();

                                places.PlaceHolder1 = Utilities.DateTimeNow().ToString();
                                places.PlaceHolder2 = "Your New Password Is: ";
                                places.PlaceHolder3 = newPassword;

                                var emailBody = MailHelper.GenerateEmailBody(places, templateBody);

                                try
                                {
                                    MailHelper.Send(AppConfig.WebsiteMainEmail, userEntity.Email, "Account Password", emailBody);

                                    StatusMessage.MessageType = StatusMessageType.Success;
                                    StatusMessage.Message = "password successfully sent to email";
                                }
                                catch (Exception ex)
                                {
                                    StatusMessage.MessageType = StatusMessageType.Error;
                                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                                }
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
                            StatusMessage.Message = "Wrong Security Answer";
                        }
                    }
                }
            }
        }

        protected void QAWizard_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (Page.IsValid)
            {
                var user = Membership.GetUser(UsernameTextBox.Text);

                if (user != null)
                {
                    SecurityQuestionLiteral.Text = user.PasswordQuestion + "?";
                }
                else
                {
                    StatusMessage.MessageType = StatusMessageType.Warning;
                    StatusMessage.Message = "Invalid User";
                    e.Cancel = true;
                }
            }
        }

        protected void QAWizard_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            e.Cancel = true;
        }
    }
}