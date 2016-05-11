<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="FolderAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.FolderAdd" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add Folder</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Gallery/FolderList.aspx">
                            <i class="icon-list-ul"></i> List Folders
                        </asp:HyperLink>
                    </div>
        <div class="content">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" ClientIDMode="Static">
                <ContentTemplate>
                    <div class="grid1">
                        <OWD:ImageSelectorComponent runat="server" ID="ImageSelectorComponent1" />
                    </div>
                    <div class="grid3">
                        <OWD:Locales runat="server" ID="LocaleDropDown" LabelText="Locale" SmallLabelText="select a language" AutoPostBack="true" />
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Name" SmallLabelText="enter a name for folder" RequiredErrorMessage="folder name is required" MaxLength="100" ValidationGroup="FolderValidationGroup" />
                        <OWD:TextBoxAdv MaxLength="250" TextMode="MultiLine" runat="server" ID="DescriptionEditor" LabelText="Description" SmallLabelText="enter the description for folder" ValidationGroup="FolderValidationGroup" />
                        <OWD:FoldersSelectComponent runat="server" ID="FoldersSelectComponent1" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ValidationGroup="FolderValidationGroup" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </div>
                    <div class="Clear"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>