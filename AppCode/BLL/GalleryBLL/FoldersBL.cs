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
    public static class FoldersBL
    {
        public static string GetFolderHTML(GY_Folders folder, string direction, Page page)
        {
            const string folderHTML = "<div class='Folder' style='direction:{6};'><span class='FolderToolbar'><a href='{5}' class='Link'>Browse</a></span><a href='{0}'><img src='{1}' /></a><span class='Title{4}' title='{3}'>{2}</span></div>";

            var anchorTagHref = page.ResolveClientUrl(string.Format("~/Downloads.aspx?DriveID={0}&FolderID={1}", folder.DriveID, folder.FolderID));
            var imageTagSrc = page.ResolveClientUrl(Utilities.GetImageThumbURL(folder.ImageID));

            return string.Format(folderHTML, "#", imageTagSrc, folder.Title, folder.Description, (string.IsNullOrWhiteSpace(folder.Description)) ? string.Empty : " tooltip", anchorTagHref, direction);
        }

        public static void Add(GY_Folders entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_Folders entity, GalleryEntities context)
        {
            context.GY_Folders.Add(entity);

            context.SaveChanges();
        }

        public static async Task AddAsync(GY_Folders entity)
        {
            using (var context = new GalleryEntities())
            {
                await AddAsync(entity, context);
            }
        }

        public static async Task AddAsync(GY_Folders entity, GalleryEntities context)
        {
            context.GY_Folders.Add(entity);

            await context.SaveChangesAsync();
        }

        public static void Delete(GY_Folders entity)
        {
            using (var context = new GalleryEntities())
            {
                Delete(entity, context);
            }
        }

        public static void Delete(GY_Folders entity, GalleryEntities context)
        {
            context.GY_Folders.Remove(entity);

            context.SaveChanges();
        }

        public static async Task DeleteAsync(GY_Folders entity)
        {
            using (var context = new GalleryEntities())
            {
                await DeleteAsync(entity, context);
            }
        }

        public static async Task DeleteAsync(GY_Folders entity, GalleryEntities context)
        {
            context.GY_Folders.Remove(entity);

            await context.SaveChangesAsync();
        }

        public static bool Exists(string title, int driveID, int? parentFolderID)
        {
            using (var context = new GalleryEntities())
            {
                return Exists(title, driveID, parentFolderID, context);
            }
        }

        public static bool Exists(string title, int driveID, long? parentFolderID, GalleryEntities context)
        {
            var query = (from set in context.GY_Folders
                         where set.DriveID == driveID && set.ParentFolderID == parentFolderID && set.Title == title
                         select set);

            return query.Any();

            //foreach(GY_Folders folder in query)
            //{
            //    if(folder.ParentFolderID == parentFolderID && folder.Title == title)
            //    {
            //        return true;
            //    }
            //}

            //return false;
        }

        public static async Task<bool> ExistsAsync(string title, int driveID, int? parentFolderID)
        {
            using (var context = new GalleryEntities())
            {
                return await ExistsAsync(title, driveID, parentFolderID, context);
            }
        }

        public static async Task<bool> ExistsAsync(string title, int driveID, long? parentFolderID, GalleryEntities context)
        {
            var query = await (from set in context.GY_Folders
                               where set.DriveID == driveID && set.ParentFolderID == parentFolderID && set.Title == title
                               select set).ToListAsync();

            return query.Any();

            //foreach(GY_Folders folder in query)
            //{
            //    if(folder.ParentFolderID == parentFolderID && folder.Title == title)
            //    {
            //        return true;
            //    }
            //}

            // return false;
        }

        public static GY_Folders GetObjectByID(long id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_Folders GetObjectByID(long id, GalleryEntities context)
        {
            var entity = (from set in context.GY_Folders
                          where set.FolderID == id
                          select set).FirstOrDefault();

            return entity;
        }

        public static async Task<GY_Folders> GetObjectByIDAsync(long id)
        {
            using (var context = new GalleryEntities())
            {
                return await GetObjectByIDAsync(id, context);
            }
        }

        public static async Task<GY_Folders> GetObjectByIDAsync(long id, GalleryEntities context)
        {
            return await (from set in context.GY_Folders
                          where set.FolderID == id
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

        public static bool RelatedRecordsExists(long id)
        {
            using (var context = new GalleryEntities())
            {
                return RelatedRecordsExists(id, context);
            }
        }

        public static bool RelatedRecordsExists(long id, GalleryEntities context)
        {
            var query = GetObjectByID(id, context);

            return (query.GY_Files.Any() || query.ChildFolders.Any());
        }

        public static async Task<bool> RelatedRecordsExistsAsync(long id)
        {
            using (var context = new GalleryEntities())
            {
                return await RelatedRecordsExistsAsync(id, context);
            }
        }

        public static async Task<bool> RelatedRecordsExistsAsync(long id, GalleryEntities context)
        {
            var query = await GetObjectByIDAsync(id, context);

            return (query.GY_Files.Any() || query.ChildFolders.Any());
        }

        public static void Save(GY_Folders entity)
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

        public static async Task SaveAsync(GY_Folders entity)
        {
            using (var context = new GalleryEntities())
            {
                await SaveAsync(context);
            }
        }

        public static async Task SaveAsync(GalleryEntities context)
        {
            await context.SaveChangesAsync();
        }
    }
}