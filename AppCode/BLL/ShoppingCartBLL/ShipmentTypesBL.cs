using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class ShipmentTypesBL
    {
        public static void Add(SC_ShipmentTypes shipmentTypeEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_ShipmentTypes.Add(shipmentTypeEntity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_ShipmentTypes entity, ShoppingCartEntities context)
        {
            context.SC_ShipmentTypes.Remove(entity);

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
            var query = (from shipmentTypes in context.SC_ShipmentTypes
                         where shipmentTypes.ShipmentTypeID == id
                         select shipmentTypes);

            return query.Any();
        }

        public static SC_ShipmentTypes GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_ShipmentTypes GetObjectByID(int id, ShoppingCartEntities context)
        {
            var shipmentTypeQuery = (from shipmentTypes in context.SC_ShipmentTypes
                                     where shipmentTypes.ShipmentTypeID == id
                                     select shipmentTypes);

            return shipmentTypeQuery.FirstOrDefault();
        }

        public static void Save(SC_ShipmentTypes shipmentTypeEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(shipmentTypeEntity.ShipmentTypeID, context))
                {
                    var shipmentTypeFromDB = GetObjectByID(shipmentTypeEntity.ShipmentTypeID, context);

                    shipmentTypeFromDB.Title = shipmentTypeEntity.Title;
                    shipmentTypeFromDB.Description = shipmentTypeEntity.Description;
                    shipmentTypeFromDB.Hide = shipmentTypeEntity.Hide;

                    context.SaveChanges();
                }
            }
        }
    }
}