using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.UI.WebControls;

namespace OWDARO.UI.Pages.Common
{
    public partial class UserList : System.Web.UI.Page
    {
        private bool CheckSearch(MS_Users user, string searchTerm, MembershipEntities context)
        {
            if (user.Email.NullableContains(searchTerm) || user.Name.NullableContains(searchTerm) || user.Username.NullableContains(searchTerm) || user.UserRole.NullableContains(searchTerm) || user.Gender.NullableContains(searchTerm))
            {
                return true;
            }

            foreach (MS_UsersData data in user.MS_UsersData)
            {
                if (data.UserData.NullableContains(searchTerm) || data.UsersDataCategory.NullableContains(searchTerm))
                {
                    return true;
                }
            }

            foreach (MS_UserAdresses address in user.MS_UserAdresses)
            {
                if (address.City.NullableContains(searchTerm) || address.Country.NullableContains(searchTerm) || address.State.NullableContains(searchTerm) || address.StreetName.NullableContains(searchTerm) || address.ZipCode.NullableContains(searchTerm))
                {
                    return true;
                }
            }

            foreach (MS_UserEducations education in user.MS_UserEducations)
            {
                if (education.InstituteName.NullableContains(searchTerm) || education.Stream.NullableContains(searchTerm) || education.MS_EducationQualificationTypes.Title.NullableContains(searchTerm) || education.MS_EducationQualificationTypes.Description.NullableContains(searchTerm))
                {
                    return true;
                }
            }

            foreach (MS_UserWorks work in user.MS_UserWorks)
            {
                if (work.City.NullableContains(searchTerm) || work.Country.NullableContains(searchTerm) || work.Description.NullableContains(searchTerm) || work.Organization.NullableContains(searchTerm) || work.Position.NullableContains(searchTerm))
                {
                    return true;
                }
            }

            return false;
        }

        private string GetSortDirection(string column)
        {
            var sortDirection = "DESC";

            var sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    var lastDirection = ViewState["SortDirection"] as string;

                    if ((lastDirection != null) && (lastDirection == "DESC"))
                    {
                        sortDirection = "ASC";
                    }
                }
            }

            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        private bool GetSuperAdminVisibility()
        {
            return !UserRoleHelper.GetRoleSetting(UserBL.GetUserRole()).HideSuperAdmin;
        }

        private bool IsSuperAdminVisible()
        {
            var keyValue = false;
            var performanceKey = "_SuperAdminVisibility__";

            Func<bool> fnc = new Func<bool>(GetSuperAdminVisibility);

            var args = new object[] { };

            Utilities.GetPerformance<bool>(PerformanceMode.None, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        protected void CategoriesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchTermTextBox.Text = string.Empty;

            using (var context = new MembershipEntities())
            {
                GridView1.DataSource = GetUsersOnFilters(context).ToList();
                GridView1.DataBind();
            }
        }

        protected void ExportToExcelButton_Click(object sender, EventArgs e)
        {
            var now = Utilities.DateTimeNow();

            using (var context = new MembershipEntities())
            {
                if (IsSuperAdminVisible())
                {
                    var usersQuery = (from users in context.MS_Users
                                      select users);
                    Utilities.ExportExcel(Controls, usersQuery.ToList(), "All_Users" + now);
                }
                {
                    var usersQuery = (from users in context.MS_Users
                                      where users.UserRole != UserRoles.SuperAdminRole
                                      select users);
                    Utilities.ExportExcel(Controls, usersQuery.ToList(), "All_Users" + now);
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            using (var context = new MembershipEntities())
            {
                if (!string.IsNullOrWhiteSpace(SearchTermTextBox.Text))
                {
                    GridView1.DataSource = GetSearchResults(context);
                    GridView1.PageIndex = e.NewPageIndex;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = GetUsersOnFilters(context).ToList();
                    GridView1.PageIndex = e.NewPageIndex;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            using (var context = new MembershipEntities())
            {
                if (!string.IsNullOrWhiteSpace(SearchTermTextBox.Text))
                {
                    GridView1.DataSource = GetSearchResults(context).AsQueryable().OrderBy(e.SortExpression + " " + GetSortDirection(e.SortExpression)).ToList();
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = GetUsersOnFilters(context).OrderBy(e.SortExpression + " " + GetSortDirection(e.SortExpression)).ToList();
                    GridView1.DataBind();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("UserListPage"));

                using (var context = new MembershipEntities())
                {
                    MembershipHelper.PopulateRoleList(RolesDropDownList, !IsSuperAdminVisible());
                    UserCategoryBL.PopulateUserCategoryList(CategoriesDropDownList, context);

                    RolesDropDownList.Items.Insert(1, new ListItem("All", "0"));
                    CategoriesDropDownList.Items.Insert(0, new ListItem("All", "0"));
                }
            }

            Utilities.RegisterAsyncPostBackControl(Page, CategoriesDropDownList);
            Utilities.RegisterAsyncPostBackControl(Page, RolesDropDownList);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Title = AppConfig.SiteName + ": User's List";
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }

        protected void RolesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchTermTextBox.Text = string.Empty;

            using (var context = new MembershipEntities())
            {
                GridView1.DataSource = GetUsersOnFilters(context).ToList();
                GridView1.DataBind();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            using (var context = new MembershipEntities())
            {
                GridView1.DataSource = GetSearchResults(context);
                GridView1.DataBind();
            }
        }

        public IQueryable<MS_Users> GetAllUsers(MembershipEntities context)
        {
            return (from users in context.MS_Users
                    select users);
        }

        public List<MS_Users> GetSearchResults(MembershipEntities context)
        {
            var searchTerm = SearchTermTextBox.Text;

            var usersList = new List<MS_Users>();

            foreach (MS_Users user in GetUsersList(context))
            {
                var isMatch = false;

                isMatch = CheckSearch(user, searchTerm, context);

                if (isMatch)
                {
                    usersList.Add(user);
                }
            }

            return usersList;
        }

        public IQueryable<MS_Users> GetUsersList(MembershipEntities context)
        {
            if (IsSuperAdminVisible())
            {
                return GetAllUsers(context);
            }
            else
            {
                return GetUsersWithoutSuperAdmin(context);
            }
        }

        public IQueryable<MS_Users> GetUsersOnFilters(MembershipEntities context)
        {
            var users = GetUsersList(context);

            var selectedRole = RolesDropDownList.SelectedValue;
            var userCategoryID = DataParser.IntParse(CategoriesDropDownList.SelectedValue);

            if (selectedRole != "0")
            {
                users = users.Where(c => c.UserRole == selectedRole);
            }

            if (userCategoryID == -1)
            {
                users = users.Where(c => c.UserCategoryID == null);
            }
            else
            {
                if (userCategoryID != 0)
                {
                    users = users.Where(c => c.UserCategoryID == userCategoryID);
                }
            }
            return users;
        }

        public IQueryable<MS_Users> GetUsersWithoutSuperAdmin(MembershipEntities context)
        {
            return GetAllUsers(context).Where(c => c.UserRole != UserRoles.SuperAdminRole);
        }
    }
}