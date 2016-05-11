using OWDARO.UI.UserControls.Controls;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.BLL.GalleryBLL
{
    public static class AlbumsBL
    {
        public static string GetAlbumHTML(GY_Albums album, string direction, Page page, string photosText)
        {
            const string albumTag = "<div class='AlbumBlock' style='direction:{0};'><a href='{1}'><img src='{2}' alt='{3}' /><div class='Description'><p>{4}</p><span class='DateCount'><span class='Date'>{5}</span><span class='Count'>{6}</span></span></div><h2>{3}</h2></a></div>";

            return string.Format(albumTag, direction,
                            page.ResolveClientUrl(String.Format("~/Photos.aspx?AlbumID={0}", album.AlbumID)),
                            page.ResolveClientUrl(Utilities.GetImageThumbURL(album.ImageID)),
                            album.Title, album.Description,
                            DataParser.GetDateFormattedString(album.TakenOn),
                            String.Format("{1}{0}", album.GY_Photos.Count(),
                            photosText));
        }

        public static void Add(GY_Albums entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_Albums entity, GalleryEntities context)
        {
            context.GY_Albums.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(GY_Albums entity)
        {
            using (var context = new GalleryEntities())
            {
                Delete(entity, context);
            }
        }

        public static void Delete(GY_Albums entity, GalleryEntities context)
        {
            context.GY_Albums.Remove(entity);

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
            var query = (from set in context.GY_Albums
                         where set.Title == title
                         select set);

            return query.Any();
        }

        public static GY_Albums GetObjectByID(int id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_Albums GetObjectByID(int id, GalleryEntities context)
        {
            var entity = (from photoAlbums in context.GY_Albums
                          where photoAlbums.AlbumID == id
                          select photoAlbums).FirstOrDefault();

            return entity;
        }

        public static async Task<GY_Albums> GetObjectByIDAsync(int id)
        {
            using (var context = new GalleryEntities())
            {
                return await GetObjectByIDAsync(id, context);
            }
        }

        public static async Task<GY_Albums> GetObjectByIDAsync(int id, GalleryEntities context)
        {
            return await (from photoAlbums in context.GY_Albums
                          where photoAlbums.AlbumID == id
                          select photoAlbums).FirstOrDefaultAsync();
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

            var relativeStoragePath = LocalStorages.Albums;

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

            var relativeStoragePath = LocalStorages.Albums;

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

            if (query != null)
            {
                return query.GY_Photos.Any();
            }

            return false;
        }


        public static void Save(GalleryEntities context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", Utilities.DateTimeNow(),
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }

                ErrorLogger.LogError(string.Join("<br /><br />", outputLines.ToArray()));

                throw e;
            }
        }
    }
}