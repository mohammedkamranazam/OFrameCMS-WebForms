<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TagCloudComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Others.TagCloudComponent" %>
<%@ Register Assembly="ProjectJKL" Namespace="OWDARO.Oframe" TagPrefix="OWD" %>

<div class="TagCloudContainer">
    <asp:Literal runat="server" ID="TitleLiteral"></asp:Literal>
    <OWD:TagCloud ID="TagCloud1" runat="server" />
    <asp:Literal runat="server" ID="NoContentLiteral"></asp:Literal>
</div>