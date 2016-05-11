using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.OEventArgs;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Threading.Tasks;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class FolderManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Admin/Gallery/FolderList.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private async void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var folderID = DataParser.IntParse(Request.QueryString["FolderID"]);

            using (var context = new GalleryEntities())
            {
                if (await FoldersBL.RelatedRecordsExistsAsync(folderID, context))
                {
                    StatusMessage.MessageType = StatusMessageType.Warning;
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                }
                else
                {
                    try
                    {
                        var query = await FoldersBL.GetObjectByIDAsync(folderID, context);

                        await FoldersBL.DeleteAsync(query, context);

                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;

                        await FoldersSelectComponent1.InitializeFolders(context);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        StatusMessage.MessageType = StatusMessageType.Error;
                        StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                    }
                }
            }
        }

        private async void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    var folderID = DataParser.IntParse(Request.QueryString["FolderID"]);

                    var folderEntity = await FoldersBL.GetObjectByIDAsync(folderID, context);

                    if (folderEntity != null)
                    {
                        if (folderEntity.Title != TitleTextBox.Text)
                        {
                            if (await FoldersBL.ExistsAsync(TitleTextBox.Text, FoldersSelectComponent1.DriveID, FoldersSelectComponent1.ParentFolderID, context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                        }

                        if (IsMovingToItsAnyChild(folderEntity) || IsMovingToItsChild(folderEntity))
                        {
                            StatusMessage.MessageType = StatusMessageType.Warning;
                            StatusMessage.Message = "Invalid Operation";
                            return;
                        }

                        folderEntity.Title = TitleTextBox.Text;
                        folderEntity.Description = DescriptionEditor.Text;
                        folderEntity.DriveID = FoldersSelectComponent1.DriveID;
                        folderEntity.ParentFolderID = FoldersSelectComponent1.ParentFolderID;
                        folderEntity.Hide = HideCheckBox.Checked;
                        folderEntity.ImageID = ImageSelectorComponent1.ImageID;
                        folderEntity.Locale = LocaleDropDown.Locale;

                        await Save(folderEntity, context);
                    }
                }
            }
        }

        private bool IsMovingToItsChild(GY_Folders entity)
        {
            bool isMoving = false;

            if (entity.FolderID == FoldersSelectComponent1.ParentFolderID)
            {
                isMoving = true;
            }

            return isMoving;
        }

        private bool IsMovingToItsAnyChild(GY_Folders entity)
        {
            bool isMoving = false;

            foreach (var folder in entity.ChildFolders)
            {
                if (folder.FolderID == FoldersSelectComponent1.ParentFolderID)
                {
                    isMoving = true;
                    return isMoving;
                }
                else
                {
                    isMoving = IsMovingToItsAnyChild(folder);

                    if (isMoving)
                    {
                        return isMoving;
                    }
                }
            }

            return isMoving;
        }

        private async Task<bool> Save(GY_Folders entity, GalleryEntities context)
        {
            try
            {
                await FoldersBL.SaveAsync(context);

                StatusMessage.MessageType = StatusMessageType.Success;
                StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;

                await FoldersSelectComponent1.InitializeFolders(context);

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                StatusMessage.MessageType = StatusMessageType.Error;
                StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);

                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["FolderID"]))
                {
                    var folderID = DataParser.IntParse(Request.QueryString["FolderID"]);

                    using (var context = new GalleryEntities())
                    {
                        var query = FoldersBL.GetObjectByID(folderID, context);

                        if (query != null)
                        {
                            var locale = query.Locale;
                            LocaleDropDown.Locale = locale;
                            InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                            FoldersSelectComponent1.Locale = locale;

                            TitleTextBox.Text = query.Title;
                            DescriptionEditor.Text = query.Description;
                            HideCheckBox.Checked = query.Hide;
                            FoldersSelectComponent1.DriveID = query.DriveID;
                            FoldersSelectComponent1.ParentFolderID = query.ParentFolderID;
                            ImageSelectorComponent1.ImageID = query.ImageID;
                        }
                        else
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                        }
                    }

                    ImageSelectorComponent1.StoragePath = LocalStorages.Folders;
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/FolderList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        private void InitializeLocaleFields(string direction)
        {
            TitleTextBox.Direction = direction;
            DescriptionEditor.Direction = direction;
        }

        private async void LocaleDropDown_LanguageDirectionChanged(object sender, LanguageDirectionChangedEventArgs e)
        {
            InitializeLocaleFields(e.Direction);

            FoldersSelectComponent1.Locale = LocaleDropDown.Locale;

            await FoldersSelectComponent1.LoadData();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}