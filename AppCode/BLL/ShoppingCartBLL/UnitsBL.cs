using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class UnitsBL
    {
        public static void Add(SC_Units entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(SC_Units entity, ShoppingCartEntities context)
        {
            context.SC_Units.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(SC_Units entity, ShoppingCartEntities context)
        {
            context.SC_Units.Remove(entity);

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
            var query = (from units in context.SC_Units
                         where units.UnitID == id
                         select units);

            return query.Any();
        }

        public static bool Exists(string title, int sectionID, int categoryID, int subCategoryID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(title, sectionID, categoryID, subCategoryID, context);
            }
        }

        public static bool Exists(string title, int sectionID, int categoryID, int subCategoryID, ShoppingCartEntities context)
        {
            var query = (from units in context.SC_Units
                         where units.Title == title && units.SectionID == sectionID && units.CategoryID == categoryID && units.SubCategoryID == subCategoryID
                         select units);

            return query.Any();
        }

        public static SC_Units GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_Units GetObjectByID(int id, ShoppingCartEntities context)
        {
            var unitQuery = (from units in context.SC_Units
                             where units.UnitID == id
                             select units);

            return unitQuery.FirstOrDefault();
        }

        public static void Save(SC_Units entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(entity, context);
            }
        }

        public static void Save(SC_Units entity, ShoppingCartEntities context)
        {
            if (Exists(entity.UnitID, context))
            {
                var query = GetObjectByID(entity.UnitID, context);

                query.Title = entity.Title;
                query.Description = entity.Description;
                query.SectionID = entity.SectionID;
                query.CategoryID = entity.CategoryID;
                query.SubCategoryID = entity.SubCategoryID;

                context.SaveChanges();
            }
        }
    }
}