<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="OrderStatusTypesAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.OrderStatusTypesAdd" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="">
                <i class="icon-list-ul"></i> XXXXXXXXXXXXXXXX
                </asp:HyperLink>
            </div>
            <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
            <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
        </div>
    </div>
</asp:content>