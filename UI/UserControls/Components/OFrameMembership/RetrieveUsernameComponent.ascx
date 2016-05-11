<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RetrieveUsernameComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.RetrieveUsernameComponent" %>
<div class="onecolumn">
    <div class="header">
        <span><span class="ico color id "></span>Username Retrieval</span>
    </div>
    <div class="Clear">
    </div>
    <div class="content">
        <div class="boxtitle">
            <span class="ico color mail "></span>retrieve your username via email
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <OWD:TextBoxAdv runat="server" ID="EmailIDTextBox" LabelText="Email ID" SmallLabelText="primary email id" MaxLength="100" RequiredErrorMessage="enter the email id"
                    ValidationGroup="RetrieveUsernameComponentValidationGroup" AutoCompleteType="Email" />
                <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCustom="true" CustomButtonText="Get Username" ValidationGroup="RetrieveUsernameComponentValidationGroup" />
                <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>