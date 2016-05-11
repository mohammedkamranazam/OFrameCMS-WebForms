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
    public partial class AudioAdd : System.Web.UI.Page
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
                if (AudioCategoriesSelectComponent1.ParentAudioCategoryID != null)
                {
                    using (var context = new GalleryEntities())
                    {
                        if (AudiosBL.Exists(TitleTextBox.Text, AudioSetDropDownList.GetSelectedValue(), context))
                        {
                            StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                        }
                        else
                        {
                            var entity = new GY_Audios();
                            entity.Title = TitleTextBox.Text;
                            entity.Description = DescriptionTextBox.Text;
                            entity.Hide = false;
                            entity.AudioCategoryID = (int)AudioCategoriesSelectComponent1.ParentAudioCategoryID;
                            entity.AudioSetID = AudioSetDropDownList.GetNullableSelectedValue();
                            entity.TakenOn = DataParser.NullableDateTimeParse(TakenOnTextBox.Text);
                            entity.AddedOn = Utilities.DateTimeNow();
                            entity.Location = LocationTextBox.Text;
                            entity.Length = AudioLengthTextBox.Text;
                            entity.ShowWebAudio = ShowWebAudioCheckBox.Checked;
                            entity.Tags = TagsTextBox.Text;
                            entity.FileID = FileSelectorComponent1.FileID;
                            entity.Locale = LocaleDropDown.Locale;

                            if (ShowWebAudioCheckBox.Checked)
                            {
                                entity.WebAudioURL = WebAudioURLTextBox.Text;
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
                                AudiosBL.Add(entity, context);
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
            if (ShowWebAudioCheckBox.Checked)
            {
                WebAudioURLTextBox.Visible = true;
            }
            else
            {
                WebAudioURLTextBox.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var locale = AppConfig.DefaultLocale;
                LocaleDropDown.Locale = locale;
                InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                AudioCategoriesSelectComponent1.Locale = locale;
                FileSelectorComponent1.Locale = locale;

                TakenOnTextBox.DateFormat = Validator.DateParseExpression;
                TakenOnTextBox.Format = Validator.DateParseExpression;
                TakenOnTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                TakenOnTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                TakenOnTextBox.ValidationExpression = Validator.CalendarValidationExpression;

                AudioLengthTextBox.ValidationExpression = Validator.FloatValidationExpression;
                AudioLengthTextBox.ValidationErrorMessage = Validator.FloatValidationErrorMessage;
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
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