using OWDARO.UI.UserControls.Controls;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OWDARO.BLL.GalleryBLL
{
    public static class PhotosBL
    {
        public static void Add(GY_Photos entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_Photos entity, GalleryEntities context)
        {
            context.GY_Photos.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(GY_Photos entity)
        {
            using (var context = new GalleryEntities())
            {
                Delete(entity, context);
            }
        }

        public static void Delete(GY_Photos entity, GalleryEntities context)
        {
            context.GY_Photos.Remove(entity);

            context.SaveChanges();
        }

        public static bool DeletePhotos(int albumID, GalleryEntities context)
        {
            var success = false;

            var photosQuery = (from set in context.GY_Photos
                               where set.AlbumID == albumID
                               select set);

            foreach (GY_Photos photo in photosQuery)
            {
                context.GY_Photos.Remove(photo);
            }

            try
            {
                context.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
            }

            return success;
        }

        public static bool Exists(string photoTitle, int albumID)
        {
            using (var context = new GalleryEntities())
            {
                var query = (from set in context.GY_Photos
                             where set.Title == photoTitle && set.AlbumID == albumID
                             select set);
                return query.Any();
            }
        }

        public static GY_Photos GetObjectByID(int id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_Photos GetObjectByID(int id, GalleryEntities context)
        {
            return (from set in context.GY_Photos
                    where set.PhotoID == id
                    select set).FirstOrDefault();
        }

        public static async Task<GY_Photos> GetObjectByIDAsync(int id)
        {
            using (var context = new GalleryEntities())
            {
                return await GetObjectByIDAsync(id, context);
            }
        }

        public static async Task<GY_Photos> GetObjectByIDAsync(int id, GalleryEntities context)
        {
            return await (from set in context.GY_Photos
                          where set.PhotoID == id
                          select set).FirstOrDefaultAsync();
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

            var relativeStoragePath = LocalStorages.Photos;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(60, 700, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static string GetUploadedImageThumbPath(FileUploadAdv fileUpload)
        {
            if (!fileUpload.Success)
            {
                return string.Empty;
            }

            var fileName = "thumb_" + Guid.NewGuid();
            var fileExtension = fileUpload.FileExtension;
            var fullFileName = fileName + fileExtension;

            var relativeStoragePath = LocalStorages.Photos;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(60, 300, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static bool RelatedRecordsExists(int id)
        {
            return false;
        }

        public static void Save(GY_Photos entity)
        {
            using (var context = new GalleryEntities())
            {
                Save(context);
            }
        }

        public static void Save(GalleryEntities context)
        {
            context.SaveChanges();
        }
    }
}