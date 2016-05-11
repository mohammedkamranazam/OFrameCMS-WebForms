using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class ProductTypesBL
    {
        public static void Add(SC_ProductTypes entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(SC_ProductTypes entity, ShoppingCartEntities context)
        {
            context.SC_ProductTypes.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(SC_ProductTypes entity, ShoppingCartEntities context)
        {
            context.SC_ProductTypes.Remove(entity);

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
            var query = (from set in context.SC_ProductTypes
                         where set.ProductTypeID == id
                         select set);

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
            var query = (from set in context.SC_ProductTypes
                         where set.Title == title && set.SectionID == sectionID && set.CategoryID == categoryID && set.SubCategoryID == subCategoryID
                         select set);

            return query.Any();
        }

        public static SC_ProductTypes GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_ProductTypes GetObjectByID(int id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_ProductTypes
                         where set.ProductTypeID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static void Save(SC_ProductTypes entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(entity, context);
            }
        }

        public static void Save(SC_ProductTypes entity, ShoppingCartEntities context)
        {
            if (Exists(entity.ProductTypeID, context))
            {
                var query = GetObjectByID(entity.ProductTypeID, context);

                query.Title = entity.Title;
                query.Description = entity.Description;
                query.CategoryID = entity.CategoryID;
                query.Hide = entity.Hide;
                query.SectionID = entity.SectionID;
                query.SubCategoryID = entity.SubCategoryID;

                context.SaveChanges();
            }
        }
    }
}