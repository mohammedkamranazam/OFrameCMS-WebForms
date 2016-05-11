<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RetrievePasswordComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.RetrievePasswordComponent" %>
<div class="onecolumn">
    <div class="header">
        <span><span class="ico color password "></span>Password Retrieval</span>
    </div>
    <div class="Clear">
    </div>
    <div class="content">
        <div class="boxtitle">
            <span class="ico color mail "></span>Retrieve password in email id
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <OWD:TextBoxAdv runat="server" ID="EmailIDTextBox" LabelText="Email ID" SmallLabelText="primary email id" MaxLength="100" RequiredErrorMessage="enter the email id"
                    ValidationGroup="RetrievePasswordComponentValidationGroup" AutoCompleteType="Email" />
                <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCustom="true" CustomButtonText="Get Password" ValidationGroup="RetrievePasswordComponentValidationGroup" />
                <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>