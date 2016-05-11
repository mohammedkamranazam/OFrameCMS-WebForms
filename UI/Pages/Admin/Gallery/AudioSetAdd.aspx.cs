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
    public partial class AudioSetAdd : System.Web.UI.Page
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
                        if (AudioSetBL.Exists(TitleTextBox.Text, (int)AudioCategoriesSelectComponent1.ParentAudioCategoryID, context))
                        {
                            StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                        }
                        else
                        {
                            var entity = new GY_AudioSet();
                            entity.Title = TitleTextBox.Text;
                            entity.Description = DescriptionTextBox.Text;
                            entity.Hide = false;
                            entity.AudioCategoryID = (int)AudioCategoriesSelectComponent1.ParentAudioCategoryID;
                            entity.TakenOn = DataParser.NullableDateTimeParse(TakenOnTextBox.Text);
                            entity.AddedOn = Utilities.DateTimeNow();
                            entity.Location = LocationTextBox.Text;
                            entity.Tags = TagsTextBox.Text;
                            entity.Locale = LocaleDropDown.Locale;

                            try
                            {
                                AudioSetBL.Add(entity, context);
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
            LocationTextBox.Direction = direction;
            TagsTextBox.Direction = direction;
            DescriptionTextBox.Direction = direction;
        }

        private async void LocaleDropDown_LanguageDirectionChanged(object sender, OWDARO.OEventArgs.LanguageDirectionChangedEventArgs e)
        {
            InitializeLocaleFields(e.Direction);

            AudioCategoriesSelectComponent1.Locale = LocaleDropDown.Locale;

            await AudioCategoriesSelectComponent1.LoadData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var locale = AppConfig.DefaultLocale;
                LocaleDropDown.Locale = locale;
                InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                AudioCategoriesSelectComponent1.Locale = locale;

                TakenOnTextBox.DateFormat = Validator.DateParseExpression;
                TakenOnTextBox.Format = Validator.DateParseExpression;
                TakenOnTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                TakenOnTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                TakenOnTextBox.ValidationExpression = Validator.CalendarValidationExpression;
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}