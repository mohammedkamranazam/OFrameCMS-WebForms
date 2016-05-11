<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Gallery.EventsComponent" %>

<h1 runat="server" id="TitleH1" class="PageTitle">
    <asp:Literal runat="server" ID="TitleLiteral"></asp:Literal>
</h1>
<asp:Panel runat="server" ID="EventsPanel" CssClass="EventListDiv">
    <asp:ListView ID="DataList1" runat="server">
        <ItemTemplate>
            <div class="EventListItemDiv" style='<%# string.Format("direction:{0};", OWDARO.Settings.LanguageHelper.GetLocaleDirection((string)Eval("Locale"))) %>'>
                <div class="EventListItemContent">
                    <%# OWDARO.Util.Utilities.GetFancyBoxHTML((int?)Eval("ImageID"), string.Empty, false, string.Empty) %>
                    <div class="DescriptionDiv">
                        <h1 class="Title">
                            <asp:HyperLink runat="server" NavigateUrl='<%#String.Format("~/Event.aspx?EventID={0}", Eval("EventID")) %>'>
                            <%#Eval("Title")%>
                            </asp:HyperLink>
                        </h1>
                        <h3 class="SubTitle" style='<%# string.Format("display:{0};", ((string.IsNullOrWhiteSpace((string)Eval("SubTitle"))) ? "none": "block")) %>'>
                            <%#Eval("SubTitle") %>
                        </h3>
                        <p class="ShortDescription">
                            <%#Eval("SubDescription") %>
                        </p>
                    </div>
                </div>
                <div class="Schedule">
                    <span class="ScheduleSpan">
                        <%#String.Format("{0}{1}", OWDARO.Settings.LanguageHelper.GetKey("EventStartsOn", (string)Eval("Locale")),
                        OWDARO.Util.DataParser.GetDateTimeFormattedString((DateTime)Eval("StartsOn")))%>
                    </span>
                    <span class="ScheduleSpan">
                        <%#String.Format("{0}{1}",OWDARO.Settings.LanguageHelper.GetKey("EventEndsOn", (string)Eval("Locale")),
                        OWDARO.Util.DataParser.GetDateTimeFormattedString((DateTime)Eval("EndsOn")))%>
                    </span>
                    <asp:HyperLink runat="server" CssClass="ReadMoreAnchor"
                        NavigateUrl='<%#String.Format("~/Event.aspx?EventID={0}&EventSchedule={1}", Eval("EventID"), Request.QueryString["EventSchedule"]) %>'
                        Text='<%# OWDARO.Settings.LanguageHelper.GetKey("ReadMore", (string)Eval("Locale")) %>' />
                    <%# GetEventScheduleIconStyle((DateTime?)Eval("EndsOn"), (DateTime?)Eval("StartsOn")) %>
                    <%# GetEventRegistrationIconStyle((int)Eval("EventTypeID")) %>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>
    <asp:Button runat="server" ID="LoadMoreButton" OnClick="LoadMoreButton_Click" CssClass="LoadMoreButton" />
</asp:Panel>
<asp:Panel runat="server" ID="EmptyDataPanel" CssClass="EmptyEvents">
    <asp:Image ID="Image4" runat="server" CssClass="EmptyEventsImage" ImageUrl="~/Themes/Default/Graphics/nothinghere.png" />
</asp:Panel>
<asp:HiddenField runat="server" ID="CurrentCountHiddenField" Value="0" />