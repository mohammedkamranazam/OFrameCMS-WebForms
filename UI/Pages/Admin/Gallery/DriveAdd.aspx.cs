using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.OEventArgs;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class DriveAdd : System.Web.UI.Page
    {
        private void Add(GalleryEntities context, GY_Drives entity)
        {
            try
            {
                DrivesBL.Add(entity, context);
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
                    if (DrivesBL.Exists(TitleTextBox.Text, context))
                    {
                        StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                        StatusMessage.MessageType = StatusMessageType.Info;
                    }
                    else
                    {
                        var entity = new GY_Drives();
                        entity.Title = TitleTextBox.Text;
                        entity.Hide = false;
                        entity.Description = DescriptionEditor.Text;
                        entity.ImageID = ImageSelectorComponent1.ImageID;
                        entity.Locale = LocaleDropDown.Locale;

                        Add(context, entity);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ImageSelectorComponent1.StoragePath = LocalStorages.Drives;

                var locale = AppConfig.DefaultLocale;
                LocaleDropDown.Locale = locale;
                InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        private void LocaleDropDown_LanguageDirectionChanged(object sender, LanguageDirectionChangedEventArgs e)
        {
            InitializeLocaleFields(e.Direction);
        }

        private void InitializeLocaleFields(string direction)
        {
            TitleTextBox.Direction = direction;
            DescriptionEditor.Direction = direction;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}