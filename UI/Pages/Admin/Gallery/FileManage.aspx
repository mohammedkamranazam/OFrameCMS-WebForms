<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="FileManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.FileManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Manage File</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                <asp:HyperLink runat="server" CssClass="btn btn-primary" NavigateUrl="~/UI/Pages/Admin/Gallery/FileList.aspx">
                    <i class="icon-arrow-left"></i> Go Back To Files List
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Gallery/FileAdd.aspx">
                <i class="icon-plus-sign"></i> Add File
                </asp:HyperLink>
            </div>
        <div class="content">
            <div class="grid1">
                <OWD:ImageSelectorComponent runat="server" ID="ImageSelectorComponent1" />
            </div>
            <div class="grid3">
                <asp:UpdatePanel runat="server" ClientIDMode="Static" ID="UpdatePanel1">
                    <ContentTemplate>
                        <OWD:Locales runat="server" ID="LocaleDropDown" LabelText="Locale" SmallLabelText="select a language" AutoPostBack="true" />
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="enter a title for file" RequiredErrorMessage="file title is required" MaxLength="100" ValidationGroup="FolderValidationGroup" />
                        <OWD:TextBoxAdv runat="server" ID="SubTitleTextBox" LabelText="Sub Title" SmallLabelText="enter a sub title for file" MaxLength="100" ValidationGroup="FolderValidationGroup" />
                        <OWD:TextBoxAdv runat="server" ID="DateTextBox" LabelText="Date" SmallLabelText="select a date for the file" CalendarDefaultView="Days" MaxLength="20" ValidationGroup="FolderValidationGroup" />
                        <OWD:TextBoxAdv MaxLength="250" TextMode="MultiLine" runat="server" ID="DescriptionEditor" LabelText="Description" SmallLabelText="enter the description for file" ValidationGroup="FolderValidationGroup" />
                        <OWD:DropDownListAdv runat="server" ID="FileTypesDropDownList" LabelText="File Type" SmallLabelText="type of the file" InitialValue="-1" RequiredFieldErrorMessage="please select a file type" ValidationGroup="FolderValidationGroup" />
                        <OWD:FoldersSelectComponent runat="server" ID="FoldersSelectComponent1" />
                        <OWD:FileUploadAdv ID="FileUpload1" runat="server" LabelText="File" SmallLabelText="file size should be less than 1GB" MaxFileSizeMB="1024" ValidationGroup="FolderValidationGroup" />
                        <OWD:TextBoxAdv runat="server" ID="TagsTextBox" LabelText="Tags" SmallLabelText="comma separated tags" MaxLength="200" ValidationGroup="FolderValidationGroup" FilterMode="InvalidChars" InvalidChars=" " />
                        <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="check this to hide the file" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" ValidationGroup="FolderValidationGroup" />
                <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
            </div>
            <div class="Clear"></div>
        </div>
    </div>
</asp:content>