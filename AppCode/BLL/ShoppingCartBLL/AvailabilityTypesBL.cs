using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class AvailabilityTypesBL
    {
        public static void Add(SC_AvailabilityTypes entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(SC_AvailabilityTypes entity, ShoppingCartEntities context)
        {
            context.SC_AvailabilityTypes.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(SC_AvailabilityTypes entity, ShoppingCartEntities context)
        {
            context.SC_AvailabilityTypes.Remove(entity);

            context.SaveChanges();
        }

        public static bool Exists(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(id, context);
            }
        }

        public static bool Exists(string title)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(title, context);
            }
        }

        public static bool Exists(int id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_AvailabilityTypes
                         where set.AvailabilityTypeID == id
                         select set);

            return query.Any();
        }

        public static bool Exists(string title, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_AvailabilityTypes
                         where set.Title == title
                         select set);

            return query.Any();
        }

        public static string GetAvailabilityTypeText(int? availabilityTypeID)
        {
            var availabilityTypeText = string.Empty;

            var availabilityTypeQuery = GetObjectByID((int)availabilityTypeID);

            availabilityTypeText = availabilityTypeQuery.Title;

            return availabilityTypeText;
        }

        public static SC_AvailabilityTypes GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_AvailabilityTypes GetObjectByID(int id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_AvailabilityTypes
                         where set.AvailabilityTypeID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static void Save(SC_AvailabilityTypes entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(entity, context);
            }
        }

        public static void Save(SC_AvailabilityTypes entity, ShoppingCartEntities context)
        {
            if (Exists(entity.AvailabilityTypeID, context))
            {
                var query = GetObjectByID(entity.AvailabilityTypeID, context);

                query.Description = entity.Description;
                query.Title = entity.Title;
                query.Hide = entity.Hide;
                query.ColorName = entity.ColorName;

                context.SaveChanges();
            }
        }
    }
}