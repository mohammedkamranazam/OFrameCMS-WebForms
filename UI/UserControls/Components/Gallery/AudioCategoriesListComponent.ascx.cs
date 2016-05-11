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
    public partial class AudioCategoriesListComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                var locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
                var direction = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureDirectionCookieKey);

                StringBuilder sb = new StringBuilder();

                List<GY_AudioCategories> postCategories = new List<GY_AudioCategories>();

                if (!string.IsNullOrWhiteSpace(Request.QueryString["AudioCategoryID"]))
                {
                    int audioCategoryID = Request.QueryString["AudioCategoryID"].IntParse();

                    postCategories = await (from set in context.GY_AudioCategories
                                            where set.Hide == false && set.ParentAudioCategoryID == audioCategoryID
                                            select set).ToListAsync();

                    if (postCategories.Any())
                    {
                        locale = postCategories.FirstOrDefault().Locale;
                        direction = LanguageHelper.GetLocaleDirection(locale);
                    }
                }
                else
                {
                    postCategories = await (from set in context.GY_AudioCategories
                                            where set.Hide == false && set.ParentAudioCategoryID == null && set.Locale == locale
                                            select set).ToListAsync();
                }

                sb.Append(GetAudioCategoriesItems(postCategories, direction));

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

            if (!string.IsNullOrWhiteSpace(Request.QueryString["AudioCategoryID"]))
            {
                int audioCategoryID = Request.QueryString["AudioCategoryID"].IntParse();

                var audioCategoryQuery = await AudioCategoriesBL.GetObjectByIDAsync(audioCategoryID, context);

                if (audioCategoryQuery != null)
                {
                    int? parentAudioCategoryID = audioCategoryQuery.ParentAudioCategoryID;

                    if (parentAudioCategoryID == null)
                    {
                        GoBackLiteral.Text = string.Format(aTag, goBackSpan, ResolveClientUrl(string.Format(goBackURL, string.Empty, "AudioCategories")), direction);
                    }
                    else
                    {
                        GoBackLiteral.Text = string.Format(aTag, goBackSpan, ResolveClientUrl(string.Format(goBackURL, string.Format("?AudioCategoryID={0}", parentAudioCategoryID), "AudioCategory")), direction);
                    }
                }
                else
                {
                    GoBackLiteral.Text = string.Empty;
                }
            }
            else if (!string.IsNullOrWhiteSpace(Request.QueryString["AudioSetID"]))
            {
                int audioSetID = Request.QueryString["AudioSetID"].IntParse();

                var audioSetQuery = await AudioSetBL.GetObjectByIDAsync(audioSetID, context);

                if (audioSetQuery != null)
                {
                    GoBackLiteral.Text = string.Format(aTag, goBackSpan, ResolveClientUrl(string.Format(goBackURL, string.Format("?AudioCategoryID={0}", audioSetQuery.AudioCategoryID), "AudioCategory")), direction);
                }
                else
                {
                    GoBackLiteral.Text = string.Empty;
                }
            }
            else if (!string.IsNullOrWhiteSpace(Request.QueryString["AudioID"]))
            {
                int audioID = Request.QueryString["AudioID"].IntParse();

                var audioQuery = await AudiosBL.GetObjectByIDAsync(audioID, context);

                if (audioQuery != null)
                {
                    if (audioQuery.AudioSetID == null)
                    {
                        GoBackLiteral.Text = string.Format(aTag, goBackSpan, ResolveClientUrl(string.Format(goBackURL, string.Format("?AudioCategoryID={0}", audioQuery.AudioCategoryID), "AudioCategory")), direction);
                    }
                    else
                    {
                        GoBackLiteral.Text = string.Format(aTag, goBackSpan, ResolveClientUrl(string.Format(goBackURL, string.Format("?AudioSetID={0}", audioQuery.AudioSetID), "AudioSet")), direction);
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

        public string GetAudioCategoriesItems(ICollection<GY_AudioCategories> categories, string direction)
        {
            StringBuilder sb = new StringBuilder();
            const string ulTag = "<ul class='PostCategoriesListComponent MultiLevelAccordianMenu' style='direction:{1};'>{0}</ul>";
            const string liTag = "<li><a href='{0}' class='{3}'>{1}</a>{2}</li>";
            const string aTagURL = "~/AudioCategory.aspx?AudioCategoryID={0}";

            if (categories.Any())
            {
                foreach (var category in categories)
                {
                    sb.Append(string.Format(liTag, ResolveClientUrl(string.Format(aTagURL, category.AudioCategoryID)), category.Title,
                         GetAudioCategoriesItems(category.GY_ChildAudioCategories, direction), direction));
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