using OWDARO.UI.UserControls.Controls;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.BLL.GalleryBLL
{
    public static class DrivesBL
    {
        public static string GetDrivesHTML(GY_Drives drive, string direction, Page page)
        {
            const string driveTag = "<div class='DriveBlock' style='direction:{0};'><a href='{1}'><img src='{2}' alt='{3}' /></a><h2 class='{5}' title='{4}'>{3}</h2></div>";

            return string.Format(driveTag, direction,
                             page.ResolveClientUrl(string.Format("~/Downloads.aspx?DriveID={0}&FolderID={1}", drive.DriveID, string.Empty)),
                             page.ResolveClientUrl(Utilities.GetImageThumbURL(drive.ImageID)),
                             drive.Title,
                             drive.Description,
                             string.Format("Title{0}", ((!string.IsNullOrWhiteSpace(drive.Description)) ? " tooltip" : string.Empty)));
        }

        public static void Add(GY_Drives entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_Drives entity, GalleryEntities context)
        {
            context.GY_Drives.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(GY_Drives entity)
        {
            using (var context = new GalleryEntities())
            {
                Delete(entity, context);
            }
        }

        public static void Delete(GY_Drives entity, GalleryEntities context)
        {
            context.GY_Drives.Remove(entity);

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
            var query = (from set in context.GY_Drives
                         where set.Title == title
                         select set);

            return query.Any();
        }

        public static GY_Drives GetObjectByID(int id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_Drives GetObjectByID(int id, GalleryEntities context)
        {
            var entity = (from set in context.GY_Drives
                          where set.DriveID == id
                          select set).FirstOrDefault();

            return entity;
        }

        public static async Task<GY_Drives> GetObjectByIDAsync(int id)
        {
            using (var context = new GalleryEntities())
            {
                return await GetObjectByIDAsync(id, context);
            }
        }

        public static async Task<GY_Drives> GetObjectByIDAsync(int id, GalleryEntities context)
        {
            return await (from set in context.GY_Drives
                          where set.DriveID == id
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

            return (query.GY_Files.Any() || query.GY_Folders.Any());
        }

        public static void Save(GY_Drives entity)
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