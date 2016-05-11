using ProjectJKL.AppCode.DAL.GalleryModel;
using System.Linq;

namespace OWDARO.AppCode.BLL.GalleryBLL
{
    public static class FileTypesBL
    {
        public static void Add(GY_FileTypes entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_FileTypes entity, GalleryEntities context)
        {
            context.GY_FileTypes.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(int id)
        {
            using (var context = new GalleryEntities())
            {
                Delete(id, context);
            }
        }

        public static void Delete(int id, GalleryEntities context)
        {
            if (Exists(id, context))
            {
                var entity = GetObjectByID(id, context);

                context.GY_FileTypes.Remove(entity);

                context.SaveChanges();
            }
        }

        public static bool Exists(string title)
        {
            using (var context = new GalleryEntities())
            {
                return Exists(title, context);
            }
        }

        public static bool Exists(int id)
        {
            using (var context = new GalleryEntities())
            {
                return Exists(id, context);
            }
        }

        public static bool Exists(string title, GalleryEntities context)
        {
            var query = (from set in context.GY_FileTypes
                         where set.Title == title
                         select set);

            return query.Any();
        }

        public static bool Exists(int id, GalleryEntities context)
        {
            var query = (from set in context.GY_FileTypes
                         where set.FileTypeID == id
                         select set);

            return query.Any();
        }

        public static GY_FileTypes GetObjectByID(int id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_FileTypes GetObjectByID(int id, GalleryEntities context)
        {
            var query = (from set in context.GY_FileTypes
                         where set.FileTypeID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static bool RelatedRecordsExists(int id)
        {
            using (var context = new GalleryEntities())
            {
                return RelatedRecordsExists(id, context);
            }
        }

        public static bool RelatedRecordsExists(int id, GalleryEntities context)
        {
            var query = GetObjectByID(id, context);

            return query.GY_Files.Any();
        }

        public static void Save(GY_FileTypes entity)
        {
            using (var context = new GalleryEntities())
            {
                Save(context);
            }
        }

        public static void Save(GalleryEntities context)
        {
            context.SaveChanges();
        }
    }
}