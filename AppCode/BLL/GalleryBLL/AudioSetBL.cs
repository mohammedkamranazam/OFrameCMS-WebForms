using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OWDARO.BLL.GalleryBLL
{
    public static class AudioSetBL
    {
        public static void Add(GY_AudioSet entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_AudioSet entity, GalleryEntities context)
        {
            context.GY_AudioSet.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(int id)
        {
            using (var context = new GalleryEntities())
            {
                Delete(id, context);
            }
        }

        public static void Delete(int id, GalleryEntities context)
        {
            if (Exists(id, context))
            {
                var entity = GetObjectByID(id, context);

                context.GY_AudioSet.Remove(entity);

                context.SaveChanges();
            }
        }

        public static bool Exists(int id)
        {
            using (var context = new GalleryEntities())
            {
                return Exists(id, context);
            }
        }

        public static bool Exists(string title, int categoryID)
        {
            using (var context = new GalleryEntities())
            {
                return Exists(title, categoryID, context);
            }
        }

        public static bool Exists(int id, GalleryEntities context)
        {
            var query = (from set in context.GY_AudioSet
                         where set.AudioSetID == id
                         select set);

            return query.Any();
        }

        public static bool Exists(string title, int categoryID, GalleryEntities context)
        {
            var query = (from set in context.GY_AudioSet
                         where set.Title == title && set.AudioCategoryID == categoryID
                         select set);

            return query.Any();
        }

        public static GY_AudioSet GetObjectByID(int id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_AudioSet GetObjectByID(int id, GalleryEntities context)
        {
            var query = (from set in context.GY_AudioSet
                         where set.AudioSetID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static async Task<GY_AudioSet> GetObjectByIDAsync(int id)
        {
            using (var context = new GalleryEntities())
            {
                return await GetObjectByIDAsync(id, context);
            }
        }

        public static async Task<GY_AudioSet> GetObjectByIDAsync(int id, GalleryEntities context)
        {
            return await (from set in context.GY_AudioSet
                          where set.AudioSetID == id
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

            var relativeStoragePath = LocalStorages.Others;

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

            var relativeStoragePath = LocalStorages.Others;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(60, 300, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static bool RelatedRecordsExists(int id)
        {
            using (var context = new GalleryEntities())
            {
                return RelatedRecordsExists(id, context);
            }
        }

        public static bool RelatedRecordsExists(int id, GalleryEntities context)
        {
            var query = GetObjectByID(id, context);

            return query.GY_Audios.Any();
        }

        public static void Save(GY_AudioSet entity)
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