using OWDARO;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace ProjectJKL
{
    public partial class Posts : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetMainThemeFile();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageCache(OWDARO.Settings.PageCacheHelper.GetCache("PostsPage"));

                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }
        }

        private async Task LoadData()
        {
            var locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);

            var tagsDictionary = new SortedDictionary<string, int>();

            using (var context = new MediaEntities())
            {
                var posts = await (from set in context.ME_Posts
                                   where set.Hide == false && set.Locale == locale
                                   select set).ToListAsync();

                posts.ForEach(post => Utilities.GetTagsSplitted(tagsDictionary, post.Tags));
            }

            TagCloudComponent1.Tags = tagsDictionary;
            TagCloudComponent1.URL = "~/Search.aspx?Search={0}";
            TagCloudComponent1.GenerateTags();
        }
    }
}