using OWDARO.BLL.GalleryBLL;
using OWDARO.OEventArgs;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.Gallery.Nested
{
    public partial class FilesSelectComponent : UserControl
    {
        public event FileSelectedEventHandler FileSelected;

        public FoldersSelectComponent FoldersSelectComponent
        {
            get
            {
                return FoldersSelectComponent1;
            }
        }

        public bool EditMode
        {
            get
            {
                return DataParser.BoolParse(IsEditModeHiddenField.Value);
            }

            set
            {
                IsEditModeHiddenField.Value = value.ToString();
            }
        }

        public long? FileID
        {
            get
            {
                return DataParser.NullableLongParse(FileIDHiddenField.Value);
            }

            set
            {
                FileIDHiddenField.Value = value.ToString();

                OpenDriveAndFolder(value);
            }
        }

        public string Locale
        {
            set
            {
                FoldersSelectComponent1.Locale = value;
            }
        }

        private async Task DriveOpened(int driveID, GalleryEntities context)
        {
            GridView1.DataSource = await (from set in context.GY_Files
                                          where set.DriveID == driveID && set.FolderID == FoldersSelectComponent1.ParentFolderID && set.Locale == FoldersSelectComponent1.Locale
                                          select set).ToListAsync();
            GridView1.DataBind();
        }

        private async Task FolderOpened(long? folderID, GalleryEntities context)
        {
            GridView1.DataSource = await (from set in context.GY_Files
                                          where set.FolderID == folderID && set.Locale == FoldersSelectComponent1.Locale
                                          select set).ToListAsync();
            GridView1.DataBind();
        }

        private async void FoldersSelectComponent1_DriveOpened(object sender, DriveOpenedEventArgs e)
        {
            using (var context = new GalleryEntities())
            {
                await DriveOpened(e.DriveID, context);
            }
        }

        private async void FoldersSelectComponent1_FolderOpened(object sender, FolderOpenedEventArgs e)
        {
            using (var context = new GalleryEntities())
            {
                await FolderOpened(e.FolderID, context);
            }
        }

        private string GetSortDirection(string column)
        {
            var sortDirection = "DESC";

            var sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    var lastDirection = ViewState["SortDirection"] as string;

                    if ((lastDirection != null) && (lastDirection == "DESC"))
                    {
                        sortDirection = "ASC";
                    }
                }
            }

            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        private void OnFileSelected(long fileID)
        {
            if (FileSelected != null)
            {
                var args = new FileSelectedEventArgs(fileID);

                FileSelected(this, args);
            }
        }

        private async Task OpenDriveAndFolder(long? value)
        {
            if (value != null)
            {
                using (var context = new GalleryEntities())
                {
                    var filesQuery = await FilesBL.GetObjectByIDAsync((long)value, context);

                    if (filesQuery != null)
                    {
                        FoldersSelectComponent1.DriveID = (int)filesQuery.DriveID;
                        FoldersSelectComponent1.ParentFolderID = filesQuery.FolderID;

                        await DriveOpened((int)filesQuery.DriveID, context);

                        if (filesQuery.FolderID != null)
                        {
                            await FolderOpened((long)filesQuery.FolderID, context);
                        }
                    }
                }
            }
        }

        protected async void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            using (var context = new GalleryEntities())
            {
                GridView1.DataSource = await (from set in context.GY_Files
                                              where set.DriveID == FoldersSelectComponent1.DriveID && set.FolderID == FoldersSelectComponent1.ParentFolderID && set.Locale == FoldersSelectComponent1.Locale
                                              select set).ToListAsync();
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                var fileID = DataParser.LongParse((string)e.CommandArgument);

                FileIDHiddenField.Value = fileID.ToString();

                OnFileSelected(fileID);
            }
        }

        protected async void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            using (var context = new GalleryEntities())
            {
                GridView1.DataSource = await (from set in context.GY_Files
                                              where set.DriveID == FoldersSelectComponent1.DriveID && set.FolderID == FoldersSelectComponent1.ParentFolderID && set.Locale == FoldersSelectComponent1.Locale
                                              select set).OrderBy(String.Format("{0} {1}", e.SortExpression, GetSortDirection(e.SortExpression))).ToListAsync();
                GridView1.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // FoldersSelectComponent1.Locale =

            FoldersSelectComponent1.FolderOpened += FoldersSelectComponent1_FolderOpened;
            FoldersSelectComponent1.DriveOpened += FoldersSelectComponent1_DriveOpened;
        }
    }
}