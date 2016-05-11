using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.IO;
using System.Linq;

namespace OWDARO.BLL.GalleryBLL
{
    public static class LayersBL
    {
        public static void Add(GY_Layers entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_Layers entity, GalleryEntities context)
        {
            context.GY_Layers.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(GY_Layers entity)
        {
            using (var context = new GalleryEntities())
            {
                Delete(entity, context);
            }
        }

        public static void Delete(GY_Layers entity, GalleryEntities context)
        {
            context.GY_Layers.Remove(entity);

            context.SaveChanges();
        }

        public static bool Exists(string title, long slideID)
        {
            using (var context = new GalleryEntities())
            {
                return Exists(title, slideID, context);
            }
        }

        public static bool Exists(string title, long slideID, GalleryEntities context)
        {
            var query = (from set in context.GY_Layers
                         where set.Title == title && set.SlideID == slideID
                         select set);

            return query.Any();
        }

        public static GY_Layers GetObjectByID(long id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_Layers GetObjectByID(long id, GalleryEntities context)
        {
            var entity = (from set in context.GY_Layers
                          where set.LayerID == id
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
            var fullFileName = fileName + fileUpload.FileExtension;

            var relativeStoragePath = LocalStorages.Banners;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);

            fileUpload.FilePath = relativeStoragePath;
            fileUpload.FileName = fileName;

            fileUpload.Upload();

            return relativeFullFilePath;
        }

        public static bool RelatedRecordsExists(long id)
        {
            using (var context = new GalleryEntities())
            {
                return RelatedRecordsExists(id, context);
            }
        }

        public static bool RelatedRecordsExists(long id, GalleryEntities context)
        {
            return false;
        }

        public static void Save(GY_Layers entity)
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