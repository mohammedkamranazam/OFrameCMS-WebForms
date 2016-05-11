<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="OWDARO.UI.Pages.LogOn.ForgotPassword" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="QAPanel">
        <OWD:SecurityQAComponent runat="server" ID="SecurityQAComponent" />
    </asp:Panel>
    <OWD:RetrievePasswordComponent runat="server" ID="RetrievePasswordComponent" />
    <OWD:RetrieveUsernameComponent runat="server" ID="RetrieveUsernameComponent" />
</asp:content>