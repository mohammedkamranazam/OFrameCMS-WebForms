using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class UserCartBL
    {
        public static void Add(SC_UserCart entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(SC_UserCart entity, ShoppingCartEntities context)
        {
            context.SC_UserCart.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(SC_UserCart entity, ShoppingCartEntities context)
        {
            context.SC_UserCart.Remove(entity);

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
            var query = (from set in context.SC_UserCart
                         where set.CartID == id
                         select set);

            return query.Any();
        }

        public static SC_UserCart GetObjectbyID(long id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_UserCart
                         where set.CartID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static SC_UserCart GetObjectByID(long id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectbyID(id, context);
            }
        }

        public static void Save(SC_UserCart entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(entity.CartID, context))
                {
                    var query = GetObjectbyID(entity.CartID, context);

                    query.ProductID = entity.ProductID;
                    query.Quantity = entity.Quantity;
                    query.Username = entity.Username;

                    context.SaveChanges();
                }
            }
        }
    }
}