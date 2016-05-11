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
    public partial class AudioCategoriesManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private async void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var audioCategoryID = DataParser.IntParse(Request.QueryString["AudioCategoryID"]);

            using (var context = new GalleryEntities())
            {
                if (AudioCategoriesBL.RelatedRecordsExists(audioCategoryID, context))
                {
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                    StatusMessage.MessageType = StatusMessageType.Warning;
                }
                else
                {
                    try
                    {
                        AudioCategoriesBL.Delete(audioCategoryID, context);
                        StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
                        StatusMessage.MessageType = StatusMessageType.Success;

                        await AudioCategoriesSelectComponent1.InitializeAudioCategories(context);
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

        private async void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    var audioCategoryID = DataParser.IntParse(Request.QueryString["AudioCategoryID"]);

                    var audioCategoryQuery = AudioCategoriesBL.GetObjectByID(audioCategoryID, context);

                    if (audioCategoryQuery.Title != TitleTextBox.Text)
                    {
                        if (AudioCategoriesBL.Exists(TitleTextBox.Text, AudioCategoriesSelectComponent1.ParentAudioCategoryID, context))
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                            return;
                        }
                    }

                    if (IsMovingToItsChild(audioCategoryQuery) || IsMovingToItsAnyChild(audioCategoryQuery))
                    {
                        StatusMessage.MessageType = StatusMessageType.Warning;
                        StatusMessage.Message = "Invalid Operation";
                        return;
                    }

                    audioCategoryQuery.Title = TitleTextBox.Text;
                    audioCategoryQuery.Description = DescriptionTextBox.Text;
                    audioCategoryQuery.Hide = HideCheckBox.Checked;
                    audioCategoryQuery.ParentAudioCategoryID = AudioCategoriesSelectComponent1.ParentAudioCategoryID;
                    audioCategoryQuery.Locale = LocaleDropDown.Locale;

                    try
                    {
                        AudioCategoriesBL.Save(context);
                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;

                        await AudioCategoriesSelectComponent1.InitializeAudioCategories(context);
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

        private bool IsMovingToItsChild(GY_AudioCategories entity)
        {
            bool isMoving = false;

            if (entity.AudioCategoryID == AudioCategoriesSelectComponent1.ParentAudioCategoryID)
            {
                isMoving = true;
            }

            return isMoving;
        }

        private bool IsMovingToItsAnyChild(GY_AudioCategories entity)
        {
            bool isMoving = false;

            foreach (var audioCategory in entity.GY_ChildAudioCategories)
            {
                if (audioCategory.AudioCategoryID == AudioCategoriesSelectComponent1.ParentAudioCategoryID)
                {
                    isMoving = true;
                    return isMoving;
                }
                else
                {
                    isMoving = IsMovingToItsAnyChild(audioCategory);

                    if (isMoving)
                    {
                        return isMoving;
                    }
                }
            }

            return isMoving;
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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["AudioCategoryID"]))
                {
                    using (var context = new GalleryEntities())
                    {
                        var audioCategoryID = DataParser.IntParse(Request.QueryString["AudioCategoryID"]);

                        var audioCategoryQuery = AudioCategoriesBL.GetObjectByID(audioCategoryID, context);

                        if (audioCategoryQuery != null)
                        {
                            var locale = audioCategoryQuery.Locale;
                            LocaleDropDown.Locale = locale;
                            InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                            AudioCategoriesSelectComponent1.Locale = locale;

                            TitleTextBox.Text = audioCategoryQuery.Title;
                            DescriptionTextBox.Text = audioCategoryQuery.Description;
                            HideCheckBox.Checked = audioCategoryQuery.Hide;
                            AudioCategoriesSelectComponent1.ParentAudioCategoryID = audioCategoryQuery.ParentAudioCategoryID;
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/Gallery/AudioCategoriesList.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/AudioCategoriesList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
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