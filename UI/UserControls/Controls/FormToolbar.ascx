<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormToolbar.ascx.cs" Inherits="OWDARO.UI.UserControls.Controls.FormToolbar" %>

<asp:Panel CssClass="section" runat="server" ID="ToolbarPanel">
    <div runat="server" id="DIV">
        <asp:Button runat="server" ID="SaveButton" Text="Save" CssClass="uibutton" OnClick="SaveButton_Click" Visible="false" />
        <asp:Button runat="server" ID="SaveGoBackButton" Text="Save & Go Back" CssClass="uibutton" OnClick="SaveGoBackButton_Click" Visible="false" />
        <asp:Button runat="server" ID="EditButton" Text="Edit" CssClass="uibutton normal" CausesValidation="False" OnClick="EditButton_Click" Visible="false" />
        <asp:Button runat="server" ID="UpdateButton" Text="Update" CssClass="uibutton" OnClick="UpdateButton_Click" Visible="false" />
        <asp:Button runat="server" ID="CustomButton" Text="Custom" CssClass="uibutton" OnClick="CustomButton_Click" Visible="false" />
        <asp:Button runat="server" ID="CustomPopupButton" Text="Custom With Popup" CssClass="uibutton" OnClick="CustomPopupButton_Click" Visible="false" />
        <asp:Button runat="server" ID="CancelButton" Text="Cancel" CssClass="uibutton confirm" CausesValidation="False" OnClick="CancelButton_Click" UseSubmitBehavior="False"
            Visible="false" />
        <asp:Button runat="server" ID="DeleteButton" Text="Delete" CssClass="uibutton special" CausesValidation="False" OnClick="DeleteButton_Click" Visible="false" />
        <asp:Button runat="server" ID="MessageButton" Visible="false" />
    </div>
</asp:Panel>
<asp:Panel runat="server" CssClass="section" ID="FalsePanel" Height="30px" Style="text-align: center; line-height: 30px; background: #333333; color: white; font-family: verdana;
    margin: 10px 0px 10px 0px;">
    FormToolbar
</asp:Panel>
<asp:Panel runat="server" ID="PopupsPanel" Visible="false">
    <asp:Panel runat="server" ID="DeletePopupPanel" Visible="false" CssClass="ModalPopup">
        <asp:Label ID="DeletePopupTitleLabel" runat="server" Text="Delete?" CssClass="PopupTitle"></asp:Label>
        <asp:Label ID="DeletePopupMessageLabel" runat="server" Text="Are You Sure, You Want To Delete?"
            CssClass="PopupMessage"></asp:Label>
        <p>
            <asp:Button runat="server" CssClass="uibutton special" ID="DeletePopupOKButton" Text="Yes" CausesValidation="false" />
            <asp:Button runat="server" CssClass="uibutton confirm" ID="DeletePopupCancelButton" Text="No" />
        </p>
    </asp:Panel>
    <asp:Panel runat="server" ID="CustomPopupPanel" Visible="false" CssClass="ModalPopup">
        <asp:Label ID="CustomPopupTitleLabel" runat="server" CssClass="PopupTitle"></asp:Label>
        <asp:Label CssClass="PopupMessage" runat="server" ID="CustomPopupMessageLabel"></asp:Label>
        <p>
            <asp:Button runat="server" CssClass="uibutton" ID="CustomPopupOKButton" />
            <asp:Button runat="server" CssClass="uibutton confirm" ID="CustomPopupCancelButton" />
        </p>
    </asp:Panel>
    <asp:Panel runat="server" ID="MessagePopupPanel" Visible="false" CssClass="ModalPopup">
        <asp:Label ID="MessagePopupTitleLabel" runat="server" Text="Status" CssClass="PopupTitle"></asp:Label>
        <asp:Label CssClass="PopupMessage" runat="server" ID="MessagePopupMessageLabel"></asp:Label>
        <p>
            <asp:Button runat="server" CssClass="uibutton" ID="MessagePopupSubmitButton" Style="display: none;" />
            <asp:Button runat="server" CssClass="uibutton confirm" ID="MessagePopupDismissButton"
                Text="OK" />
        </p>
    </asp:Panel>
    <asp:ConfirmButtonExtender ID="DeleteButton_ConfirmButtonExtender" runat="server"
        Enabled="True" TargetControlID="DeleteButton" DisplayModalPopupID="DeleteButton_ModalPopupExtender">
    </asp:ConfirmButtonExtender>
    <asp:ConfirmButtonExtender ID="CustomPopupButton_ConfirmButtonExtender" runat="server"
        Enabled="True" TargetControlID="CustomPopupButton" DisplayModalPopupID="CustomPopupButton_ModalPopupExtender">
    </asp:ConfirmButtonExtender>
    <asp:ConfirmButtonExtender ID="MessageButton_ConfirmButtonExtender" runat="server"
        Enabled="True" TargetControlID="MessageButton" DisplayModalPopupID="MessageButton_ModalPopupExtender">
    </asp:ConfirmButtonExtender>
    <asp:ModalPopupExtender ID="DeleteButton_ModalPopupExtender" runat="server" TargetControlID="DeleteButton"
        PopupControlID="DeletePopupPanel" OkControlID="DeletePopupOKButton" CancelControlID="DeletePopupCancelButton"
        BackgroundCssClass="ModalBackground" RepositionMode="RepositionOnWindowResizeAndScroll" />
    <asp:ModalPopupExtender ID="CustomPopupButton_ModalPopupExtender" runat="server"
        TargetControlID="CustomPopupButton" PopupControlID="CustomPopupPanel" OkControlID="CustomPopupOKButton"
        CancelControlID="CustomPopupCancelButton" BackgroundCssClass="ModalBackground"
        RepositionMode="RepositionOnWindowResizeAndScroll" />
    <asp:ModalPopupExtender ID="MessageButton_ModalPopupExtender" runat="server" TargetControlID="MessageButton"
        PopupControlID="MessagePopupPanel" OkControlID="MessagePopupSubmitButton" CancelControlID="MessagePopupDismissButton"
        BackgroundCssClass="ModalBackground" RepositionMode="RepositionOnWindowResizeAndScroll" />
    <asp:AnimationExtender ID="CustomPopupPanelAnimationExtender" runat="server" Enabled="True"
        TargetControlID="CustomPopupButton">
        <Animations>
    <OnClick>
        <Sequence AnimationTarget="CustomPopupPanel">
            <FadeIn Duration="0.3" MinimumOpacity="0" MaximumOpacity="1" Fps="30" />
        </Sequence>
    </OnClick>
        </Animations>
    </asp:AnimationExtender>
    <asp:AnimationExtender ID="DeletePopupPanelAnimationExtender" runat="server" Enabled="True"
        TargetControlID="DeleteButton">
        <Animations>
    <OnClick>
        <Sequence AnimationTarget="DeletePopupPanel">
            <FadeIn Duration="0.3" MinimumOpacity="0" MaximumOpacity="1" Fps="30" />
        </Sequence>
    </OnClick>
        </Animations>
    </asp:AnimationExtender>
    <asp:AnimationExtender ID="MessagePopupAnimationExtender" runat="server" Enabled="True"
        TargetControlID="MessageButton">
        <Animations>
    <OnClick>
        <Sequence AnimationTarget="MessagePopupPanel">
            <FadeIn Duration="0.3" MinimumOpacity="0" MaximumOpacity="1" Fps="30" />
        </Sequence>
    </OnClick>
        </Animations>
    </asp:AnimationExtender>
</asp:Panel>