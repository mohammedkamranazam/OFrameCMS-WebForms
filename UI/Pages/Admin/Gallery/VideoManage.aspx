<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true"
    CodeBehind="VideoManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.VideoManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Manage Video</span>
        </div>
        <div class="Clear">
        </div>
         <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Gallery/VideoList.aspx">
                <i class="icon-list-ul"></i> List Videos
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Gallery/VideoAdd.aspx">
                <i class="icon-plus-sign"></i> Add Video
                        </asp:HyperLink>
                    </div>
        <div class="content">
            <asp:UpdatePanel runat="server" ClientIDMode="Static" ID="UpdatePanel1">
                <ContentTemplate>
                    <div class="grid1">
                        <div class="center">
                            <asp:HyperLink runat="server" ID="fancybox" ClientIDMode="Static">
                                <asp:Image ID="VideoImage" runat="server" CssClass="ManageImage" />
                            </asp:HyperLink>
                        </div>
                    </div>
                    <div class="grid3">
                        <OWD:Locales runat="server" ID="LocaleDropDown" LabelText="Locale" SmallLabelText="select a language" AutoPostBack="true" />
                        <OWD:CheckBoxAdv runat="server" ID="ShowWebVideoCheckBox" LabelText="Show Web Video" SmallLabelText="check to show web video instead of local video" AutoPostBack="true" />
                        <OWD:TextBoxAdv runat="server" ID="WebVideoURLTextBox" LabelText="Web Video Code" SmallLabelText="the code of the web video" RequiredErrorMessage="video code is required" Visible="false" />
                        <OWD:FileSelectorComponent runat="server" ID="FileSelectorComponent1" />
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="name of the video" RequiredErrorMessage="video name is required" MaxLength="250" />
                        <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the video" MaxLength="500" TextMode="MultiLine" />
                        <OWD:TextBoxAdv runat="server" ID="TakenOnTextBox" LabelText="Taken On" SmallLabelText="select a date for the video" CalendarDefaultView="Days" MaxLength="20" />
                        <OWD:TextBoxAdv runat="server" ID="LocationTextBox" LabelText="Location" SmallLabelText="location where this video relates to" MaxLength="250" TextMode="MultiLine" />
                        <OWD:TextBoxAdv runat="server" ID="TagsTextBox" LabelText="Tags" SmallLabelText="comma separated tags" MaxLength="250" FilterMode="InvalidChars" InvalidChars=" " />
                        <OWD:TextBoxAdv runat="server" ID="VideoLengthTextBox" LabelText="Length In Minutes" SmallLabelText="the total duration of the video in minutes" MaxLength="10" />
                        <OWD:VideoCategoriesSelectComponent runat="server" id="VideoCategoriesSelectComponent1" ResetVisible="true" />
                        <OWD:DropDownListAdv runat="server" ID="VideoSetDropDownList" LabelText="Set" SmallLabelText="set to which the video belongs" />
                        <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="show/hide the video" HelpLabelText="switch on to hide this video" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </div>
                    <div class="Clear"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>