using OWDARO.Settings;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.BLL.GalleryBLL
{
    public static class AudiosBL
    {
        public static async Task<string> GetAudioControlHTML(GY_Audios audioQuery, GalleryEntities context, Page page)
        {
            const string audioHTML = "<audio controls><source src='{0}'>{1}</audio>";
            const string iframeHTML = "<iframe scrolling='no' frameborder='no' width='100%' height='180px' src='{0}'></iframe>";

            if (audioQuery.ShowWebAudio)
            {
                return string.Format(iframeHTML, audioQuery.WebAudioURL);
            }
            else
            {
                return string.Format(audioHTML, page.ResolveClientUrl(await FilesBL.GetFileURLAsync((long)audioQuery.FileID, context)),
                    "Your Browser Does Not Support The Audio Tag");
            }
        }

        public static async Task<string> GetAudioHTML(GY_Audios audio, string direction, Page page, GalleryEntities context)
        {
            const string webAudioOpenTag = "<a class='Open' href='{1}'>{0}</a>";
            const string audioBlockTag = "<div class='AudioBlock' style='direction: {0};'>{1}{2}</div>";
            const string tags = "<a href='{0}'><img src='{2}' alt='{1}'  /><h2{7} title='{3}'>{1}</h2></a>{4}<div class='AudioWidgetToolbar'><span class='Likes'>{5}</span><span class='Dislikes'>{6}</span></div>";

            string open = LanguageHelper.GetKey("Open", audio.Locale);
            string likes = LanguageHelper.GetKey("Likes", audio.Locale);
            string dislikes = LanguageHelper.GetKey("Dislikes", audio.Locale);

            string audioControl = await GetAudioControlHTML(audio, context, page);

            if (audio.ShowWebAudio)
            {
                return string.Format(audioBlockTag,
                             direction,
                             audioControl,
                             string.Format(webAudioOpenTag,
                             open,
                             page.ResolveClientUrl(string.Format("~/Audio.aspx?AudioID={0}", audio.AudioID))));
            }
            else
            {
                return string.Format(audioBlockTag, direction,
                           string.Format(tags,
                           page.ResolveClientUrl(string.Format("~/Audio.aspx?AudioID={0}", audio.AudioID)),
                           audio.Title,
                           page.ResolveClientUrl(await FilesBL.GetFileImageThumbURLAsync((long)audio.FileID)),
                           audio.Description,
                           audioControl,
                           string.Format("{0}{1}", likes, audio.LikesCount),
                           string.Format("{0}{1}", dislikes, audio.DislikesCount),
                           (!string.IsNullOrWhiteSpace(audio.Description)) ? " class='tooltip'" : string.Empty),
                           string.Empty);
            }
        }

        public static void Add(GY_Audios entity)
        {
            using (var context = new GalleryEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(GY_Audios entity, GalleryEntities context)
        {
            context.GY_Audios.Add(entity);

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

                context.GY_Audios.Remove(entity);

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
            var query = (from set in context.GY_Audios
                         where set.AudioID == id
                         select set);

            return query.Any();
        }

        public static bool Exists(string title, int? setID, GalleryEntities context)
        {
            var query = (from set in context.GY_Audios
                         where set.Title == title && set.AudioSetID == setID
                         select set);

            return query.Any();
        }

        public static GY_Audios GetObjectByID(int id)
        {
            using (var context = new GalleryEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static GY_Audios GetObjectByID(int id, GalleryEntities context)
        {
            var query = (from set in context.GY_Audios
                         where set.AudioID == id
                         select set);

            return query.FirstOrDefault();
        }

        public static async Task<GY_Audios> GetObjectByIDAsync(int id)
        {
            using (var context = new GalleryEntities())
            {
                return await GetObjectByIDAsync(id, context);
            }
        }

        public static async Task<GY_Audios> GetObjectByIDAsync(int id, GalleryEntities context)
        {
            return await (from set in context.GY_Audios
                          where set.AudioID == id
                          select set).FirstOrDefaultAsync();
        }

        public static bool RelatedRecordsExists(int id)
        {
            return false;
        }

        public static void Save(GY_Audios entity)
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