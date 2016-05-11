using OWDARO;
using OWDARO.BLL.MediaBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ProjectJKL.UI.Pages.Admin.Media
{
    public partial class PostsAdd : System.Web.UI.Page
    {
        private void Add(MediaEntities context, ME_Posts entity)
        {
            try
            {
                PostsBL.Add(entity, context);
                StatusMessage.MessageType = StatusMessageType.Success;
                StatusMessage.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;

                SetParentPostsPopUp(entity.PostCategoryID, context);
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
                if (PostCategoriesSelectComponent1.ParentPostCategoryID != null)
                {
                    using (var context = new MediaEntities())
                    {
                        if (PostsBL.Exists(TitleTextBox.Text, (int)PostCategoriesSelectComponent1.ParentPostCategoryID, context))
                        {
                            StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                        }
                        else
                        {
                            var entity = new ME_Posts();
                            entity.Title = TitleTextBox.Text;
                            entity.SubTitle = SubTitleTextBox.Text;
                            entity.PostContent = HttpUtility.HtmlDecode(DescriptionEditor.Text);
                            entity.AddedOn = Utilities.DateTimeNow();
                            entity.Username = Membership.GetUser().UserName;
                            entity.PostCategoryID = (int)PostCategoriesSelectComponent1.ParentPostCategoryID;
                            entity.Tags = TagsTextBox.Text;
                            entity.Hide = HideCheckBox.Checked;
                            entity.ParentPostID = PostsListPopUpComponentSelectedIDHiddenField.Value.NullableIntParse();
                            entity.ImageID = ImageSelectorComponent1.ImageID;
                            entity.Locale = LocaleDropDown.Locale;
                            DescriptionEditor.Text = entity.PostContent;

                            Add(context, entity);
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
            TagsTextBox.Direction = direction;
            SubTitleTextBox.Direction = direction;
            ParentPostSelectedIDLabel.Direction = direction;
            DescriptionEditor.Direction = direction;
        }

        private async void LocaleDropDown_LanguageDirectionChanged(object sender, OWDARO.OEventArgs.LanguageDirectionChangedEventArgs e)
        {
            InitializeLocaleFields(e.Direction);

            PostCategoriesSelectComponent1.Locale = LocaleDropDown.Locale;

            using (var context = new MediaEntities())
            {
                await PostCategoriesSelectComponent1.InitializePostCategories(context);
            }
        }

        private void PostsListPopUpComponent1_IDSelected(object sender, OWDARO.OEventArgs.IDSelectedEventArgs e)
        {
            var postQuery = PostsBL.GetObjectByID(DataParser.IntParse(e.ID));

            if (postQuery != null)
            {
                PostsListPopUpComponentSelectedIDHiddenField.Value = e.ID;
                ParentPostSelectedIDLabel.Text = string.Format("{0}<br />{1}", postQuery.Title, postQuery.SubTitle);
            }
            else
            {
                PostsListPopUpComponentSelectedIDHiddenField.Value = string.Empty;
                ParentPostSelectedIDLabel.Text = "None";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var locale = AppConfig.DefaultLocale;
                LocaleDropDown.Locale = locale;
                InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                PostCategoriesSelectComponent1.Locale = locale;

                ImageSelectorComponent1.StoragePath = LocalStorages.Posts;
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            PostsListPopUpComponent1.IDSelected += PostsListPopUpComponent1_IDSelected;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
            PostCategoriesSelectComponent1.PostCategoryOpened += PostCategoriesSelectComponent1_PostCategoryOpened;
        }

        private void PostCategoriesSelectComponent1_PostCategoryOpened(object sender, OWDARO.OEventArgs.PostCategoryOpenedEventArgs e)
        {
            using (var context = new MediaEntities())
            {
                SetParentPostsPopUp(e.PostCategoryID, context);
            }
        }

        private void SetParentPostsPopUp(int categoryID, MediaEntities context)
        {
            var parentPostsQuery = (from set in context.ME_Posts
                                    where set.Hide == false && set.ParentPostID == null && set.PostCategoryID == categoryID && set.Locale == LocaleDropDown.Locale
                                    select set);

            if (parentPostsQuery.Any())
            {
                PostsListPopUpComponent1.DataTextField = "Title";
                PostsListPopUpComponent1.DataValueField = "PostID";
                PostsListPopUpComponent1.DataSource = parentPostsQuery.ToList();

                PostsListPopUpComponent1.Enabled = true;
            }
            else
            {
                PostsListPopUpComponent1.Enabled = false;
                PostsListPopUpComponent1.DataSource = null;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}