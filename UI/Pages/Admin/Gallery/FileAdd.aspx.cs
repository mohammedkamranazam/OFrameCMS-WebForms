using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.OEventArgs;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Linq;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class FileAdd : System.Web.UI.Page
    {
        private void Add(GalleryEntities context, GY_Files entity)
        {
            try
            {
                FilesBL.Add(entity, context);
                StatusMessage.MessageType = StatusMessageType.Success;
                StatusMessage.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                StatusMessage.MessageType = StatusMessageType.Error;
                StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);

                entity.FileURL.DeleteFile();
            }
        }

        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    if (FilesBL.Exists(TitleTextBox.Text, FoldersSelectComponent1.DriveID, FoldersSelectComponent1.ParentFolderID, context))
                    {
                        StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                        return;
                    }

                    var entity = new GY_Files();
                    entity.Title = TitleTextBox.Text;
                    entity.SubTitle = SubTitleTextBox.Text;
                    entity.Date = DataParser.NullableDateTimeParse(DateTextBox.Text);
                    entity.Hide = false;
                    entity.Description = DescriptionEditor.Text;
                    entity.DriveID = FoldersSelectComponent1.DriveID;
                    entity.FolderID = FoldersSelectComponent1.ParentFolderID;
                    entity.AddedOn = Utilities.DateTimeNow();
                    entity.DownloadCount = 0;
                    entity.FileTypeID = FileTypesDropDownList.GetSelectedValue();
                    entity.ShowWebFile = false;
                    entity.Tags = TagsTextBox.Text;
                    entity.FileURL = FilesBL.GetUploadedFilePath(FileUpload1);
                    entity.ImageID = ImageSelectorComponent1.ImageID;
                    entity.Locale = LocaleDropDown.Locale;

                    if (FileUpload1.Success)
                    {
                        entity.Extension = FileUpload1.FileExtension;
                        entity.FileName = FileUpload1.FileUpload.FileName;
                        entity.Size = FileUpload1.FileContent.Length;

                        Add(context, entity);
                    }
                    else
                    {
                        StatusMessage.MessageType = StatusMessageType.Error;
                        StatusMessage.Message = FileUpload1.Message;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var locale = AppConfig.DefaultLocale;
                LocaleDropDown.Locale = locale;
                InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                FoldersSelectComponent1.Locale = locale;

                using (var context = new GalleryEntities())
                {
                    var fileTypesQuery = (from set in context.GY_FileTypes
                                          where set.Hide == false
                                          select set);

                    FileTypesDropDownList.DataTextField = "Title";
                    FileTypesDropDownList.DataValueField = "FileTypeID";
                    FileTypesDropDownList.DataSource = fileTypesQuery.ToList();
                    FileTypesDropDownList.AddSelect();
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
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        private void InitializeLocaleFields(string direction)
        {
            TitleTextBox.Direction = direction;
            SubTitleTextBox.Direction = direction;
            TagsTextBox.Direction = direction;
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