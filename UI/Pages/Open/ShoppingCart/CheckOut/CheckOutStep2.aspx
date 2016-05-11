<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="CheckOutStep2.aspx.cs" Inherits="ProjectJKL.UI.Pages.Open.ShoppingCart.CheckOut.CheckOutStep2"
    EnableEventValidation="false" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <OWD:CheckOutStep2Component runat="server" ID="CheckOutStep2Component" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>