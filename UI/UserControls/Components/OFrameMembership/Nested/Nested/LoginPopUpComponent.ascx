<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginPopUpComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.LoginPopUpComponent" %>

<asp:LinkButton ID="ShowLoginPopUpButton" runat="server" CssClass="ShowLoginPopUpButton" Text="Login"></asp:LinkButton>

<asp:Panel runat="server" ID="LoginPopUpPanel" CssClass="LoginPopUp" ScrollBars="None">
    <div>
        <asp:Button runat="server" ID="ClosePopUpButton" Text="&#9747;" CssClass="CloseButton" />
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <OWD:LoginComponent runat="server" ID="LoginComponent1" ValidationGroup="LoginPopUpComponentLoginForm" UseRedirect="true" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>

<asp:ConfirmButtonExtender ID="ShowLoginPopUpButton_ConfirmButtonExtender" runat="server" Enabled="True" TargetControlID="ShowLoginPopUpButton"
    DisplayModalPopupID="ShowLoginPopUpButton_ModalPopupExtender" />

<asp:ModalPopupExtender ID="ShowLoginPopUpButton_ModalPopupExtender" runat="server" TargetControlID="ShowLoginPopUpButton"
    PopupControlID="LoginPopUpPanel" CancelControlID="ClosePopUpButton" RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="ModalBackground">
</asp:ModalPopupExtender>

<asp:AnimationExtender ID="LoginPopUpPanelAnimationExtender" runat="server" Enabled="True"
    TargetControlID="ShowLoginPopUpButton">
    <Animations>
        <OnClick>
            <Sequence AnimationTarget="LoginPopUpPanel">
                <FadeIn Duration="0.3" MinimumOpacity="0" MaximumOpacity="1" Fps="20" />
            </Sequence>
        </OnClick>
    </Animations>
</asp:AnimationExtender>

<asp:HiddenField runat="server" ID="UseRedirectHiddenField" Value="True" Visible="false" />
<asp:HiddenField runat="server" ID="UseCustomRedirectHiddenField" Value="False" Visible="false" />
<asp:HiddenField runat="server" ID="CustomRedirectURLHiddenField" Visible="false" />