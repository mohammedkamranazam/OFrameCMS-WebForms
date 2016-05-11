using OWDARO.BLL.GalleryBLL;
using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class PhotosComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            var albumID = DataParser.IntParse(Request.QueryString["AlbumID"]);

            using (var context = new GalleryEntities())
            {
                var albumQuery = await AlbumsBL.GetObjectByIDAsync(albumID, context);

                if (albumQuery != null && !albumQuery.Hide)
                {
                    string locale = albumQuery.Locale;
                    string direction = LanguageHelper.GetLocaleDirection(locale);

                    TitleH1.Style.Add("direction", direction);
                    TitleLiteral.Text = string.Format("{0} {1}", albumQuery.Title, LanguageHelper.GetKey("PhotosComponentTitle", locale));

                    LoadMoreButton.Text = LanguageHelper.GetKey("LoadMore", locale);

                    var seoEntity = new SEO();
                    seoEntity.Title = albumQuery.Title;
                    seoEntity.Description = albumQuery.Description;
                    seoEntity.Keywords = albumQuery.Tags;

                    Utilities.SetPageSEO(Page, seoEntity);

                    LoadPhotos(context, albumQuery, locale, direction);
                }
                else
                {
                    Response.Redirect("~/Albums.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        private void LoadPhotos(GalleryEntities context, GY_Albums albumQuery, string locale, string direction)
        {
            const string emptyPanel = "<div class='EmptyPhotos'><img class='EmptyPhotosImage' src='{0}' /></div>";
            const string photosPanel = "<div class='PhotosGallery'>{0}<div class='Clear'></div></div>";

            StringBuilder sb = new StringBuilder();

            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            CurrentCountHiddenField.Value = toFetchCount.ToString();

            var photosQuery = (from set in albumQuery.GY_Photos
                               where set.Hide == false
                               select set).ToList();

            if (photosQuery.Any())
            {
                foreach (var photo in photosQuery.OrderBy(c => c.TakenOn).Take(toFetchCount))
                {
                    sb.Append(Utilities.GetFancyBoxHTML(photo.ImageID, string.Empty, true, string.Empty, string.Format("title='{0}'", String.Format("{0}: {1}", photo.Title, photo.Description))));
                }

                PhotosLiteral.Text = string.Format(photosPanel, sb);
            }
            else
            {
                PhotosLiteral.Text = string.Format(emptyPanel, ResolveClientUrl(AppConfig.EmptyPanelImage));
            }

            UpdateLoadMoreControls(toFetchCount, photosQuery.Count(), locale);
        }

        private void UpdateLoadMoreControls(int toFetchCount, int totalItemsCount, string locale)
        {
            if (totalItemsCount == 0)
            {
                LoadMoreButton.Visible = false;
            }

            if (toFetchCount >= totalItemsCount)
            {
                LoadMoreButton.Enabled = false;
                LoadMoreButton.CssClass = "LoadMoreButtonDisabled";
                LoadMoreButton.Text = LanguageHelper.GetKey("NoMoreItemsToDisplay", locale);
            }
        }

        protected async void LoadMoreButton_Click(object sender, EventArgs e)
        {
            using (var context = new GalleryEntities())
            {
                var albumID = DataParser.IntParse(Request.QueryString["AlbumID"]);

                var albumQuery = await AlbumsBL.GetObjectByIDAsync(albumID, context);

                string locale = albumQuery.Locale;
                string direction = LanguageHelper.GetLocaleDirection(locale);

                LoadPhotos(context, albumQuery, locale, direction);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["AlbumID"]))
                {
                    this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                    {
                        await LoadData();
                    }));
                }
                else
                {
                    Response.Redirect("~/Albums.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }
}