<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="AudioAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.AudioAdd" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add New Audio</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Gallery/AudioList.aspx">
                            <i class="icon-list-ul"></i> List Audios
                        </asp:HyperLink>
                    </div>
        <div class="content">
            <asp:UpdatePanel runat="server" ClientIDMode="Static" ID="UpdatePanel1">
                <ContentTemplate>
                    <div class="grid1">
                        <OWD:FileSelectorComponent runat="server" ID="FileSelectorComponent1" />
                    </div>
                    <div class="grid3">
                        <OWD:Locales runat="server" ID="LocaleDropDown" LabelText="Locale" SmallLabelText="select a language" AutoPostBack="true" />
                        <OWD:CheckBoxAdv runat="server" ID="ShowWebAudioCheckBox" LabelText="Show Web Audio" SmallLabelText="check to show web audio instead of local audio" AutoPostBack="true" />
                        <OWD:TextBoxAdv runat="server" ID="WebAudioURLTextBox" LabelText="Web Audio Code" SmallLabelText="the embed code of the web audio" RequiredErrorMessage="audio code is required" Visible="false" />
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="name of the audio" RequiredErrorMessage="audio name is required" MaxLength="250" />
                        <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the audio" MaxLength="500" TextMode="MultiLine" />
                        <OWD:TextBoxAdv runat="server" ID="TakenOnTextBox" LabelText="Taken On" SmallLabelText="select a date for the audio" CalendarDefaultView="Days" MaxLength="20" />
                        <OWD:TextBoxAdv runat="server" ID="LocationTextBox" LabelText="Location" SmallLabelText="location where this audio relates to" MaxLength="250" TextMode="MultiLine" />
                        <OWD:TextBoxAdv runat="server" ID="TagsTextBox" LabelText="Tags" SmallLabelText="comma separated tags" MaxLength="250" FilterMode="InvalidChars" InvalidChars=" " />
                        <OWD:TextBoxAdv runat="server" ID="AudioLengthTextBox" LabelText="Length In Minutes" SmallLabelText="the total duration of the audio in minutes" MaxLength="10" />
                        <OWD:AudioCategoriesSelectComponent runat="server" id="AudioCategoriesSelectComponent1" />
                        <OWD:DropDownListAdv runat="server" ID="AudioSetDropDownList" LabelText="Set" SmallLabelText="set to which the audio belongs" Visible="false" />
                        <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="show/hide the audio" HelpLabelText="switch on to hide this audio" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </div>
                    <div class="Clear"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>