using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.IO;
using System.Transactions;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace OWDARO.UI.Pages.Common
{
    public partial class UserManage : System.Web.UI.Page
    {
        private void AllowProfilePicDeletion(string profilePic)
        {
            var maleAvatar = AppConfig.MaleAvatar;
            var femaleAvatar = AppConfig.FemaleAvatar;
            var unspecifiedAvatar = AppConfig.UnspecifiedAvatar;

            if (profilePic == maleAvatar || profilePic == femaleAvatar || profilePic == unspecifiedAvatar)
            {
                DeleteImageButton.Visible = false;
                UploadImageButton.Visible = true;
            }
            else
            {
                DeleteImageButton.Visible = true;
                UploadImageButton.Visible = false;
            }
        }

        private void CategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = CategoryDropDownList.GetSelectedValue();
            InitializeCategoryUI(UserCategoryHelper.GetCategorySetting(selectedValue, PageSetting.Manage));
        }

        private void DeleteImageButton_Click(object sender, EventArgs e)
        {
            var username = Request.QueryString["Username"];

            using (var context = new MembershipEntities())
            {
                var userEntity = UserBL.GetUserByUsername(username, context);

                var maleAvatar = AppConfig.MaleAvatar;
                var femaleAvatar = AppConfig.FemaleAvatar;
                var unspecifiedAvatar = AppConfig.UnspecifiedAvatar;

                var profilePic = userEntity.ProfilePic;

                if (profilePic == maleAvatar || profilePic == femaleAvatar || profilePic == unspecifiedAvatar)
                {
                    return;
                }

                userEntity.ProfilePic = GenderDropDownList.GetGender().GetProfilePic();

                try
                {
                    UserBL.Save(userEntity, context);

                    profilePic.DeleteFile();

                    ProfilePicImage.ImageUrl = userEntity.ProfilePic;

                    DeleteImageButton.Visible = false;
                    UploadImageButton.Visible = true;

                    StatusMessage.MessageType = StatusMessageType.Success;
                    StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }
            }
        }

        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var username = Request.QueryString["Username"];

            try
            {
                UserBL.Delete(username);
                StatusMessage.MessageType = StatusMessageType.Success;
                StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
            }
            catch (Exception ex)
            {
                StatusMessage.MessageType = StatusMessageType.Error;
                StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                ErrorLogger.LogError(ex);
            }
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var username = Request.QueryString["Username"];

                var user = Membership.GetUser(username);

                if (user == null)
                {
                    return;
                }

                using (var context = new MembershipEntities())
                {
                    using (var scope = new TransactionScope())
                    {
                        var userEntity = UserBL.GetUserByUsername(username, context);

                        var currentRole = userEntity.UserRole;
                        var newRole = RolesDropDownList.SelectedValue;

                        userEntity.Name = NameTextBox.Text;
                        userEntity.DateOfBirth = DataParser.NullableDateTimeParse(DateOfBirthTextBox.Text);
                        userEntity.Gender = GenderDropDownList.SelectedValue;
                        userEntity.UserCategoryID = CategoryDropDownList.GetNullableSelectedValue();

                        userEntity.UserRole = newRole;

                        try
                        {
                            Roles.RemoveUserFromRole(username, currentRole);
                            Roles.AddUserToRole(username, newRole);

                            UserBL.Save(userEntity, context);

                            StatusMessage.MessageType = StatusMessageType.Success;
                            StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);
                            StatusMessage.MessageType = StatusMessageType.Error;
                            StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                        }

                        scope.Complete();
                    }
                }
            }
        }

        private void FormToolbar2_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var username = Request.QueryString["Username"];

                using (var context = new MembershipEntities())
                {
                    var userEntity = UserBL.GetUserByUsername(username, context);
                    userEntity.Email = EmailTextBox.Text;

                    var user = Membership.GetUser(username);
                    user.Email = userEntity.Email;

                    try
                    {
                        using (var scope = new TransactionScope())
                        {
                            Membership.UpdateUser(user);

                            UserBL.Save(userEntity, context);

                            StatusMessage1.MessageType = StatusMessageType.Success;
                            StatusMessage1.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;

                            scope.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        StatusMessage1.MessageType = StatusMessageType.Error;
                        StatusMessage1.Message = ExceptionHelper.GetExceptionMessage(ex);
                    }
                }
            }
        }

        private string GetUploadedProfilePicPath(string username, out bool success)
        {
            var imageURL = string.Empty;
            success = false;

            if (Utilities.IsFileSizeOK(ProfilePicFileUpload.FileContent, 2))
            {
                var filename = username;
                var fileExtension = Path.GetExtension(ProfilePicFileUpload.FileName);
                var fullFileName = filename + fileExtension;
                var relativeStoragePath = LocalStorages.Profiles;
                var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
                var absoluteFullFilePath = Server.MapPath(relativeFullFilePath);

                Stream originalStream = new MemoryStream();

                try
                {
                    originalStream = (Stream)ProfilePicFileUpload.FileContent;
                    ImageHelper.Resize(300, originalStream, false, absoluteFullFilePath);
                    imageURL = relativeFullFilePath;
                    success = true;
                }
                catch (Exception ex)
                {
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                    success = false;
                }
                finally
                {
                    originalStream.Dispose();
                }
            }
            else
            {
                StatusMessage.Message = "File Size Too Large. Upload File Of Size Less Than 2 MB";
                success = false;
            }

            return imageURL;
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
            NameTextBox.ValidationErrorMessage = Validator.NameValidationErrorMessage;
            NameTextBox.ValidationExpression = Validator.NameValidationExpression;

            DateOfBirthTextBox.SmallLabelText = "date format: " + Validator.DateParseExpression;
            DateOfBirthTextBox.DateFormat = Validator.DateParseExpression;
            DateOfBirthTextBox.Format = Validator.DateParseExpression;
            DateOfBirthTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
            DateOfBirthTextBox.ValidationExpression = Validator.CalendarValidationExpression;

            GenderDropDownList.Items.Add(new ListItem(Gender.Male.ToString()));
            GenderDropDownList.Items.Add(new ListItem(Gender.Female.ToString()));
            GenderDropDownList.Items.Add(new ListItem(Gender.Unspecified.ToString()));

            ImageRegularExpressionValidator.ValidationExpression = Validator.ImageValidationExpression;
            ImageRegularExpressionValidator.ErrorMessage = Validator.ImageValidationErrorMessage;

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

        private void LoginBlockedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var username = Request.QueryString["Username"];

            var user = Membership.GetUser(username);

            if (user != null)
            {
                user.IsApproved = LoginBlockedCheckBox.Checked;

                try
                {
                    Membership.UpdateUser(user);
                    StatusMessage.MessageType = StatusMessageType.Success;
                    StatusMessage.Message = "changes saved successfully";
                }
                catch (Exception ex)
                {
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                    ErrorLogger.LogError(ex);
                }
            }
        }

        private void UnlockUserCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var username = Request.QueryString["Username"];

            var user = Membership.GetUser(username);

            if (user != null)
            {
                try
                {
                    user.UnlockUser();
                    Membership.UpdateUser(user);
                    StatusMessage.MessageType = StatusMessageType.Success;
                    StatusMessage.Message = "User unlocked successfully";
                }
                catch (Exception ex)
                {
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                    ErrorLogger.LogError(ex);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("UserManagePage"));

                if (!string.IsNullOrWhiteSpace(Request.QueryString["Username"]))
                {
                    var username = Request.QueryString["Username"];

                    var user = Membership.GetUser(username);

                    if (user != null)
                    {
                        LoginBlockedCheckBox.Checked = user.IsApproved;

                        UnlockUserCheckBox.Checked = user.IsLockedOut;
                        UnlockUserCheckBox.Enabled = user.IsLockedOut;

                        using (var context = new MembershipEntities())
                        {
                            var userEntity = UserBL.GetUserByUsername(username, context);

                            if (userEntity != null)
                            {
                                InitializeCommonUI(username);

                                InitializeCategoryUI(UserCategoryHelper.GetCategorySetting(UserBL.GetUserCategoryID(userEntity), PageSetting.Manage));

                                var roleSetting = UserRoleHelper.GetRoleSetting(userEntity.UserRole);

                                MembershipHelper.PopulateRoleList(RolesDropDownList, roleSetting.HideSuperAdmin);
                                UserCategoryBL.PopulateUserCategoryList(CategoryDropDownList, context);

                                ProfilePicImage.ImageUrl = userEntity.ProfilePic;
                                NameTextBox.Text = userEntity.Name;
                                RolesDropDownList.SelectedValue = userEntity.UserRole;
                                CategoryDropDownList.SelectedValue = UserBL.GetUserCategoryID(userEntity).ToString();
                                GenderDropDownList.SelectedValue = userEntity.Gender;
                                DateOfBirthTextBox.Text = DataParser.GetDateFormattedString(userEntity.DateOfBirth);

                                EmailTextBox.Text = userEntity.Email;

                                AllowProfilePicDeletion(userEntity.ProfilePic);
                            }
                            else
                            {
                                Response.Redirect("~/UI/Pages/Common/UserList.aspx", false);
                                Context.ApplicationInstance.CompleteRequest();
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("~/UI/Pages/Common/UserList.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Common/UserList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }

            FormToolbar2.Save += FormToolbar2_Save;
            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
            LoginBlockedCheckBox.CheckedChanged += LoginBlockedCheckBox_CheckedChanged;
            UnlockUserCheckBox.CheckedChanged += UnlockUserCheckBox_CheckedChanged;
            DeleteImageButton.Click += DeleteImageButton_Click;
            UploadImageButton.Click += UploadImageButton_Click;
            WebCamUploadButton.Click += WebCamUploadButton_Click;
            CategoryDropDownList.SelectedIndexChanged += CategoryDropDownList_SelectedIndexChanged;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }

        protected void UploadImageButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var username = Request.QueryString["Username"];

                var imageURL = string.Empty;
                var success = false;

                if (ProfilePicFileUpload.HasFile)
                {
                    imageURL = GetUploadedProfilePicPath(username, out success);
                }

                if (success)
                {
                    using (var context = new MembershipEntities())
                    {
                        var userEntity = UserBL.GetUserByUsername(username, context);

                        userEntity.ProfilePic = imageURL;

                        try
                        {
                            UserBL.Save(userEntity, context);

                            AllowProfilePicDeletion(imageURL);
                            ProfilePicImage.ImageUrl = imageURL;

                            StatusMessage.MessageType = StatusMessageType.Success;
                            StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
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

        protected void WebCamUploadButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(WebCamComponent.ImageBase64))
            {
                var username = Request.QueryString["Username"];

                using (var context = new MembershipEntities())
                {
                    var userEntity = UserBL.GetUserByUsername(username, context);

                    var url = Path.Combine(LocalStorages.Profiles, userEntity.Username) + ".jpg";
                    var absUrl = Server.MapPath(url);

                    userEntity.ProfilePic = url;

                    try
                    {
                        ImageHelper.Compress(70, ImageHelper.Resize(300, ImageHelper.Base64ToStream(WebCamComponent.ImageBase64), false), absUrl);

                        UserBL.Save(userEntity, context);

                        ProfilePicImage.ImageUrl = url;
                        AllowProfilePicDeletion(url);

                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
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
}