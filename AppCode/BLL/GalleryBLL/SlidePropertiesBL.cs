using ProjectJKL.AppCode.DAL.GalleryModel;
using System.Linq;

namespace OWDARO.BLL.GalleryBLL
{
    public static class SlidePropertiesBL
    {
        public static void Add(GY_SlideProperties entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_SlideProperties entity, GalleryEntities context)
        {
            context.GY_SlideProperties.Add(entity);
        }

        public static void Add(long slideID, string attribute, string value, bool isImageProperty)
        {
            using (var context = new GalleryEntities())
            {
                Add(slideID, attribute, value, isImageProperty, context);
            }
        }

        public static void Add(long slideID, string attribute, string value, bool isImageProperty, GalleryEntities context)
        {
            var entity = new GY_SlideProperties();
            entity.Attribute = attribute;
            entity.IsImageProperty = isImageProperty;
            entity.SlideID = slideID;
            entity.Value = value;

            if (Exists(slideID, attribute, context))
            {
                Update(entity, context);
            }
            else
            {
                Add(entity, context);
            }
        }

        public static void Delete(GY_SlideProperties entity)
        {
            using (var context = new GalleryEntities())
            {
                Delete(entity, context);
            }
        }

        public static void Delete(GY_SlideProperties entity, GalleryEntities context)
        {
            context.GY_SlideProperties.Remove(entity);

            context.SaveChanges();
        }

        public static bool Exists(long slideID, string attribute)
        {
            using (var context = new GalleryEntities())
            {
                return Exists(slideID, attribute, context);
            }
        }

        public static bool Exists(long slideID, string attribute, GalleryEntities context)
        {
            var query = (from set in context.GY_SlideProperties
                         where set.SlideID == slideID && set.Attribute == attribute
                         select set);

            return query.Any();
        }

        public static string GetAttributeValue(long slideID, string attribute, bool isImageProperty, GalleryEntities context)
        {
            var query = GetSlideProperty(slideID, attribute, isImageProperty, context);

            return query != null ? query.Value : string.Empty;
        }

        public static string GetAttributeValueString(long slideID, string attribute, bool isImageProperty, GalleryEntities context)
        {
            var query = GetSlideProperty(slideID, attribute, isImageProperty, context);

            return query != null ? string.Format("{0}='{1}'", query.Attribute, query.Value) : string.Empty;
        }

        public static GY_SlideProperties GetObjectByID(int id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_SlideProperties GetObjectByID(int id, GalleryEntities context)
        {
            var entity = (from set in context.GY_SlideProperties
                          where set.SlidePropertyID == id
                          select set).FirstOrDefault();

            return entity;
        }

        public static GY_SlideProperties GetSlideProperty(long slideID, string attribute, bool isImageProperty, GalleryEntities context)
        {
            return (from set in context.GY_SlideProperties
                    where set.SlideID == slideID && set.Attribute == attribute && set.IsImageProperty == isImageProperty
                    select set).FirstOrDefault();
        }

        public static void Save(GalleryEntities context)
        {
            context.SaveChanges();
        }

        public static void Update(GY_SlideProperties entity, GalleryEntities context)
        {
            var query = GetSlideProperty(entity.SlideID, entity.Attribute, entity.IsImageProperty, context);

            query.Value = entity.Value;
            query.IsImageProperty = entity.IsImageProperty;
        }
    }
}