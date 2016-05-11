<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAddPopUpComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.UserAddPopUpComponent" %>

<div class="onecolumn">
    <div class="header">
        <span><span class="ico color user_woman"></span>
            New User</span>
    </div>
    <div class="Clear">
    </div>
    <div class="content">
        <div class="boxtitle">
            <span class="ico color id"></span>
            Click on the button to register yourself
        </div>
        <p>
            <asp:HyperLink runat="server" ID="UserAddHyperLink" NavigateUrl="~/UI/Pages/LogOn/Register.aspx" Text="Register Here" CssClass="UserAddPopUpButton"></asp:HyperLink>
        </p>
    </div>
</div>