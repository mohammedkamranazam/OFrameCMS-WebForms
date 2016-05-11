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
    public partial class FoldersSelectComponent : UserControl
    {
        public event DriveOpenedEventHandler DriveOpened;

        public event FolderOpenedEventHandler FolderOpened;

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

        public int DriveID
        {
            get
            {
                return DriveIDHiddenField.Value.IntParse();
            }

            set
            {
                DriveIDHiddenField.Value = value.ToString();
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

        public long? ParentFolderID
        {
            get
            {
                return DataParser.NullableLongParse(CurrentFolderIDHiddenField.Value);
            }

            set
            {
                CurrentFolderIDHiddenField.Value = value.ToString();
            }
        }

        private void CheckBackStackForEmpty()
        {
            var stackElements = FolderStackBackHiddenField.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

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

        private async void DrivesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DriveIDHiddenField.Value = DrivesDropDownList.SelectedValue;

            CurrentFolderIDHiddenField.Value = string.Empty;

            await InitializeFolders();

            DisableBackButton();

            OnDriveOpened(DrivesDropDownList.GetSelectedValue());
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
            CurrentFolderIDHiddenField.Value = string.Empty;

            var driveID = DrivesDropDownList.GetSelectedValue();

            var folders = await (from set in context.GY_Folders
                                 where set.DriveID == driveID && set.Locale == Locale
                                 select set).ToListAsync();

            var foldersList = folders.Where(folder => folder.ParentFolderID == null);

            FoldersRepeater.DataSource = foldersList;
            FoldersRepeater.DataBind();

            StatusLiteral.Text = BuildCategoryPath(context, null);
        }

        public async Task LoadData()
        {
            await PopulateDrives();

            DrivesDropDownList.SelectedValue = DriveID.ToString();

            await InitializeFolders();

            DisableBackButton();
        }

        private void OnDriveOpened(int driveID)
        {
            if (DriveOpened != null)
            {
                var args = new DriveOpenedEventArgs(driveID);

                DriveOpened(this, args);
            }
        }

        private void OnFolderOpened(long folderID)
        {
            if (FolderOpened != null)
            {
                var args = new FolderOpenedEventArgs(folderID);

                FolderOpened(this, args);
            }
        }

        private async Task OpenFolder(long folderID, bool pushToStack)
        {
            using (var context = new GalleryEntities())
            {
                var folderQuery = await FoldersBL.GetObjectByIDAsync(folderID, context);

                if (folderQuery != null)
                {
                    FoldersRepeater.DataSource = folderQuery.ChildFolders.ToList();
                    FoldersRepeater.DataBind();

                    CurrentFolderIDHiddenField.Value = folderID.ToString();

                    StatusLiteral.Text = BuildCategoryPath(context, folderID);

                    if (pushToStack)
                    {
                        PushToBackStack(folderQuery.ParentFolderID);
                    }

                    OnFolderOpened(folderID);
                }
            }
        }

        private long? PopFromBackStack()
        {
            var stackElements = FolderStackBackHiddenField.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            var lastElement = stackElements.LastOrDefault();

            FolderStackBackHiddenField.Value = FolderStackBackHiddenField.Value.Replace(lastElement + ";", string.Empty);

            lastElement = lastElement.Replace("]", string.Empty);
            lastElement = lastElement.Replace("[", string.Empty);

            var folderID = DataParser.NullableLongParse((lastElement == "-1") ? string.Empty : lastElement);

            CheckBackStackForEmpty();

            return folderID;
        }

        private async Task PopulateDrives()
        {
            using (var context = new GalleryEntities())
            {
                var drivesQuery = await (from set in context.GY_Drives
                                         where set.Locale == Locale
                                         select set).ToListAsync();

                DrivesDropDownList.DataTextField = "Title";
                DrivesDropDownList.DataValueField = "DriveID";
                DrivesDropDownList.DataSource = drivesQuery;
                DrivesDropDownList.AddSelect();
            }
        }

        private void PushToBackStack(long? folderID)
        {
            FolderStackBackHiddenField.Value += string.Format("[{0}];", ((folderID == null) ? "-1" : folderID.ToString()));

            CheckBackStackForEmpty();
        }

        protected async void BackButton_Click(object sender, EventArgs e)
        {
            if (DrivesDropDownList.GetNullableSelectedValue() != null)
            {
                var folderID = PopFromBackStack();

                if (folderID == null)
                {
                    await GoHome();

                    FolderStackBackHiddenField.Value = string.Empty;

                    CheckBackStackForEmpty();

                    OnDriveOpened(DrivesDropDownList.GetSelectedValue());
                }
                else
                {
                    await OpenFolder((long)folderID, false);
                }
            }
        }

        protected async void FoldersRepeater_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            await OpenFolder(DataParser.LongParse(e.CommandName), true);
        }

        protected async void HomeButton_Click(object sender, EventArgs e)
        {
            if (DrivesDropDownList.GetNullableSelectedValue() != null)
            {
                await GoHome();

                FolderStackBackHiddenField.Value = string.Empty;

                CheckBackStackForEmpty();

                OnDriveOpened(DrivesDropDownList.GetSelectedValue());
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

            DrivesDropDownList.SelectedIndexChanged += DrivesDropDownList_SelectedIndexChanged;
        }

        public async Task InitializeFolders()
        {
            using (var context = new GalleryEntities())
            {
                await InitializeFolders(context);
            }
        }

        public async Task InitializeFolders(GalleryEntities context)
        {
            var driveID = DrivesDropDownList.GetSelectedValue();
            var parentFolderID = DataParser.NullableLongParse(CurrentFolderIDHiddenField.Value);

            var folders = await (from set in context.GY_Folders
                                 where set.DriveID == driveID && set.Locale == Locale
                                 select set).ToListAsync();

            var foldersList = folders.Where(folder => folder.ParentFolderID == parentFolderID);

            FoldersRepeater.DataSource = foldersList;
            FoldersRepeater.DataBind();

            StatusLiteral.Text = BuildCategoryPath(context, parentFolderID);
        }

        protected async void ResetButton_Click(object sender, EventArgs e)
        {
            ParentFolderID = InitialValue;

            await InitializeFolders();
        }

        public string BuildCategoryPath(GalleryEntities context, long? categoryID)
        {
            StringBuilder path = new StringBuilder();

            if (categoryID == null)
            {
                return string.Empty;
            }

            var category = (from set in context.GY_Folders where set.FolderID == (long)categoryID select set).FirstOrDefault();

            if (category != null)
            {
                path.Insert(0, string.Format("{0}/", category.Title));

                if (category.ParentFolderID != null)
                {
                    path.Insert(0, string.Format("{0}", BuildCategoryPath(context, category.ParentFolderID)));
                }
            }

            return path.ToString();
        }
    }
}