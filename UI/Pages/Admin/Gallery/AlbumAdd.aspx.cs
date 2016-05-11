using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.OEventArgs;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Linq;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class AlbumAdd : System.Web.UI.Page
    {
        private void Add(GalleryEntities context, GY_Albums entity)
        {
            try
            {
                AlbumsBL.Add(entity, context);
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
                    if (AlbumsBL.Exists(TitleTextBox.Text, context))
                    {
                        StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                    }
                    else
                    {
                        var entity = new GY_Albums();
                        entity.EventID = EventsDropDownList.GetNullableSelectedValue();
                        entity.Title = TitleTextBox.Text;
                        entity.Hide = false;
                        entity.Description = DescriptionEditor.Text;
                        entity.TakenOn = DataParser.NullableDateTimeParse(TakenOnTextBox.Text);
                        entity.AddedOn = Utilities.DateTimeNow();
                        entity.Location = LocationTextBox.Text;
                        entity.Tags = TagsTextBox.Text;
                        entity.ImageID = ImageSelectorComponent1.ImageID;
                        entity.Locale = LocaleDropDown.Locale;

                        Add(context, entity);
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

        private void LocaleDropDown_LanguageDirectionChanged(object sender, LanguageDirectionChangedEventArgs e)
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
            var eventsQuery = (from events in context.GY_Events
                               where events.Hide == false && events.EndsOn < now && events.Locale == LocaleDropDown.Locale
                               select events);
            EventsDropDownList.DataTextField = "Title";
            EventsDropDownList.DataValueField = "EventID";
            EventsDropDownList.DataSource = eventsQuery.ToList();
            EventsDropDownList.AddSelect();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var locale = AppConfig.DefaultLocale;
                LocaleDropDown.Locale = locale;
                InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                PopulateEvents();

                ImageSelectorComponent1.StoragePath = LocalStorages.Albums;

                TakenOnTextBox.DateFormat = Validator.DateParseExpression;
                TakenOnTextBox.Format = Validator.DateParseExpression;
                TakenOnTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                TakenOnTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                TakenOnTextBox.ValidationExpression = Validator.CalendarValidationExpression;
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}