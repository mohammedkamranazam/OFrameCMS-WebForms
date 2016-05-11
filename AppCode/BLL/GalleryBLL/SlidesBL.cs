using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace OWDARO.BLL.GalleryBLL
{
    public static class SlidesBL
    {
        public static void Add(GY_Slides entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_Slides entity, GalleryEntities context)
        {
            context.GY_Slides.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(GY_Slides entity)
        {
            using (var context = new GalleryEntities())
            {
                Delete(entity, context);
            }
        }

        public static void Delete(GY_Slides entity, GalleryEntities context)
        {
            context.GY_Slides.Remove(entity);

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
            var query = (from set in context.GY_Slides
                         where set.Title == title
                         select set);

            return query.Any();
        }

        public static GY_Slides GetObjectByID(long id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_Slides GetObjectByID(long id, GalleryEntities context)
        {
            var entity = (from set in context.GY_Slides
                          where set.SlideID == id
                          select set).FirstOrDefault();

            return entity;
        }

        public static int GetPosition(GalleryEntities context)
        {
            var positionQuery = (from set in context.GY_Slides
                                 select set).OrderByDescending(c => c.Position).Take(1).FirstOrDefault();

            return positionQuery != null ? (int)positionQuery.Position + 1 : 1;
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

        public static void PopulateEasing(DropDownListAdv dropDownList)
        {
            dropDownList.Items.Add(new ListItem("Linear.easeNone"));
            dropDownList.Items.Add(new ListItem("Power0.easeIn"));
            dropDownList.Items.Add(new ListItem("Power0.easeInOut"));
            dropDownList.Items.Add(new ListItem("Power0.easeOut"));
            dropDownList.Items.Add(new ListItem("Power1.easeIn"));
            dropDownList.Items.Add(new ListItem("Power1.easeInOut"));
            dropDownList.Items.Add(new ListItem("Power1.easeOut"));
            dropDownList.Items.Add(new ListItem("Power2.easeIn"));
            dropDownList.Items.Add(new ListItem("Power2.easeInOut"));
            dropDownList.Items.Add(new ListItem("Power2.easeOut"));
            dropDownList.Items.Add(new ListItem("Power3.easeIn"));
            dropDownList.Items.Add(new ListItem("Power3.easeInOut"));
            dropDownList.Items.Add(new ListItem("Power3.easeOut"));
            dropDownList.Items.Add(new ListItem("Power4.easeIn"));
            dropDownList.Items.Add(new ListItem("Power4.easeInOut"));
            dropDownList.Items.Add(new ListItem("Power4.easeOut"));
            dropDownList.Items.Add(new ListItem("Quad.easeIn"));
            dropDownList.Items.Add(new ListItem("Quad.easeInOut"));
            dropDownList.Items.Add(new ListItem("Quad.easeOut"));
            dropDownList.Items.Add(new ListItem("Cubic.easeIn"));
            dropDownList.Items.Add(new ListItem("Cubic.easeInOut"));
            dropDownList.Items.Add(new ListItem("Cubic.easeOut"));
            dropDownList.Items.Add(new ListItem("Quart.easeIn"));
            dropDownList.Items.Add(new ListItem("Quart.easeInOut"));
            dropDownList.Items.Add(new ListItem("Quart.easeOut"));
            dropDownList.Items.Add(new ListItem("Quint.easeIn"));
            dropDownList.Items.Add(new ListItem("Quint.easeInOut"));
            dropDownList.Items.Add(new ListItem("Quint.easeOut"));
            dropDownList.Items.Add(new ListItem("Strong.easeIn"));
            dropDownList.Items.Add(new ListItem("Strong.easeInOut"));
            dropDownList.Items.Add(new ListItem("Strong.easeOut"));
            dropDownList.Items.Add(new ListItem("Back.easeIn"));
            dropDownList.Items.Add(new ListItem("Back.easeInOut"));
            dropDownList.Items.Add(new ListItem("Back.easeOut"));
            dropDownList.Items.Add(new ListItem("Bounce.easeIn"));
            dropDownList.Items.Add(new ListItem("Bounce.easeInOut"));
            dropDownList.Items.Add(new ListItem("Bounce.easeOut"));
            dropDownList.Items.Add(new ListItem("Circ.easeIn"));
            dropDownList.Items.Add(new ListItem("Circ.easeInOut"));
            dropDownList.Items.Add(new ListItem("Circ.easeOut"));
            dropDownList.Items.Add(new ListItem("Elastic.easeIn"));
            dropDownList.Items.Add(new ListItem("Elastic.easeInOut"));
            dropDownList.Items.Add(new ListItem("Elastic.easeOut"));
            dropDownList.Items.Add(new ListItem("Expo.easeIn"));
            dropDownList.Items.Add(new ListItem("Expo.easeInOut"));
            dropDownList.Items.Add(new ListItem("Expo.easeOut"));
            dropDownList.Items.Add(new ListItem("Sine.easeIn"));
            dropDownList.Items.Add(new ListItem("Sine.easeInOut"));
            dropDownList.Items.Add(new ListItem("Sine.easeOut"));
            dropDownList.Items.Add(new ListItem("SlowMo.ease"));
            dropDownList.Items.Add(new ListItem("easeOutBack"));
            dropDownList.Items.Add(new ListItem("easeInQuad"));
            dropDownList.Items.Add(new ListItem("easeOutQuad"));
            dropDownList.Items.Add(new ListItem("easeInOutQuad"));
            dropDownList.Items.Add(new ListItem("easeInCubic"));
            dropDownList.Items.Add(new ListItem("easeOutCubic"));
            dropDownList.Items.Add(new ListItem("easeInOutCubic"));
            dropDownList.Items.Add(new ListItem("easeInQuart"));
            dropDownList.Items.Add(new ListItem("easeOutQuart"));
            dropDownList.Items.Add(new ListItem("easeInOutQuart"));
            dropDownList.Items.Add(new ListItem("easeInQuint"));
            dropDownList.Items.Add(new ListItem("easeOutQuint"));
            dropDownList.Items.Add(new ListItem("easeInOutQuint"));
            dropDownList.Items.Add(new ListItem("easeInSine"));
            dropDownList.Items.Add(new ListItem("easeOutSine"));
            dropDownList.Items.Add(new ListItem("easeInOutSine"));
            dropDownList.Items.Add(new ListItem("easeInExpo"));
            dropDownList.Items.Add(new ListItem("easeOutExpo"));
            dropDownList.Items.Add(new ListItem("easeInOutExpo"));
            dropDownList.Items.Add(new ListItem("easeInCirc"));
            dropDownList.Items.Add(new ListItem("easeOutCirc"));
            dropDownList.Items.Add(new ListItem("easeInOutCirc"));
            dropDownList.Items.Add(new ListItem("easeInElastic"));
            dropDownList.Items.Add(new ListItem("easeOutElastic"));
            dropDownList.Items.Add(new ListItem("easeInOutElastic"));
            dropDownList.Items.Add(new ListItem("easeInBack"));
            dropDownList.Items.Add(new ListItem("easeOutBack"));
            dropDownList.Items.Add(new ListItem("easeInOutBack"));
            dropDownList.Items.Add(new ListItem("easeInBounce"));
            dropDownList.Items.Add(new ListItem("easeOutBounce"));
            dropDownList.Items.Add(new ListItem("easeInOutBounce"));
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

        public static void Save(GY_Slides entity)
        {
            using (var context = new GalleryEntities())
            {
                Save(entity, context);
            }
        }

        public static void Save(GY_Slides entity, GalleryEntities context)
        {
            context.SaveChanges();
        }

        public static void ShiftPositions(int position, GalleryEntities context)
        {
            var query = (from set in context.GY_Slides
                         where set.Position == position
                         select set).FirstOrDefault();

            if (query != null)
            {
                query.Position++;
                ShiftPositions((int)query.Position, context);
            }
            else
            {
                return;
            }
        }
    }
}