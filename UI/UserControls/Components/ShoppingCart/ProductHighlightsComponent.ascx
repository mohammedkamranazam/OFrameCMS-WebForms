<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductHighlightsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.ShoppingCart.ProductHighlightsComponent" %>

<asp:Repeater ID="HighlightsRepeater" runat="server">
    <ItemTemplate>
        <span><%#Eval("Highlight") %></span>
    </ItemTemplate>
</asp:Repeater>
<div class="Clear"></div>