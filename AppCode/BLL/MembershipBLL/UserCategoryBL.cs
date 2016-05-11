using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System.Linq;
using System.Web.UI.WebControls;

namespace OWDARO.BLL.MembershipBLL
{
    public static class UserCategoryBL
    {
        public static MS_UserCategories GetCategoryByID(int id)
        {
            using (var context = new MembershipEntities())
            {
                return GetCategoryByID(id, context);
            }
        }

        public static MS_UserCategories GetCategoryByID(int id, MembershipEntities context)
        {
            var query = (from categories in context.MS_UserCategories
                         where categories.UserCategoryID == id
                         select categories);

            return query.Any() ? query.FirstOrDefault() : null;
        }

        public static MS_UserCategories GetCategoryByTitle(string title)
        {
            using (var context = new MembershipEntities())
            {
                return GetCategoryByTitle(title, context);
            }
        }

        public static MS_UserCategories GetCategoryByTitle(string title, MembershipEntities context)
        {
            var query = (from categories in context.MS_UserCategories
                         where categories.Title == title
                         select categories);

            return query.Any() ? query.FirstOrDefault() : null;
        }

        public static IQueryable<MS_UserCategories> GetUserCategories()
        {
            using (var context = new MembershipEntities())
            {
                return GetUserCategories(context);
            }
        }

        public static IQueryable<MS_UserCategories> GetUserCategories(MembershipEntities context)
        {
            return (from categories in context.MS_UserCategories
                    select categories);
        }

        public static void PopulateUserCategoryList(DropDownListAdv CategoryDropDownList)
        {
            using (var context = new MembershipEntities())
            {
                PopulateUserCategoryList(CategoryDropDownList.DropDownList, context);
            }
        }

        public static void PopulateUserCategoryList(DropDownList CategoryDropDownList)
        {
            using (var context = new MembershipEntities())
            {
                PopulateUserCategoryList(CategoryDropDownList, context);
            }
        }

        public static void PopulateUserCategoryList(DropDownListAdv CategoryDropDownList, MembershipEntities context)
        {
            PopulateUserCategoryList(CategoryDropDownList.DropDownList, context);
        }

        public static void PopulateUserCategoryList(DropDownList CategoryDropDownList, MembershipEntities context)
        {
            var query = GetUserCategories(context);

            if (query.Any())
            {
                CategoryDropDownList.DataTextField = "Title";
                CategoryDropDownList.DataValueField = "UserCategoryID";
                CategoryDropDownList.DataSource = query.ToList();
                CategoryDropDownList.DataBind();
            }

            CategoryDropDownList.Items.Insert(0, new ListItem("None", "-1"));
        }
    }
}