using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.IO;
using System.Linq;

namespace OWDARO.BLL.MediaBLL
{
    public static class TestimonialsBL
    {
        public static void Add(ME_Testimonials entity)
        {
            using (var context = new MediaEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(ME_Testimonials entity, MediaEntities context)
        {
            context.ME_Testimonials.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(int id)
        {
            using (var context = new MediaEntities())
            {
                Delete(id, context);
            }
        }

        public static void Delete(int id, MediaEntities context)
        {
            var query = GetObjectByID(id, context);

            context.ME_Testimonials.Remove(query);

            context.SaveChanges();
        }

        public static bool Exists(string testimonial)
        {
            using (var context = new MediaEntities())
            {
                return Exists(testimonial, context);
            }
        }

        public static bool Exists(string testimonial, MediaEntities context)
        {
            var query = (from set in context.ME_Testimonials
                         where set.Testimonial == testimonial
                         select set);
            return query.Any();
        }

        public static ME_Testimonials GetObjectByID(int id)
        {
            using (var context = new MediaEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static ME_Testimonials GetObjectByID(int id, MediaEntities context)
        {
            var entity = (ME_Testimonials)(from set in context.ME_Testimonials
                                           where set.TestimonialID == id
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
            var fileExtension = fileUpload.FileExtension;
            var fullFileName = fileName + fileExtension;

            var relativeStoragePath = LocalStorages.Others;

            var relativeFullFilePath = Path.Combine(relativeStoragePath, fullFileName);
            var absoluteFullFilePath = fileUpload.Server.MapPath(relativeFullFilePath);

            fileUpload.ResizeCompressAndUpload(60, 1000, absoluteFullFilePath, false);

            return relativeFullFilePath;
        }

        public static bool RelatedRecordsExists(int id)
        {
            using (var context = new MediaEntities())
            {
                return RelatedRecordsExists(id, context);
            }
        }

        public static bool RelatedRecordsExists(int id, MediaEntities context)
        {
            return false;
        }

        public static void Save(MediaEntities context)
        {
            context.SaveChanges();
        }
    }
}