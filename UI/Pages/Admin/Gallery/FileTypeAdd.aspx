<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="FileTypeAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.FileTypeAdd" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add New File Type</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Gallery/FileTypeList.aspx">
                <i class="icon-list-ul"></i> List File Types
                </asp:HyperLink>
            </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
                <ContentTemplate>
                    <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="name of the file type" RequiredErrorMessage="file type name is required" MaxLength="250" />
                    <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the file type" MaxLength="250" TextMode="MultiLine" />
                    <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>