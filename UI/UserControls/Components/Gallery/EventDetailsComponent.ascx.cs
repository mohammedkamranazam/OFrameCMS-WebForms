using OWDARO.BLL.GalleryBLL;
using OWDARO.Models;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class EventDetailsComponent : System.Web.UI.UserControl
    {
        private void CheckAndRedirect()
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["EventSchedule"]))
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["EventID"]))
                {
                    Response.Redirect(string.Format("~/Events.aspx?EventSchedule={0}", Request.QueryString["EventSchedule"]), false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["EventID"]))
                {
                    Response.Redirect(string.Format("~/Events.aspx?EventSchedule={0}", EventSchedule.All.ToString()), false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        private void GenerateRegistrationURL(GY_Events eventQuery, Models.EventTypes eventType)
        {
            if (!eventType.IsRegisterable)
            {
                RegistrationHyperlink.Visible = false;
            }
            else
            {
                if (eventQuery.UseExternalForm)
                {
                    if (!string.IsNullOrWhiteSpace(eventQuery.ExternalFormEmbedCode))
                    {
                        RegistrationHyperlink.NavigateUrl = string.Format("~/ExternalFormEmbedCode.aspx?EventID={0}", eventQuery.EventID);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(eventQuery.ExternalFormURL))
                        {
                            RegistrationHyperlink.NavigateUrl = eventQuery.ExternalFormURL;

                            if (eventQuery.PopUpExternalForm)
                            {
                                RegistrationHyperlink.Target = "_blank";
                            }
                        }
                        else
                        {
                            if (eventQuery.ExternalFormID != null)
                            {
                                RegistrationHyperlink.NavigateUrl = string.Format("~/ExternalFormID.aspx?EventID={0}", eventQuery.EventID);
                            }
                        }
                    }
                }
                else
                {
                    if (eventType.UseExternalForm)
                    {
                        if (!string.IsNullOrWhiteSpace(eventType.ExternalFormEmbedCode))
                        {
                            RegistrationHyperlink.NavigateUrl = string.Format("~/ExternalFormEmbedCode.aspx?EventTypeID={0}", eventType.EventTypeID);
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(eventType.ExternalFormURL))
                            {
                                RegistrationHyperlink.NavigateUrl = eventType.ExternalFormURL;

                                if (eventType.PopUpExternalForm)
                                {
                                    RegistrationHyperlink.Target = "_blank";
                                }
                            }
                            else
                            {
                                if (eventType.ExternalFormID != null)
                                {
                                    RegistrationHyperlink.NavigateUrl = string.Format("~/ExternalFormID.aspx?EventTypeID={0}", eventType.EventTypeID);
                                }
                            }
                        }
                    }
                    else
                    {
                        RegistrationHyperlink.NavigateUrl = eventType.RegistrationPageURL;
                    }
                }
            }

            SetRegistrationButton(eventQuery);
        }

        private async Task LoadData()
        {
            var eventID = DataParser.IntParse(Request.QueryString["EventID"]);

            var eventQuery = await EventsBL.GetObjectByIDAsync(eventID);

            if (eventQuery != null)
            {
                var seoEntity = new SEO();
                seoEntity.Title = eventQuery.Title;
                seoEntity.Description = eventQuery.SubDescription;
                seoEntity.Keywords = eventQuery.Tags;

                Utilities.SetPageSEO(Page, seoEntity);

                EventDetailsDIV.Style.Add("direction", LanguageHelper.GetLocaleDirection(eventQuery.Locale));

                TitleLiteral.Text = eventQuery.Title;
                SubTitleLiteral.Text = eventQuery.SubTitle;
                SubDescriptionLiteral.Text = eventQuery.SubDescription;
                FancyBoxLiteral.Text = Utilities.GetFancyBoxHTML(eventQuery.ImageID, string.Empty, false, "style='width:100%; height:100%;'");
                StartsOnDateLiteral.Text = string.Format("{0}{1}", LanguageHelper.GetKey("StartDate", eventQuery.Locale), DataParser.GetDateFormattedString(eventQuery.StartsOn));
                StartsOnTimeLiteral.Text = string.Format("{0}{1}", LanguageHelper.GetKey("StartTime", eventQuery.Locale), DataParser.GetDateFormattedString(eventQuery.StartsOn));
                EndsOnDateLiteral.Text = string.Format("{0}{1}", LanguageHelper.GetKey("EndDate", eventQuery.Locale), DataParser.GetDateFormattedString(eventQuery.EndsOn));
                EndsOnTimeLiteral.Text = string.Format("{0}{1}", LanguageHelper.GetKey("EndTime", eventQuery.Locale), DataParser.GetDateFormattedString(eventQuery.EndsOn));
                EventScheduleIconLiteral.Text = GetEventScheduleIconStyle(eventQuery);
                LocationLiteral.Text = eventQuery.Location;
                DescriptionLiteral.Text = eventQuery.Description;

                WhenLiteral.Text = LanguageHelper.GetKey("When", eventQuery.Locale);
                WhereLiteral.Text = LanguageHelper.GetKey("Where", eventQuery.Locale);
                AreYouThereLiteral.Text = LanguageHelper.GetKey("AreYouThere", eventQuery.Locale);
                RegistrationHyperlink.Text = LanguageHelper.GetKey("Register", eventQuery.Locale);

                var eventType = EventTypesHelper.Get(eventQuery.EventTypeID);

                GenerateRegistrationURL(eventQuery, eventType);

                EventRegistrationIconLiteral.Text = GetEventRegistrationIconStyle(eventType.IsRegisterable, eventQuery.Locale);
            }
            else
            {
                CheckAndRedirect();
            }
        }

        private void SetRegistrationButton(GY_Events eventQuery)
        {
            var now = Utilities.DateTimeNow();

            if (eventQuery.RegistrationStartDateTime != null && eventQuery.RegistrationEndDateTime == null)
            {
                if (now < eventQuery.RegistrationStartDateTime)
                {
                    RegistrationHyperlink.Text = LanguageHelper.GetKey("RegistrationNotOpenYet", eventQuery.Locale);
                    RegistrationHyperlink.NavigateUrl = string.Empty;
                    RegistrationHyperlink.Enabled = false;
                }

                if (eventQuery.RegistrationStartDateTime <= now)
                {
                    RegistrationHyperlink.Text = LanguageHelper.GetKey("RegistrationOpen", eventQuery.Locale);
                    RegistrationHyperlink.Enabled = true;
                }
            }
            else
            {
                if (eventQuery.RegistrationStartDateTime == null && eventQuery.RegistrationEndDateTime != null)
                {
                    if (now <= eventQuery.RegistrationEndDateTime)
                    {
                        RegistrationHyperlink.Text = LanguageHelper.GetKey("RegistrationOpen", eventQuery.Locale);
                        RegistrationHyperlink.Enabled = true;
                    }

                    if (eventQuery.RegistrationEndDateTime < now)
                    {
                        RegistrationHyperlink.Text = LanguageHelper.GetKey("RegistrationClosed", eventQuery.Locale);
                        RegistrationHyperlink.NavigateUrl = string.Empty;
                        RegistrationHyperlink.Enabled = false;
                    }
                }
                else
                {
                    if (eventQuery.RegistrationStartDateTime != null && eventQuery.RegistrationEndDateTime != null)
                    {
                        if (now >= eventQuery.RegistrationStartDateTime && now <= eventQuery.RegistrationEndDateTime)
                        {
                            RegistrationHyperlink.Text = LanguageHelper.GetKey("RegistrationOpen", eventQuery.Locale);
                            RegistrationHyperlink.Enabled = true;
                        }
                        else
                        {
                            if (now > eventQuery.RegistrationEndDateTime)
                            {
                                RegistrationHyperlink.Text = LanguageHelper.GetKey("RegistrationClosed", eventQuery.Locale);
                                RegistrationHyperlink.NavigateUrl = string.Empty;
                                RegistrationHyperlink.Enabled = false;
                            }
                            else
                            {
                                if (now < eventQuery.RegistrationStartDateTime)
                                {
                                    RegistrationHyperlink.Text = LanguageHelper.GetKey("RegistrationNotOpenYet", eventQuery.Locale);
                                    RegistrationHyperlink.NavigateUrl = string.Empty;
                                    RegistrationHyperlink.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckAndRedirect();

                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }
        }

        public string GetEventRegistrationIconStyle(bool isRegisterable, string locale)
        {
            const string html = "<div class='{0}'><span class='tip'><a title='{1}'></a></span></div>";

            if (isRegisterable)
            {
                return string.Format(html, "RegistrationRequired", LanguageHelper.GetKey("RequiresRegistration", locale));
            }
            else
            {
                return string.Format(html, "RegistrationNotRequired", LanguageHelper.GetKey("DoNotRequireRegistration", locale));
            }
        }

        public string GetEventScheduleIconStyle(GY_Events eventQuery)
        {
            const string html = "<div class='{0}'><span class='tip'><a title='{1}'></a></span></div>";
            var now = Utilities.DateTimeNow();

            if (eventQuery.EndsOn > now && now > eventQuery.StartsOn)
            {
                return string.Format(html, "ContinuingEvents", LanguageHelper.GetKey("OnGoingEvents", eventQuery.Locale));
            }
            else
            {
                if (eventQuery.EndsOn < now)
                {
                    return string.Format(html, "PastEvents", LanguageHelper.GetKey("PastEvents", eventQuery.Locale));
                }
                else
                {
                    if (eventQuery.StartsOn > now)
                    {
                        return string.Format(html, "UpComingEvents", LanguageHelper.GetKey("UpComingEvents", eventQuery.Locale));
                    }
                    else
                    {
                        return string.Format(html, "NormalEvents", string.Empty);
                    }
                }
            }
        }
    }
}