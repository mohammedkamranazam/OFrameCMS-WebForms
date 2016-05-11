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
    public partial class AudioSetManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var audioSetID = DataParser.IntParse(Request.QueryString["AudioSetID"]);

            using (var context = new GalleryEntities())
            {
                if (AudioSetBL.RelatedRecordsExists(audioSetID, context))
                {
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                    StatusMessage.MessageType = StatusMessageType.Warning;
                }
                else
                {
                    try
                    {
                        AudioSetBL.Delete(audioSetID, context);
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
                        var audioSetID = DataParser.IntParse(Request.QueryString["AudioSetID"]);

                        var audioSetQuery = AudioSetBL.GetObjectByID(audioSetID, context);

                        if (audioSetQuery.Title != TitleTextBox.Text)
                        {
                            if (AudioSetBL.Exists(TitleTextBox.Text, (int)AudioCategoriesSelectComponent1.ParentAudioCategoryID, context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                        }

                        audioSetQuery.Title = TitleTextBox.Text;
                        audioSetQuery.Description = DescriptionTextBox.Text;
                        audioSetQuery.Hide = HideCheckBox.Checked;
                        audioSetQuery.AudioCategoryID = (int)AudioCategoriesSelectComponent1.ParentAudioCategoryID;
                        audioSetQuery.TakenOn = DataParser.NullableDateTimeParse(TakenOnTextBox.Text);
                        audioSetQuery.Tags = TagsTextBox.Text;
                        audioSetQuery.Location = LocationTextBox.Text;
                        audioSetQuery.Locale = LocaleDropDown.Locale;

                        try
                        {
                            AudioSetBL.Save(context);

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
            LocationTextBox.Direction = direction;
            TagsTextBox.Direction = direction;
            DescriptionTextBox.Direction = direction;
        }

        private void LocaleDropDown_LanguageDirectionChanged(object sender, OWDARO.OEventArgs.LanguageDirectionChangedEventArgs e)
        {
            InitializeLocaleFields(e.Direction);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["AudioSetID"]))
                {
                    using (var context = new GalleryEntities())
                    {
                        var audioSetID = DataParser.IntParse(Request.QueryString["AudioSetID"]);

                        var audioSetQuery = AudioSetBL.GetObjectByID(audioSetID, context);

                        if (audioSetQuery != null)
                        {
                            var locale = audioSetQuery.Locale;
                            LocaleDropDown.Locale = locale;
                            InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                            AudioCategoriesSelectComponent1.Locale = locale;

                            TitleTextBox.Text = audioSetQuery.Title;
                            DescriptionTextBox.Text = audioSetQuery.Description;
                            HideCheckBox.Checked = audioSetQuery.Hide;
                            TakenOnTextBox.Text = DataParser.GetDateFormattedString(audioSetQuery.TakenOn);
                            AudioCategoriesSelectComponent1.ParentAudioCategoryID = audioSetQuery.AudioCategoryID;
                            TagsTextBox.Text = audioSetQuery.Tags;
                            LocationTextBox.Text = audioSetQuery.Location;
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/Gallery/AudioSetList.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/AudioSetList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                TakenOnTextBox.DateFormat = Validator.DateParseExpression;
                TakenOnTextBox.Format = Validator.DateParseExpression;
                TakenOnTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                TakenOnTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                TakenOnTextBox.ValidationExpression = Validator.CalendarValidationExpression;
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