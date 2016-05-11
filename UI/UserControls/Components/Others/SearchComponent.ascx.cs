using OWDARO.Helpers;
using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Others
{
    public partial class SearchComponent : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utilities.SetPageSEO(Page, SEOHelper.GetPageSEO("SearchPage"));

                if (!string.IsNullOrWhiteSpace(Request.QueryString["Search"]))
                {
                    this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                    {
                        await GetSearchResults();
                    }));
                }
                else
                {
                    LoadMoreButton.Enabled = false;
                    LoadMoreButton.Text = LanguageHelper.GetKey("NoSearchTermsProvided");
                    LoadMoreButton.CssClass = "LoadMoreButtonDisabled";
                }
            }
        }

        private async Task GetSearchResults()
        {
            int itemsToGet = DataParser.IntParse(CurrentCountHiddenField.Value) + 10;
            CurrentCountHiddenField.Value = itemsToGet.ToString();

            string searchTerm = Request.QueryString["Search"];
            string[] searchTerms = searchTerm.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);

            SearchTermTextBox.Text = searchTerm;

            TitleLiteral.Text = searchTerm;

            const string searchItemTag = "<li class='animated fadeIn'><a href='{2}' class='ReadMore'><h2>{0}</h2></a><p>{1}</p></li>";
            const string noSearchResultsHTML = "<p class='NoResults'>{0}</p>";
            StringBuilder sb = new StringBuilder();

            List<SearchItems> searchItems = new List<SearchItems>();

            using (var context = new GalleryEntities())
            {
                searchItems.AddRange(await GetEventsSearchResults(searchTerms, locale, context));

                searchItems.AddRange(await GetVideosSearchResults(searchTerms, locale, context));

                searchItems.AddRange(await GetAudiosSearchResults(searchTerms, locale, context));

                searchItems.AddRange(await GetAlbumSearchResults(searchTerms, locale, context));
            }

            using (var context = new MediaEntities())
            {
                searchItems.AddRange(await GetPostsSearchResults(searchTerms, locale, context));
            }

            //using (var context = new ShoppingCartEntities())
            //{
            //    searchItems.AddRange(await GetProductsSearchResults(searchTerms, locale, context));
            //}

            IEnumerable<SearchItems> toTakeItems = searchItems.Take(itemsToGet);

            if (toTakeItems.Count() > 0)
            {
                foreach (SearchItems searchItem in toTakeItems)
                {
                    sb.Append(string.Format(searchItemTag, searchItem.Title, searchItem.Description, ResolveClientUrl(searchItem.URL)));
                }

                SearchLiteral.Text = sb.ToString();
            }
            else
            {
                SearchLiteral.Text = string.Format(noSearchResultsHTML, LanguageHelper.GetKey("NoResultsFound"));
            }

            if (searchItems.Count() <= itemsToGet)
            {
                LoadMoreButton.Enabled = false;
                LoadMoreButton.Text = LanguageHelper.GetKey("NoMoreItemsToDisplay");
                LoadMoreButton.CssClass = "LoadMoreButtonDisabled";
            }
        }

        private async Task<List<SearchItems>> GetPostsSearchResults(string[] searchTerms, string locale, MediaEntities context)
        {
            List<SearchItems> searchItems = new List<SearchItems>();

            const string navigateURL = "~/Post.aspx?PostID={0}";

            var allPosts = await (from set in context.ME_Posts
                                  where set.Hide == false && set.Locale == locale
                                  select set).ToListAsync();

            var items = (from set in allPosts
                         from searchTerm in searchTerms
                         where (set.Title.NullableContains(searchTerm) ||
                             set.Tags.NullableContains(searchTerm) ||
                             set.SubTitle.NullableContains(searchTerm) ||
                             set.PostContent.NullableContains(searchTerm))
                         select set).Distinct();

            foreach (ME_Posts item in items)
            {
                searchItems.Add(new SearchItems(item.Title, item.SubTitle, string.Format(navigateURL, item.PostID)));
            }

            return searchItems;
        }

        private async Task<List<SearchItems>> GetEventsSearchResults(string[] searchTerms, string locale, GalleryEntities context)
        {
            List<SearchItems> searchItems = new List<SearchItems>();

            const string navigateURL = "~/Event.aspx?EventID={0}";

            var allEvents = await (from set in context.GY_Events
                                   where set.Hide == false && set.Locale == locale
                                   select set).ToListAsync();

            var items = (from set in allEvents
                         from searchTerm in searchTerms
                         where (set.Title.NullableContains(searchTerm) ||
                             set.Description.NullableContains(searchTerm) ||
                             set.Location.NullableContains(searchTerm) ||
                             set.SubDescription.NullableContains(searchTerm) ||
                             set.SubTitle.NullableContains(searchTerm) ||
                             set.Tags.NullableContains(searchTerm))
                         select set).Distinct();

            foreach (GY_Events item in items)
            {
                searchItems.Add(new SearchItems(item.Title, item.SubDescription, string.Format(navigateURL, item.EventID)));
            }

            return searchItems;
        }

        private async Task<List<SearchItems>> GetVideosSearchResults(string[] searchTerms, string locale, GalleryEntities context)
        {
            List<SearchItems> searchItems = new List<SearchItems>();

            const string navigateURL = "~/Video.aspx?VideoID={0}";

            var allVideos = await (from set in context.GY_Videos
                                   where set.Hide == false && set.Locale == locale
                                   select set).ToListAsync();

            var items = (from set in allVideos
                         from searchTerm in searchTerms
                         where (set.Title.NullableContains(searchTerm) ||
                             set.Description.NullableContains(searchTerm) ||
                             set.Location.NullableContains(searchTerm) ||
                             set.Tags.NullableContains(searchTerm))
                         select set).Distinct();

            foreach (GY_Videos item in items)
            {
                searchItems.Add(new SearchItems(item.Title, item.Description, string.Format(navigateURL, item.VideoID)));
            }

            return searchItems;
        }

        private async Task<List<SearchItems>> GetAudiosSearchResults(string[] searchTerms, string locale, GalleryEntities context)
        {
            List<SearchItems> searchItems = new List<SearchItems>();

            const string navigateURL = "~/Audio.aspx?AudioID={0}";

            var allAudios = await (from set in context.GY_Audios
                                   where set.Hide == false && set.Locale == locale
                                   select set).ToListAsync();

            var items = (from set in allAudios
                         from searchTerm in searchTerms
                         where (set.Title.NullableContains(searchTerm) ||
                             set.Description.NullableContains(searchTerm) ||
                             set.Location.NullableContains(searchTerm) ||
                             set.Tags.NullableContains(searchTerm))
                         select set).Distinct();

            foreach (GY_Audios item in items)
            {
                searchItems.Add(new SearchItems(item.Title, item.Description, string.Format(navigateURL, item.AudioID)));
            }

            return searchItems;
        }

        private async Task<List<SearchItems>> GetAlbumSearchResults(string[] searchTerms, string locale, GalleryEntities context)
        {
            List<SearchItems> searchItems = new List<SearchItems>();

            const string navigateURL = "~/Photos.aspx?AlbumID={0}";

            var allAlbums = await (from set in context.GY_Albums
                                   where set.Hide == false && set.Locale == locale
                                   select set).ToListAsync();

            var items = (from set in allAlbums
                         from searchTerm in searchTerms
                         where (set.Title.NullableContains(searchTerm) ||
                             set.Description.NullableContains(searchTerm) ||
                             set.Location.NullableContains(searchTerm) ||
                             set.Tags.NullableContains(searchTerm))
                         select set).Distinct();

            foreach (GY_Albums item in items)
            {
                searchItems.Add(new SearchItems(item.Title, item.Description, string.Format(navigateURL, item.AlbumID)));
            }

            return searchItems;
        }

        //private async Task<List<SearchItems>> GetProductsSearchResults(string[] searchTerms, string locale, ShoppingCartEntities context)
        //{
        //    List<SearchItems> searchItems = new List<SearchItems>();

        //    string navigateURL = "~/Product.aspx?ProductID={0}";

        //    var allProducts = await (from set in context.SC_Products where set.Hide == false && set.ShowInCart == true select set).ToListAsync();

        //    var items = (from set in allProducts
        //                 from searchTerm in searchTerms
        //                 where (set.Title.NullableContains(searchTerm) ||
        //                     set.Description.NullableContains(searchTerm) ||
        //                     set.Details.NullableContains(searchTerm) ||
        //                     set.ItemNumber.NullableContains(searchTerm) ||
        //                     set.Manufacturer.NullableContains(searchTerm) ||
        //                     set.Model.NullableContains(searchTerm) ||
        //                     set.SubTitle.NullableContains(searchTerm) ||
        //                     set.Tags.NullableContains(searchTerm))
        //                 select set).Distinct();

        //    foreach (SC_Products item in items)
        //    {
        //        searchItems.Add(new SearchItems(item.Title, item.Details, string.Format(navigateURL, item.ProductID)));
        //    }

        //    return searchItems;
        //}

        protected async void LoadMoreButton_Click(object sender, EventArgs e)
        {
            await GetSearchResults();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/Search.aspx?Search={0}", SearchTermTextBox.Text));
        }
    }
}