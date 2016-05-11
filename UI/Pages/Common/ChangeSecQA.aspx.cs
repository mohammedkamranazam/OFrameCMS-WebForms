using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using System;
using System.Transactions;
using System.Web.Security;

namespace OWDARO.UI.Pages.Common
{
    public partial class ChangeSecQA : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder());
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var user = Membership.GetUser();

                if (user != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        if (user.ChangePasswordQuestionAndAnswer(CurrentPasswordTextBox.Text, NewQuestionTextBox.Text, NewAnswerTextBox.Text))
                        {
                            var userEntity = UserBL.GetUserByUsername(user.UserName);

                            userEntity.SecurityQuestion = NewQuestionTextBox.Text;
                            userEntity.SecurityAnswer = NewAnswerTextBox.Text;

                            try
                            {
                                UserBL.Save(userEntity);
                                StatusLabel.MessageType = StatusMessageType.Success;
                                StatusLabel.Message = "security question and answer successfully changed";
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError(ex);
                                StatusLabel.MessageType = StatusMessageType.Error;
                                StatusLabel.Message = "Error while changing security question and answer" + ExceptionHelper.GetExceptionMessage(ex);
                            }
                        }
                        else
                        {
                            StatusLabel.MessageType = StatusMessageType.Info;
                            StatusLabel.Message = "incorrect password";
                        }

                        scope.Complete();
                    }
                }
                else
                {
                    StatusLabel.MessageType = StatusMessageType.Warning;
                    StatusLabel.Message = "invalid logged in user";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("ChangeSecQAPage"));
            }

            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += new EventHandler(FormToolbar1_Cancel);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Title = AppConfig.SiteName + ": Change Security Question & Answer";
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetRoleThemeFile();
        }
    }
}