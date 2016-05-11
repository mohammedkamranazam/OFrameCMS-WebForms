using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Models;
using OWDARO.Settings;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Transactions;
using System.Web.Security;

namespace OWDARO.UI.Pages.Common
{
    public partial class ChangeContact : System.Web.UI.Page
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
                    using (var context = new MembershipEntities())
                    {
                        var userEntity = UserBL.GetUserByUsername(user.UserName, context);

                        userEntity.Email = EmailTextBox.Text;

                        try
                        {
                            using (var scope = new TransactionScope())
                            {
                                user.Email = userEntity.Email;

                                Membership.UpdateUser(user);

                                UserBL.Save(userEntity, context);

                                StatusMessage.MessageType = StatusMessageType.Success;
                                StatusMessage.Message = "Saved Successfully";

                                scope.Complete();
                            }
                        }
                        catch (Exception ex)
                        {
                            StatusMessage.MessageType = StatusMessageType.Error;
                            StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                        }
                    }
                }
            }
        }

        private void InitializeCategoryUI(UserCategorySettings categorySetting)
        {
            EmailPanel.Visible = true;
            EmailsUserDataDetailsComponent.ComponentVisible = true;

            MobilePanel.Visible = categorySetting.ShowMobile;
            MobileUserDataDetailsComponent.ComponentVisible = categorySetting.ShowMobile;

            LandlinePanel.Visible = categorySetting.ShowLandline;
            LandlineUserDataDetailsComponent.ComponentVisible = categorySetting.ShowLandline;

            FaxPanel.Visible = categorySetting.ShowFax;
            FaxUserDataDetailsComponent.ComponentVisible = categorySetting.ShowFax;

            WebsitePanel.Visible = categorySetting.ShowWebsite;
            WebsiteUserDataDetailsComponent.ComponentVisible = categorySetting.ShowWebsite;

            WorkPanel.Visible = categorySetting.ShowWork;
            WorkDetailsComponent.ComponentVisible = categorySetting.ShowWork;

            AddressPanel.Visible = categorySetting.ShowAddress;
            AddressDetailsComponent.ComponentVisible = categorySetting.ShowAddress;

            EducationPanel.Visible = categorySetting.ShowEducation;
            EducationDetailsComponent.ComponentVisible = categorySetting.ShowEducation;
        }

        private void InitializeCommonUI(string username)
        {
            EmailTextBox.ValidationErrorMessage = Validator.EmailValidationErrorMessage;
            EmailTextBox.ValidationExpression = Validator.EmailValidationExpression;

            EmailsUserDataDetailsComponent.Username = username;
            MobileUserDataDetailsComponent.Username = username;
            LandlineUserDataDetailsComponent.Username = username;
            FaxUserDataDetailsComponent.Username = username;
            WebsiteUserDataDetailsComponent.Username = username;
            AddressDetailsComponent.Username = username;
            WorkDetailsComponent.Username = username;
            EducationDetailsComponent.Username = username;

            EmailsUserDataDetailsComponent.DataCategory = UserDataCategories.EmailCategory.Value;
            MobileUserDataDetailsComponent.DataCategory = UserDataCategories.MobileCategory.Value;
            LandlineUserDataDetailsComponent.DataCategory = UserDataCategories.LandlineCategory.Value;
            FaxUserDataDetailsComponent.DataCategory = UserDataCategories.FaxCategory.Value;
            WebsiteUserDataDetailsComponent.DataCategory = UserDataCategories.WebsiteCategory.Value;

            EmailsUserDataDetailsComponent.HeaderTitle = "Secondary Emails";
            EmailsUserDataDetailsComponent.BoxTitle = "All The Secondary Emails Of The User";
            EmailsUserDataDetailsComponent.DataTextBoxLabelText = "Email ID";
            EmailsUserDataDetailsComponent.DataTextBoxSmallLabelText = "secondary email id for secondary email notifications";
            EmailsUserDataDetailsComponent.DataTextBoxRequiredErrorMessage = "please enter the secondary email id";
            EmailsUserDataDetailsComponent.DataTextBoxValidationErrorMessage = Validator.EmailValidationErrorMessage;
            EmailsUserDataDetailsComponent.DataTextBoxValidationExpression = Validator.EmailValidationExpression;
            EmailsUserDataDetailsComponent.ValidationGroup = "SecondaryEmailsValidationGroup";
            EmailsUserDataDetailsComponent.ToolBarCustomButtonText = "Add Email";
            EmailsUserDataDetailsComponent.GridViewLiteralText = "Secondary Email: ";
            EmailsUserDataDetailsComponent.GridViewHyperLinkFormatString = "mailto:";

            MobileUserDataDetailsComponent.HeaderTitle = "Mobile Numbers";
            MobileUserDataDetailsComponent.BoxTitle = "All The Mobile Numbers Of The User";
            MobileUserDataDetailsComponent.DataTextBoxLabelText = "Mobile";
            MobileUserDataDetailsComponent.DataTextBoxSmallLabelText = "mobile number of the user";
            MobileUserDataDetailsComponent.DataTextBoxRequiredErrorMessage = "please enter the mobile number";
            MobileUserDataDetailsComponent.DataTextBoxValidationErrorMessage = Validator.MobileValidationErrorMessage;
            MobileUserDataDetailsComponent.DataTextBoxValidationExpression = Validator.MobileValidationExpression;
            MobileUserDataDetailsComponent.ValidationGroup = "MobileNumbersValidationGroup";
            MobileUserDataDetailsComponent.ToolBarCustomButtonText = "Add Mobile Number";
            MobileUserDataDetailsComponent.GridViewLiteralText = "Mobile: ";
            MobileUserDataDetailsComponent.GridViewHyperLinkFormatString = "callto://";

            LandlineUserDataDetailsComponent.HeaderTitle = "Landline Numbers";
            LandlineUserDataDetailsComponent.BoxTitle = "All The Landline Numbers Of The User";
            LandlineUserDataDetailsComponent.DataTextBoxLabelText = "Landline";
            LandlineUserDataDetailsComponent.DataTextBoxSmallLabelText = "landline number of the user";
            LandlineUserDataDetailsComponent.DataTextBoxRequiredErrorMessage = "please enter the landline number";
            LandlineUserDataDetailsComponent.DataTextBoxValidationErrorMessage = Validator.LandlineValidationErrorMessage;
            LandlineUserDataDetailsComponent.DataTextBoxValidationExpression = Validator.LandlineValidationExpression;
            LandlineUserDataDetailsComponent.ValidationGroup = "LandlineNumbersValidationGroup";
            LandlineUserDataDetailsComponent.ToolBarCustomButtonText = "Add Landline Number";
            LandlineUserDataDetailsComponent.GridViewLiteralText = "Landline: ";
            LandlineUserDataDetailsComponent.GridViewHyperLinkFormatString = "callto://";

            FaxUserDataDetailsComponent.HeaderTitle = "Fax Numbers";
            FaxUserDataDetailsComponent.BoxTitle = "All The Fax Numbers Of The User";
            FaxUserDataDetailsComponent.DataTextBoxLabelText = "Fax";
            FaxUserDataDetailsComponent.DataTextBoxSmallLabelText = "fax number of the user";
            FaxUserDataDetailsComponent.DataTextBoxRequiredErrorMessage = "please enter the fax number";
            FaxUserDataDetailsComponent.DataTextBoxValidationErrorMessage = Validator.LandlineValidationErrorMessage;
            FaxUserDataDetailsComponent.DataTextBoxValidationExpression = Validator.LandlineValidationExpression;
            FaxUserDataDetailsComponent.ValidationGroup = "FaxNumbersValidationGroup";
            FaxUserDataDetailsComponent.ToolBarCustomButtonText = "Add Fax Number";
            FaxUserDataDetailsComponent.GridViewLiteralText = "Fax: ";
            FaxUserDataDetailsComponent.GridViewHyperLinkFormatString = "callto://";

            WebsiteUserDataDetailsComponent.HeaderTitle = "Websites";
            WebsiteUserDataDetailsComponent.BoxTitle = "All The Websites Of The User";
            WebsiteUserDataDetailsComponent.DataTextBoxLabelText = "Website";
            WebsiteUserDataDetailsComponent.DataTextBoxSmallLabelText = "user's website url";
            WebsiteUserDataDetailsComponent.DataTextBoxRequiredErrorMessage = "please enter the website address";
            WebsiteUserDataDetailsComponent.DataTextBoxValidationErrorMessage = Validator.UrlValidationErrorMessage;
            WebsiteUserDataDetailsComponent.DataTextBoxValidationExpression = Validator.UrlValidationExpression;
            WebsiteUserDataDetailsComponent.ValidationGroup = "WebsiteURLValidationGroup";
            WebsiteUserDataDetailsComponent.ToolBarCustomButtonText = "Add Website";
            WebsiteUserDataDetailsComponent.GridViewLiteralText = "Website: ";
            WebsiteUserDataDetailsComponent.GridViewHyperLinkFormatString = string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("ChangeContactPage"));

                var user = Membership.GetUser();

                if (user == null)
                {
                    Response.Redirect("~/UI/Pages/LogOn/");
                }

                var userEntity = UserBL.GetUserByUsername(user.UserName);

                InitializeCommonUI(userEntity.Username);

                InitializeCategoryUI(UserCategoryHelper.GetCategorySetting(UserBL.GetUserCategoryID(userEntity), PageSetting.Manage));

                EmailTextBox.Text = userEntity.Email;
            }

            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += new EventHandler(FormToolbar1_Cancel);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Title = AppConfig.SiteName + ": Change Contact Details";
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetRoleThemeFile();
        }
    }
}