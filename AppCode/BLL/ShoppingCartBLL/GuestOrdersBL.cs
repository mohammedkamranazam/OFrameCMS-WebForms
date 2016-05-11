using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class GuestOrdersBL
    {
        public static void Add(SC_GuestOrders entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_GuestOrders.Add(entity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_GuestOrders entity, ShoppingCartEntities context)
        {
            context.SC_GuestOrders.Remove(entity);

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
            var query = (from set in context.SC_GuestOrders
                         where set.GuestOrderID == id
                         select set);

            return query.Any();
        }

        public static SC_GuestOrders GetObjectByID(long id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_GuestOrders GetObjectByID(long id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_GuestOrders
                         where set.GuestOrderID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static void Save(SC_GuestOrders entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(entity.GuestOrderID, context))
                {
                    var query = GetObjectByID(entity.GuestOrderID, context);

                    query.OrderNumber = entity.OrderNumber;
                    query.EmailID = entity.EmailID;
                    query.Mobile = entity.Mobile;

                    context.SaveChanges();
                }
            }
        }
    }
}