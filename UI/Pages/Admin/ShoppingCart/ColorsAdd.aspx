<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="ColorsAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.ColorsAdd" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add New Color</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ColorsList.aspx">
                <i class="icon-list-ul"></i> Colors List
                </asp:HyperLink>
            </div>
            <OWD:TextBoxAdv ID="TitleTextBox" runat="server" LabelText="Title" SmallLabelText="color name" RequiredErrorMessage="color name is required" MaxLength="50" />
            <OWD:TextBoxAdv ID="NameTextBox" runat="server" LabelText="System Name" SmallLabelText="system name of the color" RequiredErrorMessage="system name of the color is required" MaxLength="50" />
            <OWD:TextBoxAdv ID="HexTextBox" runat="server" LabelText="Hex Code" SmallLabelText="hex code of the color" MaxLength="6" />
            <OWD:FileUploadAdv ID="FileUpload1" runat="server" LabelText="Image" SmallLabelText="image of the color" MaxFileSizeMB="5" />
            <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
            <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
        </div>
    </div>
</asp:content>