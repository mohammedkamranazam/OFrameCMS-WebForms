using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.IO;
using System.Linq;

namespace OWDARO.BLL.MediaBLL
{
    public static class PortfoliosBL
    {
        public static void Add(ME_Portfolios entity)
        {
            using (var context = new MediaEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(ME_Portfolios entity, MediaEntities context)
        {
            context.ME_Portfolios.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(int id)
        {
            using (var context = new MediaEntities())
            {
                Delete(id, context);
            }
        }

        public static void Delete(int id, MediaEntities context)
        {
            var query = GetObjectByID(id, context);

            context.ME_Portfolios.Remove(query);

            context.SaveChanges();
        }

        public static ME_Portfolios GetObjectByID(int id)
        {
            using (var context = new MediaEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static ME_Portfolios GetObjectByID(int id, MediaEntities context)
        {
            var entity = (ME_Portfolios)(from set in context.ME_Portfolios
                                         where set.PortfolioID == id
                                         select set).FirstOrDefault();

            return entity;
        }

        public static string GetUploadedImagePath(FileUploadAdv fileUpload)
        {
            if (!fileUpload.HasFile)
            {
                return string.Empty;
            }

            var fileName = Guid.NewGuid().ToString();
            var fileExtension = fileUpload.FileExtension;
            var fullFileName = fileName + fileExtension;

            var relativeStoragePath = LocalStorages.Others;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(60, 1000, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static string GetUploadedImageThumbPath(FileUploadAdv fileUpload)
        {
            if (!fileUpload.HasFile)
            {
                return string.Empty;
            }

            var fileName = Guid.NewGuid().ToString();
            var fileExtension = fileUpload.FileExtension;
            var fullFileName = fileName + fileExtension;

            var relativeStoragePath = LocalStorages.Others;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(60, 400, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static bool RelatedRecordsExists(int id)
        {
            using (var context = new MediaEntities())
            {
                return RelatedRecordsExists(id, context);
            }
        }

        public static bool RelatedRecordsExists(int id, MediaEntities context)
        {
            return false;
        }

        public static void Save(MediaEntities context)
        {
            context.SaveChanges();
        }
    }
}