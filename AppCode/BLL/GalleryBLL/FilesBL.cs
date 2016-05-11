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
    public static class FilesBL
    {
        public static string GetFileHTML(GY_Files file, string direction, Page page)
        {
            const string fileHTML = "<div class='File' style='direction:{7};'><span class='FileToolbar'><a href='{5}' class='Link' target='_blank'>Open</a><a href='{6}' class='Link'>Download</a></span><a href='{0}'><img src='{1}' /></a><span class='Title{4}' title='{3}'>{2}</span></div>";

            var downloadLinkHref = page.ResolveClientUrl(string.Format("~/UI/Pages/Helpers/DownloadGet.aspx?FileID={0}", file.FileID));
            var openLinkHref = page.ResolveClientUrl(string.Format("~/UI/Pages/Helpers/DownloadOpen.aspx?FileID={0}", file.FileID));
            var imageTagSrc = page.ResolveClientUrl(Utilities.GetImageThumbURL(file.ImageID));

            return string.Format(fileHTML, "#", imageTagSrc, file.Title, file.Description, (string.IsNullOrWhiteSpace(file.Description)) ? string.Empty : " tooltip", openLinkHref, downloadLinkHref, direction);
        }

        public static void Add(GY_Files entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_Files entity, GalleryEntities context)
        {
            context.GY_Files.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(GY_Files entity)
        {
            using (var context = new GalleryEntities())
            {
                Delete(entity, context);
            }
        }

        public static void Delete(GY_Files entity, GalleryEntities context)
        {
            context.GY_Files.Remove(entity);

            context.SaveChanges();
        }

        public static bool Exists(string title, int driveID, long? folderID)
        {
            using (var context = new GalleryEntities())
            {
                return Exists(title, driveID, folderID, context);
            }
        }

        public static bool Exists(string title, int driveID, long? folderID, GalleryEntities context)
        {
            var query = (from set in context.GY_Files
                         where set.Title == title && set.DriveID == driveID && set.FolderID == folderID
                         select set);

            return query.Any();
        }

        public static string GetFileImageThumbURL(long fileID)
        {
            using (var context = new GalleryEntities())
            {
                return GetFileImageThumbURL(fileID, context);
            }
        }

        public static string GetFileImageThumbURL(long fileID, GalleryEntities context)
        {
            var filesQuery = GetObjectByID(fileID, context);

            return Utilities.GetImageThumbURL(filesQuery.ImageID);
        }

        public static async Task<string> GetFileImageThumbURLAsync(long fileID)
        {
            using (var context = new GalleryEntities())
            {
                return await GetFileImageThumbURLAsync(fileID, context);
            }
        }

        public static async Task<string> GetFileImageThumbURLAsync(long fileID, GalleryEntities context)
        {
            var filesQuery = await GetObjectByIDAsync(fileID, context);

            return Utilities.GetImageThumbURL(filesQuery.ImageID);
        }

        public static string GetFileImageURL(long fileID)
        {
            using (var context = new GalleryEntities())
            {
                return GetFileImageURL(fileID, context);
            }
        }

        public static string GetFileImageURL(long fileID, GalleryEntities context)
        {
            var filesQuery = GetObjectByID(fileID, context);

            return Utilities.GetImageURL(filesQuery.ImageID);
        }

        public static async Task<string> GetFileImageURLAsync(long fileID)
        {
            using (var context = new GalleryEntities())
            {
                return await GetFileImageURLAsync(fileID, context);
            }
        }

        public static async Task<string> GetFileImageURLAsync(long fileID, GalleryEntities context)
        {
            var filesQuery = await GetObjectByIDAsync(fileID, context);

            return Utilities.GetImageURL(filesQuery.ImageID);
        }

        public static string GetFileURL(long fileID)
        {
            using (var context = new GalleryEntities())
            {
                return GetFileURL(fileID, context);
            }
        }

        public static string GetFileURL(long fileID, GalleryEntities context)
        {
            var filesQuery = GetObjectByID(fileID, context);

            return filesQuery.FileURL;
        }

        public static async Task<string> GetFileURLAsync(long fileID)
        {
            using (var context = new GalleryEntities())
            {
                return await GetFileURLAsync(fileID, context);
            }
        }

        public static async Task<string> GetFileURLAsync(long fileID, GalleryEntities context)
        {
            return (await GetObjectByIDAsync(fileID, context)).FileURL;
        }

        public static GY_Files GetObjectByID(long id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_Files GetObjectByID(long id, GalleryEntities context)
        {
            var entity = (from set in context.GY_Files
                          where set.FileID == id
                          select set).FirstOrDefault();

            return entity;
        }

        public static async Task<GY_Files> GetObjectByIDAsync(long id)
        {
            using (var context = new GalleryEntities())
            {
                return await GetObjectByIDAsync(id, context);
            }
        }

        public static async Task<GY_Files> GetObjectByIDAsync(long id, GalleryEntities context)
        {
            return await (from set in context.GY_Files
                          where set.FileID == id
                          select set).FirstOrDefaultAsync();
        }

        public static string GetUploadedFilePath(FileUploadAdv fileUpload)
        {
            if (!fileUpload.HasFile)
            {
                return string.Empty;
            }

            var fileName = Guid.NewGuid().ToString();
            var fullFileName = fileName + fileUpload.FileExtension;

            var relativeStoragePath = LocalStorages.Downloads;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);

            fileUpload.FilePath = relativeStoragePath;
            fileUpload.FileName = fileName;

            fileUpload.Upload();

            return relativeFullFilePath;
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

        public static void Save(GY_Files entity)
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