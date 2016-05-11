using ProjectJKL.AppCode.DAL.GalleryModel;
using System.Linq;

namespace OWDARO.BLL.GalleryBLL
{
    public static class LayerPropertiesBL
    {
        public static void Add(GY_LayerProperties entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_LayerProperties entity, GalleryEntities context)
        {
            context.GY_LayerProperties.Add(entity);
        }

        public static void Add(long layerID, string attribute, string value)
        {
            using (var context = new GalleryEntities())
            {
                Add(layerID, attribute, value, context);
            }
        }

        public static void Add(long layerID, string attribute, string value, GalleryEntities context)
        {
            var entity = new GY_LayerProperties();
            entity.Attribute = attribute;
            entity.LayerID = layerID;
            entity.Value = value;

            if (Exists(layerID, attribute, context))
            {
                Update(entity, context);
            }
            else
            {
                Add(entity, context);
            }
        }

        public static void Delete(GY_LayerProperties entity)
        {
            using (var context = new GalleryEntities())
            {
                Delete(entity, context);
            }
        }

        public static void Delete(GY_LayerProperties entity, GalleryEntities context)
        {
            context.GY_LayerProperties.Remove(entity);

            context.SaveChanges();
        }

        public static void Delete(long layerID, string attribute)
        {
            using (var context = new GalleryEntities())
            {
                Delete(layerID, attribute, context);
            }
        }

        public static void Delete(long layerID, string attribute, GalleryEntities context)
        {
            var entity = (from set in context.GY_LayerProperties
                          where set.LayerID == layerID && set.Attribute == attribute
                          select set).FirstOrDefault();

            if (entity != null)
            {
                context.GY_LayerProperties.Remove(entity);
            }
        }

        public static bool Exists(long layerID, string attribute)
        {
            using (var context = new GalleryEntities())
            {
                return Exists(layerID, attribute, context);
            }
        }

        public static bool Exists(long layerID, string attribute, GalleryEntities context)
        {
            var query = (from set in context.GY_LayerProperties
                         where set.LayerID == layerID && set.Attribute == attribute
                         select set);

            return query.Any();
        }

        public static string GetAttributeValue(long layerID, string attribute, GalleryEntities context)
        {
            var query = GetLayerProperty(layerID, attribute, context);

            return query != null ? query.Value : string.Empty;
        }

        public static string GetAttributeValueString(long layerID, string attribute, GalleryEntities context)
        {
            var query = GetLayerProperty(layerID, attribute, context);

            return query != null ? string.Format("{0}='{1}'", query.Attribute, query.Value) : string.Empty;
        }

        public static GY_LayerProperties GetLayerProperty(long layerID, string attribute, GalleryEntities context)
        {
            return (from set in context.GY_LayerProperties
                    where set.LayerID == layerID && set.Attribute == attribute
                    select set).FirstOrDefault();
        }

        public static GY_LayerProperties GetObjectByID(int id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_LayerProperties GetObjectByID(int id, GalleryEntities context)
        {
            var entity = (from set in context.GY_LayerProperties
                          where set.LayerPropertyID == id
                          select set).FirstOrDefault();

            return entity;
        }

        public static void Save(GY_LayerProperties entity)
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

        public static void Update(GY_LayerProperties entity, GalleryEntities context)
        {
            var query = GetLayerProperty(entity.LayerID, entity.Attribute, context);

            query.Value = entity.Value;
        }
    }
}