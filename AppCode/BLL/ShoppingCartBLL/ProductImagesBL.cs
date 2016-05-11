using OWDARO;
using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.IO;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class ProductImagesBL
    {
        public static void Add(SC_ProductImages entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(SC_ProductImages entity, ShoppingCartEntities context)
        {
            context.SC_ProductImages.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(SC_ProductImages entity, ShoppingCartEntities context)
        {
            context.SC_ProductImages.Remove(entity);

            context.SaveChanges();
        }

        public static bool Exists(long id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(id, context);
            }
        }

        public static bool Exists(long id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_ProductImages
                         where set.ProductImageID == id
                         select set);

            return query.Any();
        }

        public static SC_ProductImages GetObjectByID(long id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_ProductImages GetObjectByID(long id, ShoppingCartEntities context)
        {
            var query = (from set in context.SC_ProductImages
                         where set.ProductImageID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static string GetProductFirstImage(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetProductFirstImage(productID, context);
            }
        }

        public static string GetProductFirstImage(int productID, ShoppingCartEntities context)
        {
            var imageURL = AppConfig.NoImage;

            var productQuery = ProductsBL.GetObjectByID(productID, context);

            var firstImage = productQuery.SC_ProductImages.OrderBy(c => c.ProductImageID).FirstOrDefault();

            if (firstImage != null)
            {
                return firstImage.ImageURL;
            }

            return imageURL;
        }

        public static string GetProductFirstImageThumb(int productID)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetProductFirstImageThumb(productID, context);
            }
        }

        public static string GetProductFirstImageThumb(int productID, ShoppingCartEntities context)
        {
            var imageURL = AppConfig.NoImage;

            var productQuery = ProductsBL.GetObjectByID(productID, context);

            var firstImage = productQuery.SC_ProductImages.OrderBy(c => c.ProductImageID).FirstOrDefault();

            if (firstImage != null)
            {
                return firstImage.ImageThumbURL;
            }

            return imageURL;
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

            var relativeStoragePath = LocalStorages.Products;

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

            var relativeStoragePath = LocalStorages.Products;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(60, 300, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static void Save(SC_ProductImages entity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(entity, context);
            }
        }

        public static void Save(SC_ProductImages entity, ShoppingCartEntities context)
        {
            if (Exists(entity.ProductImageID, context))
            {
                var query = GetObjectByID(entity.ProductImageID, context);

                query.ProductID = entity.ProductID;
                query.ImageURL = entity.ImageURL;
                query.ImageThumbURL = entity.ImageThumbURL;
                query.AlternateText = entity.AlternateText;

                context.SaveChanges();
            }
        }
    }
}