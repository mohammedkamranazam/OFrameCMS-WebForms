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
    public partial class AlbumsComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            const string emptyPanelTag = "<div class='EmptyAlbums'><img class='EmptyAlbumsImage' src='{0}' /></div>";
            const string albumsPanel = "<div class='AlbumsComponent'>{0}<div class='Clear'></div></div>";

            StringBuilder sb = new StringBuilder();

            var locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
            var direction = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureDirectionCookieKey);
            string photos = LanguageHelper.GetKey("Photos", locale);

            TitleH1.Style.Add("direction", direction);
            TitleLiteral.Text = LanguageHelper.GetKey("PhotoGallery", locale);

            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            CurrentCountHiddenField.Value = toFetchCount.ToString();

            using (var context = new GalleryEntities())
            {
                var albumsQuery = await (from set in context.GY_Albums
                                         where set.Hide == false && set.Locale == locale
                                         select set).ToListAsync();

                if (albumsQuery.Any())
                {
                    foreach (var album in albumsQuery.OrderByDescending(c => c.TakenOn).Take(toFetchCount))
                    {
                        sb.Append(AlbumsBL.GetAlbumHTML(album, direction, Page, photos));
                    }

                    AlbumsLiteral.Text = string.Format(albumsPanel, sb);
                }
                else
                {
                    AlbumsLiteral.Text = string.Format(emptyPanelTag, ResolveClientUrl(AppConfig.EmptyPanelImage));
                }

                UpdateLoadMoreControls(toFetchCount, albumsQuery.Count(), locale);
            }
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
            await LoadData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("AlbumsPage"));

                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }
        }
    }
}