<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductIconsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.ShoppingCart.ProductIconsComponent" %>

<div class="ProductIcons">
    <asp:Repeater runat="server" ID="IconsRepeater">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <asp:Image runat="server" CssClass="tooltip" title='<%# string.Format("{0}<br />{1}", Eval("SC_Icons.Title") , Eval("SC_Icons.Description")) %>' AlternateText='<%#Eval("SC_Icons.AlternateText") %>'
                    ImageUrl='<%#Eval("SC_Icons.IconURL") %>' />
            </li>
        </ItemTemplate>
        <FooterTemplate>
            <div class="Clear"></div>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
    <div class="Clear"></div>
</div>