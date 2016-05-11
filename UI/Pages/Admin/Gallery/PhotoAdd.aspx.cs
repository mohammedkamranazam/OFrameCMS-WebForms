using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class PhotoAdd : System.Web.UI.Page
    {
        private void Add(GalleryEntities context, GY_Photos photo)
        {
            try
            {
                PhotosBL.Add(photo);

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
            var albumID = DataParser.IntParse(Request.QueryString["AlbumId"]);
            Response.Redirect(string.Format("~/UI/Pages/Admin/Gallery/PhotoList.aspx?AlbumID={0}", albumID));
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    var albumId = DataParser.IntParse(Request.QueryString["AlbumId"]);

                    var entity = new GY_Photos();
                    entity.AlbumID = albumId;
                    entity.Title = TitleTextBox.Text;
                    entity.Description = DescriptionEditor.Text;
                    entity.AddedOn = Utilities.DateTimeNow();
                    entity.TakenOn = DataParser.NullableDateTimeParse(TakenOnTextBox.Text);
                    entity.Location = LocationTextBox.Text;
                    entity.Tags = TagsTextBox.Text;
                    entity.Hide = false;
                    entity.ImageID = ImageSelectorComponent1.ImageID;
                    entity.Locale = string.Empty;

                    Add(context, entity);
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["AlbumId"]))
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/AlbumList.aspx");
                }

                var albumID = DataParser.IntParse(Request.QueryString["AlbumId"]);

                var albumEntity = AlbumsBL.GetObjectByID(albumID);

                var locale = albumEntity.Locale;
                var direction = LanguageHelper.GetLocaleDirection(locale);

                InitializeLocaleFields(direction);

                PhotoListHyperLink.NavigateUrl = string.Format("~/UI/Pages/Admin/Gallery/PhotoList.aspx?AlbumID={0}", albumID);
                AlbumManageHyperLink.NavigateUrl = string.Format("~/UI/Pages/Admin/Gallery/AlbumManage.aspx?AlbumID={0}", albumID);

                ImageSelectorComponent1.StoragePath = LocalStorages.Photos;

                TakenOnTextBox.DateFormat = Validator.DateParseExpression;
                TakenOnTextBox.Format = Validator.DateParseExpression;
                TakenOnTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                TakenOnTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                TakenOnTextBox.ValidationExpression = Validator.CalendarValidationExpression;
            }

            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += FormToolbar1_Cancel;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}