<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="IconsManage.aspx.cs"
    Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.IconsManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Manage Icon</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/IconsList.aspx">
                <i class="icon-list-ul"></i> Icons List
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/IconsAdd.aspx">
                <i class="icon-plus-sign"></i> Add Icon
                </asp:HyperLink>
            </div>
            <div class="grid1">
                <div class="center">
                    <asp:Image ID="IconImage" runat="server" CssClass="ManageImage" Width="50px" Height="50px" />
                </div>
            </div>
            <div class="grid3">
                <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="name of the icon"
                    RequiredErrorMessage="icon name is required" MaxLength="50" />
                <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the icon"
                    MaxLength="200" TextMode="MultiLine" RequiredErrorMessage="description of the icon is required" />
                <OWD:FileUploadAdv runat="server" ID="FileUpload1" LabelText="Icon Image" SmallLabelText="image assigned to this icon"
                    MaxFileSizeMB="5" RequiredErrorMessage="icon image is required" />
                <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
            </div>
            <div class="Clear"></div>
        </div>
    </div>
</asp:content>