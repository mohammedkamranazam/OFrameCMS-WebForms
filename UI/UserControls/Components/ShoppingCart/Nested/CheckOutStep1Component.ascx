<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckOutStep1Component.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.ShoppingCart.CheckOutStep1Component" %>

<div class="onecolumn" id="CheckOutStep1Component">
    <div class="header">
        <span><span class="ico color money_bag"></span>
            <asp:Literal runat="server" ID="HeaderTitleLiteral" Text="Checkout"></asp:Literal></span>
    </div>
    <div class="Clear">
    </div>
    <div class="content">
        <div class="boxtitle">
            <span class="ico color sphere"></span>
            <asp:Literal runat="server" ID="BoxTitleLiteral" Text="You can buy your products after checking out from here"></asp:Literal>
        </div>
        <div class="grid2">
            <asp:Panel runat="server" ID="LoginComponentPanel">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <OWD:LoginComponent runat="server" ID="LoginComponent" UseRedirect="true"></OWD:LoginComponent>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <div class="grid2">
            <div class="onecolumn">
                <div class="header">
                    <span><span class="ico color help2"></span>
                        <asp:Literal runat="server" ID="Literal1" Text="Guest User"></asp:Literal></span>
                </div>
                <div class="Clear">
                </div>
                <div class="content">
                    <div class="boxtitle">
                        <span class="ico color mail"></span>
                        <asp:Literal runat="server" ID="Literal2" Text="Buy As A Guest User"></asp:Literal>
                    </div>
                    <OWD:TextBoxAdv runat="server" ID="GuestEmailIDTextBox" LabelText="Guest Email ID" SmallLabelText="email id which will be used to confirm orders"
                        MaxLength="150" RequiredErrorMessage="please enter your email id" AutoCompleteType="Email" />
                    <OWD:FormToolbar runat="server" ID="GuestBuyToolbar" ShowCustom="true" CustomButtonText="Continue" />
                </div>
            </div>
        </div>
        <div class="Clear"></div>
    </div>
</div>