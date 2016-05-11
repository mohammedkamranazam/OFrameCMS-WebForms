using OWDARO;
using OWDARO.BLL.MediaBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.OEventArgs;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Threading.Tasks;

namespace ProjectJKL.UI.Pages.Admin.Media
{
    public partial class PostCategoriesManage : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }

        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private async void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var postCategoryID = DataParser.IntParse(Request.QueryString["PostCategoryID"]);

            using (var context = new MediaEntities())
            {
                if (PostCategoriesBL.RelatedRecordsExists(postCategoryID, context))
                {
                    StatusMessage.MessageType = StatusMessageType.Warning;
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                }
                else
                {
                    try
                    {
                        var postCategoryQuery = await PostCategoriesBL.GetObjectByIDAsync(postCategoryID, context);

                        PostCategoriesBL.Delete(postCategoryQuery.PostCategoryID, context);

                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;

                        await PostCategoriesSelectComponent1.InitializePostCategories(context);
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

        private async void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new MediaEntities())
                {
                    var postCategoryID = DataParser.IntParse(Request.QueryString["PostCategoryID"]);

                    var postCategoryQuery = await PostCategoriesBL.GetObjectByIDAsync(postCategoryID, context);

                    if (postCategoryQuery != null)
                    {
                        if (postCategoryQuery.Title != TitleTextBox.Text)
                        {
                            if (PostCategoriesBL.Exists(TitleTextBox.Text, PostCategoriesSelectComponent1.ParentPostCategoryID, context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                        }

                        if (IsMovingToItsChild(postCategoryQuery) || IsMovingToItsAnyChild(postCategoryQuery))
                        {
                            StatusMessage.MessageType = StatusMessageType.Warning;
                            StatusMessage.Message = "Invalid Operation";
                            return;
                        }

                        postCategoryQuery.Title = TitleTextBox.Text;
                        postCategoryQuery.Description = DescriptionTextBox.Text;
                        postCategoryQuery.ParentPostCategoryID = PostCategoriesSelectComponent1.ParentPostCategoryID;
                        postCategoryQuery.Hide = HideCheckBox.Checked;
                        postCategoryQuery.ImageID = ImageSelectorComponent1.ImageID;
                        postCategoryQuery.Locale = LocaleDropDown.Locale;

                        await Save(postCategoryQuery, context);
                    }
                }
            }
        }

        private bool IsMovingToItsChild(ME_PostCategories entity)
        {
            bool isMoving = false;

            if (entity.PostCategoryID == PostCategoriesSelectComponent1.ParentPostCategoryID)
            {
                isMoving = true;
            }

            return isMoving;
        }

        private bool IsMovingToItsAnyChild(ME_PostCategories entity)
        {
            bool isMoving = false;

            foreach (var postCategory in entity.ME_ChildPostCategories)
            {
                if (postCategory.PostCategoryID == PostCategoriesSelectComponent1.ParentPostCategoryID)
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

        private async Task<bool> Save(ME_PostCategories entity, MediaEntities context)
        {
            try
            {
                PostCategoriesBL.Save(context);

                StatusMessage.MessageType = StatusMessageType.Success;
                StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;

                await PostCategoriesSelectComponent1.InitializePostCategories(context);

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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["PostCategoryID"]))
                {
                    var postCategoryID = DataParser.IntParse(Request.QueryString["PostCategoryID"]);

                    using (var context = new MediaEntities())
                    {
                        var query = PostCategoriesBL.GetObjectByID(postCategoryID, context);

                        if (query != null)
                        {
                            var locale = query.Locale;
                            LocaleDropDown.Locale = locale;
                            InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                            PostCategoriesSelectComponent1.Locale = locale;

                            TitleTextBox.Text = query.Title;
                            DescriptionTextBox.Text = query.Description;
                            HideCheckBox.Checked = query.Hide;
                            PostCategoriesSelectComponent1.ParentPostCategoryID = query.ParentPostCategoryID;
                            ImageSelectorComponent1.ImageID = query.ImageID;
                        }
                        else
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                        }
                    }

                    ImageSelectorComponent1.StoragePath = LocalStorages.PostCategories;
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Media/PostCategoriesList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        private void InitializeLocaleFields(string direction)
        {
            TitleTextBox.Direction = direction;
            DescriptionTextBox.Direction = direction;
        }

        private async void LocaleDropDown_LanguageDirectionChanged(object sender, LanguageDirectionChangedEventArgs e)
        {
            InitializeLocaleFields(e.Direction);

            PostCategoriesSelectComponent1.Locale = LocaleDropDown.Locale;

            await PostCategoriesSelectComponent1.LoadData();
        }
    }
}