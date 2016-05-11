<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAddressSelectionComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.UserAddressSelectionComponent" %>

<div class="AddressHoverSelection" runat="server" id="Component">
    <asp:Literal runat="server" ID="TitleLiteral"></asp:Literal>
    <div class="DisplayDiv">
        <ul class="Addresses">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <li>
                        <%# Eval("StreetName") %><br />
                        <%# Eval("City") %><br />
                        <%# Eval("ZipCode") %><br />
                        <%# Eval("State") %><br />
                        <%# Eval("Country") %><br />
                        <asp:Button runat="server" ID="AddressSelectButton" CssClass="AddressSelectButton" Text="Select" CausesValidation="false" CommandArgument='<%#Eval("AddressID") %>'
                            OnCommand="AddressSelectButton_Command" />
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div class="Clear"></div>
    </div>
</div>