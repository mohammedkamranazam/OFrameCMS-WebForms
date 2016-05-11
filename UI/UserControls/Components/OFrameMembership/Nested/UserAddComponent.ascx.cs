using OWDARO.BLL.MembershipBLL;
using OWDARO.BLL.OFrameBLL;
using OWDARO.Helpers;
using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class UserAddComponent : System.Web.UI.UserControl
    {
        public string BoxTitle
        {
            set
            {
                BoxTitleLiteral.Text = value;
            }
        }

        public string HeaderTitle
        {
            set
            {
                HeaderTitleLiteral.Text = value;
            }
        }

        private void AddUsersAddressData(string username, MembershipEntities context)
        {
            AddUsersAddressData(username, HomeAddressComponent.Street, HomeAddressComponent.City, HomeAddressComponent.ZipCode, HomeAddressComponent.State, HomeAddressComponent.Country, UserDataCategories.HomeAddressCategory.Value, AddressPanel.Visible, context);
            AddUsersAddressData(username, BillingAddressComponent.Street, BillingAddressComponent.City, BillingAddressComponent.ZipCode, BillingAddressComponent.State, BillingAddressComponent.Country, UserDataCategories.BillingAddressCategory.Value, BillingAddressPanel.Visible, context);
            AddUsersAddressData(username, DeliveryAddressComponent.Street, DeliveryAddressComponent.City, DeliveryAddressComponent.ZipCode, DeliveryAddressComponent.State, DeliveryAddressComponent.Country, UserDataCategories.DeliveryAddressCategory.Value, DeliveryAddressPanel.Visible, context);
        }

        private void AddUsersAddressData(string username, string streetname, string city, string zipCode, string state, string country, string addressCategory, bool visible, MembershipEntities context)
        {
            if (visible)
            {
                var userAddressEntity = new MS_UserAdresses();
                userAddressEntity.StreetName = streetname;
                userAddressEntity.Username = username;
                userAddressEntity.City = city;
                userAddressEntity.ZipCode = zipCode;
                userAddressEntity.State = state;
                userAddressEntity.Country = country;
                userAddressEntity.AddressCategory = addressCategory;

                context.MS_UserAdresses.Add(userAddressEntity);
            }
        }

        private void AddUsersData(string username, MembershipEntities context)
        {
            AddUsersData(username, UserDataCategories.MobileCategory.Value, MobileTextBox.Text, MobilePanel.Visible, context);
            AddUsersData(username, UserDataCategories.LandlineCategory.Value, LandlineTextBox.Text, LandlinePanel.Visible, context);
            AddUsersData(username, UserDataCategories.FaxCategory.Value, FaxTextBox.Text, FaxPanel.Visible, context);
            AddUsersData(username, UserDataCategories.WebsiteCategory.Value, WebsiteTextBox.Text, WebsitePanel.Visible, context);
        }

        private void AddUsersData(string username, string category, string data, bool visible, MembershipEntities context)
        {
            if (visible && !string.IsNullOrWhiteSpace(data))
            {
                var usersData = new MS_UsersData();
                usersData.Username = username;
                usersData.UsersDataCategory = category;
                usersData.UserData = data;

                context.MS_UsersData.Add(usersData);
            }
        }

        private void AddUsersEducationData(string username, MembershipEntities context)
        {
            if (EducationPanel.Visible)
            {
                var userEducationEntity = new MS_UserEducations();

                userEducationEntity.EducationQualificationTypeID = DataParser.IntParse(EducationQualificationTypeDropDownList.SelectedValue);
                userEducationEntity.EndDate = DataParser.NullableDateTimeParse(EducationEndDateTextBox.Text);
                userEducationEntity.InstituteName = EducationInstituteTextBox.Text;
                userEducationEntity.StartDate = DataParser.NullableDateTimeParse(EducationStartDateTextBox.Text);
                userEducationEntity.Stream = EducationStreamTextBox.Text;
                userEducationEntity.Username = username;

                context.MS_UserEducations.Add(userEducationEntity);
            }
        }

        private void AddUsersWorkData(string username, MembershipEntities context)
        {
            if (WorkPanel.Visible)
            {
                var userWorkEntity = new MS_UserWorks();
                userWorkEntity.City = null;
                userWorkEntity.Country = null;
                userWorkEntity.Description = null;
                userWorkEntity.EndDate = null;
                userWorkEntity.Organization = WorkOrganizationTextBox.Text;
                userWorkEntity.Position = null;
                userWorkEntity.StartDate = null;
                userWorkEntity.Username = username;
                userWorkEntity.WorkHere = true;

                context.MS_UserWorks.Add(userWorkEntity);
            }
        }

        private bool AddUserToRole(string username, string role)
        {
            var success = false;

            try
            {
                Roles.AddUserToRole(username, role);
                StatusMessageLabel.MessageType = StatusMessageType.Success;
                StatusMessageLabel.Message = "User Successfully Added To Role";
                success = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                StatusMessageLabel.MessageType = StatusMessageType.Error;
                StatusMessageLabel.Message = "Error While Adding User To Role" + ExceptionHelper.GetExceptionMessage(ex);
                success = false;
            }

            return success;
        }

        private bool CreateUserLogin(string username, string password, string email, string question, string answer, bool isApproved)
        {
            var success = false;

            MembershipCreateStatus creationStatus;

            Membership.CreateUser(username, password, email, question, answer, isApproved, out creationStatus);

            switch (creationStatus)
            {
                case MembershipCreateStatus.Success:
                    success = true;
                    StatusMessageLabel.MessageType = StatusMessageType.Success;
                    StatusMessageLabel.Message = "user login created successfully";
                    break;

                case MembershipCreateStatus.InvalidPassword:
                    success = false;
                    StatusMessageLabel.MessageType = StatusMessageType.Info;
                    StatusMessageLabel.Message = "password is not formatted correctly";
                    break;

                case MembershipCreateStatus.InvalidEmail:
                    success = false;
                    StatusMessageLabel.MessageType = StatusMessageType.Info;
                    StatusMessageLabel.Message = "email is not formatted correctly";
                    break;

                case MembershipCreateStatus.UserRejected:
                    success = false;
                    StatusMessageLabel.MessageType = StatusMessageType.Error;
                    StatusMessageLabel.Message = "username already in use";
                    break;

                case MembershipCreateStatus.DuplicateEmail:
                    success = false;
                    StatusMessageLabel.MessageType = StatusMessageType.Warning;
                    StatusMessageLabel.Message = "email id already in use";
                    break;

                default:
                    success = false;
                    StatusMessageLabel.MessageType = StatusMessageType.Error;
                    StatusMessageLabel.Message = "error while creating user login. unknown error";
                    break;
            }

            return success;
        }

        private bool DeleteUserFromRole(string username, string role)
        {
            var success = false;

            try
            {
                Roles.RemoveUserFromRole(username, role);
                StatusMessageLabel.MessageType = StatusMessageType.Success;
                StatusMessageLabel.Message = "Successfully Removed User From Role";
                success = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                StatusMessageLabel.MessageType = StatusMessageType.Error;
                StatusMessageLabel.Message = "Error While Removing User From Role" + ExceptionHelper.GetExceptionMessage(ex);
                success = false;
            }

            return success;
        }

        private bool DeleteUserLogin(string username)
        {
            var success = false;

            try
            {
                Membership.DeleteUser(username, true);
                StatusMessageLabel.MessageType = StatusMessageType.Success;
                StatusMessageLabel.Message = "User Login Successfully Deleted";
                success = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                StatusMessageLabel.MessageType = StatusMessageType.Error;
                StatusMessageLabel.Message = "Error While Deleting User Login" + ExceptionHelper.GetExceptionMessage(ex);
                success = false;
            }

            return success;
        }

        private async void FormToolbar1_CustomClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var roleSetting = UserRoleHelper.GetRoleSetting(UserBL.GetUserRole());

                var signUpTask = UserSignUp(roleSetting);

                await Task<bool>.WhenAll(signUpTask); /*UNDER TEST-Remove await keyword*/

                if (await signUpTask)
                {
                    if (roleSetting.Login)
                    {
                        ActionOnLogin.Login(UsernameTextBox.Text, false, true, false, string.Empty);
                    }
                    else
                    {
                        StatusMessageLabel.MessageType = StatusMessageType.Success;
                        StatusMessageLabel.Message = "User Added Successfully";

                        ActionOnUserAdd.PerformAction();
                    }
                }
            }
        }

        private void InitializeCategoryUI(UserCategorySettings categorySetting)
        {
            EducationPanel.Visible = categorySetting.ShowEducation;

            WorkPanel.Visible = categorySetting.ShowWork;

            MobilePanel.Visible = categorySetting.ShowMobile;

            LandlinePanel.Visible = categorySetting.ShowLandline;

            FaxPanel.Visible = categorySetting.ShowFax;

            WebsitePanel.Visible = categorySetting.ShowWebsite;

            AddressPanel.Visible = categorySetting.ShowAddress;

            BillingAddressPanel.Visible = categorySetting.ShowBillingAddress;

            DeliveryAddressPanel.Visible = categorySetting.ShowDeliveryAddress;

            DateOfBirthPanel.Visible = categorySetting.ShowDateOfBirth;

            GenderPanel.Visible = categorySetting.ShowGender;
        }

        private void InitializeCommonUI(MembershipEntities context)
        {
            QAPanel.Visible = AppConfig.EnableQA;

            NameTextBox.ValidationErrorMessage = Validator.NameValidationErrorMessage;
            NameTextBox.ValidationExpression = Validator.NameValidationExpression;

            EmailIDTextBox.ValidationExpression = Validator.EmailValidationExpression;

            UsernameTextBox.ValidationExpression = Validator.UsernameValidationExpression;
            UsernameTextBox.ValidationErrorMessage = Validator.UsernameValidationErrorMessage;

            GenderDropDown.Items.Add(new ListItem(Gender.Male.ToString()));
            GenderDropDown.Items.Add(new ListItem(Gender.Female.ToString()));
            GenderDropDown.Items.Add(new ListItem(Gender.Unspecified.ToString()));

            DateOfBirthTextBox.SmallLabelText = "date format: " + Validator.DateParseExpression;
            DateOfBirthTextBox.DateFormat = Validator.DateParseExpression;
            DateOfBirthTextBox.Format = Validator.DateParseExpression;
            DateOfBirthTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
            DateOfBirthTextBox.ValidationExpression = Validator.CalendarValidationExpression;

            DeliveryAddressComponent.ZipCodeValidationErrorMessage = Validator.ZipCodeValidationErrorMessage;
            DeliveryAddressComponent.ZipCodeValidationExpression = Validator.ZipCodeValidationExpression;

            BillingAddressComponent.ZipCodeValidationErrorMessage = Validator.ZipCodeValidationErrorMessage;
            BillingAddressComponent.ZipCodeValidationExpression = Validator.ZipCodeValidationExpression;

            HomeAddressComponent.ZipCodeValidationErrorMessage = Validator.ZipCodeValidationErrorMessage;
            HomeAddressComponent.ZipCodeValidationExpression = Validator.ZipCodeValidationExpression;

            WebsiteTextBox.ValidationErrorMessage = Validator.UrlValidationErrorMessage;
            WebsiteTextBox.ValidationExpression = Validator.UrlValidationExpression;

            FaxTextBox.ValidationErrorMessage = Validator.LandlineValidationErrorMessage;
            FaxTextBox.ValidationExpression = Validator.LandlineValidationExpression;

            LandlineTextBox.ValidationErrorMessage = Validator.LandlineValidationErrorMessage;
            LandlineTextBox.ValidationExpression = Validator.LandlineValidationExpression;

            MobileTextBox.ValidationErrorMessage = Validator.MobileValidationErrorMessage;
            MobileTextBox.ValidationExpression = Validator.MobileValidationExpression;

            EducationStartDateTextBox.SmallLabelText = "date format: " + Validator.DateParseExpression;
            EducationStartDateTextBox.DateFormat = Validator.DateParseExpression;
            EducationStartDateTextBox.Format = Validator.DateParseExpression;
            EducationStartDateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
            EducationStartDateTextBox.ValidationExpression = Validator.CalendarValidationExpression;

            EducationEndDateTextBox.SmallLabelText = "date format: " + Validator.DateParseExpression;
            EducationEndDateTextBox.DateFormat = Validator.DateParseExpression;
            EducationEndDateTextBox.Format = Validator.DateParseExpression;
            EducationEndDateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
            EducationEndDateTextBox.ValidationExpression = Validator.CalendarValidationExpression;

            EducationQualificationTypeBL.PopulateEducationQualificationTypesList(EducationQualificationTypeDropDownList, context);
        }

        private void InitializeRoleUI(UserRoleSettings roleSetting, MembershipEntities context)
        {
            RolesPanel.Visible = roleSetting.ShowRoles;
            CategoryPanel.Visible = roleSetting.ShowCategory;

            if (roleSetting.RegistrationBlocked)
            {
                RegistrationForm.Visible = false;
                StatusMessageLabel.Message = "Registration Blocked";
                StatusMessageLabel.MessageType = StatusMessageType.Warning;
                return;
            }

            if (RolesPanel.Visible)
            {
                MembershipHelper.PopulateRoleList(RolesDropDownList, roleSetting.HideSuperAdmin);
            }

            if (CategoryPanel.Visible)
            {
                UserCategoryBL.PopulateUserCategoryList(CategoryDropDownList, context);

                CategoryDropDownList.SelectedValue = UserCategoryHelper.GetDefaultCategoryID().ToString();
            }
        }

        private async Task<bool> UserSignUp(UserRoleSettings roleSetting)
        {
            var roleName = UserRoles.DefaultRole;
            var roleRootFolder = UserRoles.DefaultRolePath;

            var userEntity = new MS_Users();

            userEntity.Username = UsernameTextBox.Text;
            userEntity.Name = NameTextBox.Text;
            userEntity.Email = EmailIDTextBox.Text;
            userEntity.ProfilePic = AppConfig.MaleAvatar;

            if (RolesPanel.Visible)
            {
                roleName = RolesDropDownList.SelectedValue;
                roleRootFolder = UserRoleHelper.GetRoleSetting(roleName).Path;
            }

            userEntity.UserRole = roleName;

            if (QAPanel.Visible)
            {
                userEntity.SecurityQuestion = SecurityQuestionTextBox.Text;
                userEntity.SecurityAnswer = SecurityAnswerTextBox.Text;
            }
            else
            {
                userEntity.SecurityQuestion = Guid.NewGuid().ToString();
                userEntity.SecurityAnswer = Guid.NewGuid().ToString();
            }

            if (CategoryPanel.Visible)
            {
                userEntity.UserCategoryID = CategoryDropDownList.GetNullableSelectedValue();
            }
            else
            {
                userEntity.UserCategoryID = UserCategoryHelper.GetDefaultCategoryID();
            }

            userEntity.DateOfBirth = DataParser.NullableDateTimeParse(DateOfBirthTextBox.Text);

            userEntity.Gender = Gender.Unspecified.ToString();
            userEntity.ProfilePic = GenderDropDown.GetGender().GetProfilePic();

            if (GenderPanel.Visible)
            {
                userEntity.Gender = GenderDropDown.SelectedValue;
            }

            if (Membership.GetUser(userEntity.Username) != null)
            {
                StatusMessageLabel.MessageType = StatusMessageType.Info;
                StatusMessageLabel.Message = "Username Already In Use. Try A Different Username";
                return false;
            }

            if (CreateUserLogin(userEntity.Username, PasswordConfirmationComponent.Password, userEntity.Email, userEntity.SecurityQuestion, userEntity.SecurityAnswer, true))
            {
                if (AddUserToRole(userEntity.Username, roleName))
                {
                    if (await UserBL.AddAsync(userEntity))
                    {
                        using (var context = new MembershipEntities())
                        {
                            AddUsersData(userEntity.Username, context);
                            AddUsersAddressData(userEntity.Username, context);
                            AddUsersWorkData(userEntity.Username, context);
                            AddUsersEducationData(userEntity.Username, context);

                            try
                            {
                                await context.SaveChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError(ex);
                            }
                        }

                        return true;
                    }
                    else
                    {
                        DeleteUserFromRole(userEntity.Username, roleName);
                        DeleteUserLogin(userEntity.Username);
                        StatusMessageLabel.MessageType = StatusMessageType.Error;
                        StatusMessageLabel.Message = "Error While Adding User Profile";
                    }
                }
                else
                {
                    DeleteUserLogin(userEntity.Username);
                }
            }

            return false;
        }

        protected void CategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCategoryUI(UserCategoryHelper.GetCategorySetting(CategoryDropDownList.GetSelectedValue(), PageSetting.Add));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var context = new MembershipEntities())
                {
                    InitializeCommonUI(context);

                    InitializeRoleUI(UserRoleHelper.GetRoleSetting(UserBL.GetUserRole()), context);

                    InitializeCategoryUI(UserCategoryHelper.GetCategorySetting(UserCategoryHelper.GetDefaultCategoryID().NullReverser(), PageSetting.Add));
                }
            }

            CategoryDropDownList.SelectedIndexChanged += new EventHandler(CategoryDropDownList_SelectedIndexChanged);
            FormToolbar1.CustomClick += new EventHandler(FormToolbar1_CustomClick);
        }
    }
}