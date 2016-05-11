using ProjectJKL.AppCode.DAL.MediaModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OWDARO.BLL.MediaBLL
{
    public static class PostsBL
    {
        public static void Add(ME_Posts entity)
        {
            using (var context = new MediaEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(ME_Posts entity, MediaEntities context)
        {
            context.ME_Posts.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(int id)
        {
            using (var context = new MediaEntities())
            {
                Delete(id, context);
            }
        }

        public static void Delete(int id, MediaEntities context)
        {
            var query = GetObjectByID(id, context);

            context.ME_Posts.Remove(query);

            context.SaveChanges();
        }

        public static bool Exists(string title, int categoryID)
        {
            using (var context = new MediaEntities())
            {
                return Exists(title, categoryID, context);
            }
        }

        public static bool Exists(string title, int categoryID, MediaEntities context)
        {
            return (from set in context.ME_Posts
                    where set.Title == title && set.PostCategoryID == categoryID
                    select set).Any();
        }

        public static ME_Posts GetObjectByID(int id)
        {
            using (var context = new MediaEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static ME_Posts GetObjectByID(int id, MediaEntities context)
        {
            return (from set in context.ME_Posts
                    where set.PostID == id
                    select set).FirstOrDefault();
        }

        public async static Task<ME_Posts> GetObjectByIDAsync(int id)
        {
            using (var context = new MediaEntities())
            {
                return await GetObjectByIDAsync(id, context);
            }
        }

        public async static Task<ME_Posts> GetObjectByIDAsync(int id, MediaEntities context)
        {
            return await (from set in context.ME_Posts
                          where set.PostID == id
                          select set).FirstOrDefaultAsync();
        }

        public static bool RelatedRecordsExists(int id)
        {
            using (var context = new MediaEntities())
            {
                return RelatedRecordsExists(id, context);
            }
        }

        public static bool RelatedRecordsExists(int id, MediaEntities context)
        {
            return false;
        }

        public static void Save(MediaEntities context)
        {
            context.SaveChanges();
        }
    }
}