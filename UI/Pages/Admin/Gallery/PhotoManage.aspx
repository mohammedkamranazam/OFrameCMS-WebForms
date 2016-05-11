<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="PhotoManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.PhotoManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Manage Photo</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink runat="server" ID="PhotoListHyperLink" CssClass="btn btn-primary">
                            <i class="icon-arrow-left"></i> Go Back To Photo List
                        </asp:HyperLink>
                        <asp:HyperLink runat="server" ID="AlbumManageHyperLink" CssClass="btn btn-success">
                            <i class="icon-cog"></i> Manage Album
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Gallery/AlbumList.aspx">
                            <i class="icon-list-ul"></i> List Albums
                        </asp:HyperLink>
                    </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
                <ContentTemplate>
                    <div class="grid1">
                        <OWD:ImageSelectorComponent runat="server" ID="ImageSelectorComponent1" />
                    </div>
                    <div class="grid3">
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Name" SmallLabelText="enter a title for photo" MaxLength="50" />
                        <OWD:TextBoxAdv runat="server" ID="DescriptionEditor" MaxLength="250" TextMode="MultiLine" LabelText="Description" SmallLabelText="enter description for photo" />
                        <OWD:TextBoxAdv runat="server" ID="TakenOnTextBox" LabelText="Taken On" SmallLabelText="select a date for the photo" CalendarDefaultView="Days" MaxLength="20" />
                        <OWD:TextBoxAdv runat="server" ID="LocationTextBox" LabelText="Location" SmallLabelText="location where this photo relates to" MaxLength="250" TextMode="MultiLine" />
                        <OWD:TextBoxAdv runat="server" ID="TagsTextBox" LabelText="Tags" SmallLabelText="comma separated tags" MaxLength="250" FilterMode="InvalidChars" InvalidChars=" " />
                        <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="check this to hide the video" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </div>
                    <div class="Clear"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="AlbumIDHiddenField" Visible="false" />
</asp:content>