using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class OrdersBL
    {
        public static bool Add(SC_Orders entity, ShoppingCartEntities context)
        {
            var success = false;

            context.SC_Orders.Add(entity);

            try
            {
                context.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
            }

            return success;
        }

        public static void Delete(SC_Orders entity, ShoppingCartEntities context)
        {
            context.SC_Orders.Remove(entity);

            context.SaveChanges();
        }

        public static bool Exists(string orderNumber)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(orderNumber, context);
            }
        }

        public static bool Exists(string orderNumber, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_Orders
                         where set.OrderNumber == orderNumber
                         select set);

            return query.Any();
        }

        public static SC_Orders GetObjectByID(string orderNumber)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(orderNumber, context);
            }
        }

        public static SC_Orders GetObjectByID(string orderNumber, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_Orders
                         where set.OrderNumber == orderNumber
                         select set);

            return query.FirstOrDefault();
        }

        public static void Save(SC_Orders entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(entity.OrderNumber, context))
                {
                    var query = GetObjectByID(entity.OrderNumber, context);

                    query.DateTime = entity.DateTime;
                    query.BillingAddress = entity.BillingAddress;
                    query.OrderTotal = entity.OrderTotal;
                    query.ShipmentCost = entity.ShipmentCost;
                    query.ShipmentTypeID = entity.ShipmentTypeID;
                    query.ShippingAddress = entity.ShippingAddress;
                    query.EmailID = entity.EmailID;
                    query.Mobile = entity.Mobile;
                    query.IsCancelled = entity.IsCancelled;
                    query.IsCompleted = entity.IsCompleted;
                    query.IsConfirmed = entity.IsConfirmed;
                    query.IsPaid = entity.IsPaid;
                    query.IsDispatched = entity.IsPaid;
                    query.IsFailed = entity.IsFailed;
                    query.IsRefund = entity.IsRefund;
                    query.IsReturned = entity.IsReturned;

                    context.SaveChanges();
                }
            }
        }
    }
}