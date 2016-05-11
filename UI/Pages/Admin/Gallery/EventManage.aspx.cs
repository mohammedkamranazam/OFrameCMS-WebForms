using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Web;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class EventManage : System.Web.UI.Page
    {
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
            Response.Redirect("~/UI/Pages/Admin/Gallery/EventList.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            using (var context = new GalleryEntities())
            {
                var eventID = DataParser.IntParse(Request.QueryString["EventID"]);

                if (EventsBL.RelatedRecordsExists(eventID, context))
                {
                    StatusMessage.MessageType = StatusMessageType.Warning;
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                }
                else
                {
                    try
                    {
                        var entity = EventsBL.GetObjectByID(eventID, context);

                        EventsBL.Delete(entity, context);

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
                    var eventID = DataParser.IntParse(Request.QueryString["EventID"]);

                    var entity = EventsBL.GetObjectByID(eventID, context);

                    if (entity != null)
                    {
                        if (entity.Title != TitleTextBox.Text)
                        {
                            if (EventsBL.Exists(TitleTextBox.Text, context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                        }

                        entity.Title = TitleTextBox.Text;
                        entity.SubTitle = SubTitleTextBox.Text;
                        entity.SubDescription = SubDescriptionTextBox.Text;
                        entity.Description = HttpUtility.HtmlDecode(DescriptionEditor.Text);
                        entity.Location = LocationTextBox.Text;
                        entity.StartsOn = DataParser.DateTimeParse(string.Concat(StartsOnDateTextBox.Text, " ", StartsOnTimeTextBox.Text));
                        entity.EndsOn = DataParser.DateTimeParse(string.Concat(EndsOnDateTextBox.Text, " ", EndsOnTimeTextBox.Text));
                        entity.EventTypeID = EventTypeDropDownList.GetSelectedValue();
                        entity.Tags = TagsTextBox.Text;
                        entity.Hide = HideCheckBox.Checked;
                        entity.UseExternalForm = UseExternalFormCheckBox.Checked;
                        DescriptionEditor.Text = entity.Description;
                        entity.ImageID = ImageSelectorComponent1.ImageID;
                        entity.Locale = LocaleDropDown.Locale;

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

                        Save(entity, context);
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

        private bool Save(GY_Events eventEntity, GalleryEntities context)
        {
            try
            {
                EventsBL.Save(context);

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

        private void UseExternalFormCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InitializeRegisterableFormIfUsingExternalForm(UseExternalFormCheckBox.Checked);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["EventId"]))
                {
                    var eventId = DataParser.IntParse(Request.QueryString["EventId"]);

                    var eventEntity = EventsBL.GetObjectByID(eventId);

                    if (eventEntity != null)
                    {
                        var locale = eventEntity.Locale;
                        LocaleDropDown.Locale = locale;
                        InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                        EventTypeDropDownList.DataSource = EventTypesHelper.GetEvents();
                        EventTypeDropDownList.DataTextField = "Title";
                        EventTypeDropDownList.DataValueField = "EventTypeID";
                        EventTypeDropDownList.DataBind();
                        EventTypeDropDownList.AddSelect();

                        Utilities.PopulateExternalFormTypes(ExternalFormTypesDropDownList);
                        ExternalFormIDDropDownList.AddSelect();

                        TitleTextBox.Text = eventEntity.Title;
                        SubTitleTextBox.Text = eventEntity.SubTitle;
                        SubDescriptionTextBox.Text = eventEntity.SubDescription;
                        DescriptionEditor.Text = eventEntity.Description;
                        HideCheckBox.Checked = eventEntity.Hide;
                        LocationTextBox.Text = eventEntity.Location;
                        StartsOnDateTextBox.Text = DataParser.GetDateFormattedString(eventEntity.StartsOn);
                        StartsOnTimeTextBox.Text = DataParser.GetTimeFormattedString(eventEntity.StartsOn);
                        EndsOnDateTextBox.Text = DataParser.GetDateFormattedString(eventEntity.EndsOn);
                        EndsOnTimeTextBox.Text = DataParser.GetTimeFormattedString(eventEntity.EndsOn);
                        TagsTextBox.Text = eventEntity.Tags;
                        EventTypeDropDownList.SelectedValue = eventEntity.EventTypeID.ToString();
                        RegistrationEndDateTextBox.Text = DataParser.GetDateFormattedString(eventEntity.RegistrationEndDateTime);
                        RegistrationStartDateTextBox.Text = DataParser.GetDateFormattedString(eventEntity.RegistrationStartDateTime);
                        ImageSelectorComponent1.ImageID = eventEntity.ImageID;

                        var eventType = EventTypesHelper.Get(eventEntity.EventTypeID);

                        if (eventType.IsRegisterable)
                        {
                            UseExternalFormCheckBox.Visible = true;
                            UseExternalFormCheckBox.Checked = false;

                            RegistrationEndDateTextBox.Visible = true;
                            RegistrationStartDateTextBox.Visible = true;

                            if (eventEntity.UseExternalForm)
                            {
                                UseExternalFormCheckBox.Checked = eventEntity.UseExternalForm;
                                ExternalFormTypesDropDownList.Visible = true;

                                if (!string.IsNullOrWhiteSpace(eventEntity.ExternalFormEmbedCode))
                                {
                                    ExternalFormEmbedCodeTextBox.Text = eventEntity.ExternalFormEmbedCode;
                                    ExternalFormTypesDropDownList.SelectedValue = "1";
                                    InitializeRegisterableFormByExternalFormType("1");
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(eventEntity.ExternalFormURL))
                                    {
                                        ExternalFormURLTextBox.Text = eventEntity.ExternalFormURL;
                                        OpenExternalFormInNewTabCheckBox.Checked = eventEntity.PopUpExternalForm;
                                        ExternalFormTypesDropDownList.SelectedValue = "2";
                                        InitializeRegisterableFormByExternalFormType("2");
                                    }
                                    else
                                    {
                                        if (eventEntity.ExternalFormID != null)
                                        {
                                            ExternalFormIDDropDownList.SelectedValue = eventEntity.ExternalFormID.ToString();
                                            ExternalFormTypesDropDownList.SelectedValue = "3";
                                            InitializeRegisterableFormByExternalFormType("3");
                                        }
                                        else
                                        {
                                            InitializeRegisterableFormByExternalFormType("-1");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("~/UI/Pages/Admin/Gallery/EventList.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/EventList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

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
            }

            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += new EventHandler(FormToolbar1_Cancel);
            FormToolbar1.Delete += new EventHandler(FormToolbar1_Delete);
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