<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OAuthComponent.ascx.cs" Inherits="ProjectJKL.UI.UserControls.Components.OFrameMembership.Nested.OAuthComponent" %>

<div class="OAuthComponent">
    <h2>
        <asp:Literal runat="server" ID="TitleLiteral"></asp:Literal>
    </h2>
    <asp:HyperLink runat="server" ID="FacebookHyperLink" CssClass="Facebook" Text="Facebook" Visible="false"></asp:HyperLink>
    <asp:HyperLink runat="server" ID="GoogleHyperLink" CssClass="Google" Text="Google" Visible="false"></asp:HyperLink>
    <asp:HyperLink runat="server" ID="TwitterHyperLink" CssClass="Twitter" Text="Twitter" Visible="false"></asp:HyperLink>
</div>