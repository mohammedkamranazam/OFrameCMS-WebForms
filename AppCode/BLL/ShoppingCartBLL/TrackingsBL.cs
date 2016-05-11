using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class TrackingsBL
    {
        public static void Add(SC_Trackings entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_Trackings.Add(entity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_Trackings entity, ShoppingCartEntities context)
        {
            context.SC_Trackings.Remove(entity);

            context.SaveChanges();
        }

        public static bool Exists(long id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(id, context);
            }
        }

        public static bool Exists(long id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_Trackings
                         where set.TrackingID == id
                         select set);

            return query.Any();
        }

        public static SC_Trackings GetObjectbyID(long id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_Trackings
                         where set.TrackingID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static SC_Trackings GetObjectByID(long id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectbyID(id, context);
            }
        }

        public static void Save(SC_Trackings entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(entity.TrackingID, context))
                {
                    var query = GetObjectbyID(entity.TrackingID, context);

                    query.Title = entity.Title;

                    context.SaveChanges();
                }
            }
        }
    }
}