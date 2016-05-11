<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageShareComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Others.PageShareComponent" %>

<span id="ShareSocial" class="animated bounceIn">
    <asp:HyperLink runat="server" ID="FacebookHyperlink" CssClass="smedia facebook tooltip" title="Share On Facebook" Target="_blank"></asp:HyperLink>
    <asp:HyperLink runat="server" ID="TwitterHyperlink" CssClass="smedia twitter tooltip" title="Share On Twitter" Target="_blank"></asp:HyperLink>
    <asp:HyperLink runat="server" ID="GooglePlusHyperlink" CssClass="smedia googleplus tooltip" title="Share On Google Plus" Target="_blank"></asp:HyperLink>
</span>