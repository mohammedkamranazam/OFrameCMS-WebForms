using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class UserOrdersBL
    {
        public static void Add(SC_UserOrders entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_UserOrders.Add(entity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_UserOrders entity, ShoppingCartEntities context)
        {
            context.SC_UserOrders.Remove(entity);

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
            var query = (from set in context.SC_UserOrders
                         where set.UserOrderID == id
                         select set);

            return query.Any();
        }

        public static SC_UserOrders GetObjectbyID(long id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_UserOrders
                         where set.UserOrderID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static SC_UserOrders GetObjectByID(long id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectbyID(id, context);
            }
        }

        public static void Save(SC_UserOrders entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(entity.UserOrderID, context))
                {
                    var query = GetObjectbyID(entity.UserOrderID, context);

                    query.OrderNumber = entity.OrderNumber;
                    query.Username = entity.Username;

                    context.SaveChanges();
                }
            }
        }
    }
}