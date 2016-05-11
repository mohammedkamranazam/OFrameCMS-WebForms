using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.UI.UserControls.Controls;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Transactions;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.OFrameMembership
{
    public partial class UserProfileComponent : System.Web.UI.UserControl
    {
        public string DateOfBirth
        {
            get
            {
                return DateOfBirthTextBox.Text;
            }

            set
            {
                DateOfBirthTextBox.Text = value;
            }
        }

        public bool IsMale
        {
            get
            {
                return (GenderDropDownList.GetGender() == Gender.Male);
            }
        }

        public string Name
        {
            get
            {
                return NameTextBox.Text;
            }

            set
            {
                NameTextBox.Text = value;
            }
        }

        public Gender SelectedGender
        {
            get
            {
                return GenderDropDownList.GetGender();
            }

            set
            {
                GenderDropDownList.SelectedValue = value.ToString();
            }
        }

        public StatusMessageJQuery StatusMessageJquery
        {
            get
            {
                return StatusMessage;
            }
        }

        public int UserCategoryID
        {
            get
            {
                return CategoryDropDownList.GetSelectedValue();
            }

            set
            {
                CategoryDropDownList.SelectedValue = value.ToString();
            }
        }

        public string Username
        {
            get
            {
                return (ViewState["Username"] == null) ? string.Empty : ViewState["Username"].ToString();
            }

            set
            {
                ViewState["Username"] = value;
            }
        }

        public string UserRole
        {
            get
            {
                return RolesDropDownList.SelectedValue;
            }

            set
            {
                RolesDropDownList.SelectedValue = value;
            }
        }

        private void FillForm(MS_Users userEntity)
        {
            NameTextBox.Text = userEntity.Name;

            if (RolesPanel.Visible)
            {
                RolesDropDownList.SelectedValue = userEntity.UserRole;
            }

            if (CategoryPanel.Visible)
            {
                CategoryDropDownList.SelectedValue = UserBL.GetUserCategoryID(userEntity).ToString();
            }

            if (GenderPanel.Visible)
            {
                GenderDropDownList.SelectedValue = userEntity.Gender;
            }

            DateOfBirthTextBox.Text = DataParser.GetDateFormattedString(userEntity.DateOfBirth);
        }

        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(Username));
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            Page.Validate("UserProfileValidationGroup");

            if (Page.IsValid)
            {
                using (var context = new MembershipEntities())
                {
                    using (var scope = new TransactionScope())
                    {
                        var userEntity = UserBL.GetUserByUsername(Username, context);

                        userEntity.Name = NameTextBox.Text;

                        if (DateOfBirthPanel.Visible)
                        {
                            userEntity.DateOfBirth = DataParser.NullableDateTimeParse(DateOfBirthTextBox.Text);
                        }

                        if (RolesPanel.Visible)
                        {
                            var currentRole = userEntity.UserRole;
                            var newRole = RolesDropDownList.SelectedValue;

                            Roles.RemoveUserFromRole(Username, currentRole);
                            Roles.AddUserToRole(Username, newRole);
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

                            UserCategoryID = UserBL.GetUserCategoryID(userEntity);
                            UserRole = userEntity.UserRole;
                            Username = userEntity.Username;

                            ReInitialize(userEntity, context);

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

        private void InitializeCategoryUI(UserCategorySettings categorySetting)
        {
            DateOfBirthPanel.Visible = categorySetting.ShowDateOfBirth;
            GenderPanel.Visible = categorySetting.ShowGender;
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

        private void ReInitialize(MS_Users userEntity, MembershipEntities context)
        {
            InitializeRoleUI(UserRoleHelper.GetRoleSetting(UserRole), context);

            InitializeCategoryUI(UserCategoryHelper.GetCategorySetting(UserCategoryID, PageSetting.Manage));

            FillForm(userEntity);
        }

        protected void CategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCategoryUI(UserCategoryHelper.GetCategorySetting(CategoryDropDownList.GetSelectedValue(), PageSetting.Manage));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

                using (var context = new MembershipEntities())
                {
                    InitializeRoleUI(UserRoleHelper.GetRoleSetting(UserRole), context);

                    InitializeCategoryUI(UserCategoryHelper.GetCategorySetting(UserCategoryID, PageSetting.Manage));
                }
            }

            CategoryDropDownList.SelectedIndexChanged += new EventHandler(CategoryDropDownList_SelectedIndexChanged);
            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += new EventHandler(FormToolbar1_Cancel);
        }
    }
}