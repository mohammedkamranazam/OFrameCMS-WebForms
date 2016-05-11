using OWDARO.BLL.GalleryBLL;
using OWDARO.OEventArgs;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class VideoCategoriesSelectComponent : UserControl
    {
        public event VideoCategoryOpenedEventHandler VideoCategoryOpened;

        public bool ResetVisible
        {
            set
            {
                ResetButton.Visible = value;
            }
        }

        public string Locale
        {
            set
            {
                if (string.IsNullOrWhiteSpace(LocaleHiddenField.Value))
                {
                    LocaleHiddenField.Value = AppConfig.DefaultLocale;
                }

                if (!string.IsNullOrWhiteSpace(value))
                {
                    LocaleHiddenField.Value = value;
                }
            }

            get
            {
                return LocaleHiddenField.Value;
            }
        }

        public bool EditMode
        {
            get
            {
                return DataParser.BoolParse(EditModeHiddenField.Value);
            }

            set
            {
                EditModeHiddenField.Value = value.ToString();
            }
        }

        public int InitialValue
        {
            set
            {
                InitialValueHiddenField.Value = value.ToString();
            }

            get
            {
                return InitialValueHiddenField.Value.IntParse();
            }
        }

        public int? ParentVideoCategoryID
        {
            get
            {
                return DataParser.NullableIntParse(CurrentVideoCategoryIDHiddenField.Value);
            }

            set
            {
                CurrentVideoCategoryIDHiddenField.Value = value.ToString();
            }
        }

        private void CheckBackStackForEmpty()
        {
            var stackElements = VideoCategoryStackBackHiddenField.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            if (stackElements.Length > 0)
            {
                EnableBackButton();
            }
            else
            {
                DisableBackButton();
            }
        }

        private void DisableBackButton()
        {
            BackButton.Enabled = false;
            BackButton.CssClass += " disabled";
        }

        private void EnableBackButton()
        {
            BackButton.Enabled = true;
            BackButton.CssClass = "btn btn-info";
        }

        private async Task GoHome()
        {
            using (var context = new GalleryEntities())
            {
                await GoHome(context);
            }
        }

        private async Task GoHome(GalleryEntities context)
        {
            CurrentVideoCategoryIDHiddenField.Value = string.Empty;

            var videoCategories = await (from set in context.GY_VideoCategories
                                         where set.ParentVideoCategoryID == null && set.Locale == Locale
                                         select set).ToListAsync();

            VideoCategoriesRepeater.DataSource = videoCategories;
            VideoCategoriesRepeater.DataBind();

            StatusLiteral.Text = BuildCategoryPath(context, null);
        }

        public async Task LoadData()
        {
            await InitializeVideoCategories();

            DisableBackButton();
        }

        private void OnVideoCategoryOpened(int videoCategoryID)
        {
            if (VideoCategoryOpened != null)
            {
                var args = new VideoCategoryOpenedEventArgs(videoCategoryID);

                VideoCategoryOpened(this, args);
            }
        }

        private async Task OpenVideoCategory(int videoCategoryID, bool pushToStack)
        {
            using (var context = new GalleryEntities())
            {
                var videoCategoryQuery = await VideoCategoriesBL.GetObjectByIDAsync(videoCategoryID, context);

                if (videoCategoryQuery != null)
                {
                    VideoCategoriesRepeater.DataSource = videoCategoryQuery.GY_ChildVideoCategories.ToList();
                    VideoCategoriesRepeater.DataBind();

                    CurrentVideoCategoryIDHiddenField.Value = videoCategoryID.ToString();

                    StatusLiteral.Text = BuildCategoryPath(context, videoCategoryID);

                    if (pushToStack)
                    {
                        PushToBackStack(videoCategoryQuery.ParentVideoCategoryID);
                    }

                    OnVideoCategoryOpened(videoCategoryID);
                }
            }
        }

        private int? PopFromBackStack()
        {
            var stackElements = VideoCategoryStackBackHiddenField.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            var lastElement = stackElements.LastOrDefault();

            VideoCategoryStackBackHiddenField.Value = VideoCategoryStackBackHiddenField.Value.Replace(lastElement + ";", string.Empty);

            lastElement = lastElement.Replace("]", string.Empty);
            lastElement = lastElement.Replace("[", string.Empty);

            var videoCategoryID = DataParser.NullableIntParse((lastElement == "-1") ? string.Empty : lastElement);

            CheckBackStackForEmpty();

            return videoCategoryID;
        }

        private void PushToBackStack(int? videoCategoryID)
        {
            VideoCategoryStackBackHiddenField.Value += string.Format("[{0}];", ((videoCategoryID == null) ? "-1" : videoCategoryID.ToString()));

            CheckBackStackForEmpty();
        }

        protected async void BackButton_Click(object sender, EventArgs e)
        {
            var videoCategoryID = PopFromBackStack();

            if (videoCategoryID == null)
            {
                await GoHome();

                VideoCategoryStackBackHiddenField.Value = string.Empty;

                CheckBackStackForEmpty();
            }
            else
            {
                await OpenVideoCategory((int)videoCategoryID, false);
            }
        }

        protected async void VideoCategoriesRepeater_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            await OpenVideoCategory(DataParser.IntParse(e.CommandName), true);
        }

        protected async void HomeButton_Click(object sender, EventArgs e)
        {
            await GoHome();

            VideoCategoryStackBackHiddenField.Value = string.Empty;

            CheckBackStackForEmpty();
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

        public async Task InitializeVideoCategories()
        {
            using (var context = new GalleryEntities())
            {
                await InitializeVideoCategories(context);
            }
        }

        public async Task InitializeVideoCategories(GalleryEntities context)
        {
            var parentCategoryID = DataParser.NullableLongParse(CurrentVideoCategoryIDHiddenField.Value);

            var categories = await (from set in context.GY_VideoCategories
                                    where set.ParentVideoCategoryID == parentCategoryID && set.Locale == Locale
                                    select set).ToListAsync();

            VideoCategoriesRepeater.DataSource = categories;
            VideoCategoriesRepeater.DataBind();

            StatusLiteral.Text = BuildCategoryPath(context, parentCategoryID);
        }

        protected async void ResetButton_Click(object sender, EventArgs e)
        {
            ParentVideoCategoryID = InitialValue;

            await InitializeVideoCategories();
        }

        public string BuildCategoryPath(GalleryEntities context, long? categoryID)
        {
            StringBuilder path = new StringBuilder();

            if (categoryID == null)
            {
                return string.Empty;
            }

            var category = (from set in context.GY_VideoCategories where set.VideoCategoryID == (long)categoryID select set).FirstOrDefault();

            if (category != null)
            {
                path.Insert(0, string.Format("{0}/", category.Title));

                if (category.ParentVideoCategoryID != null)
                {
                    path.Insert(0, string.Format("{0}", BuildCategoryPath(context, category.ParentVideoCategoryID)));
                }
            }

            return path.ToString();
        }
    }
}