using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Linq;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class FileManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Admin/Gallery/FileList.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var fileID = DataParser.IntParse(Request.QueryString["FileID"]);

            using (var context = new GalleryEntities())
            {
                if (FilesBL.RelatedRecordsExists(fileID, context))
                {
                    StatusMessage.MessageType = StatusMessageType.Warning;
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                }
                else
                {
                    try
                    {
                        var query = FilesBL.GetObjectByID(fileID, context);

                        FilesBL.Delete(query, context);

                        query.FileURL.DeleteFile();

                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
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

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    var fileID = DataParser.IntParse(Request.QueryString["FileID"]);

                    var fileEntity = FilesBL.GetObjectByID(fileID, context);

                    if (fileEntity != null)
                    {
                        if (fileEntity.Title != TitleTextBox.Text)
                        {
                            if (FilesBL.Exists(TitleTextBox.Text, FoldersSelectComponent1.DriveID, FoldersSelectComponent1.ParentFolderID, context))
                            {
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                StatusMessage.MessageType = StatusMessageType.Info;
                                return;
                            }
                        }

                        fileEntity.Title = TitleTextBox.Text;
                        fileEntity.SubTitle = SubTitleTextBox.Text;
                        fileEntity.Date = DataParser.NullableDateTimeParse(DateTextBox.Text);
                        fileEntity.Hide = HideCheckBox.Checked;
                        fileEntity.Description = DescriptionEditor.Text;
                        fileEntity.DriveID = FoldersSelectComponent1.DriveID;
                        fileEntity.FolderID = FoldersSelectComponent1.ParentFolderID;
                        fileEntity.FileTypeID = FileTypesDropDownList.GetSelectedValue();
                        fileEntity.ShowWebFile = false;
                        fileEntity.Tags = TagsTextBox.Text;
                        fileEntity.ImageID = ImageSelectorComponent1.ImageID;
                        fileEntity.Locale = LocaleDropDown.Locale;

                        Save(fileEntity, context);
                    }
                }
            }
        }

        private bool Save(GY_Files entity, GalleryEntities context)
        {
            var oldFileURL = entity.FileURL;
            var fileChanged = false;

            if (FileUpload1.HasFile)
            {
                fileChanged = Upload(context, entity);

                if (!fileChanged)
                {
                    return false;
                }
            }

            try
            {
                FilesBL.Save(context);

                if (fileChanged)
                {
                    oldFileURL.DeleteFile();
                }

                StatusMessage.MessageType = StatusMessageType.Success;
                StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;

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

        private bool Upload(GalleryEntities context, GY_Files entity)
        {
            entity.FileURL = FilesBL.GetUploadedFilePath(FileUpload1);

            if (FileUpload1.Success)
            {
                entity.Extension = FileUpload1.FileExtension;
                entity.FileName = FileUpload1.FileUpload.FileName;
                entity.Size = FileUpload1.FileContent.Length;

                return true;
            }
            else
            {
                StatusMessage.MessageType = StatusMessageType.Error;
                StatusMessage.Message = FileUpload1.Message;

                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["FileID"]))
                {
                    var fileID = DataParser.IntParse(Request.QueryString["FileID"]);

                    using (var context = new GalleryEntities())
                    {
                        var query = FilesBL.GetObjectByID(fileID, context);

                        if (query != null)
                        {
                            var fileTypesQuery = (from set in context.GY_FileTypes
                                                  where set.Hide == false
                                                  select set);

                            FileTypesDropDownList.DataTextField = "Title";
                            FileTypesDropDownList.DataValueField = "FileTypeID";
                            FileTypesDropDownList.DataSource = fileTypesQuery.ToList();
                            FileTypesDropDownList.AddSelect();

                            var locale = query.Locale;
                            LocaleDropDown.Locale = locale;
                            InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                            FoldersSelectComponent1.Locale = locale;

                            TitleTextBox.Text = query.Title;
                            SubTitleTextBox.Text = query.SubTitle;
                            DateTextBox.Text = DataParser.GetDateFormattedString(query.Date);
                            DescriptionEditor.Text = query.Description;
                            FileTypesDropDownList.SelectedValue = query.FileTypeID.ToString();
                            FoldersSelectComponent1.DriveID = (int)query.DriveID;
                            FoldersSelectComponent1.ParentFolderID = query.FolderID;
                            HideCheckBox.Checked = query.Hide;
                            TagsTextBox.Text = query.Tags;
                            ImageSelectorComponent1.ImageID = query.ImageID;
                        }
                        else
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/FileList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                ImageSelectorComponent1.StoragePath = LocalStorages.Files;

                DateTextBox.DateFormat = Validator.DateParseExpression;
                DateTextBox.Format = Validator.DateParseExpression;
                DateTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                DateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                DateTextBox.ValidationExpression = Validator.CalendarValidationExpression;
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        private void InitializeLocaleFields(string direction)
        {
            TitleTextBox.Direction = direction;
            TagsTextBox.Direction = direction;
            DescriptionEditor.Direction = direction;
            SubTitleTextBox.Direction = direction;
        }

        private async void LocaleDropDown_LanguageDirectionChanged(object sender, OWDARO.OEventArgs.LanguageDirectionChangedEventArgs e)
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