using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class HighlightsBL
    {
        public static void Add(SC_Highlights entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_Highlights.Add(entity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_Highlights entity, ShoppingCartEntities context)
        {
            context.SC_Highlights.Remove(entity);

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
            var query = (from set in context.SC_Highlights
                         where set.HighlightID == id
                         select set);

            return query.Any();
        }

        public static SC_Highlights GetObjectbyID(int id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_Highlights
                         where set.HighlightID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static SC_Highlights GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectbyID(id, context);
            }
        }

        public static void Save(SC_Highlights entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(entity.HighlightID, context))
                {
                    var query = GetObjectbyID(entity.HighlightID, context);

                    query.ProductID = entity.ProductID;
                    query.Highlight = entity.Highlight;

                    context.SaveChanges();
                }
            }
        }
    }
}