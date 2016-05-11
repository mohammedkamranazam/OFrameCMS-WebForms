<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="AlbumManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.AlbumManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Manage Photo Album</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink runat="server" CssClass="btn btn-primary" NavigateUrl="~/UI/Pages/Admin/Gallery/AlbumList.aspx">
                            <i class="icon-arrow-left"></i> Go Back To Albums List
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Gallery/AlbumAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Album
                        </asp:HyperLink>
                        <asp:HyperLink runat="server" ID="PhotoAddHyperLink" CssClass="btn btn-success">
                            <i class="icon-plus-sign"></i> Add Photo
                        </asp:HyperLink>
                        <asp:HyperLink ID="PhotoListHyperlink" runat="server" CssClass="btn">
                            <i class="icon-list-ul"></i> List Photos
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
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Name" SmallLabelText="enter a name for album" RequiredErrorMessage="album name is required" MaxLength="50" />
                        <OWD:TextBoxAdv MaxLength="250" TextMode="MultiLine" runat="server" ID="DescriptionEditor" LabelText="Description" SmallLabelText="enter description for album" />
                        <OWD:TextBoxAdv runat="server" ID="TakenOnTextBox" LabelText="Taken On" SmallLabelText="select a date for the album" CalendarDefaultView="Days" MaxLength="20" />
                        <OWD:TextBoxAdv runat="server" ID="LocationTextBox" LabelText="Location" SmallLabelText="location where this album relates to" MaxLength="250" TextMode="MultiLine" />
                        <OWD:TextBoxAdv runat="server" ID="TagsTextBox" LabelText="Tags" SmallLabelText="tags separated by semi colon" MaxLength="250" FilterMode="InvalidChars" InvalidChars=" " />
                        <OWD:DropDownListAdv runat="server" ID="EventsDropDownList" LabelText="Events" SmallLabelText="Event to which this album belongs" />
                        <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="check this to hide the album" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </div>
                    <div class="Clear"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>