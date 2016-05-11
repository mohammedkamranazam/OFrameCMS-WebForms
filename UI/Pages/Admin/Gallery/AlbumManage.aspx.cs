using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Linq;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class AlbumManage : System.Web.UI.Page
    {
        private void DeleteAlbum(int albumID, GalleryEntities context)
        {
            var albumQuery = AlbumsBL.GetObjectByID(albumID, context);

            if (albumQuery != null)
            {
                try
                {
                    AlbumsBL.Delete(albumQuery, context);

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

        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Admin/Gallery/AlbumList.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var albumID = DataParser.IntParse(Request.QueryString["AlbumID"]);

            using (var context = new GalleryEntities())
            {
                if (AlbumsBL.RelatedRecordsExists(albumID, context))
                {
                    if (PhotosBL.DeletePhotos(albumID, context))
                    {
                        DeleteAlbum(albumID, context);
                    }
                }
                else
                {
                    DeleteAlbum(albumID, context);
                }
            }
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    var albumId = DataParser.IntParse(Request.QueryString["AlbumID"]);

                    var albumEntity = AlbumsBL.GetObjectByID(albumId, context);

                    if (albumEntity != null)
                    {
                        if (albumEntity.Title != TitleTextBox.Text)
                        {
                            if (AlbumsBL.Exists(TitleTextBox.Text, context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                        }

                        albumEntity.Title = TitleTextBox.Text;
                        albumEntity.Description = DescriptionEditor.Text;
                        albumEntity.TakenOn = DataParser.NullableDateTimeParse(TakenOnTextBox.Text);
                        albumEntity.Hide = HideCheckBox.Checked;
                        albumEntity.Tags = TagsTextBox.Text;
                        albumEntity.Location = LocationTextBox.Text;
                        albumEntity.EventID = EventsDropDownList.GetNullableSelectedValue();
                        albumEntity.ImageID = ImageSelectorComponent1.ImageID;
                        albumEntity.Locale = LocaleDropDown.Locale;

                        Save(albumEntity, context);
                    }
                }
            }
        }

        private void InitializeLocaleFields(string direction)
        {
            TitleTextBox.Direction = direction;
            LocationTextBox.Direction = direction;
            EventsDropDownList.Direction = direction;
            TagsTextBox.Direction = direction;
            DescriptionEditor.Direction = direction;
        }

        private void LocaleDropDown_LanguageDirectionChanged(object sender, OWDARO.OEventArgs.LanguageDirectionChangedEventArgs e)
        {
            InitializeLocaleFields(e.Direction);

            PopulateEvents();
        }

        private void PopulateEvents()
        {
            using (var context = new GalleryEntities())
            {
                PopulateEvents(context);
            }
        }

        private void PopulateEvents(GalleryEntities context)
        {
            var now = Utilities.DateTimeNow();
            var eventsQuery = (from set in context.GY_Events
                               where set.Hide == false && set.EndsOn < now && set.Locale == LocaleDropDown.Locale
                               select set);
            EventsDropDownList.DataTextField = "Title";
            EventsDropDownList.DataValueField = "EventID";
            EventsDropDownList.DataSource = eventsQuery.ToList();
            EventsDropDownList.AddSelect();
        }

        private bool Save(GY_Albums albumEntity, GalleryEntities context)
        {
            try
            {
                AlbumsBL.Save(context);

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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["AlbumId"]))
                {
                    var albumID = DataParser.IntParse(Request.QueryString["AlbumId"]);

                    PhotoListHyperlink.NavigateUrl = string.Format("~/UI/Pages/Admin/Gallery/PhotoList.aspx?AlbumId={0}", albumID);
                    PhotoAddHyperLink.NavigateUrl = String.Format("~/UI/Pages/Admin/Gallery/PhotoAdd.aspx?AlbumId={0}", albumID);

                    using (var context = new GalleryEntities())
                    {
                        var albumQuery = AlbumsBL.GetObjectByID(albumID, context);

                        if (albumQuery != null)
                        {
                            var locale = albumQuery.Locale;
                            LocaleDropDown.Locale = locale;
                            InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                            PopulateEvents(context);

                            TitleTextBox.Text = albumQuery.Title;
                            DescriptionEditor.Text = albumQuery.Description;
                            TakenOnTextBox.Text = DataParser.GetDateFormattedString(albumQuery.TakenOn);
                            EventsDropDownList.SelectedValue = albumQuery.EventID.ToString();
                            HideCheckBox.Checked = albumQuery.Hide;
                            TagsTextBox.Text = albumQuery.Tags;
                            LocationTextBox.Text = albumQuery.Location;
                            ImageSelectorComponent1.ImageID = albumQuery.ImageID;
                        }
                        else
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                        }
                    }

                    ImageSelectorComponent1.StoragePath = LocalStorages.Albums;
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/AlbumList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                TakenOnTextBox.DateFormat = Validator.DateParseExpression;
                TakenOnTextBox.Format = Validator.DateParseExpression;
                TakenOnTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                TakenOnTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                TakenOnTextBox.ValidationExpression = Validator.CalendarValidationExpression;
            }

            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += new EventHandler(FormToolbar1_Cancel);
            FormToolbar1.Delete += new EventHandler(FormToolbar1_Delete);
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}