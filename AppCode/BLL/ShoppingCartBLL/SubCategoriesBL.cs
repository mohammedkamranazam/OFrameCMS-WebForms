using OWDARO;
using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.IO;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class SubCategoriesBL
    {
        public static void Add(SC_SubCategories subCategoryEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.SC_SubCategories.Add(subCategoryEntity);

                context.SaveChanges();
            }
        }

        public static void Delete(SC_SubCategories entity, ShoppingCartEntities context)
        {
            context.SC_SubCategories.Remove(entity);

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

        public static bool Exists(string title, int categoryID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(title, categoryID, context);
            }
        }

        public static bool Exists(int id, ShoppingCartEntities context)
        {
            var query = (from subCategories in context.SC_SubCategories
                         where subCategories.SubCategoryID == id
                         select subCategories);

            return query.Any();
        }

        public static bool Exists(string title, ShoppingCartEntities context)
        {
            var query = (from subCategories in context.SC_SubCategories
                         where subCategories.Title == title
                         select subCategories);

            return query.Any();
        }

        public static bool Exists(string title, int categoryID, ShoppingCartEntities context)
        {
            var query = (from subCategories in context.SC_SubCategories
                         where subCategories.Title == title && subCategories.CategoryID == categoryID
                         select subCategories);

            return query.Any();
        }

        public static SC_SubCategories GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_SubCategories GetObjectByID(int id, ShoppingCartEntities context)
        {
            var subCategoryQuery = (from subCategories in context.SC_SubCategories
                                    where subCategories.SubCategoryID == id
                                    select subCategories);

            return subCategoryQuery.FirstOrDefault();
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

            var relativeStoragePath = LocalStorages.ProductSubCategories;

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

            var fileName = "thumb_" + Guid.NewGuid().ToString();
            var fileExtension = fileUpload.FileExtension;
            var fullFileName = fileName + fileExtension;

            var relativeStoragePath = LocalStorages.ProductSubCategories;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(60, 300, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static void Save(SC_SubCategories subCategoryEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(subCategoryEntity, context);
            }
        }

        public static void Save(SC_SubCategories subCategoryEntity, ShoppingCartEntities context)
        {
            if (Exists(subCategoryEntity.SubCategoryID, context))
            {
                var subCategoryFromDB = GetObjectByID(subCategoryEntity.SubCategoryID, context);

                subCategoryFromDB.Title = subCategoryEntity.Title;
                subCategoryFromDB.Description = subCategoryEntity.Description;
                subCategoryFromDB.ImageURL = subCategoryEntity.ImageURL;
                subCategoryFromDB.CategoryID = subCategoryFromDB.CategoryID;
                subCategoryFromDB.Hide = subCategoryEntity.Hide;

                context.SaveChanges();
            }
        }
    }
}