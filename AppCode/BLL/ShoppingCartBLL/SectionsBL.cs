using OWDARO;
using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.IO;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class SectionsBL
    {
        public static void Add(SC_Sections sectionEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Add(sectionEntity, context);
            }
        }

        public static void Add(SC_Sections sectionEntity, ShoppingCartEntities context)
        {
            context.SC_Sections.Add(sectionEntity);

            context.SaveChanges();
        }

        public static void Delete(SC_Sections entity, ShoppingCartEntities context)
        {
            context.SC_Sections.Remove(entity);

            context.SaveChanges();
        }

        public static bool Exists(string title)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(title, context);
            }
        }

        public static bool Exists(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return Exists(id, context);
            }
        }

        public static bool Exists(string title, ShoppingCartEntities context)
        {
            var query = (from sections in context.SC_Sections
                         where sections.Title == title
                         select sections);

            return query.Any();
        }

        public static bool Exists(int id, ShoppingCartEntities context)
        {
            var query = (from sections in context.SC_Sections
                         where sections.SectionID == id
                         select sections);

            return query.Any();
        }

        public static SC_Sections GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_Sections GetObjectByID(int id, ShoppingCartEntities context)
        {
            var SectionQuery = (from sections in context.SC_Sections
                                where sections.SectionID == id
                                select sections);

            return SectionQuery.FirstOrDefault();
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

            var relativeStoragePath = LocalStorages.ProductSections;

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

            var relativeStoragePath = LocalStorages.ProductSections;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(60, 300, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static void Save(SC_Sections sectionEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(sectionEntity, context);
            }
        }

        public static void Save(SC_Sections sectionEntity, ShoppingCartEntities context)
        {
            if (Exists(sectionEntity.SectionID, context))
            {
                var sectionFromDB = GetObjectByID(sectionEntity.SectionID, context);

                sectionFromDB.Title = sectionEntity.Title;
                sectionFromDB.Description = sectionEntity.Description;
                sectionFromDB.ImageURL = sectionEntity.ImageURL;
                sectionFromDB.Hide = sectionEntity.Hide;

                context.SaveChanges();
            }
        }
    }
}