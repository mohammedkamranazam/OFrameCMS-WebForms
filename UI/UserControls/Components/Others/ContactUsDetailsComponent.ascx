<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactUsDetailsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Others.ContactUsDetailsComponent" %>

<h1 class="PageTitle" runat="server" id="TitleH1">
    <asp:Literal runat="server" ID="TitleLiteral"></asp:Literal>
</h1>

<div class="ContactUsDetails">
    <OWD:PostEmbedComponent runat="server" ID="PostEmbedComponent1" />
    <div class="Clear"></div>
</div>
