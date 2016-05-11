using OWDARO.BLL.GalleryBLL;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class DrivesAndFoldersListComponent : System.Web.UI.UserControl
    {
        private async Task LoadData()
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["DriveID"]))
            {
                var driveID = Request.QueryString["DriveID"].IntParse();

                using (var context = new GalleryEntities())
                {
                    var driveQuery = await DrivesBL.GetObjectByIDAsync(driveID, context);

                    if (driveQuery != null)
                    {
                        string locale = driveQuery.Locale;
                        string direction = LanguageHelper.GetLocaleDirection(locale);

                        string goBackSpan = string.Format("<span class='GoBackSpan'></span> {0}", LanguageHelper.GetKey("GoBackText", locale));

                        const string ulTag = "<ul class='DrivesFolders MultiLevelAccordianMenu' style='direction:{1};'>{0}</ul>";
                        const string liTag = "<li><a href='{0}'{2}>{1}</a></li>";

                        var goBackHTML = string.Empty;
                        var goBackURL = "~/Drives.aspx";

                        StringBuilder sb = new StringBuilder();

                        GY_Folders folderQuery = null;

                        if (!string.IsNullOrWhiteSpace(Request.QueryString["FolderID"]))
                        {
                            var folderID = Request.QueryString["FolderID"].LongParse();

                            folderQuery = await FoldersBL.GetObjectByIDAsync(folderID, context);

                            if (folderQuery != null)
                            {
                                const string gobackRelativeURL = "~/Downloads.aspx?DriveID={0}&FolderID={1}";

                                if (folderQuery != null)
                                {
                                    if (folderQuery.ParentFolderID == null)
                                    {
                                        goBackURL = string.Format(gobackRelativeURL, driveID, string.Empty);
                                    }
                                    else
                                    {
                                        goBackURL = string.Format(gobackRelativeURL, driveID, folderQuery.ParentFolderID);
                                    }
                                }
                            }
                        }

                        goBackHTML = string.Format(liTag, ResolveClientUrl(goBackURL), goBackSpan, " class='GoBackFixCSSClass'");

                        sb.Append(goBackHTML);

                        if (folderQuery != null)
                        {
                            sb.Append(GetFolderListItems(folderQuery.ChildFolders, direction));
                        }
                        else
                        {
                            sb.Append(GetFolderListItems((from set in driveQuery.GY_Folders
                                                          where set.ParentFolderID == null && set.Locale == locale
                                                          select set).ToList(), direction));
                        }

                        FoldersLiteral.Text = string.Format(ulTag, sb, direction);
                    }
                    else
                    {
                        Response.Redirect("~/Drives.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
            }
        }

        public string GetFolderListItems(ICollection<GY_Folders> childFolders, string direction)
        {
            StringBuilder sb = new StringBuilder();
            const string liTag = "<li><a href='{0}' class='{3}'>{1}</a>{2}</li>";
            const string aTagURL = "~/Downloads.aspx?DriveID={0}&FolderID={1}";
            const string subULTag = "<ul class='MultiLevelAccordianMenu'>{0}</ul>";

            if (childFolders.Any())
            {
                foreach (var folder in childFolders)
                {
                    sb.Append(string.Format(liTag, ResolveClientUrl(string.Format(aTagURL, folder.DriveID, folder.FolderID)), folder.Title,
                         GetFolderListItems(folder.ChildFolders, direction), direction));
                }

                return string.Format(subULTag, sb);
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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["DriveID"]))
                {
                    this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                    {
                        await LoadData();
                    }));
                }
            }
        }
    }
}