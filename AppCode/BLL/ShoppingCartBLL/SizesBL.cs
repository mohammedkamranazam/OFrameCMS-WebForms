using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class SizesBL
    {
        public static void Add(SC_Sizes entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(SC_Sizes entity, ShoppingCartEntities context)
        {
            context.SC_Sizes.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(SC_Sizes entity, ShoppingCartEntities context)
        {
            context.SC_Sizes.Remove(entity);

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
            var query = (from sizes in context.SC_Sizes
                         where sizes.SizeID == id
                         select sizes);

            return query.Any();
        }

        public static bool Exists(string title, int SectionID, int categoryID, int subCategoryID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(title, SectionID, categoryID, subCategoryID, context);
            }
        }

        public static bool Exists(string title, int SectionID, int categoryID, int subCategoryID, ShoppingCartEntities context)
        {
            var query = (from sizes in context.SC_Sizes
                         where sizes.Title == title && sizes.SectionID == SectionID && sizes.CategoryID == categoryID && sizes.SubCategoryID == subCategoryID
                         select sizes);

            return query.Any();
        }

        public static SC_Sizes GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_Sizes GetObjectByID(int id, ShoppingCartEntities context)
        {
            var query = (from sizes in context.SC_Sizes
                         where sizes.SizeID == id
                         select sizes);

            return query.FirstOrDefault();
        }

        public static string GetSize(int productID)
        {
            var tag = string.Empty;

            using (var context = new ShoppingCartEntities())
            {
                var productQuery = ProductsBL.GetObjectByID(productID, context);

                if (productQuery.SizeID != null)
                {
                    tag = string.Format("<span class='Size'><strong>Size:</strong> {0}</span>", productQuery.SC_Sizes.Title);
                }
            }

            return tag;
        }

        public static void Save(SC_Sizes entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(entity, context);
            }
        }

        public static void Save(SC_Sizes entity, ShoppingCartEntities context)
        {
            if (Exists(entity.SizeID, context))
            {
                var query = GetObjectByID(entity.SizeID, context);

                query.Title = entity.Title;
                query.Description = entity.Description;

                context.SaveChanges();
            }
        }
    }
}