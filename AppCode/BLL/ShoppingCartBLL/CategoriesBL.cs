using OWDARO;
using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.IO;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class CategoriesBL
    {
        public static void Add(SC_Categories categoryEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_Categories.Add(categoryEntity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_Categories entity, ShoppingCartEntities context)
        {
            context.SC_Categories.Remove(entity);

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

        public static bool Exists(string title, int sectionID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(title, sectionID, context);
            }
        }

        public static bool Exists(int id, ShoppingCartEntities context)
        {
            var query = (from categories in context.SC_Categories
                         where categories.CategoryID == id
                         select categories);

            return query.Any();
        }

        public static bool Exists(string title, ShoppingCartEntities context)
        {
            var query = (from categories in context.SC_Categories
                         where categories.Title == title
                         select categories);

            return query.Any();
        }

        public static bool Exists(string title, int sectionID, ShoppingCartEntities context)
        {
            var query = (from categories in context.SC_Categories
                         where categories.Title == title && categories.SectionID == sectionID
                         select categories);

            return query.Any();
        }

        public static SC_Categories GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_Categories GetObjectByID(int id, ShoppingCartEntities context)
        {
            var categoryQuery = (from categories in context.SC_Categories
                                 where categories.CategoryID == id
                                 select categories);

            return categoryQuery.FirstOrDefault();
        }

        public static string GetUploadedImagePath(FileUploadAdv fileUpload)
        {
            if (!fileUpload.HasFile)
            {
                fileUpload.Success = false;
                return "NO IMAGE SELECTED";
            }

            var fileName = Guid.NewGuid().ToString();
            var fileExtension = fileUpload.FileExtension;
            var fullFileName = fileName + fileExtension;

            var relativeStoragePath = LocalStorages.ProductCategories;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(60, 700, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static string GetUploadedImageThumbnailPath(FileUploadAdv fileUpload)
        {
            if (!fileUpload.Success)
            {
                return string.Empty;
            }

            var fileName = "thumb_" + Guid.NewGuid();
            var fileExtension = fileUpload.FileExtension;
            var fullFileName = fileName + fileExtension;

            var relativeStoragePath = LocalStorages.ProductCategories;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(60, 300, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static void Save(SC_Categories categoryEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(categoryEntity, context);
            }
        }

        public static void Save(SC_Categories categoryEntity, ShoppingCartEntities context)
        {
            if (Exists(categoryEntity.CategoryID, context))
            {
                var categoryQuery = GetObjectByID(categoryEntity.CategoryID, context);
                categoryQuery.SectionID = categoryEntity.SectionID;
                categoryQuery.Title = categoryEntity.Title;
                categoryQuery.Description = categoryEntity.Description;
                categoryQuery.ImageURL = categoryEntity.ImageURL;
                categoryQuery.Hide = categoryEntity.Hide;

                context.SaveChanges();
            }
        }
    }
}