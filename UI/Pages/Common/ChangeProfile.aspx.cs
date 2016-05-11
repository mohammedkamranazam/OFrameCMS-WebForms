using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Models;
using OWDARO.Performance;
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
    public partial class ChangeProfile : System.Web.UI.Page
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

        private void FillForm(MS_Users userEntity)
        {
            ProfilePicImage.ImageUrl = userEntity.ProfilePic;

            NameTextBox.Text = userEntity.Name;

            RolesDropDownList.SelectedValue = userEntity.UserRole;

            CategoryDropDownList.SelectedValue = UserBL.GetUserCategoryID(userEntity).ToString();

            GenderDropDownList.SelectedValue = userEntity.Gender;

            DateOfBirthTextBox.Text = DataParser.GetDateFormattedString(userEntity.DateOfBirth);
        }

        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder());
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var user = Membership.GetUser();

                if (user == null)
                {
                    return;
                }

                var username = user.UserName;

                using (var context = new MembershipEntities())
                {
                    using (var scope = new TransactionScope())
                    {
                        var userEntity = UserBL.GetUserByUsername(username, context);

                        userEntity.Name = NameTextBox.Text;

                        if (DateOfBirthPanel.Visible)
                        {
                            userEntity.DateOfBirth = DataParser.NullableDateTimeParse(DateOfBirthTextBox.Text);
                        }

                        if (RolesPanel.Visible)
                        {
                            var currentRole = userEntity.UserRole;
                            var newRole = RolesDropDownList.SelectedValue;

                            Roles.RemoveUserFromRole(username, currentRole);
                            Roles.AddUserToRole(username, newRole);
                            userEntity.UserRole = newRole;
                        }

                        if (CategoryPanel.Visible)
                        {
                            userEntity.UserCategoryID = CategoryDropDownList.GetNullableSelectedValue();
                        }

                        if (GenderPanel.Visible)
                        {
                            userEntity.Gender = GenderDropDownList.SelectedValue;
                        }

                        try
                        {
                            UserBL.Save(userEntity, context);

                            Initialize(userEntity, context);

                            StatusMessage.MessageType = StatusMessageType.Success;
                            StatusMessage.Message = "Successfully Saved";
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);
                            StatusMessage.MessageType = StatusMessageType.Error;
                            StatusMessage.Message = "Error while saving the user profile" + ExceptionHelper.GetExceptionMessage(ex);
                        }

                        scope.Complete();
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
                StatusMessage.MessageType = StatusMessageType.Warning;
                StatusMessage.Message = "File Size Too Large. Upload File Of Size Less Than 2 MB";
                success = false;
            }

            return imageURL;
        }

        private void Initialize(MS_Users userEntity, MembershipEntities context)
        {
            InitializeRoleUI(UserRoleHelper.GetRoleSetting(userEntity.UserRole), context);

            InitializeCategoryUI(UserCategoryHelper.GetCategorySetting(UserBL.GetUserCategoryID(userEntity), PageSetting.Manage));

            FillForm(userEntity);
        }

        private void InitializeCategoryUI(UserCategorySettings categorySetting)
        {
            DateOfBirthPanel.Visible = categorySetting.ShowDateOfBirth;
            GenderPanel.Visible = categorySetting.ShowGender;
        }

        private void InitializeCommonUI()
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
        }

        private void InitializeRoleUI(UserRoleSettings roleSetting, MembershipEntities context)
        {
            RolesPanel.Visible = roleSetting.ShowRoles;

            if (RolesPanel.Visible)
            {
                MembershipHelper.PopulateRoleList(RolesDropDownList, roleSetting.HideSuperAdmin);
            }

            CategoryPanel.Visible = roleSetting.ShowCategory;

            if (CategoryPanel.Visible)
            {
                UserCategoryBL.PopulateUserCategoryList(CategoryDropDownList, context);
            }
        }

        protected void CategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = CategoryDropDownList.GetSelectedValue();
            InitializeCategoryUI(UserCategoryHelper.GetCategorySetting(selectedValue, PageSetting.Manage));
        }

        protected void DeleteImageButton_Click(object sender, EventArgs e)
        {
            var user = Membership.GetUser();

            if (user != null)
            {
                using (var context = new MembershipEntities())
                {
                    var userEntity = UserBL.GetUserByUsername(user.UserName, context);

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
                        SessionHelper.Add<string>(Constants.Keys.AvatarPathPerformanceKey, userEntity.ProfilePic);

                        DeleteImageButton.Visible = false;
                        UploadImageButton.Visible = true;

                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = "Profile Image Deleted Successfully";
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        StatusMessage.MessageType = StatusMessageType.Error;
                        StatusMessage.Message = "Error While Deleting Profile Image" + ExceptionHelper.GetExceptionMessage(ex);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("ChangeProfilePage"));

                var user = Membership.GetUser();

                if (user != null)
                {
                    using (var context = new MembershipEntities())
                    {
                        var userEntity = UserBL.GetUserByUsername(user.UserName, context);

                        InitializeCommonUI();
                        Initialize(userEntity, context);

                        AllowProfilePicDeletion(userEntity.ProfilePic);
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/LogOn/", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }

            CategoryDropDownList.SelectedIndexChanged += new EventHandler(CategoryDropDownList_SelectedIndexChanged);
            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += new EventHandler(FormToolbar1_Cancel);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetRoleThemeFile();
        }

        protected void UploadImageButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var user = Membership.GetUser();

                if (user == null)
                {
                    return;
                }

                var imageURL = string.Empty;
                var success = false;

                if (ProfilePicFileUpload.HasFile)
                {
                    imageURL = GetUploadedProfilePicPath(user.UserName, out success);
                }

                if (success)
                {
                    using (var context = new MembershipEntities())
                    {
                        var userEntity = UserBL.GetUserByUsername(user.UserName, context);
                        userEntity.ProfilePic = imageURL;

                        try
                        {
                            UserBL.Save(userEntity, context);

                            AllowProfilePicDeletion(imageURL);

                            ProfilePicImage.ImageUrl = imageURL;
                            SessionHelper.Add<string>(Constants.Keys.AvatarPathPerformanceKey, imageURL);

                            StatusMessage.MessageType = StatusMessageType.Success;
                            StatusMessage.Message = "Profile Image Updated Successfully";
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
                var user = Membership.GetUser();
                if (user == null)
                {
                    return;
                }

                using (var context = new MembershipEntities())
                {
                    var userEntity = UserBL.GetUserByUsername(user.UserName, context);

                    var url = Path.Combine(LocalStorages.Profiles, userEntity.Username) + ".jpg";
                    var absUrl = Server.MapPath(url);

                    userEntity.ProfilePic = url;

                    try
                    {
                        ImageHelper.Resize(300, ImageHelper.Base64ToStream(WebCamComponent.ImageBase64), false, absUrl);

                        UserBL.Save(userEntity, context);

                        ProfilePicImage.ImageUrl = url;
                        SessionHelper.Add<string>(Constants.Keys.AvatarPathPerformanceKey, url);

                        DeleteImageButton.Visible = true;
                        UploadImageButton.Visible = false;

                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = "Profile Image Updated Successfully";
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