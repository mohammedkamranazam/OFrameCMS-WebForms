<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="SectionsAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.SectionsAdd" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add New Section</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/SectionsList.aspx">
                <i class="icon-list-ul"></i> List Sections
                </asp:HyperLink>
            </div>
            <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="name of the section"
                RequiredErrorMessage="section name is required" MaxLength="50" />
            <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the section"
                MaxLength="200" TextMode="MultiLine" />
            <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="show/hide the section"
                HelpLabelText="switch on to hide this section" />
            <OWD:FileUploadAdv runat="server" ID="FileUpload1" LabelText="Section Image" SmallLabelText="image assigned to this section"
                MaxFileSizeMB="5" RequiredErrorMessage="select an image to upload" />
            <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
            <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
        </div>
    </div>
</asp:content>