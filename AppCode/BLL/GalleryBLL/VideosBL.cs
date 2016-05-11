using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.BLL.GalleryBLL
{
    public static class VideosBL
    {
        public static async Task<string> GetVideoControlHTML(GY_Videos videoQuery, GalleryEntities context, Page page)
        {
            const string videoHTML = "<video controls><source src='{0}'>{1}</video>";
            const string iframeHTML = "<iframe allowfullscreen scrolling='no' frameborder='no' width='100%' height='400px' src='{0}'></iframe>";

            if (videoQuery.ShowWebVideo)
            {
                return string.Format(iframeHTML, string.Format("http://www.youtube.com/embed/{0}", videoQuery.WebVideoURL));
            }
            else
            {
                return string.Format(videoHTML, page.ResolveClientUrl(await FilesBL.GetFileURLAsync((long)videoQuery.FileID, context)),
                    "Your Browser Does Not Support The Video Tag");
            }
        }

        public static async Task<string> GetVideoHTML(GY_Videos video, string direction, GalleryEntities context, Page page)
        {
            const string tags = "<div class='VideoBlock' style='direction:{4};'><a href='{0}'><img src='{2}' alt='{1}' /><span class='{5}'>{6}</span><h2{7} title='{3}'>{1}</h2></a></div>";

            string timeString = string.Empty;

            if (!string.IsNullOrWhiteSpace(video.Length))
            {
                TimeSpan span = TimeSpan.FromMinutes(video.Length.IntParse());
                timeString = span.ToString(@"hh\:mm");
            }

            return string.Format(tags, page.ResolveClientUrl(string.Format("~/Video.aspx?VideoID={0}", video.VideoID)),
                        video.Title, page.ResolveClientUrl(await GetVideoImageThumbURLAsync(video, context)),
                        video.Description, direction, ((string.IsNullOrWhiteSpace(video.Length)) ? "TimeHidden" : "ShowTime"), timeString,
                        (!string.IsNullOrWhiteSpace(video.Description)) ? " class='tooltip'" : string.Empty);
        }

        public static void Add(GY_Videos entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_Videos entity, GalleryEntities context)
        {
            context.GY_Videos.Add(entity);

            context.SaveChanges();
        }

        public static void Delete(int id)
        {
            using (var context = new GalleryEntities())
            {
                Delete(id, context);
            }
        }

        public static void Delete(int id, GalleryEntities context)
        {
            if (Exists(id, context))
            {
                var entity = GetObjectByID(id, context);

                context.GY_Videos.Remove(entity);

                context.SaveChanges();
            }
        }

        public static bool Exists(int id)
        {
            using (var context = new GalleryEntities())
            {
                return Exists(id, context);
            }
        }

        public static bool Exists(string title, int? setID)
        {
            using (var context = new GalleryEntities())
            {
                return Exists(title, setID, context);
            }
        }

        public static bool Exists(int id, GalleryEntities context)
        {
            var query = (from set in context.GY_Videos
                         where set.VideoID == id
                         select set);

            return query.Any();
        }

        public static bool Exists(string title, int? setID, GalleryEntities context)
        {
            var query = (from set in context.GY_Videos
                         where set.Title == title && set.VideoSetID == setID
                         select set);

            return query.Any();
        }

        public static GY_Videos GetObjectByID(int id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_Videos GetObjectByID(int id, GalleryEntities context)
        {
            var query = (from set in context.GY_Videos
                         where set.VideoID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static async Task<GY_Videos> GetObjectByIDAsync(int id)
        {
            using (var context = new GalleryEntities())
            {
                return await GetObjectByIDAsync(id, context);
            }
        }

        public static async Task<GY_Videos> GetObjectByIDAsync(int id, GalleryEntities context)
        {
            return await (from set in context.GY_Videos
                          where set.VideoID == id
                          select set).FirstOrDefaultAsync();
        }

        public static string GetVideoImageThumbURL(int videoID)
        {
            using (var context = new GalleryEntities())
            {
                var video = VideosBL.GetObjectByID(videoID, context);

                return GetVideoImageThumbURL(video, context);
            }
        }

        public static string GetVideoImageThumbURL(GY_Videos video, GalleryEntities context)
        {
            return video.ShowWebVideo ? string.Format("http://i1.ytimg.com/vi/{0}/mqdefault.jpg", video.WebVideoURL) : FilesBL.GetFileImageThumbURL((long)video.FileID, context);
        }

        public static async Task<string> GetVideoImageThumbURLAsync(int videoID)
        {
            using (var context = new GalleryEntities())
            {
                var video = await VideosBL.GetObjectByIDAsync(videoID, context);

                return await GetVideoImageThumbURLAsync(video, context);
            }
        }

        public static async Task<string> GetVideoImageThumbURLAsync(GY_Videos video, GalleryEntities context)
        {
            return video.ShowWebVideo ? string.Format("http://i1.ytimg.com/vi/{0}/mqdefault.jpg", video.WebVideoURL) : await FilesBL.GetFileImageThumbURLAsync((long)video.FileID, context);
        }

        public static string GetVideoImageURL(int videoID)
        {
            using (var context = new GalleryEntities())
            {
                var video = VideosBL.GetObjectByID(videoID, context);

                return GetVideoImageURL(video, context);
            }
        }

        public static string GetVideoImageURL(GY_Videos video, GalleryEntities context)
        {
            return video.ShowWebVideo ? string.Format("http://i1.ytimg.com/vi/{0}/mqdefault.jpg", video.WebVideoURL) : FilesBL.GetFileImageURL((long)video.FileID, context);
        }

        public static async Task<string> GetVideoImageURLAsync(int videoID)
        {
            using (var context = new GalleryEntities())
            {
                var video = await VideosBL.GetObjectByIDAsync(videoID, context);

                return await GetVideoImageURLAsync(video, context);
            }
        }

        public static async Task<string> GetVideoImageURLAsync(GY_Videos video, GalleryEntities context)
        {
            return video.ShowWebVideo ? string.Format("http://i1.ytimg.com/vi/{0}/mqdefault.jpg", video.WebVideoURL) : await FilesBL.GetFileImageURLAsync((long)video.FileID, context);
        }

        public static bool RelatedRecordsExists(int id)
        {
            return false;
        }

        public static void Save(GalleryEntities context)
        {
            context.SaveChanges();
        }
    }
}