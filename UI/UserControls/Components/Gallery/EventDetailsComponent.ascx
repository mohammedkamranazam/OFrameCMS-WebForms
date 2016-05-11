<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventDetailsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Gallery.EventDetailsComponent" %>

<div class="EventDetailsDiv" runat="server" id="EventDetailsDIV">
    <asp:Literal runat="server" ID="FancyBoxLiteral"></asp:Literal>
    <div class="EventContent">
        <h1>
            <asp:Literal runat="server" ID="TitleLiteral" />
        </h1>
        <h2>
            <asp:Literal runat="server" ID="SubTitleLiteral" />
        </h2>
        <p>
            <asp:Literal runat="server" ID="SubDescriptionLiteral" />
        </p>
    </div>
    <div class="Clear"></div>
    <div class="InfoDiv">
        <div class="ScheduleDiv">
            <span class="BlockTitle When">
                <asp:Literal runat="server" ID="WhenLiteral" />
            </span>
            <span class="StartsOnDateSpan">
                <asp:Literal runat="server" ID="StartsOnDateLiteral" />
            </span>
            <span class="StartsOnTimeSpan">
                <asp:Literal runat="server" ID="StartsOnTimeLiteral" />
            </span>
            <span class="EndsOnDateSpan">
                <asp:Literal runat="server" ID="EndsOnDateLiteral" />
            </span>
            <span class="EndsOnTimeSpan">
                <asp:Literal runat="server" ID="EndsOnTimeLiteral" />
            </span>
        </div>
        <div class="LocationDiv">
            <span class="BlockTitle Where">
                <asp:Literal runat="server" ID="WhereLiteral" />
            </span>
            <p>
                <asp:Literal runat="server" ID="LocationLiteral"></asp:Literal>
            </p>
        </div>
        <div class="RegistrationDiv">
            <span class="BlockTitle AreYouThere">
                <asp:Literal runat="server" ID="AreYouThereLiteral" />
            </span>
            <asp:HyperLink runat="server" ID="RegistrationHyperlink" CssClass="RegistrationLink"></asp:HyperLink>
            <div class="EventIconDiv">
                <asp:Literal runat="server" ID="EventScheduleIconLiteral"></asp:Literal>
                <asp:Literal runat="server" ID="EventRegistrationIconLiteral"></asp:Literal>
            </div>
        </div>
        <div class="Clear"></div>
    </div>
    <div class="Clear"></div>
    <div class="EventRichDescription">
        <asp:Literal runat="server" ID="DescriptionLiteral"></asp:Literal>
        <div class="Clear"></div>
    </div>
</div>