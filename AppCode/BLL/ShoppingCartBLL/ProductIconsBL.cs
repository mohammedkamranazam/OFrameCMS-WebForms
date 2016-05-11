using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class ProductIconsBL
    {
        public static void Add(SC_ProductIcons entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_ProductIcons.Add(entity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_ProductIcons entity, ShoppingCartEntities context)
        {
            context.SC_ProductIcons.Remove(entity);

            context.SaveChanges();
        }

        public static bool Exists(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(id, context);
            }
        }

        public static bool Exists(int id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_ProductIcons
                         where set.ProductIconID == id
                         select set);

            return query.Any();
        }

        public static SC_ProductIcons GetObjectbyID(int id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_ProductIcons
                         where set.ProductIconID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static SC_ProductIcons GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectbyID(id, context);
            }
        }

        public static void Save(SC_ProductIcons entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(entity.ProductIconID, context))
                {
                    var query = GetObjectbyID(entity.ProductIconID, context);

                    query.ProductID = entity.ProductID;
                    query.IconID = entity.IconID;

                    context.SaveChanges();
                }
            }
        }
    }
}