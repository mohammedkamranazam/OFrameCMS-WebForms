using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class PhotoManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            var albumID = DataParser.IntParse(AlbumIDHiddenField.Value);

            Response.Redirect(string.Format("~/UI/Pages/Admin/Gallery/PhotoList.aspx?AlbumID={0}", albumID));
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var photoID = DataParser.IntParse(Request.QueryString["PhotoID"]);

            using (var context = new GalleryEntities())
            {
                try
                {
                    var photoQuery = PhotosBL.GetObjectByID(photoID, context);
                    PhotosBL.Delete(photoQuery, context);

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

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    var photoId = DataParser.IntParse(Request.QueryString["PhotoID"]);

                    var photoEntity = PhotosBL.GetObjectByID(photoId, context);

                    if (photoEntity != null)
                    {
                        photoEntity.Title = TitleTextBox.Text;
                        photoEntity.Description = DescriptionEditor.Text;
                        photoEntity.Hide = HideCheckBox.Checked;
                        photoEntity.TakenOn = DataParser.NullableDateTimeParse(TakenOnTextBox.Text);
                        photoEntity.Location = LocationTextBox.Text;
                        photoEntity.Tags = TagsTextBox.Text;
                        photoEntity.ImageID = ImageSelectorComponent1.ImageID;

                        Save(photoEntity, context);
                    }
                }
            }
        }

        private void InitializeLocaleFields(string direction)
        {
            TitleTextBox.Direction = direction;
            DescriptionEditor.Direction = direction;
            LocationTextBox.Direction = direction;
            TagsTextBox.Direction = direction;
        }

        private bool Save(GY_Photos photoEntity, GalleryEntities context)
        {
            try
            {
                PhotosBL.Save(context);

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
                if (string.IsNullOrWhiteSpace(Request.QueryString["PhotoId"]))
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/AlbumList.aspx");
                }

                var photoId = DataParser.IntParse(Request.QueryString["PhotoId"]);

                using (var context = new GalleryEntities())
                {
                    var photoQuery = PhotosBL.GetObjectByID(photoId, context);

                    if (photoQuery != null)
                    {
                        TitleTextBox.Text = photoQuery.Title;
                        DescriptionEditor.Text = photoQuery.Description;
                        HideCheckBox.Checked = photoQuery.Hide;
                        AlbumIDHiddenField.Value = photoQuery.AlbumID.ToString();
                        TakenOnTextBox.Text = DataParser.GetDateFormattedString(photoQuery.TakenOn);
                        TagsTextBox.Text = photoQuery.Tags;
                        LocationTextBox.Text = photoQuery.Location;
                        ImageSelectorComponent1.ImageID = photoQuery.ImageID;

                        var locale = photoQuery.GY_Albums.Locale;
                        var direction = LanguageHelper.GetLocaleDirection(locale);

                        InitializeLocaleFields(direction);

                        PhotoListHyperLink.NavigateUrl = string.Format("~/UI/Pages/Admin/Gallery/PhotoList.aspx?AlbumID={0}", photoQuery.AlbumID);
                        AlbumManageHyperLink.NavigateUrl = string.Format("~/UI/Pages/Admin/Gallery/AlbumManage.aspx?AlbumID={0}", photoQuery.AlbumID);
                    }
                    else
                    {
                        StatusMessage.MessageType = StatusMessageType.Info;
                        StatusMessage.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                    }
                }

                ImageSelectorComponent1.StoragePath = LocalStorages.Photos;

                TakenOnTextBox.DateFormat = Validator.DateParseExpression;
                TakenOnTextBox.Format = Validator.DateParseExpression;
                TakenOnTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                TakenOnTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                TakenOnTextBox.ValidationExpression = Validator.CalendarValidationExpression;
            }

            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += new EventHandler(FormToolbar1_Cancel);
            FormToolbar1.Delete += new EventHandler(FormToolbar1_Delete);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}