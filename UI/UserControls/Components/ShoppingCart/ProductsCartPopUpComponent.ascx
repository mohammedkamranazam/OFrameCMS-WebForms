<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductsCartPopUpComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.ShoppingCart.ProductsCartPopUpComponent" %>

<asp:Button runat="server" ID="AddToCartButton" CssClass="AddToCartButton" CausesValidation="false" OnClick="AddToCartButton_Click" UseSubmitBehavior="False" />
<asp:LinkButton ID="ShowCartPopUpButton" runat="server" CssClass="ShowCartPopUpButton" TabIndex="9999"></asp:LinkButton>
<asp:Panel runat="server" ID="CartPopupPanel" CssClass="CartPopUp" ScrollBars="Auto">
    <div class="Title">
        <asp:Label runat="server" CssClass="MessageLabel" ID="MessageLabel"></asp:Label>
        <asp:Button runat="server" ID="ClosePopUpButton" CssClass="CloseButton" Text="&#9747;" TabIndex="9999" CausesValidation="false" OnClientClick="return false;" />
    </div>
    <OWD:ProductCartGridComponent runat="server" ID="CartGridComponent1" />
</asp:Panel>
<asp:ConfirmButtonExtender ID="ShowCartPopUpButton_ConfirmButtonExtender" runat="server" Enabled="True" TargetControlID="ShowCartPopUpButton" DisplayModalPopupID="ShowCartPopUpButton_ModalPopupExtender" />
<asp:ModalPopupExtender ID="ShowCartPopUpButton_ModalPopupExtender" runat="server" TargetControlID="ShowCartPopUpButton" PopupControlID="CartPopupPanel" CancelControlID="ClosePopUpButton"
    RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="ModalBackground">
</asp:ModalPopupExtender>
<asp:AnimationExtender ID="CartPopupPanelAnimationExtender" runat="server" Enabled="True"
    TargetControlID="ShowCartPopUpButton">
    <Animations>
        <OnClick>
            <Sequence AnimationTarget="CartPopupPanel">
                <FadeIn Duration="0.3" MinimumOpacity="0" MaximumOpacity="1" Fps="20" />
            </Sequence>
        </OnClick>
    </Animations>
</asp:AnimationExtender>