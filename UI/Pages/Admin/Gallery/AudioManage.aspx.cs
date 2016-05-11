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
    public partial class AudioManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var audioID = DataParser.IntParse(Request.QueryString["AudioID"]);

            using (var context = new GalleryEntities())
            {
                if (AudiosBL.RelatedRecordsExists(audioID))
                {
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                    StatusMessage.MessageType = StatusMessageType.Warning;
                }
                else
                {
                    try
                    {
                        AudiosBL.Delete(audioID, context);
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
                if (AudioCategoriesSelectComponent1.ParentAudioCategoryID != null)
                {
                    using (var context = new GalleryEntities())
                    {
                        var audioID = DataParser.IntParse(Request.QueryString["AudioID"]);

                        var audioQuery = AudiosBL.GetObjectByID(audioID, context);

                        if (audioQuery.Title != TitleTextBox.Text)
                        {
                            if (AudiosBL.Exists(TitleTextBox.Text, AudioSetDropDownList.GetNullableSelectedValue(), context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                        }

                        audioQuery.Title = TitleTextBox.Text;
                        audioQuery.Description = DescriptionTextBox.Text;
                        audioQuery.Hide = HideCheckBox.Checked;
                        audioQuery.AudioCategoryID = (int)AudioCategoriesSelectComponent1.ParentAudioCategoryID;
                        audioQuery.AudioSetID = AudioSetDropDownList.GetNullableSelectedValue();
                        audioQuery.TakenOn = DataParser.NullableDateTimeParse(TakenOnTextBox.Text);
                        audioQuery.Tags = TagsTextBox.Text;
                        audioQuery.Location = LocationTextBox.Text;
                        audioQuery.Length = AudioLengthTextBox.Text;
                        audioQuery.ShowWebAudio = ShowWebAudioCheckBox.Checked;
                        audioQuery.FileID = FileSelectorComponent1.FileID;
                        audioQuery.Locale = LocaleDropDown.Locale;

                        if (ShowWebAudioCheckBox.Checked)
                        {
                            audioQuery.WebAudioURL = WebAudioURLTextBox.Text;
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
                            AudiosBL.Save(context);

                            StatusMessage.MessageType = StatusMessageType.Success;
                            StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
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
            AudioSetDropDownList.Direction = direction;
        }

        private async void LocaleDropDown_LanguageDirectionChanged(object sender, OWDARO.OEventArgs.LanguageDirectionChangedEventArgs e)
        {
            InitializeLocaleFields(e.Direction);

            AudioCategoriesSelectComponent1.Locale = LocaleDropDown.Locale;
            FileSelectorComponent1.Locale = LocaleDropDown.Locale;

            await AudioCategoriesSelectComponent1.InitializeAudioCategories();
        }

        private void PopulateAudioSet(int categoryID)
        {
            using (var context = new GalleryEntities())
            {
                PopulateAudioSet(categoryID, context);
            }
        }

        private void PopulateAudioSet(int categoryID, GalleryEntities context)
        {
            var query = (from set in context.GY_AudioSet
                         where set.AudioCategoryID == categoryID && set.Locale == LocaleDropDown.Locale
                         select set).ToList();

            if (query.Any())
            {
                AudioSetDropDownList.DataTextField = "Title";
                AudioSetDropDownList.DataValueField = "AudioSetID";
                AudioSetDropDownList.DataSource = query;
                AudioSetDropDownList.AddSelect();

                AudioSetDropDownList.Visible = true;
            }
            else
            {
                AudioSetDropDownList.Items.Clear();
                AudioSetDropDownList.Visible = false;
            }
        }

        private void ShowWebAudioCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            WebAudioURLTextBox.Visible = ShowWebAudioCheckBox.Checked;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["AudioID"]))
                {
                    using (var context = new GalleryEntities())
                    {
                        var audioID = DataParser.IntParse(Request.QueryString["AudioID"]);

                        var audioQuery = AudiosBL.GetObjectByID(audioID, context);

                        if (audioQuery != null)
                        {
                            var locale = audioQuery.Locale;
                            LocaleDropDown.Locale = locale;
                            InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                            AudioCategoriesSelectComponent1.Locale = locale;
                            FileSelectorComponent1.Locale = locale;

                            AudioCategoriesSelectComponent1.ParentAudioCategoryID = audioQuery.AudioCategoryID;
                            AudioCategoriesSelectComponent1.InitialValue = audioQuery.AudioCategoryID;

                            TitleTextBox.Text = audioQuery.Title;
                            DescriptionTextBox.Text = audioQuery.Description;
                            HideCheckBox.Checked = audioQuery.Hide;
                            TakenOnTextBox.Text = DataParser.GetDateFormattedString(audioQuery.TakenOn);
                            TagsTextBox.Text = audioQuery.Tags;
                            LocationTextBox.Text = audioQuery.Location;
                            AudioLengthTextBox.Text = audioQuery.Length;
                            WebAudioURLTextBox.Text = audioQuery.WebAudioURL;
                            WebAudioURLTextBox.Visible = ShowWebAudioCheckBox.Checked = audioQuery.ShowWebAudio;
                            FileSelectorComponent1.FileID = audioQuery.FileID;

                            PopulateAudioSet(audioQuery.AudioCategoryID, context);
                            AudioSetDropDownList.SelectedValue = audioQuery.AudioSetID.ToString();
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/Gallery/AudioList.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/AudioList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                AudioLengthTextBox.ValidationExpression = Validator.FloatValidationExpression;
                AudioLengthTextBox.ValidationErrorMessage = Validator.FloatValidationErrorMessage;

                TakenOnTextBox.DateFormat = Validator.DateParseExpression;
                TakenOnTextBox.Format = Validator.DateParseExpression;
                TakenOnTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                TakenOnTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                TakenOnTextBox.ValidationExpression = Validator.CalendarValidationExpression;
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
            ShowWebAudioCheckBox.CheckedChanged += ShowWebAudioCheckBox_CheckedChanged;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
            AudioCategoriesSelectComponent1.AudioCategoryOpened += AudioCategoriesSelectComponent1_AudioCategoryOpened;
        }

        private void AudioCategoriesSelectComponent1_AudioCategoryOpened(object sender, OWDARO.OEventArgs.AudioCategoryOpenedEventArgs e)
        {
            PopulateAudioSet(e.AudioCategoryID);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}