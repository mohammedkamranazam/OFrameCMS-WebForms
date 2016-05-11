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
    public partial class AudioCategoriesAdd : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private async void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    if (AudioCategoriesBL.Exists(TitleTextBox.Text, AudioCategoriesSelectComponent1.ParentAudioCategoryID, context))
                    {
                        StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                    }
                    else
                    {
                        var entity = new GY_AudioCategories();

                        entity.Title = TitleTextBox.Text;
                        entity.Description = DescriptionTextBox.Text;
                        entity.Hide = false;
                        entity.ParentAudioCategoryID = AudioCategoriesSelectComponent1.ParentAudioCategoryID;
                        entity.Locale = LocaleDropDown.Locale;

                        try
                        {
                            AudioCategoriesBL.Add(entity, context);
                            StatusMessage.MessageType = StatusMessageType.Success;
                            StatusMessage.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;

                            await AudioCategoriesSelectComponent1.InitializeAudioCategories();
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
        }

        private void InitializeLocaleFields(string direction)
        {
            TitleTextBox.Direction = direction;
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