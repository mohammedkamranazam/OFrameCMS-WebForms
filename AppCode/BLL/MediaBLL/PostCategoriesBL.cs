using ProjectJKL.AppCode.DAL.MediaModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OWDARO.BLL.MediaBLL
{
    public static class PostCategoriesBL
    {
        public static void Add(ME_PostCategories entity)
        {
            using (var context = new MediaEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(ME_PostCategories entity, MediaEntities context)
        {
            context.ME_PostCategories.Add(entity);

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

            context.ME_PostCategories.Remove(query);

            context.SaveChanges();
        }

        public static bool Exists(string title)
        {
            using (var context = new MediaEntities())
            {
                return Exists(title, context);
            }
        }

        public static bool Exists(string title, MediaEntities context)
        {
            var query = (from set in context.ME_PostCategories
                         where set.Title == title
                         select set);
            return query.Any();
        }

        public static bool Exists(string title, int? parentCategoryID)
        {
            using (var context = new MediaEntities())
            {
                return Exists(title, parentCategoryID, context);
            }
        }

        public static bool Exists(string title, int? parentCategoryID, MediaEntities context)
        {
            return (from set in context.ME_PostCategories
                    where set.Title == title && set.ParentPostCategoryID == parentCategoryID
                    select set).Any();
        }

        public static ME_PostCategories GetObjectByID(int id)
        {
            using (var context = new MediaEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static ME_PostCategories GetObjectByID(int id, MediaEntities context)
        {
            return (from set in context.ME_PostCategories
                    where set.PostCategoryID == id
                    select set).FirstOrDefault();
        }

        public async static Task<ME_PostCategories> GetObjectByIDAsync(int id)
        {
            using (var context = new MediaEntities())
            {
                return await GetObjectByIDAsync(id, context);
            }
        }

        public async static Task<ME_PostCategories> GetObjectByIDAsync(int id, MediaEntities context)
        {
            return await (from set in context.ME_PostCategories
                          where set.PostCategoryID == id
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
            var query = GetObjectByID(id, context);

            return query.ME_Posts.Any() || query.ME_ChildPostCategories.Any();
        }

        public static void Save(MediaEntities context)
        {
            context.SaveChanges();
        }
    }
}