using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Linq;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class VideoAdd : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (VideoCategoriesSelectComponent1.ParentVideoCategoryID != null)
                {
                    using (var context = new GalleryEntities())
                    {
                        if (VideosBL.Exists(TitleTextBox.Text, VideoSetDropDownList.GetSelectedValue(), context))
                        {
                            StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                        }
                        else
                        {
                            var entity = new GY_Videos();
                            entity.Title = TitleTextBox.Text;
                            entity.Description = DescriptionTextBox.Text;
                            entity.Hide = false;
                            entity.VideoCategoryID = (int)VideoCategoriesSelectComponent1.ParentVideoCategoryID;
                            entity.VideoSetID = VideoSetDropDownList.GetNullableSelectedValue();
                            entity.TakenOn = DataParser.NullableDateTimeParse(TakenOnTextBox.Text);
                            entity.AddedOn = Utilities.DateTimeNow();
                            entity.Location = LocationTextBox.Text;
                            entity.Length = VideoLengthTextBox.Text;
                            entity.ShowWebVideo = ShowWebVideoCheckBox.Checked;
                            entity.Tags = TagsTextBox.Text;
                            entity.FileID = FileSelectorComponent1.FileID;
                            entity.Locale = LocaleDropDown.Locale;

                            if (ShowWebVideoCheckBox.Checked)
                            {
                                entity.WebVideoURL = YoutubeVideoURLTextBox.Text;
                            }
                            else
                            {
                                if (FileSelectorComponent1.FileID == null)
                                {
                                    StatusMessage.MessageType = StatusMessageType.Warning;
                                    StatusMessage.Message = "no file selected";
                                    return;
                                }
                            }

                            try
                            {
                                VideosBL.Add(entity, context);
                                StatusMessage.MessageType = StatusMessageType.Success;
                                StatusMessage.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;
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
            VideoSetDropDownList.Direction = direction;
        }

        private async void LocaleDropDown_LanguageDirectionChanged(object sender, OWDARO.OEventArgs.LanguageDirectionChangedEventArgs e)
        {
            InitializeLocaleFields(e.Direction);

            VideoCategoriesSelectComponent1.Locale = LocaleDropDown.Locale;

            FileSelectorComponent1.Locale = LocaleDropDown.Locale;

            await VideoCategoriesSelectComponent1.InitializeVideoCategories();
        }

        private void PopulateVideoSet(int categoryID)
        {
            using (var context = new GalleryEntities())
            {
                PopulateVideoSet(context, categoryID);
            }
        }

        private void PopulateVideoSet(GalleryEntities context, int categoryID)
        {
            var videoSetQuery = (from set in context.GY_VideoSet
                                 where set.Hide == false && set.Locale == LocaleDropDown.Locale && set.VideoCategoryID == categoryID
                                 select set).ToList();

            if (videoSetQuery.Any())
            {
                VideoSetDropDownList.DataTextField = "Title";
                VideoSetDropDownList.DataValueField = "VideoSetID";
                VideoSetDropDownList.DataSource = videoSetQuery;
                VideoSetDropDownList.AddSelect();

                VideoSetDropDownList.Visible = true;
            }
            else
            {
                VideoSetDropDownList.Items.Clear();
                VideoSetDropDownList.Visible = false;
            }
        }

        private void ShowWebVideoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowWebVideoCheckBox.Checked)
            {
                YoutubeVideoURLTextBox.Visible = true;
            }
            else
            {
                YoutubeVideoURLTextBox.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var locale = AppConfig.DefaultLocale;
                LocaleDropDown.Locale = locale;
                InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                VideoCategoriesSelectComponent1.Locale = locale;
                FileSelectorComponent1.Locale = locale;

                TakenOnTextBox.DateFormat = Validator.DateParseExpression;
                TakenOnTextBox.Format = Validator.DateParseExpression;
                TakenOnTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                TakenOnTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                TakenOnTextBox.ValidationExpression = Validator.CalendarValidationExpression;

                VideoLengthTextBox.ValidationExpression = Validator.FloatValidationExpression;
                VideoLengthTextBox.ValidationErrorMessage = Validator.FloatValidationErrorMessage;
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            VideoCategoriesSelectComponent1.VideoCategoryOpened += VideoCategoriesSelectComponent1_VideoCategoryOpened;
            ShowWebVideoCheckBox.CheckedChanged += ShowWebVideoCheckBox_CheckedChanged;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        private void VideoCategoriesSelectComponent1_VideoCategoryOpened(object sender, OWDARO.OEventArgs.VideoCategoryOpenedEventArgs e)
        {
            PopulateVideoSet(e.VideoCategoryID);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}