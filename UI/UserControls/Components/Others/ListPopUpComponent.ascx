<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListPopUpComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Others.ListPopUpComponent" %>

<asp:LinkButton ID="ComponentPopUpButton" runat="server" CssClass="ComponentPopUpButton" Text="List"></asp:LinkButton>

<asp:Panel runat="server" ID="PopUpPanel" CssClass="PopUp" ScrollBars="Auto">
    <div class="TopNav">
        <asp:Label runat="server" ID="TitleLabel" CssClass="Title" Text="List" /><asp:Button runat="server" ID="ClosePopUpButton" Text="X" CssClass="CloseButton" />
    </div>
    <asp:RadioButtonList runat="server" ID="RadioButtonList" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList_SelectedIndexChanged">
    </asp:RadioButtonList>
</asp:Panel>

<asp:ConfirmButtonExtender ID="ShowPopUpButton_ConfirmButtonExtender" runat="server" Enabled="True" TargetControlID="ComponentPopUpButton"
    DisplayModalPopupID="ShowPopUpButton_ModalPopupExtender" />

<asp:ModalPopupExtender ID="ShowPopUpButton_ModalPopupExtender" runat="server" TargetControlID="ComponentPopUpButton"
    PopupControlID="PopUpPanel" CancelControlID="ClosePopUpButton" RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="ModalBackground">
</asp:ModalPopupExtender>

<asp:AnimationExtender ID="PopUpPanelAnimationExtender" runat="server" Enabled="True"
    TargetControlID="ComponentPopUpButton">
    <Animations>
        <OnClick>
            <Sequence AnimationTarget="PopUpPanel">
                <FadeIn Duration="0.3" MinimumOpacity="0" MaximumOpacity="1" Fps="30" />
            </Sequence>
        </OnClick>
    </Animations>
</asp:AnimationExtender>