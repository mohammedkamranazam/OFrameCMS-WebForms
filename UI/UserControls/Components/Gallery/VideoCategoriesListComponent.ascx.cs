using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class VideoCategoriesListComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                var locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
                var direction = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureDirectionCookieKey);

                StringBuilder sb = new StringBuilder();

                List<GY_VideoCategories> postCategories = new List<GY_VideoCategories>();

                if (!string.IsNullOrWhiteSpace(Request.QueryString["VideoCategoryID"]))
                {
                    int videoCategoryID = Request.QueryString["VideoCategoryID"].IntParse();

                    postCategories = await (from set in context.GY_VideoCategories
                                            where set.Hide == false && set.ParentVideoCategoryID == videoCategoryID
                                            select set).ToListAsync();

                    if (postCategories.Any())
                    {
                        locale = postCategories.FirstOrDefault().Locale;
                        direction = LanguageHelper.GetLocaleDirection(locale);
                    }
                }
                else
                {
                    postCategories = await (from set in context.GY_VideoCategories
                                            where set.Hide == false && set.ParentVideoCategoryID == null && set.Locale == locale
                                            select set).ToListAsync();
                }

                sb.Append(GetVideoCategoriesItems(postCategories, direction));

                ItemsLiteral.Text = sb.ToString();

                await BuildGoBackMenuItem(context, locale, direction);
            }
        }

        private async Task BuildGoBackMenuItem(GalleryEntities context, string locale, string direction)
        {
            string aTag = "<a href='{1}' class='GoBackAnchor' style='direction:{2};'>{0}</a>";
            string goBackSpan = string.Format("<span class='GoBackSpan'></span> {0}", LanguageHelper.GetKey("GoBackText", locale));
            var goBackHTML = string.Empty;
            var goBackURL = "~/{1}.aspx{0}";

            if (!string.IsNullOrWhiteSpace(Request.QueryString["VideoCategoryID"]))
            {
                int videoCategoryID = Request.QueryString["VideoCategoryID"].IntParse();

                var videoCategoryQuery = await VideoCategoriesBL.GetObjectByIDAsync(videoCategoryID, context);

                if (videoCategoryQuery != null)
                {
                    int? parentVideoCategoryID = videoCategoryQuery.ParentVideoCategoryID;

                    if (parentVideoCategoryID == null)
                    {
                        GoBackLiteral.Text = string.Format(aTag, goBackSpan, ResolveClientUrl(string.Format(goBackURL, string.Empty, "VideoCategories")), direction);
                    }
                    else
                    {
                        GoBackLiteral.Text = string.Format(aTag, goBackSpan, ResolveClientUrl(string.Format(goBackURL, string.Format("?VideoCategoryID={0}", parentVideoCategoryID), "VideoCategory")), direction);
                    }
                }
                else
                {
                    GoBackLiteral.Text = string.Empty;
                }
            }
            else if (!string.IsNullOrWhiteSpace(Request.QueryString["VideoSetID"]))
            {
                int videoSetID = Request.QueryString["VideoSetID"].IntParse();

                var videoSetQuery = await VideoSetBL.GetObjectByIDAsync(videoSetID, context);

                if (videoSetQuery != null)
                {
                    GoBackLiteral.Text = string.Format(aTag, goBackSpan, ResolveClientUrl(string.Format(goBackURL, string.Format("?VideoCategoryID={0}", videoSetQuery.VideoCategoryID), "VideoCategory")), direction);
                }
                else
                {
                    GoBackLiteral.Text = string.Empty;
                }
            }
            else if (!string.IsNullOrWhiteSpace(Request.QueryString["VideoID"]))
            {
                int videoID = Request.QueryString["VideoID"].IntParse();

                var videoQuery = await VideosBL.GetObjectByIDAsync(videoID, context);

                if (videoQuery != null)
                {
                    if (videoQuery.VideoSetID == null)
                    {
                        GoBackLiteral.Text = string.Format(aTag, goBackSpan, ResolveClientUrl(string.Format(goBackURL, string.Format("?VideoCategoryID={0}", videoQuery.VideoCategoryID), "VideoCategory")), direction);
                    }
                    else
                    {
                        GoBackLiteral.Text = string.Format(aTag, goBackSpan, ResolveClientUrl(string.Format(goBackURL, string.Format("?VideoSetID={0}", videoQuery.VideoSetID), "VideoSet")), direction);
                    }
                }
                else
                {
                    GoBackLiteral.Text = string.Empty;
                }
            }
            else
            {
                GoBackLiteral.Text = string.Empty;
            }
        }

        public string GetVideoCategoriesItems(ICollection<GY_VideoCategories> categories, string direction)
        {
            StringBuilder sb = new StringBuilder();
            const string ulTag = "<ul class='PostCategoriesListComponent MultiLevelAccordianMenu' style='direction:{1};'>{0}</ul>";
            const string liTag = "<li><a href='{0}' class='{3}'>{1}</a>{2}</li>";
            const string aTagURL = "~/VideoCategory.aspx?VideoCategoryID={0}";

            if (categories.Any())
            {
                foreach (var category in categories)
                {
                    sb.Append(string.Format(liTag, ResolveClientUrl(string.Format(aTagURL, category.VideoCategoryID)), category.Title,
                         GetVideoCategoriesItems(category.GY_ChildVideoCategories, direction), direction));
                }

                return string.Format(ulTag, sb, direction);
            }
            else
            {
                return string.Empty;
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