using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class ProductModelsBL
    {
        public static void Add(SC_ProductModels entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(SC_ProductModels entity, ShoppingCartEntities context)
        {
            context.SC_ProductModels.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(SC_ProductModels entity, ShoppingCartEntities context)
        {
            context.SC_ProductModels.Remove(entity);

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
            var query = (from set in context.SC_ProductModels
                         where set.ProductModelID == id
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
            var query = (from set in context.SC_ProductModels
                         where set.Title == title && set.SectionID == sectionID && set.CategoryID == categoryID && set.SubCategoryID == subCategoryID
                         select set);

            return query.Any();
        }

        public static SC_ProductModels GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_ProductModels GetObjectByID(int id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_ProductModels
                         where set.ProductModelID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static void Save(SC_ProductModels entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(entity, context);
            }
        }

        public static void Save(SC_ProductModels entity, ShoppingCartEntities context)
        {
            if (Exists(entity.ProductModelID, context))
            {
                var query = GetObjectByID(entity.ProductModelID, context);

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