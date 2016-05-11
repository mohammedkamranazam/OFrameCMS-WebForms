using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Web;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class EventAdd : System.Web.UI.Page
    {
        private void Add(GalleryEntities context, GY_Events entity)
        {
            try
            {
                EventsBL.Add(entity, context);
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

        private void EventTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var eventTypeID = DataParser.IntParse(EventTypeDropDownList.SelectedValue);

            InitializeRegisterableFormByEventType(eventTypeID);
        }

        private void ExternalFormTypesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeRegisterableFormByExternalFormType(ExternalFormTypesDropDownList.SelectedValue);
        }

        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder());
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    if (EventsBL.Exists(TitleTextBox.Text, context))
                    {
                        StatusMessage.MessageType = StatusMessageType.Info;
                        StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                    }
                    else
                    {
                        var entity = new GY_Events();
                        entity.Title = TitleTextBox.Text;
                        entity.SubTitle = SubTitleTextBox.Text;
                        entity.SubDescription = SubDescriptionTextBox.Text;
                        entity.Description = HttpUtility.HtmlDecode(DescriptionEditor.Text);
                        entity.Location = LocationTextBox.Text;
                        entity.StartsOn = DataParser.DateTimeParse(string.Concat(StartsOnDateTextBox.Text, " ", StartsOnTimeTextBox.Text));
                        entity.EndsOn = DataParser.DateTimeParse(string.Concat(EndsOnDateTextBox.Text, " ", EndsOnTimeTextBox.Text));
                        entity.EventTypeID = EventTypeDropDownList.GetSelectedValue();
                        entity.Tags = TagsTextBox.Text;
                        entity.UseExternalForm = UseExternalFormCheckBox.Checked;
                        entity.ImageID = ImageSelectorComponent1.ImageID;
                        entity.Locale = LocaleDropDown.Locale;

                        DescriptionEditor.Text = entity.Description;

                        var eventType = EventTypesHelper.Get(entity.EventTypeID);

                        if (eventType.IsRegisterable)
                        {
                            entity.RegistrationStartDateTime = DataParser.NullableDateTimeParse(RegistrationStartDateTextBox.Text);
                            entity.RegistrationEndDateTime = DataParser.NullableDateTimeParse(RegistrationEndDateTextBox.Text);
                        }

                        if (UseExternalFormCheckBox.Checked)
                        {
                            switch (ExternalFormTypesDropDownList.SelectedValue)
                            {
                                case "-1":
                                    entity.ExternalFormEmbedCode = null;
                                    entity.ExternalFormURL = null;
                                    entity.PopUpExternalForm = false;
                                    entity.ExternalFormID = null;
                                    break;

                                case "1":
                                    entity.ExternalFormEmbedCode = ExternalFormEmbedCodeTextBox.Text;
                                    entity.ExternalFormURL = null;
                                    entity.PopUpExternalForm = false;
                                    entity.ExternalFormID = null;
                                    break;

                                case "2":
                                    entity.ExternalFormEmbedCode = null;
                                    entity.ExternalFormURL = ExternalFormURLTextBox.Text;
                                    entity.PopUpExternalForm = false;
                                    entity.ExternalFormID = null;
                                    break;

                                case "3":
                                    entity.ExternalFormEmbedCode = null;
                                    entity.ExternalFormURL = null;
                                    entity.PopUpExternalForm = false;
                                    entity.ExternalFormID = ExternalFormIDDropDownList.GetNullableSelectedValue();
                                    break;

                                default:
                                    entity.ExternalFormEmbedCode = null;
                                    entity.ExternalFormURL = null;
                                    entity.PopUpExternalForm = false;
                                    entity.ExternalFormID = null;
                                    break;
                            }
                        }

                        Add(context, entity);
                    }
                }
            }
        }

        private void InitializeLocaleFields(string direction)
        {
            TitleTextBox.Direction = direction;
            SubTitleTextBox.Direction = direction;
            SubDescriptionTextBox.Direction = direction;
            LocationTextBox.Direction = direction;
            TagsTextBox.Direction = direction;
            DescriptionEditor.Direction = direction;
        }

        private void InitializeRegisterableFormByEventType(int eventTypeID)
        {
            var eventType = EventTypesHelper.Get(eventTypeID);

            InitializeRegisterableFormIfRegisterable(eventType.IsRegisterable);
        }

        private void InitializeRegisterableFormByExternalFormType(string formTypeID)
        {
            switch (formTypeID)
            {
                case "-1":
                    ExternalFormEmbedCodeTextBox.Visible = false;
                    ExternalFormURLTextBox.Visible = false;
                    OpenExternalFormInNewTabCheckBox.Visible = false;
                    ExternalFormIDDropDownList.Visible = false;
                    break;

                case "1":
                    ExternalFormEmbedCodeTextBox.Visible = true;
                    ExternalFormURLTextBox.Visible = false;
                    OpenExternalFormInNewTabCheckBox.Visible = false;
                    ExternalFormIDDropDownList.Visible = false;
                    break;

                case "2":
                    ExternalFormEmbedCodeTextBox.Visible = false;
                    ExternalFormURLTextBox.Visible = true;
                    OpenExternalFormInNewTabCheckBox.Visible = true;
                    ExternalFormIDDropDownList.Visible = false;
                    break;

                case "3":
                    ExternalFormEmbedCodeTextBox.Visible = false;
                    ExternalFormURLTextBox.Visible = false;
                    OpenExternalFormInNewTabCheckBox.Visible = false;
                    ExternalFormIDDropDownList.Visible = true;
                    break;
            }
        }

        private void InitializeRegisterableFormIfRegisterable(bool isRegisterable)
        {
            UseExternalFormCheckBox.Visible = isRegisterable;
            UseExternalFormCheckBox.Checked = false;

            RegistrationEndDateTextBox.Visible = isRegisterable;
            RegistrationStartDateTextBox.Visible = isRegisterable;

            InitializeRegisterableFormIfUsingExternalForm(UseExternalFormCheckBox.Checked);
        }

        private void InitializeRegisterableFormIfUsingExternalForm(bool useExternalForm)
        {
            ExternalFormTypesDropDownList.Visible = useExternalForm;
            ExternalFormTypesDropDownList.SelectedValue = "-1";

            InitializeRegisterableFormByExternalFormType(ExternalFormTypesDropDownList.SelectedValue);
        }

        private void LocaleDropDown_LanguageDirectionChanged(object sender, OWDARO.OEventArgs.LanguageDirectionChangedEventArgs e)
        {
            InitializeLocaleFields(e.Direction);
        }

        private void UseExternalFormCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InitializeRegisterableFormIfUsingExternalForm(UseExternalFormCheckBox.Checked);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ImageSelectorComponent1.StoragePath = LocalStorages.Events;

                StartsOnDateTextBox.DateFormat = Validator.DateParseExpression;
                StartsOnDateTextBox.Format = Validator.DateParseExpression;
                StartsOnDateTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                StartsOnDateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                StartsOnDateTextBox.ValidationExpression = Validator.CalendarValidationExpression;

                EndsOnDateTextBox.DateFormat = Validator.DateParseExpression;
                EndsOnDateTextBox.Format = Validator.DateParseExpression;
                EndsOnDateTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                EndsOnDateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                EndsOnDateTextBox.ValidationExpression = Validator.CalendarValidationExpression;

                RegistrationStartDateTextBox.DateFormat = Validator.DateParseExpression;
                RegistrationStartDateTextBox.Format = Validator.DateParseExpression;
                RegistrationStartDateTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                RegistrationStartDateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                RegistrationStartDateTextBox.ValidationExpression = Validator.CalendarValidationExpression;

                RegistrationEndDateTextBox.DateFormat = Validator.DateParseExpression;
                RegistrationEndDateTextBox.Format = Validator.DateParseExpression;
                RegistrationEndDateTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
                RegistrationEndDateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
                RegistrationEndDateTextBox.ValidationExpression = Validator.CalendarValidationExpression;

                StartsOnTimeTextBox.SmallLabelText = "valid time format is: " + Validator.TimeParseExpression;
                EndsOnTimeTextBox.SmallLabelText = "valid time format is: " + Validator.TimeParseExpression;

                StartsOnTimeTextBox.ValidationExpression = Validator.TimeValidationExpression;
                StartsOnTimeTextBox.ValidationErrorMessage = Validator.TimeValidationErrorMessage;

                EndsOnTimeTextBox.ValidationExpression = Validator.TimeValidationExpression;
                EndsOnTimeTextBox.ValidationErrorMessage = Validator.TimeValidationErrorMessage;

                ExternalFormURLTextBox.ValidationExpression = Validator.UrlValidationExpression;
                ExternalFormURLTextBox.ValidationErrorMessage = Validator.UrlValidationErrorMessage;

                EventTypeDropDownList.DataSource = EventTypesHelper.GetEvents();
                EventTypeDropDownList.DataTextField = "Title";
                EventTypeDropDownList.DataValueField = "EventTypeID";
                EventTypeDropDownList.DataBind();
                EventTypeDropDownList.AddSelect();

                Utilities.PopulateExternalFormTypes(ExternalFormTypesDropDownList);
                ExternalFormIDDropDownList.AddSelect();

                var locale = AppConfig.DefaultLocale;
                LocaleDropDown.Locale = locale;
                InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));
            }

            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            UseExternalFormCheckBox.CheckedChanged += UseExternalFormCheckBox_CheckedChanged;
            ExternalFormTypesDropDownList.SelectedIndexChanged += ExternalFormTypesDropDownList_SelectedIndexChanged;
            EventTypeDropDownList.SelectedIndexChanged += EventTypeDropDownList_SelectedIndexChanged;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}