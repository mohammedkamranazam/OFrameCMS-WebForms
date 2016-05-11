using ProjectJKL.AppCode.DAL.MediaModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OWDARO.BLL.MediaBLL
{
    public static class SubscriptionsBL
    {
        public static void Add(ME_Subscriptions entity)
        {
            using (var context = new MediaEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(ME_Subscriptions entity, MediaEntities context)
        {
            context.ME_Subscriptions.Add(entity);

            context.SaveChanges();
        }

        public static async Task AddAsync(ME_Subscriptions entity)
        {
            using (var context = new MediaEntities())
            {
                await AddAsync(entity, context);
            }
        }

        public static async Task AddAsync(ME_Subscriptions entity, MediaEntities context)
        {
            context.ME_Subscriptions.Add(entity);

            await context.SaveChangesAsync();
        }

        public static void Delete(string email)
        {
            using (var context = new MediaEntities())
            {
                Delete(email, context);
            }
        }

        public static void Delete(string email, MediaEntities context)
        {
            var query = GetObjectByID(email, context);

            context.ME_Subscriptions.Remove(query);

            context.SaveChanges();
        }

        public static bool Exists(string email)
        {
            using (var context = new MediaEntities())
            {
                return Exists(email, context);
            }
        }

        public static bool Exists(string email, MediaEntities context)
        {
            var query = (from set in context.ME_Subscriptions
                         where set.Email == email
                         select set);
            return query.Any();
        }

        public static async Task<bool> ExistsAsync(string email)
        {
            using (var context = new MediaEntities())
            {
                return await ExistsAsync(email, context);
            }
        }

        public static async Task<bool> ExistsAsync(string email, MediaEntities context)
        {
            return await (from set in context.ME_Subscriptions
                          where set.Email == email
                          select set).AnyAsync();
        }

        public static ME_Subscriptions GetObjectByID(string email)
        {
            using (var context = new MediaEntities())
            {
                return GetObjectByID(email, context);
            }
        }

        public static ME_Subscriptions GetObjectByID(string email, MediaEntities context)
        {
            var entity = (ME_Subscriptions)(from set in context.ME_Subscriptions
                                            where set.Email == email
                                            select set).FirstOrDefault();

            return entity;
        }

        public static bool RelatedRecordsExists(string email)
        {
            using (var context = new MediaEntities())
            {
                return RelatedRecordsExists(email, context);
            }
        }

        public static bool RelatedRecordsExists(string email, MediaEntities context)
        {
            return false;
        }

        public static void Save(MediaEntities context)
        {
            context.SaveChanges();
        }
    }
}