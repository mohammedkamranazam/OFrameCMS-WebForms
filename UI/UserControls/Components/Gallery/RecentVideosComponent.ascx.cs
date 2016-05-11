using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class RecentVideosComponent : System.Web.UI.UserControl
    {
        public int Count
        {
            get;
            set;
        }

        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                string locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
                string direction = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureDirectionCookieKey);

                TitleLiteral.Text = LanguageHelper.GetKey("RecentVideos", locale);
                TitleH1.Style.Add("direction", direction);

                const string tags = "<div class='VideoBlock' style='direction:{4};'><a href='{0}'><img src='{2}' alt='{1}' /><span class='{5}'>{6}</span><h2{7} title='{3}'>{1}</h2></a></div>";

                StringBuilder sb = new StringBuilder();

                var videosQuery = await (from set in context.GY_Videos
                                         where set.Hide == false &&
                                         set.Locale == locale
                                         select set).OrderByDescending(c => c.AddedOn).Take(Count).ToListAsync();

                if (videosQuery.Any())
                {
                    foreach (GY_Videos video in videosQuery)
                    {
                        string timeString = string.Empty;

                        if (!string.IsNullOrWhiteSpace(video.Length))
                        {
                            TimeSpan span = TimeSpan.FromMinutes(video.Length.IntParse());
                            timeString = span.ToString(@"hh\:mm");
                        }

                        sb.Append(string.Format(tags, ResolveClientUrl(string.Format("~/Video.aspx?VideoID={0}", video.VideoID)),
                            video.Title, ResolveClientUrl(await VideosBL.GetVideoImageThumbURLAsync(video, context)),
                            video.Description, direction, ((string.IsNullOrWhiteSpace(video.Length)) ? "TimeHidden" : "ShowTime"), timeString,
                            (!string.IsNullOrWhiteSpace(video.Description)) ? " class='tooltip'" : string.Empty));
                    }

                    VideosLiteral.Text = sb.ToString();
                }
                else
                {
                    this.Visible = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }
        }
    }
}