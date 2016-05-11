using OWDARO;
using OWDARO.BLL.MediaBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Linq;
using System.Web;

namespace ProjectJKL.UI.Pages.Admin.Media
{
    public partial class PostsManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Admin/Media/PostsList.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            using (var context = new MediaEntities())
            {
                var postID = DataParser.IntParse(Request.QueryString["PostID"]);

                if (PostsBL.RelatedRecordsExists(postID, context))
                {
                    StatusMessage.MessageType = StatusMessageType.Warning;
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                }
                else
                {
                    try
                    {
                        PostsBL.Delete(postID, context);

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
                if (PostCategoriesSelectComponent1.ParentPostCategoryID != null)
                {
                    using (var context = new MediaEntities())
                    {
                        var postID = DataParser.IntParse(Request.QueryString["PostID"]);

                        var entity = PostsBL.GetObjectByID(postID, context);

                        bool postExists = PostsBL.Exists(TitleTextBox.Text, (int)PostCategoriesSelectComponent1.ParentPostCategoryID, context);

                        if (entity.PostCategoryID != PostCategoriesSelectComponent1.ParentPostCategoryID)
                        {
                            if (postExists)
                            {
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                        }

                        if (entity.Title != TitleTextBox.Text)
                        {
                            if (postExists)
                            {
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                        }

                        entity.Title = TitleTextBox.Text;
                        entity.SubTitle = SubTitleTextBox.Text;
                        entity.PostContent = HttpUtility.HtmlDecode(DescriptionEditor.Text);
                        entity.PostCategoryID = (int)PostCategoriesSelectComponent1.ParentPostCategoryID;
                        entity.Tags = TagsTextBox.Text;
                        entity.Hide = HideCheckBox.Checked;
                        entity.ParentPostID = PostsListPopUpComponentSelectedIDHiddenField.Value.NullableIntParse();
                        entity.ImageID = ImageSelectorComponent1.ImageID;
                        entity.Locale = LocaleDropDown.Locale;

                        DescriptionEditor.Text = entity.PostContent;

                        Save(entity, context);
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

        private bool Save(ME_Posts entity, MediaEntities context)
        {
            try
            {
                PostsBL.Save(context);

                StatusMessage.MessageType = StatusMessageType.Success;
                StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;

                SetParentPostsPopUp(entity.PostCategoryID, context);

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["PostID"]))
                {
                    using (var context = new MediaEntities())
                    {
                        var postID = DataParser.IntParse(Request.QueryString["PostID"]);

                        var postQuery = PostsBL.GetObjectByID(postID, context);

                        if (postQuery != null)
                        {
                            var locale = postQuery.Locale;
                            LocaleDropDown.Locale = locale;
                            InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                            PostCategoriesSelectComponent1.Locale = locale;

                            PostCategoriesSelectComponent1.ParentPostCategoryID = postQuery.PostCategoryID;
                            PostCategoriesSelectComponent1.InitialValue = postQuery.PostCategoryID;

                            TitleTextBox.Text = postQuery.Title;
                            SubTitleTextBox.Text = postQuery.SubTitle;
                            DescriptionEditor.Text = postQuery.PostContent;
                            HideCheckBox.Checked = postQuery.Hide;
                            TagsTextBox.Text = postQuery.Tags;
                            ImageSelectorComponent1.ImageID = postQuery.ImageID;

                            if (postQuery.ParentPostID == null)
                            {
                                PostsListPopUpComponentSelectedIDHiddenField.Value = string.Empty;
                                ParentPostSelectedIDLabel.Text = "None";
                            }
                            else
                            {
                                var parentPostQuery = PostsBL.GetObjectByID((int)postQuery.ParentPostID, context);

                                if (parentPostQuery != null)
                                {
                                    PostsListPopUpComponentSelectedIDHiddenField.Value = parentPostQuery.PostID.ToString();
                                    ParentPostSelectedIDLabel.Text = string.Format("{0}<br />{1}", parentPostQuery.Title, parentPostQuery.SubTitle);
                                }
                                else
                                {
                                    PostsListPopUpComponentSelectedIDHiddenField.Value = string.Empty;
                                    ParentPostSelectedIDLabel.Text = "None";
                                }
                            }

                            SetParentPostsPopUp(postQuery.PostCategoryID, context);
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/Media/PostsList.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Media/PostsList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                ImageSelectorComponent1.StoragePath = LocalStorages.Posts;
            }

            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += new EventHandler(FormToolbar1_Cancel);
            FormToolbar1.Delete += new EventHandler(FormToolbar1_Delete);
            PostCategoriesSelectComponent1.PostCategoryOpened += PostCategoriesSelectComponent1_PostCategoryOpened;
            PostsListPopUpComponent1.IDSelected += PostsListPopUpComponent1_IDSelected;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        private void PostCategoriesSelectComponent1_PostCategoryOpened(object sender, OWDARO.OEventArgs.PostCategoryOpenedEventArgs e)
        {
            using (var context = new MediaEntities())
            {
                SetParentPostsPopUp(e.PostCategoryID, context);
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}