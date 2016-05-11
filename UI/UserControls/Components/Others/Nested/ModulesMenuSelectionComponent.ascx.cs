using OWDARO.OEventArgs;
using ProjectJKL.AppCode.DAL.GalleryModel;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Others.Nested
{
    public partial class ModulesMenuSelectionComponent : System.Web.UI.UserControl
    {
        public event URLGeneratedEventHandler URLGenerated;

        public string Locale
        {
            get;
            set;
        }

        private void AlbumsListPopUpComponent_IDSelected(object sender, OWDARO.OEventArgs.IDSelectedEventArgs e)
        {
            GenerateURL(e, "~/Photos.aspx?AlbumID=");
        }

        private void DrivesListPopUpComponent_IDSelected(object sender, IDSelectedEventArgs e)
        {
            GenerateURL(e, "~/Drive.aspx?DriveID=");
        }

        private void EventsListPopUpComponent_IDSelected(object sender, OWDARO.OEventArgs.IDSelectedEventArgs e)
        {
            GenerateURL(e, "~/Event.aspx?EventID=");
        }

        private void PostsListPopUpComponent_IDSelected(object sender, IDSelectedEventArgs e)
        {
            GenerateURL(e, "~/Post.aspx?PostID=");
        }

        private void GenerateURL(IDSelectedEventArgs e, string url)
        {
            if (e.ID.ToString() == "-1")
            {
                OnURLGenerated("#");
            }
            else
            {
                OnURLGenerated(string.Format("{0}{1}&Lang={2}", url, e.ID, Locale));
            }
        }

        public async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                var albumsQuery = await (from set in context.GY_Albums
                                         where set.Hide == false && set.Locale == Locale
                                         select set).ToListAsync();

                if (albumsQuery.Any())
                {
                    AlbumsListPopUpComponent.DataTextField = "Title";
                    AlbumsListPopUpComponent.DataValueField = "AlbumID";
                    AlbumsListPopUpComponent.DataSource = albumsQuery;
                }
                else
                {
                    AlbumsListPopUpComponent.Enabled = false;
                }

                var eventsQuery = await (from set in context.GY_Events
                                         where set.Hide == false && set.Locale == Locale
                                         select set).ToListAsync();

                if (eventsQuery.Any())
                {
                    EventsListPopUpComponent.DataTextField = "Title";
                    EventsListPopUpComponent.DataValueField = "EventID";
                    EventsListPopUpComponent.DataSource = eventsQuery;
                }
                else
                {
                    EventsListPopUpComponent.Enabled = false;
                }

                var drivesQuery = await (from set in context.GY_Drives
                                         where set.Hide == false
                                         select set).ToListAsync();

                if (drivesQuery.Any())
                {
                    DrivesListPopUpComponent.DataTextField = "Title";
                    DrivesListPopUpComponent.DataValueField = "DriveID";
                    DrivesListPopUpComponent.DataSource = drivesQuery;
                }
                else
                {
                    DrivesListPopUpComponent.Enabled = false;
                }
            }

            using (var context = new MediaEntities())
            {
                var postsQuery = await (from set in context.ME_Posts
                                        where set.Hide == false && set.Locale == Locale
                                        select set).ToListAsync();

                if (postsQuery.Any())
                {
                    PostsListPopUpComponent.DataTextField = "Title";
                    PostsListPopUpComponent.DataValueField = "PostID";
                    PostsListPopUpComponent.DataSource = postsQuery;
                }
                else
                {
                    PostsListPopUpComponent.Enabled = false;
                }
            }
        }

        private void OnURLGenerated(string url)
        {
            if (URLGenerated != null)
            {
                var args = new URLGeneratedEventArgs(url);

                URLGenerated(this, args);
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

            AlbumsListPopUpComponent.IDSelected += AlbumsListPopUpComponent_IDSelected;
            EventsListPopUpComponent.IDSelected += EventsListPopUpComponent_IDSelected;
            PostsListPopUpComponent.IDSelected += PostsListPopUpComponent_IDSelected;
            DrivesListPopUpComponent.IDSelected += DrivesListPopUpComponent_IDSelected;
        }
    }
}