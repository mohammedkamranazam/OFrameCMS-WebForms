<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="AvailabilityTypesManage.aspx.cs"
    Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.AvailabilityTypesManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Manage Availability Type</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/AvailabilityTypesList.aspx">
                <i class="icon-list-ul"></i> Availability Types List
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/AvailabilityTypesAdd.aspx">
                <i class="icon-plus-sign"></i> Add Availability Type
                </asp:HyperLink>
            </div>
            <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="availability type title"
                RequiredErrorMessage="availability type title is required" MaxLength="50" />
            <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the availability type"
                MaxLength="200" TextMode="MultiLine" />
            <OWD:TextBoxAdv runat="server" ID="ColorTextBox" LabelText="Color" SmallLabelText="system name of the color to display availability type as"
                RequiredErrorMessage="system color name is required" MaxLength="50" />
            <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="show/hide the availability type"
                HelpLabelText="switch on to hide this availbility type" />
            <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
            <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
        </div>
    </div>
</asp:content>