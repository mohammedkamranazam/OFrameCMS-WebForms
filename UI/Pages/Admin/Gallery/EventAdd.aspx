<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="EventAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.EventAdd" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add Event</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Gallery/EventList.aspx">
                            <i class="icon-list-ul"></i> List Events
                        </asp:HyperLink>
                    </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
                <ContentTemplate>
                    <div class="grid1">
                        <OWD:ImageSelectorComponent runat="server" ID="ImageSelectorComponent1" />
                    </div>
                    <div class="grid3">
                        <OWD:Locales runat="server" ID="LocaleDropDown" LabelText="Locale" SmallLabelText="select a language" AutoPostBack="true" />
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="title for the event" RequiredErrorMessage="event name is required" MaxLength="100" />
                        <OWD:TextBoxAdv runat="server" ID="SubTitleTextBox" LabelText="Sub Title" SmallLabelText="sub title for the event" MaxLength="100" />
                        <OWD:TextBoxAdv runat="server" ID="SubDescriptionTextBox" LabelText="Short Description" SmallLabelText="a short description for the event" RequiredErrorMessage="short description is required" TextMode="MultiLine" MaxLength="250" />
                        <OWD:CKEditor runat="server" ID="DescriptionEditor" LabelText="Description" SmallLabelText="rich text description for the event" RequiredErrorMessage="description is required" />
                        <OWD:TextBoxAdv runat="server" ID="LocationTextBox" LabelText="Event Location" SmallLabelText="location where this event will be held" RequiredErrorMessage="event location is required" MaxLength="250" TextMode="MultiLine" />
                        <OWD:TextBoxAdv runat="server" ID="StartsOnDateTextBox" LabelText="Start Date" SmallLabelText="start date for the event" RequiredErrorMessage="event start date is required" CalendarDefaultView="Months" MaxLength="20" />
                        <OWD:TextBoxAdv runat="server" ID="EndsOnDateTextBox" LabelText="End Date" SmallLabelText="end date for the event" RequiredErrorMessage="event end date is required" CalendarDefaultView="Months" MaxLength="20" />
                        <OWD:TextBoxAdv runat="server" ID="StartsOnTimeTextBox" LabelText="Start Time" RequiredErrorMessage="event start time is required" MaxLength="20" MEAcceptAMPM="true" MEMaskType="Time" MEMask="99:99:99" MEOnInvalidCssClass="MaskedEditOnError" IsValidEmpty="False" EmptyValueMessage="Time is required" InvalidValueMessage="Time is invalid" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*" MEVVisible="true" />
                        <OWD:TextBoxAdv runat="server" ID="EndsOnTimeTextBox" LabelText="End Time" RequiredErrorMessage="event end time is required" MaxLength="20" MEAcceptAMPM="true" MEMaskType="Time" MEMask="99:99:99" MEOnInvalidCssClass="MaskedEditOnError" IsValidEmpty="False" EmptyValueMessage="Time is required" EmptyValueBlurredText="*" InvalidValueMessage="Time is invalid" InvalidValueBlurredMessage="*" MEVVisible="true" />
                        <OWD:TextBoxAdv runat="server" ID="TagsTextBox" LabelText="Tags" SmallLabelText="comma separated tags" MaxLength="250" FilterMode="InvalidChars" InvalidChars=" " />
                        <OWD:DropDownListAdv runat="server" ID="EventTypeDropDownList" LabelText="Event Type" SmallLabelText="the type of event it will be" InitialValue="-1" RequiredFieldErrorMessage="please select an event type" AutoPostBack="true" OnChange="return OnChange(this);" />
                        <OWD:CheckBoxAdv runat="server" ID="UseExternalFormCheckBox" LabelText="Use External Form" SmallLabelText="select this to use external form" AutoPostBack="true" Visible="false" />
                        <OWD:DropDownListAdv runat="server" ID="ExternalFormTypesDropDownList" LabelText="External Form Type" SmallLabelText="the type of external form to use" AutoPostBack="true" OnChange="return OnChange(this);" RequiredFieldErrorMessage="please select a form type" InitialValue="-1" Visible="false" />
                        <OWD:UnsafeTextBox runat="server" ID="ExternalFormEmbedCodeTextBox" LabelText="External Form Embed Code" SmallLabelText="the embed code of the external form to show" RequiredErrorMessage="embed code is required" Visible="false" EditorHeight="100px" />
                        <OWD:TextBoxAdv runat="server" ID="ExternalFormURLTextBox" LabelText="External Form URL" SmallLabelText="the url of the external form to show" RequiredErrorMessage="external form url is required" Visible="false" />
                        <OWD:CheckBoxAdv runat="server" ID="OpenExternalFormInNewTabCheckBox" LabelText="Open Form In New Tab" SmallLabelText="select to open the external form in a new tab" Visible="false" />
                        <OWD:DropDownListAdv runat="server" ID="ExternalFormIDDropDownList" LabelText="External Form" SmallLabelText="select an external form from pre built forms" InitialValue="-1" RequiredFieldErrorMessage="please select a form" Visible="false" />
                        <OWD:TextBoxAdv runat="server" ID="RegistrationStartDateTextBox" LabelText="Registration Starts On" SmallLabelText="date when the registration for the event opens up" CalendarDefaultView="Months" MaxLength="20" Visible="false" />
                        <OWD:TextBoxAdv runat="server" ID="RegistrationEndDateTextBox" LabelText="Registration Ends On" SmallLabelText="date when the registration for the event closes" CalendarDefaultView="Months" MaxLength="20" Visible="false" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </div>
                    <div class="Clear"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>