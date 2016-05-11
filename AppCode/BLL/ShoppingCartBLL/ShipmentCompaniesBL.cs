using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class ShipmentCompaniesBL
    {
        public static void Add(SC_ShipmentCompanies shipmentCompanyEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_ShipmentCompanies.Add(shipmentCompanyEntity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_ShipmentCompanies entity, ShoppingCartEntities context)
        {
            context.SC_ShipmentCompanies.Remove(entity);

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
            var query = (from shipmentConpanies in context.SC_ShipmentCompanies
                         where shipmentConpanies.ShipmentCompanyID == id
                         select shipmentConpanies);

            return query.Any();
        }

        public static SC_ShipmentCompanies GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_ShipmentCompanies GetObjectByID(int id, ShoppingCartEntities context)
        {
            var shipmentCompanyQuery = (from shipmentCompanies in context.SC_ShipmentCompanies
                                        where shipmentCompanies.ShipmentCompanyID == id
                                        select shipmentCompanies);

            return shipmentCompanyQuery.FirstOrDefault();
        }

        public static void Save(SC_ShipmentCompanies shipmentCompanyEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                if (Exists(shipmentCompanyEntity.ShipmentCompanyID, context))
                {
                    var shipmentCompanyFromDB = GetObjectByID(shipmentCompanyEntity.ShipmentCompanyID, context);

                    shipmentCompanyFromDB.Title = shipmentCompanyEntity.Title;
                    shipmentCompanyFromDB.Description = shipmentCompanyEntity.Description;
                    shipmentCompanyFromDB.ImageURL = shipmentCompanyEntity.ImageURL;
                    shipmentCompanyFromDB.Hide = shipmentCompanyEntity.Hide;

                    context.SaveChanges();
                }
            }
        }
    }
}