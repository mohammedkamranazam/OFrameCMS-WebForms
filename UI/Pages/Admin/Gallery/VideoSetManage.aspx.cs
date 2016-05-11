using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class VideoSetManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var videoSetID = DataParser.IntParse(Request.QueryString["VideoSetID"]);

            using (var context = new GalleryEntities())
            {
                if (VideoSetBL.RelatedRecordsExists(videoSetID, context))
                {
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                    StatusMessage.MessageType = StatusMessageType.Warning;
                }
                else
                {
                    try
                    {
                        VideoSetBL.Delete(videoSetID, context);
                        StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
                        StatusMessage.MessageType = StatusMessageType.Success;
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                        StatusMessage.MessageType = StatusMessageType.Error;
                    }
                }
            }
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (VideoCategoriesSelectComponent1.ParentVideoCategoryID != null)
                {
                    using (var context = new GalleryEntities())
                    {
                        var videoSetID = DataParser.IntParse(Request.QueryString["VideoSetID"]);

                        var videSetEntity = VideoSetBL.GetObjectByID(videoSetID, context);

                        if (videSetEntity != null)
                        {
                            if (videSetEntity.Title != TitleTextBox.Text)
                            {
                                if (VideoSetBL.Exists(TitleTextBox.Text, (int)VideoCategoriesSelectComponent1.ParentVideoCategoryID, context))
                                {
                                    StatusMessage.MessageType = StatusMessageType.Info;
                                    StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                    return;
                                }
                            }

                            videSetEntity.Title = TitleTextBox.Text;
                            videSetEntity.Description = DescriptionTextBox.Text;
                            videSetEntity.Hide = HideCheckBox.Checked;
                            videSetEntity.VideoCategoryID = (int)VideoCategoriesSelectComponent1.ParentVideoCategoryID;
                            videSetEntity.TakenOn = DataParser.NullableDateTimeParse(TakenOnTextBox.Text);
                            videSetEntity.Tags = TagsTextBox.Text;
                            videSetEntity.Location = LocationTextBox.Text;
                            videSetEntity.ImageID = ImageSelectorComponent1.ImageID;
                            videSetEntity.Locale = LocaleDropDown.Locale;

                            Save(videSetEntity, context);
                        }
                    }
                }
                else
                {
                    StatusMessage.Message = "select a Category first";
                    StatusMessage.MessageType = StatusMessageType.Warning;
                }
            }
        }

        private void InitializeLocaleFields(string direction)
        {
            TitleTextBox.Direction = direction;
            DescriptionTextBox.Direction = direction;
            TagsTextBox.Direction = direction;
            LocationTextBox.Direction = direction;
        }

        private async void LocaleDropDown_LanguageDirectionChanged(object sender, OWDARO.OEventArgs.LanguageDirectionChangedEventArgs e)
        {
            InitializeLocaleFields(e.Direction);

            VideoCategoriesSelectComponent1.Locale = LocaleDropDown.Locale;

            await VideoCategoriesSelectComponent1.InitializeVideoCategories();
        }

        private bool Save(GY_VideoSet albumEntity, GalleryEntities context)
        {
            try
            {
                VideoSetBL.Save(context);

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["VideoSetID"]))
                {
                    using (var context = new GalleryEntities())
                    {
                        var videoSetID = DataParser.IntParse(Request.QueryString["VideoSetID"]);

                        var videoSetQuery = VideoSetBL.GetObjectByID(videoSetID, context);

                        if (videoSetQuery != null)
                        {
                            var locale = videoSetQuery.Locale;
                            LocaleDropDown.Locale = locale;
                            InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                            VideoCategoriesSelectComponent1.Locale = locale;

                            VideoCategoriesSelectComponent1.ParentVideoCategoryID = videoSetQuery.VideoCategoryID;
                            VideoCategoriesSelectComponent1.InitialValue = videoSetQuery.VideoCategoryID;

                            TitleTextBox.Text = videoSetQuery.Title;
                            DescriptionTextBox.Text = videoSetQuery.Description;
                            HideCheckBox.Checked = videoSetQuery.Hide;
                            TakenOnTextBox.Text = DataParser.GetDateFormattedString(videoSetQuery.TakenOn);
                            TagsTextBox.Text = videoSetQuery.Tags;
                            LocationTextBox.Text = videoSetQuery.Location;
                            ImageSelectorComponent1.ImageID = videoSetQuery.ImageID;
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/Gallery/VideoSetList.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/VideoSetList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                TakenOnTextBox.DateFormat = Validator.DateParseExpression;
                TakenOnTextBox.Format = Validator.DateParseExpression;
                TakenOnTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                TakenOnTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                TakenOnTextBox.ValidationExpression = Validator.CalendarValidationExpression;

                ImageSelectorComponent1.StoragePath = LocalStorages.VideoSet;
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}