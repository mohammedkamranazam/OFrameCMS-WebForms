using OWDARO;
using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.IO;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class BrandsBL
    {
        public static void Add(SC_Brands brandEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Add(brandEntity, context);
            }
        }

        public static void Add(SC_Brands brandEntity, ShoppingCartEntities context)
        {
            context.SC_Brands.Add(brandEntity);

            context.SaveChanges();
        }

        public static void Delete(SC_Brands entity, ShoppingCartEntities context)
        {
            context.SC_Brands.Remove(entity);

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
            var query = (from brands in context.SC_Brands
                         where brands.BrandID == id
                         select brands);
            return query.Any();
        }

        public static bool Exists(string title, ShoppingCartEntities context)
        {
            var query = (from brands in context.SC_Brands
                         where brands.Title == title
                         select brands);
            return query.Any();
        }

        public static SC_Brands GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_Brands GetObjectByID(int id, ShoppingCartEntities context)
        {
            var brandQuery = (from brands in context.SC_Brands
                              where brands.BrandID == id
                              select brands);

            return brandQuery.FirstOrDefault();
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

            var relativeStoragePath = LocalStorages.ProductBrands;

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

        public static void Save(SC_Brands brandEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(brandEntity, context);
            }
        }

        public static void Save(SC_Brands brandEntity, ShoppingCartEntities context)
        {
            if (Exists(brandEntity.BrandID, context))
            {
                var brandFromDB = GetObjectByID(brandEntity.BrandID, context);
                brandFromDB.Title = brandEntity.Title;
                brandFromDB.Description = brandEntity.Description;
                brandFromDB.ImageURL = brandEntity.ImageURL;
                brandFromDB.Hide = brandEntity.Hide;

                context.SaveChanges();
            }
        }
    }
}