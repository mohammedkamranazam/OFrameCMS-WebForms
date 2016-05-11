<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="BrandsAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.BrandsAdd" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add New Brand</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/BrandsList.aspx">
                <i class="icon-list-ul"></i> Brands List
                </asp:HyperLink>
            </div>
            <OWD:TextBoxAdv ID="TitleTextBox" runat="server" LabelText="Title" SmallLabelText="brand name" RequiredErrorMessage="brand name is required" MaxLength="50" />
            <OWD:TextBoxAdv ID="DescriptionTextBox" runat="server" LabelText="Description" SmallLabelText="brand description" TextMode="MultiLine" MaxLength="250" />
            <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="show/hide the brand" HelpLabelText="switch on to hide this brand" />
            <OWD:FileUploadAdv ID="FileUpload1" runat="server" LabelText="Image" SmallLabelText="image of the brand" MaxFileSizeMB="5" />
            <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
            <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
        </div>
    </div>
</asp:content>