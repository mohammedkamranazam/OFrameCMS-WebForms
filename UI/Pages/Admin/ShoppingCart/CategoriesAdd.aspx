<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="CategoriesAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.CategoriesAdd" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add New Category</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/CategoriesList.aspx">
                <i class="icon-list-ul"></i> List Categories
                </asp:HyperLink>
            </div>
            <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="name of the category" RequiredErrorMessage="category name is required"
                MaxLength="50" />
            <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the category" MaxLength="200" TextMode="MultiLine" />
            <OWD:DropDownListAdv runat="server" ID="SectionsDropDownList" LabelText="Section" SmallLabelText="section to which the category belongs"
                RequiredFieldErrorMessage="please select a section" InitialValue="-1" />
            <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="show/hide the category" HelpLabelText="switch on to hide this category" />
            <OWD:FileUploadAdv runat="server" ID="FileUpload1" LabelText="Category Image" SmallLabelText="image assigned to this category" MaxFileSizeMB="5"
                RequiredErrorMessage="select an image to upload" />
            <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
            <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
        </div>
    </div>
</asp:content>