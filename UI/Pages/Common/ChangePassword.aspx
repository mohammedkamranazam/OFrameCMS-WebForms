<%@ Page Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="OWDARO.UI.Pages.Common.ChangePassword" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color login "></span>Password Management</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="boxtitle">
                <span class="ico color encrypt "></span>change your password from here
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <OWD:TextBoxAdv runat="server" ID="CurrentPasswordTextBox" LabelText="Current Password"
                        SmallLabelText="password currently in use" RequiredErrorMessage="enter current password"
                        MaxLength="50" TextMode="Password" />
                    <OWD:PasswordConfirmationComponent runat="server" ID="PasswordConfirmationComponent" />
                    <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCancel="true" ShowSave="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusLabel" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>