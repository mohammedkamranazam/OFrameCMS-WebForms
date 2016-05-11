<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginStatus.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.LoginStatus" %>

<ul runat="server" id="LoginStatusUnorderedList">
    <li>
        <asp:HyperLink ID="LoginHyperLink" runat="server" NavigateUrl="~/UI/Pages/LogOn/Default.aspx"></asp:HyperLink>
        <asp:LinkButton ID="LogoutLinkButton" runat="server" OnClick="LogoutLinkButton_Click" CausesValidation="false"></asp:LinkButton>
    </li>
    <li runat="server" id="AccountLI">
        <asp:HyperLink ID="AccountHyperLink" runat="server"></asp:HyperLink>
    </li>
    <li runat="server" id="ContactLI">
        <asp:HyperLink ID="ContactHyperLink" runat="server" NavigateUrl="~/UI/Pages/Common/ChangeContact.aspx"></asp:HyperLink>
    </li>
    <li runat="server" id="ProfileLI">
        <asp:HyperLink ID="ProfileHyperLink" runat="server" NavigateUrl="~/UI/Pages/Common/ChangeProfile.aspx"></asp:HyperLink>
    </li>
    <li runat="server" id="PasswordLI">
        <asp:HyperLink ID="PasswordHyperLink" runat="server" NavigateUrl="~/UI/Pages/Common/ChangePassword.aspx"></asp:HyperLink>
    </li>
    <li runat="server" id="SecQALI">
        <asp:HyperLink ID="QAHyperLink" runat="server" NavigateUrl="~/UI/Pages/Common/ChangeSecQA.aspx"></asp:HyperLink>
    </li>
</ul>