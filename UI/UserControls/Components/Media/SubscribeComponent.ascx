<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubscribeComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Media.SubscribeComponent" %>
<div class="SubscribeForm" runat="server" id="SubscribeForm">
    <asp:TextBox runat="server" ID="NameTextBox" MaxLength="50" ValidationGroup="UserSubscribeValidationGroup" AutoCompleteType="DisplayName" />
    <asp:RequiredFieldValidator ID="NameTetBoxRequiredFieldValidator" runat="server" Display="Static" SetFocusOnError="true" CssClass="Error" ControlToValidate="NameTextBox"
        ValidationGroup="UserSubscribeValidationGroup" />
    <asp:TextBox runat="server" ID="EmailTextBox" MaxLength="150" ValidationGroup="UserSubscribeValidationGroup" AutoCompleteType="Email" />
    <asp:RequiredFieldValidator ID="EmailTextBoxRequiredFieldValidator" runat="server" Display="Static" SetFocusOnError="true" CssClass="Error" ControlToValidate="EmailTextBox"
        ValidationGroup="UserSubscribeValidationGroup" />
    <asp:TextBox runat="server" ID="MobileTextBox" MaxLength="20" ValidationGroup="UserSubscribeValidationGroup" AutoCompleteType="Cellular" />
    <asp:RequiredFieldValidator ID="MobileTextBoxRequiredFieldValidator" runat="server" Display="Static" SetFocusOnError="true" CssClass="Error" ControlToValidate="MobileTextBox"
        ValidationGroup="UserSubscribeValidationGroup" />
    <asp:Button runat="server" ID="Subscribe" CssClass="btn" ValidationGroup="UserSubscribeValidationGroup" OnClick="Subscribe_Click" />
    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
</div>