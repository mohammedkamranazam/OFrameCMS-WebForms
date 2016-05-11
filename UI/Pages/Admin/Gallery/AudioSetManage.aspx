<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true"
    CodeBehind="AudioSetManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.AudioSetManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Edit Audio Set</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Gallery/AudioSetList.aspx">
                            <i class="icon-list-ul"></i> List Audio Set
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Gallery/AudioSetAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Audio Set
                        </asp:HyperLink>
                    </div>
        <div class="content">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" ClientIDMode="Static">
                <ContentTemplate>
                    <OWD:Locales runat="server" ID="LocaleDropDown" LabelText="Locale" SmallLabelText="select a language" AutoPostBack="true" />
                    <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="name of the audio set" RequiredErrorMessage="audio set name is required" MaxLength="250" />
                    <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the audio set" MaxLength="500" TextMode="MultiLine" />
                    <OWD:TextBoxAdv runat="server" ID="TakenOnTextBox" LabelText="Taken On" SmallLabelText="select a date for the audio set" CalendarDefaultView="Days" MaxLength="20" />
                    <OWD:TextBoxAdv runat="server" ID="LocationTextBox" LabelText="Location" SmallLabelText="location where this audio set relates to" MaxLength="250" TextMode="MultiLine" />
                    <OWD:TextBoxAdv runat="server" ID="TagsTextBox" LabelText="Tags" SmallLabelText="comma separated tags" MaxLength="250" FilterMode="InvalidChars" InvalidChars=" " />
                    <owd:AudioCategoriesSelectComponent runat="server" id="AudioCategoriesSelectComponent1" />
                    <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="show/hide the audio set" HelpLabelText="switch on to hide this audio set" />
                    <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>