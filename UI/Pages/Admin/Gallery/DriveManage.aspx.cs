using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class DriveManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Admin/Gallery/DriveList.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var driveID = DataParser.IntParse(Request.QueryString["DriveID"]);

            using (var context = new GalleryEntities())
            {
                if (DrivesBL.RelatedRecordsExists(driveID, context))
                {
                    StatusMessage.MessageType = StatusMessageType.Warning;
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                }
                else
                {
                    try
                    {
                        var query = DrivesBL.GetObjectByID(driveID, context);

                        DrivesBL.Delete(query, context);

                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
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

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    var driveID = DataParser.IntParse(Request.QueryString["DriveID"]);

                    var driveEntity = DrivesBL.GetObjectByID(driveID, context);

                    if (driveEntity != null)
                    {
                        if (driveEntity.Title != TitleTextBox.Text)
                        {
                            if (DrivesBL.Exists(TitleTextBox.Text, context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                        }

                        driveEntity.Title = TitleTextBox.Text;
                        driveEntity.Description = DescriptionEditor.Text;
                        driveEntity.Hide = HideCheckBox.Checked;
                        driveEntity.ImageID = ImageSelectorComponent1.ImageID;
                        driveEntity.Locale = LocaleDropDown.Locale;

                        Save(driveEntity, context);
                    }
                }
            }
        }

        private bool Save(GY_Drives entity, GalleryEntities context)
        {
            try
            {
                DrivesBL.Save(context);

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
                if (string.IsNullOrWhiteSpace(Request.QueryString["DriveID"]))
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/DriveList.aspx");
                }

                var driveID = DataParser.IntParse(Request.QueryString["DriveID"]);

                using (var context = new GalleryEntities())
                {
                    var query = DrivesBL.GetObjectByID(driveID, context);

                    if (query != null)
                    {
                        var locale = query.Locale;
                        LocaleDropDown.Locale = locale;
                        InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                        TitleTextBox.Text = query.Title;
                        DescriptionEditor.Text = query.Description;
                        HideCheckBox.Checked = query.Hide;
                        ImageSelectorComponent1.ImageID = query.ImageID;
                    }
                    else
                    {
                        StatusMessage.MessageType = StatusMessageType.Info;
                        StatusMessage.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                    }
                }

                ImageSelectorComponent1.StoragePath = LocalStorages.Drives;
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        private void LocaleDropDown_LanguageDirectionChanged(object sender, OWDARO.OEventArgs.LanguageDirectionChangedEventArgs e)
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