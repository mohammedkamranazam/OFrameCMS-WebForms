using ProjectJKL.AppCode.DAL.MembershipModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OWDARO.BLL.MembershipBLL
{
    public static class UsersDataBL
    {
        public static void AddUsersData(MS_UsersData entity)
        {
            using (var context = new MembershipEntities())
            {
                AddUsersData(entity, context);
            }
        }

        public static void AddUsersData(MS_UsersData entity, MembershipEntities context)
        {
            context.MS_UsersData.Add(entity);

            context.SaveChanges();
        }

        public static async Task AddUsersDataAsync(MS_UsersData entity)
        {
            using (var context = new MembershipEntities())
            {
                await AddUsersDataAsync(entity, context);
            }
        }

        public static async Task AddUsersDataAsync(MS_UsersData entity, MembershipEntities context)
        {
            context.MS_UsersData.Add(entity);

            await context.SaveChangesAsync();
        }

        public static void DeleteUserData(int id)
        {
            using (var context = new MembershipEntities())
            {
                DeleteUserData(id, context);
            }
        }

        public static void DeleteUserData(int id, MembershipEntities context)
        {
            var query = (from userDatas in context.MS_UsersData
                         where userDatas.UserDataID == id
                         select userDatas);

            if (query.Any())
            {
                context.MS_UsersData.Remove(query.FirstOrDefault());
                context.SaveChanges();
            }
        }

        public static async Task DeleteUserDataAsync(int id)
        {
            using (var context = new MembershipEntities())
            {
                await DeleteUserDataAsync(id, context);
            }
        }

        public static async Task DeleteUserDataAsync(int id, MembershipEntities context)
        {
            var query = await (from userDatas in context.MS_UsersData
                               where userDatas.UserDataID == id
                               select userDatas).FirstOrDefaultAsync();

            if (query != null)
            {
                context.MS_UsersData.Remove(query);
                await context.SaveChangesAsync();
            }
        }

        public static MS_UsersData GetUsersData(string username, string category)
        {
            using (var context = new MembershipEntities())
            {
                return GetUsersData(username, category, context);
            }
        }

        public static MS_UsersData GetUsersData(string username, string category, MembershipEntities context)
        {
            var usersDataEntity = new MS_UsersData();
            usersDataEntity.Username = username;
            usersDataEntity.UsersDataCategory = category;
            usersDataEntity.UserData = string.Empty;

            var query = (from usersData in context.MS_UsersData
                         where usersData.Username == username && usersData.UsersDataCategory == category
                         select usersData);

            if (query.Any())
            {
                usersDataEntity.UserData = query.First().UserData;
            }

            return usersDataEntity;
        }

        public static IQueryable<MS_UsersData> GetUsersDataCollection(string username, string category)
        {
            using (var context = new MembershipEntities())
            {
                return GetUsersDataCollection(username, category, context);
            }
        }

        public static IQueryable<MS_UsersData> GetUsersDataCollection(string username, string category, MembershipEntities context)
        {
            var query = (from usersData in context.MS_UsersData
                         where usersData.Username == username && usersData.UsersDataCategory == category
                         select usersData);

            return query;
        }

        public static async Task<List<MS_UsersData>> GetUsersDataCollectionAsync(string username, string category)
        {
            using (var context = new MembershipEntities())
            {
                return await GetUsersDataCollectionAsync(username, category, context);
            }
        }

        public static async Task<List<MS_UsersData>> GetUsersDataCollectionAsync(string username, string category, MembershipEntities context)
        {
            return await (from usersData in context.MS_UsersData
                          where usersData.Username == username && usersData.UsersDataCategory == category
                          select usersData).ToListAsync();
        }

        public static IQueryable<MS_UsersData> GetUsersDatas(string username)
        {
            using (var context = new MembershipEntities())
            {
                return GetUsersDatas(username, context);
            }
        }

        public static IQueryable<MS_UsersData> GetUsersDatas(string username, MembershipEntities context)
        {
            var query = (from usersData in context.MS_UsersData
                         where usersData.Username == username
                         select usersData);

            return query;
        }

        public static void UpdateUsersData(MS_UsersData entity)
        {
            using (var context = new MembershipEntities())
            {
                var query = (from usersData in context.MS_UsersData
                             where usersData.Username == entity.Username && usersData.UsersDataCategory == entity.UsersDataCategory
                             select usersData);

                if (query.Any())
                {
                    query.FirstOrDefault().UserData = entity.UserData;

                    context.SaveChanges();
                }
                else
                {
                    AddUsersData(entity, context);
                }
            }
        }
    }
}