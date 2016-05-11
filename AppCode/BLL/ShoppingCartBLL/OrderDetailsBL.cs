using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class OrderDetailsBL
    {
        public static void Add(SC_OrderDetails entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_OrderDetails.Add(entity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_OrderDetails entity, ShoppingCartEntities context)
        {
            context.SC_OrderDetails.Remove(entity);

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
            var query = (from set in context.SC_OrderDetails
                         where set.OrderDetailID == id
                         select set);

            return query.Any();
        }

        public static SC_OrderDetails GetObjectbyID(long id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_OrderDetails
                         where set.OrderDetailID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static SC_OrderDetails GetObjectByID(long id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectbyID(id, context);
            }
        }

        public static void Save(SC_OrderDetails entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(entity.OrderDetailID, context))
                {
                    var query = GetObjectbyID(entity.OrderDetailID, context);

                    query.ProductID = entity.ProductID;
                    query.OrderNumber = entity.OrderNumber;
                    query.Price = entity.Price;
                    query.Quantity = entity.Quantity;
                    query.ShipmentDate = entity.ShipmentDate;
                    query.DeliveryDate = entity.DeliveryDate;
                    query.ShipmentCompanyID = entity.ShipmentCompanyID;
                    query.TrackingID = entity.TrackingID;

                    context.SaveChanges();
                }
            }
        }
    }
}