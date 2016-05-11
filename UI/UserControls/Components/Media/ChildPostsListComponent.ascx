<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChildPostsListComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Media.ChildPostsListComponent" %>

<h4 class="ChildPostListComponentWidgetTitle" runat="server" id="TitleHeader">
    <asp:Literal runat="server" ID="TitleHeaderLiteral"></asp:Literal>
</h4>
<ul class="ChildPostsListComponent" runat="server" id="ChildPostsListComponentUL">
    <asp:Literal runat="server" ID="ItemsLiteral"></asp:Literal>
</ul>