<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatusMessageJQuery.ascx.cs"
    Inherits="OWDARO.UI.UserControls.Controls.StatusMessageJQuery" %>

<asp:Panel Visible="false" runat="server" ID="StatusMessagePanel" CssClass="alertMessage"
    ClientIDMode="Static" onclick="Close()" Style="cursor: pointer;">
    <asp:Label runat="server" ID="StatusMessageLabel"></asp:Label>
</asp:Panel>