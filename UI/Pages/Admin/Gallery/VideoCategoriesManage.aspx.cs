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
    public partial class VideoCategoriesManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var videoCategoryID = DataParser.IntParse(Request.QueryString["VideoCategoryID"]);

            using (var context = new GalleryEntities())
            {
                if (VideoCategoriesBL.RelatedRecordsExists(videoCategoryID, context))
                {
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                    StatusMessage.MessageType = StatusMessageType.Warning;
                }
                else
                {
                    try
                    {
                        VideoCategoriesBL.Delete(videoCategoryID, context);
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
                using (var context = new GalleryEntities())
                {
                    var videoCategoryID = DataParser.IntParse(Request.QueryString["VideoCategoryID"]);

                    var videoCategoryQuery = VideoCategoriesBL.GetObjectByID(videoCategoryID, context);

                    if (videoCategoryQuery.Title != TitleTextBox.Text)
                    {
                        if (VideoCategoriesBL.Exists(TitleTextBox.Text, (int)VideoCategoriesSelectComponent1.ParentVideoCategoryID, context))
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                            return;
                        }
                    }

                    if (IsMovingToItsChild(videoCategoryQuery) || IsMovingToItsAnyChild(videoCategoryQuery))
                    {
                        StatusMessage.MessageType = StatusMessageType.Warning;
                        StatusMessage.Message = "Invalid Operation";
                        return;
                    }

                    videoCategoryQuery.Title = TitleTextBox.Text;
                    videoCategoryQuery.Description = DescriptionTextBox.Text;
                    videoCategoryQuery.Hide = HideCheckBox.Checked;
                    videoCategoryQuery.ParentVideoCategoryID = VideoCategoriesSelectComponent1.ParentVideoCategoryID;
                    videoCategoryQuery.ImageID = ImageSelectorComponent1.ImageID;
                    videoCategoryQuery.Locale = LocaleDropDown.Locale;

                    try
                    {
                        VideoCategoriesBL.Save(context);
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
        }

        private bool IsMovingToItsChild(GY_VideoCategories entity)
        {
            bool isMoving = false;

            if (entity.VideoCategoryID == VideoCategoriesSelectComponent1.ParentVideoCategoryID)
            {
                isMoving = true;
            }

            return isMoving;
        }

        private bool IsMovingToItsAnyChild(GY_VideoCategories entity)
        {
            bool isMoving = false;

            foreach (var postCategory in entity.GY_ChildVideoCategories)
            {
                if (postCategory.VideoCategoryID == VideoCategoriesSelectComponent1.ParentVideoCategoryID)
                {
                    isMoving = true;
                    return isMoving;
                }
                else
                {
                    isMoving = IsMovingToItsAnyChild(postCategory);

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

            VideoCategoriesSelectComponent1.Locale = LocaleDropDown.Locale;

            await VideoCategoriesSelectComponent1.InitializeVideoCategories();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["VideoCategoryID"]))
                {
                    using (var context = new GalleryEntities())
                    {
                        var videoCategoryID = DataParser.IntParse(Request.QueryString["VideoCategoryID"]);

                        var videoCategoryQuery = VideoCategoriesBL.GetObjectByID(videoCategoryID, context);

                        if (videoCategoryQuery != null)
                        {
                            var locale = videoCategoryQuery.Locale;
                            LocaleDropDown.Locale = locale;
                            InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                            VideoCategoriesSelectComponent1.Locale = locale;

                            TitleTextBox.Text = videoCategoryQuery.Title;
                            DescriptionTextBox.Text = videoCategoryQuery.Description;
                            HideCheckBox.Checked = videoCategoryQuery.Hide;
                            ImageSelectorComponent1.ImageID = videoCategoryQuery.ImageID;
                            VideoCategoriesSelectComponent1.ParentVideoCategoryID = videoCategoryQuery.ParentVideoCategoryID;
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/Gallery/VideoCategoriesList.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/VideoCategoriesList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                ImageSelectorComponent1.StoragePath = LocalStorages.VideoCategories;
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