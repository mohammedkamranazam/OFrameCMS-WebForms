using OWDARO;
using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.IO;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class ColorsBL
    {
        public static void Add(SC_Colors colorEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Add(colorEntity, context);
            }
        }

        public static void Add(SC_Colors colorEntity, ShoppingCartEntities context)
        {
            context.SC_Colors.Add(colorEntity);

            context.SaveChanges();
        }

        public static void Delete(SC_Colors entity, ShoppingCartEntities context)
        {
            context.SC_Colors.Remove(entity);

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
            var query = (from colors in context.SC_Colors
                         where colors.ColorID == id
                         select colors);

            return query.Any();
        }

        public static bool Exists(string title, ShoppingCartEntities context)
        {
            var query = (from colors in context.SC_Colors
                         where colors.Title == title
                         select colors);

            return query.Any();
        }

        public static string GetColor(int productID)
        {
            var tag = string.Empty;

            using (var context = new ShoppingCartEntities())
            {
                var productQuery = ProductsBL.GetObjectByID(productID, context);

                if (productQuery.ColorID != null)
                {
                    tag = string.Format("<span class='Color'><strong>Color:</strong> {0}</span>", productQuery.SC_Colors.Title);
                }
            }

            return tag;
        }

        public static SC_Colors GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_Colors GetObjectByID(int id, ShoppingCartEntities context)
        {
            var colorQuery = (from colors in context.SC_Colors
                              where colors.ColorID == id
                              select colors);

            return colorQuery.FirstOrDefault();
        }

        public static string GetUploadedImagePath(FileUploadAdv fileUpload)
        {
            if (!fileUpload.HasFile)
            {
                fileUpload.Success = false;
                return string.Empty;
            }

            var fileName = Guid.NewGuid().ToString();
            var fileExtension = fileUpload.FileExtension;
            var fullFileName = fileName + fileExtension;

            var relativeStoragePath = LocalStorages.Others;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(100, 50, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static void Save(SC_Colors colorEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(colorEntity, context);
            }
        }

        public static void Save(SC_Colors colorEntity, ShoppingCartEntities context)
        {
            if (Exists(colorEntity.ColorID, context))
            {
                var colorFromDB = GetObjectByID(colorEntity.ColorID, context);

                colorFromDB.Title = colorEntity.Title;
                colorFromDB.Name = colorEntity.Name;
                colorFromDB.Hex = colorEntity.Hex;

                context.SaveChanges();
            }
        }
    }
}