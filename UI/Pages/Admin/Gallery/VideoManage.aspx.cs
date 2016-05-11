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
    public partial class VideoManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var videoID = DataParser.IntParse(Request.QueryString["VideoID"]);

            using (var context = new GalleryEntities())
            {
                if (VideosBL.RelatedRecordsExists(videoID))
                {
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                    StatusMessage.MessageType = StatusMessageType.Warning;
                }
                else
                {
                    try
                    {
                        VideosBL.Delete(videoID, context);
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
                        var videoID = DataParser.IntParse(Request.QueryString["VideoID"]);

                        var videoQuery = VideosBL.GetObjectByID(videoID, context);

                        if (videoQuery.Title != TitleTextBox.Text)
                        {
                            if (VideosBL.Exists(TitleTextBox.Text, VideoSetDropDownList.GetNullableSelectedValue(), context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                        }

                        videoQuery.Title = TitleTextBox.Text;
                        videoQuery.Description = DescriptionTextBox.Text;
                        videoQuery.Hide = HideCheckBox.Checked;
                        videoQuery.VideoCategoryID = (int)VideoCategoriesSelectComponent1.ParentVideoCategoryID;
                        videoQuery.VideoSetID = VideoSetDropDownList.GetNullableSelectedValue();
                        videoQuery.TakenOn = DataParser.NullableDateTimeParse(TakenOnTextBox.Text);
                        videoQuery.Tags = TagsTextBox.Text;
                        videoQuery.Location = LocationTextBox.Text;
                        videoQuery.Length = VideoLengthTextBox.Text;
                        videoQuery.ShowWebVideo = ShowWebVideoCheckBox.Checked;
                        videoQuery.FileID = FileSelectorComponent1.FileID;
                        videoQuery.Locale = LocaleDropDown.Locale;

                        if (ShowWebVideoCheckBox.Checked)
                        {
                            videoQuery.WebVideoURL = WebVideoURLTextBox.Text;
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
                            VideosBL.Save(context);

                            StatusMessage.MessageType = StatusMessageType.Success;
                            StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;

                            VideoImage.ImageUrl = string.Format("http://i1.ytimg.com/vi/{0}/mqdefault.jpg", videoQuery.WebVideoURL);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);
                            StatusMessage.MessageType = StatusMessageType.Error;
                            StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                        }
                    }
                }
                else
                {
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
            WebVideoURLTextBox.Visible = ShowWebVideoCheckBox.Checked;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["VideoID"]))
                {
                    using (var context = new GalleryEntities())
                    {
                        var videoID = DataParser.IntParse(Request.QueryString["VideoID"]);

                        var videoQuery = VideosBL.GetObjectByID(videoID, context);

                        if (videoQuery != null)
                        {
                            var locale = videoQuery.Locale;
                            LocaleDropDown.Locale = locale;
                            InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                            VideoCategoriesSelectComponent1.Locale = locale;
                            FileSelectorComponent1.Locale = locale;

                            VideoCategoriesSelectComponent1.ParentVideoCategoryID = videoQuery.VideoCategoryID;
                            VideoCategoriesSelectComponent1.InitialValue = videoQuery.VideoCategoryID;

                            TitleTextBox.Text = videoQuery.Title;
                            DescriptionTextBox.Text = videoQuery.Description;
                            HideCheckBox.Checked = videoQuery.Hide;
                            TakenOnTextBox.Text = DataParser.GetDateFormattedString(videoQuery.TakenOn);
                            TagsTextBox.Text = videoQuery.Tags;
                            LocationTextBox.Text = videoQuery.Location;
                            VideoImage.ImageUrl = VideosBL.GetVideoImageThumbURL(videoQuery, context);
                            fancybox.NavigateUrl = VideosBL.GetVideoImageURL(videoQuery, context);
                            VideoLengthTextBox.Text = videoQuery.Length;
                            WebVideoURLTextBox.Text = videoQuery.WebVideoURL;
                            WebVideoURLTextBox.Visible = ShowWebVideoCheckBox.Checked = videoQuery.ShowWebVideo;
                            FileSelectorComponent1.FileID = videoQuery.FileID;

                            PopulateVideoSet(context, videoQuery.VideoCategoryID);
                            VideoSetDropDownList.SelectedValue = videoQuery.VideoSetID.ToString();
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/Gallery/VideoList.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/VideoList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                VideoLengthTextBox.ValidationExpression = Validator.FloatValidationExpression;
                VideoLengthTextBox.ValidationErrorMessage = Validator.FloatValidationErrorMessage;

                TakenOnTextBox.DateFormat = Validator.DateParseExpression;
                TakenOnTextBox.Format = Validator.DateParseExpression;
                TakenOnTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                TakenOnTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                TakenOnTextBox.ValidationExpression = Validator.CalendarValidationExpression;
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
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