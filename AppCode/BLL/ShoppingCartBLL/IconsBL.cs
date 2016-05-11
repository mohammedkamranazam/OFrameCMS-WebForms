using OWDARO;
using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.IO;
using System.Linq;

namespace ProjectJKL.BLL.ShoppingCartBLL
{
    public static class IconsBL
    {
        public static void Add(SC_Icons iconEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Add(iconEntity, context);
            }
        }

        public static void Add(SC_Icons iconEntity, ShoppingCartEntities context)
        {
            context.SC_Icons.Add(iconEntity);

            context.SaveChanges();
        }

        public static void Delete(SC_Icons entity, ShoppingCartEntities context)
        {
            context.SC_Icons.Remove(entity);

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
            var query = (from icons in context.SC_Icons
                         where icons.IconID == id
                         select icons);

            return query.Any();
        }

        public static bool Exists(string title, ShoppingCartEntities context)
        {
            var query = (from icons in context.SC_Icons
                         where icons.Title == title
                         select icons);

            return query.Any();
        }

        public static SC_Icons GetObjectByID(int id)
        {
            using (var context = new ShoppingCartEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static SC_Icons GetObjectByID(int id, ShoppingCartEntities context)
        {
            var iconQuery = (from icons in context.SC_Icons
                             where icons.IconID == id
                             select icons);

            return iconQuery.FirstOrDefault();
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

            var relativeStoragePath = LocalStorages.Icons;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(100, 50, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static void Save(SC_Icons iconEntity)
        {
            using (var context = new ShoppingCartEntities())
            {
                Save(iconEntity, context);
            }
        }

        public static void Save(SC_Icons iconEntity, ShoppingCartEntities context)
        {
            if (Exists(iconEntity.IconID, context))
            {
                var iconFromDB = GetObjectByID(iconEntity.IconID, context);

                iconFromDB.Title = iconEntity.Title;
                iconFromDB.Description = iconEntity.Description;
                iconFromDB.IconURL = iconEntity.IconURL;
                iconFromDB.AlternateText = iconEntity.AlternateText;
            }
        }
    }
}