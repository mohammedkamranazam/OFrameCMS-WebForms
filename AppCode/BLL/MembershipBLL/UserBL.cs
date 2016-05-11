using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Security;

namespace OWDARO.BLL.MembershipBLL
{
    public static class UserBL
    {
        public static bool Add(MS_Users entity)
        {
            using (var context = new MembershipEntities())
            {
                return Add(entity, context);
            }
        }

        public static bool Add(MS_Users entity, MembershipEntities context)
        {
            var success = false;

            context.MS_Users.Add(entity);

            try
            {
                context.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
            }

            return success;
        }

        public static async Task<bool> AddAsync(MS_Users entity)
        {
            using (var context = new MembershipEntities())
            {
                return await AddAsync(entity, context);
            }
        }

        public static async Task<bool> AddAsync(MS_Users entity, MembershipEntities context)
        {
            var success = false;

            context.MS_Users.Add(entity);

            try
            {
                await context.SaveChangesAsync();
                success = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
            }

            return success;
        }

        public static bool Delete(string username)
        {
            var success = false;

            var user = Membership.GetUser(username);

            if (user != null)
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions()
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted
                }))
                {
                    Membership.DeleteUser(username, true);

                    using (var context = new MembershipEntities())
                    {
                        var userQuery = GetUserByUsername(username, context);

                        var usersDataQuery = userQuery.MS_UsersData;

                        if (usersDataQuery != null)
                        {
                            foreach (MS_UsersData usersData in usersDataQuery)
                            {
                                context.MS_UsersData.Remove(usersData);
                            }
                        }

                        try
                        {
                            context.SaveChanges();

                            context.MS_Users.Remove(userQuery);
                            context.SaveChanges();

                            success = true;
                        }
                        catch (Exception ex)
                        {
                            success = false;
                            ErrorLogger.LogError(ex);
                        }
                    }

                    scope.Complete();
                }
            }

            return success;
        }

        public static string GetRootFolder()
        {
            var rootFolder = "~/";

            var user = Membership.GetUser();

            if (user != null)
            {
                rootFolder = GetRootFolder(user.UserName);
            }

            return rootFolder;
        }

        public static string GetRootFolder(string username)
        {
            return UserRoleHelper.GetRoleSetting(GetUserRole(username)).Path;
        }

        public static MS_Users GetUserByUsername(string username)
        {
            using (var context = new MembershipEntities())
            {
                return GetUserByUsername(username, context);
            }
        }

        public static MS_Users GetUserByUsername(string username, MembershipEntities context)
        {
            var query = (from users in context.MS_Users
                         where users.Username == username
                         select users).FirstOrDefault();

            return query;
        }

        public static async Task<MS_Users> GetUserByUsernameAsync(string username)
        {
            using (var context = new MembershipEntities())
            {
                return await GetUserByUsernameAsync(username, context);
            }
        }

        public static async Task<MS_Users> GetUserByUsernameAsync(string username, MembershipEntities context)
        {
            return await (from users in context.MS_Users
                          where users.Username == username
                          select users).FirstOrDefaultAsync();
        }

        public static int GetUserCategoryID()
        {
            var categoryID = -1;

            var user = Membership.GetUser();

            if (user != null)
            {
                categoryID = GetUserCategoryID(user.UserName);
            }

            return categoryID;
        }

        public static int GetUserCategoryID(string username)
        {
            return GetUserCategoryID(GetUserByUsername(username));
        }

        public static int GetUserCategoryID(MS_Users entity)
        {
            return entity.UserCategoryID == null ? -1 : (int)entity.UserCategoryID;
        }

        public static string GetUsernameFromEmail(string email)
        {
            using (var context = new MembershipEntities())
            {
                return GetUsernameFromEmail(email, context);
            }
        }

        public static string GetUsernameFromEmail(string email, MembershipEntities context)
        {
            string username = null;

            var query = (from users in context.MS_Users
                         where users.Email == email
                         select users);

            if (query.Any())
            {
                username = query.FirstOrDefault().Username;
            }

            return username;
        }

        public static MS_Users GetUserFromEmail(string email)
        {
            using (var context = new MembershipEntities())
            {
                return GetUserFromEmail(email, context);
            }
        }

        public static MS_Users GetUserFromEmail(string email, MembershipEntities context)
        {
            return (from set in context.MS_Users
                    where set.Email == email
                    select set).FirstOrDefault();
        }

        public static string GetUserRole()
        {
            var role = UserRoles.AnonymousRole;

            var user = Membership.GetUser();

            if (user != null)
            {
                role = GetUserRole(user.UserName);
            }

            return role;
        }

        public static string GetUserRole(string username)
        {
            return Roles.GetRolesForUser(username).Single();
        }

        public static bool IfUserExistsFromEmail(string email, out string username)
        {
            using (var context = new MembershipEntities())
            {
                return IfUserExistsFromEmail(email, out username, context);
            }
        }

        public static bool IfUserExistsFromEmail(string email, out string username, MembershipEntities context)
        {
            var boolValue = false;
            username = string.Empty;

            var query = (from users in context.MS_Users
                         where users.Email == email
                         select users);

            if (query.Any())
            {
                boolValue = true;
                username = query.FirstOrDefault().Username;
            }

            return boolValue;
        }

        public static async Task<MS_Users> IfUserExistsFromEmailAsync(string email)
        {
            using (var context = new MembershipEntities())
            {
                return await IfUserExistsFromEmailAsync(email, context);
            }
        }

        public static async Task<MS_Users> IfUserExistsFromEmailAsync(string email, MembershipEntities context)
        {
            return await (from users in context.MS_Users
                          where users.Email == email
                          select users).FirstOrDefaultAsync();
        }

        public static void Save(MS_Users entity)
        {
            using (var context = new MembershipEntities())
            {
                Save(entity, context);
            }
        }

        public static void Save(MS_Users entity, MembershipEntities context)
        {
            var query = GetUserByUsername(entity.Username, context) as MS_Users;

            query.DateOfBirth = entity.DateOfBirth;
            query.Email = entity.Email;
            query.Gender = entity.Gender;
            query.Name = entity.Name;
            query.ProfilePic = entity.ProfilePic;
            query.SecurityAnswer = entity.SecurityAnswer;
            query.SecurityQuestion = entity.SecurityQuestion;
            query.UserRole = entity.UserRole;
            query.UserCategoryID = entity.UserCategoryID;

            context.SaveChanges();
        }
    }
}