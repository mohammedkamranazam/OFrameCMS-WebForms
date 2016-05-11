using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class EventsComponent : UserControl
    {
        public EventSchedule EventSchedule
        {
            get
            {
                return (ViewState["EventSchedule"] == null) ? EventSchedule.All : (EventSchedule)ViewState["EventSchedule"];
            }

            set
            {
                ViewState["EventSchedule"] = value;
            }
        }

        private async Task<IQueryable<GY_Events>> GetEvents(GalleryEntities context)
        {
            var now = Utilities.DateTimeNow();

            var locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
            var direction = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureDirectionCookieKey);

            TitleH1.Style.Add("direction", direction);
            LoadMoreButton.Text = LanguageHelper.GetKey("LoadMore", locale);

            var eventsQuery = await (from set in context.GY_Events
                                     where set.Hide == false && set.Locale == locale
                                     select set).ToListAsync();

            switch (EventSchedule)
            {
                case EventSchedule.Continuing:
                    TitleLiteral.Text = LanguageHelper.GetKey("OnGoingEvents", locale);
                    Utilities.SetPageSEO(Page, SEOHelper.GetPageSEO("OnGoingEventsPage"));
                    return (from events in eventsQuery.AsQueryable<GY_Events>()
                            where (events.EndsOn > now && now > events.StartsOn)
                            select events).OrderBy(c => c.StartsOn);

                case EventSchedule.Past:
                    TitleLiteral.Text = LanguageHelper.GetKey("PastEvents", locale);
                    Utilities.SetPageSEO(Page, SEOHelper.GetPageSEO("PastEventsPage"));
                    return (from events in eventsQuery.AsQueryable<GY_Events>()
                            where events.EndsOn < now
                            select events).OrderByDescending(c => c.StartsOn);

                case EventSchedule.UpComing:
                    TitleLiteral.Text = LanguageHelper.GetKey("UpComingEvents", locale);
                    Utilities.SetPageSEO(Page, SEOHelper.GetPageSEO("UpcomingEventsPage"));
                    return (from events in eventsQuery.AsQueryable<GY_Events>()
                            where events.StartsOn > now
                            select events).OrderBy(c => c.StartsOn);

                case EventSchedule.All:
                default:
                    TitleLiteral.Text = LanguageHelper.GetKey("AllEvents", locale);
                    Utilities.SetPageSEO(Page, SEOHelper.GetPageSEO("AllEventsPage"));
                    return (from events in eventsQuery.AsQueryable<GY_Events>()
                            select events).OrderByDescending(c => c.StartsOn);
            }
        }

        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                await LoadEvents(context);
            }
        }

        private async Task LoadEvents(GalleryEntities context)
        {
            var currentCount = DataParser.IntParse(CurrentCountHiddenField.Value);
            var toFetchCount = currentCount + 20;
            CurrentCountHiddenField.Value = toFetchCount.ToString();

            var eventsQuery = await GetEvents(context);
            DataList1.DataSource = eventsQuery.Take(toFetchCount);
            DataList1.DataBind();

            UpdateLoadMoreControls(toFetchCount, eventsQuery.Count());
        }

        private void SetVisibility(int count)
        {
            if (count > 0)
            {
                EventsPanel.Visible = true;
                EmptyDataPanel.Visible = false;
            }
            else
            {
                EventsPanel.Visible = false;
                EmptyDataPanel.Visible = true;
            }
        }

        private void UpdateLoadMoreControls(int toFetchCount, int totalItemsCount)
        {
            if (totalItemsCount == 0)
            {
                LoadMoreButton.Visible = false;
            }

            if (toFetchCount >= totalItemsCount)
            {
                LoadMoreButton.Enabled = false;
                LoadMoreButton.CssClass = "LoadMoreButtonDisabled";
                LoadMoreButton.Text = LanguageHelper.GetKey("NoMoreItemsToDisplay");
            }

            SetVisibility(totalItemsCount);
        }

        protected async void LoadMoreButton_Click(object sender, EventArgs e)
        {
            using (var context = new GalleryEntities())
            {
                await LoadEvents(context);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["EventSchedule"]))
                {
                    EventSchedule eventSchedule;

                    if (Enum.TryParse<EventSchedule>(Request.QueryString["EventSchedule"], out eventSchedule))
                    {
                        EventSchedule = eventSchedule;
                    }
                    else
                    {
                        EventSchedule = EventSchedule.All;
                    }
                }
                else
                {
                    EventSchedule = EventSchedule.All;
                }

                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }
        }

        public string GetEventRegistrationIconStyle(int eventTypeID)
        {
            var html = "<div class='{0}'><span class='tip'><a title='{1}'></a></span></div>";

            var eventType = EventTypesHelper.Get(eventTypeID);

            if (eventType.IsRegisterable)
            {
                return string.Format(html, "RegistrationRequired", LanguageHelper.GetKey("RequiresRegistration"));
            }
            else
            {
                return string.Format(html, "RegistrationNotRequired", LanguageHelper.GetKey("DoNotRequireRegistration"));
            }
        }

        public string GetEventScheduleIconStyle(DateTime? endsOn, DateTime? startsOn)
        {
            var html = "<div class='{0}'><span class='tip'><a title='{1}'></a></span></div>";
            var now = Utilities.DateTimeNow();

            if (EventSchedule == EventSchedule.All)
            {
                if (endsOn > now && now > startsOn)
                {
                    return string.Format(html, "ContinuingEvents", LanguageHelper.GetKey("OnGoingEvents"));
                }
                else
                {
                    if (endsOn < now)
                    {
                        return string.Format(html, "PastEvents", LanguageHelper.GetKey("PastEvents"));
                    }
                    else
                    {
                        if (startsOn > now)
                        {
                            return string.Format(html, "UpComingEvents", LanguageHelper.GetKey("UpComingEvents"));
                        }
                        else
                        {
                            return string.Format(html, "NormalEvents", string.Empty);
                        }
                    }
                }
            }
            else
            {
                return string.Format(html, "NormalEvents", string.Empty);
            }
        }
    }
}