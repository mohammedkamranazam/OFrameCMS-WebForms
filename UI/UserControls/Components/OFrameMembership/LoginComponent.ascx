<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.LoginComponent" %>
<div class="onecolumn">
    <div class="header">
        <span><span class="ico color login"></span>
            <asp:Literal runat="server" ID="HeaderTitleLiteral" Text="User Login"></asp:Literal></span>
    </div>
    <div class="Clear">
    </div>
    <div class="content">
        <div class="boxtitle">
            <span class="ico color key"></span>
            <asp:Literal runat="server" ID="BoxTitleLiteral" Text="Login Into The Account From Here"></asp:Literal>
        </div>
        <OWD:TextBoxAdv runat="server" ID="UsernameTextBox" LabelText="Username" MaxLength="50" ValidationGroup="LoginForm" ValidChars="1234567890-._abcdefghijklmnopqrstuvwxyz"
            RequiredErrorMessage="enter the username" />
        <OWD:TextBoxAdv runat="server" ID="PasswordTextBox" LabelText="Password" MaxLength="50" TextMode="Password" ValidationGroup="LoginForm" RequiredErrorMessage="enter the password" />
        <OWD:CheckBoxAdv runat="server" ID="RememberMeCheckBox" LabelText="Remember Me..." />
        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCustom="true" CustomButtonText="Login" ValidationGroup="LoginForm" />
        <OWD:StatusMessageJQuery runat="server" ID="StatusMessageLabel" />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="ForgotPasswordLink" Text="Forgot Password" NavigateUrl="~/UI/Pages/LogOn/ForgotPassword.aspx"></asp:HyperLink>
    </div>
    <div class="content" runat="server" id="OAuthComponentDIV">
        <OWD:OAuthComponent runat="server"></OWD:OAuthComponent>
    </div>
</div>
<asp:HiddenField runat="server" ID="UseRedirectHiddenField" Value="True" Visible="false" />
<asp:HiddenField runat="server" ID="UseCustomRedirectHiddenField" Value="False" Visible="false" />
<asp:HiddenField runat="server" ID="CustomRedirectURLHiddenField" Visible="false" />