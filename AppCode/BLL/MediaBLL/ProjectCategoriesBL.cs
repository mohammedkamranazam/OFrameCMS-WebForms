using ProjectJKL.AppCode.DAL.MediaModel;
using System.Linq;

namespace OWDARO.BLL.MediaBLL
{
    public static class ProjectCategoriesBL
    {
        public static void Add(ME_ProjectCategories entity)
        {
            using (var context = new MediaEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(ME_ProjectCategories entity, MediaEntities context)
        {
            context.ME_ProjectCategories.Add(entity);

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

            context.ME_ProjectCategories.Remove(query);

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
            var query = (from set in context.ME_ProjectCategories
                         where set.Title == title
                         select set);
            return query.Any();
        }

        public static ME_ProjectCategories GetObjectByID(int id)
        {
            using (var context = new MediaEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static ME_ProjectCategories GetObjectByID(int id, MediaEntities context)
        {
            var entity = (ME_ProjectCategories)(from set in context.ME_ProjectCategories
                                                where set.ProjectCategoryID == id
                                                select set).FirstOrDefault();

            return entity;
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