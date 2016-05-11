using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OWDARO.BLL.GalleryBLL
{
    public static class EventsBL
    {
        public static void Add(GY_Events entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_Events entity, GalleryEntities context)
        {
            context.GY_Events.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(GY_Events entity)
        {
            using (var context = new GalleryEntities())
            {
                Delete(entity, context);
            }
        }

        public static void Delete(GY_Events entity, GalleryEntities context)
        {
            context.GY_Events.Remove(entity);

            context.SaveChanges();
        }

        public static bool Exists(string title)
        {
            using (var context = new GalleryEntities())
            {
                return Exists(title, context);
            }
        }

        public static bool Exists(string title, GalleryEntities context)
        {
            var query = (from events in context.GY_Events
                         where events.Title == title
                         select events);
            return query.Any();
        }

        public static GY_Events GetObjectByID(int id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_Events GetObjectByID(int id, GalleryEntities context)
        {
            var entity = (GY_Events)(from events in context.GY_Events
                                     where events.EventID == id
                                     select events).FirstOrDefault();

            return entity;
        }

        public static async Task<GY_Events> GetObjectByIDAsync(int id)
        {
            using (var context = new GalleryEntities())
            {
                return await GetObjectByIDAsync(id, context);
            }
        }

        public static async Task<GY_Events> GetObjectByIDAsync(int id, GalleryEntities context)
        {
            return await (from events in context.GY_Events
                          where events.EventID == id
                          select events).FirstOrDefaultAsync();
        }

        public static string GetUploadedImagePath(FileUploadAdv fileupload)
        {
            if (!fileupload.HasFile)
            {
                return string.Empty;
            }

            var fileName = Guid.NewGuid().ToString();
            var fileExtension = fileupload.FileExtension;
            var fullFileName = fileName + fileExtension;

            var relativeStoragePath = LocalStorages.Events;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileupload.Server.MapPath(relativeFullFilePath);

            fileupload.ResizeCompressAndUpload(60, 700, absoluteFullFilePath, false);

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

            var relativeStoragePath = LocalStorages.Events;

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
            return false;
        }

        public static void Save(GY_Events entity)
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