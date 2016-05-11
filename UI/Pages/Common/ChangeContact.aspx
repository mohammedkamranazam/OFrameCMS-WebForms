<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="ChangeContact.aspx.cs" Inherits="OWDARO.UI.Pages.Common.ChangeContact" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color satellite"></span>Contact Details</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <OWD:TextBoxAdv runat="server" ID="EmailTextBox" LabelText="Primary Email" SmallLabelText="email id for all major account operations"
                        MaxLength="100" RequiredErrorMessage="enter the email id" />
                    <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCancel="true" ShowSave="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Panel runat="server" ID="EmailPanel">
        <OWD:UserDataDetailsComponent runat="server" ID="EmailsUserDataDetailsComponent"
            HeaderIcon="mail" BoxIcon="mail" DataTextBoxMaxLength="200" />
    </asp:Panel>
    <asp:Panel runat="server" ID="MobilePanel">
        <OWD:UserDataDetailsComponent runat="server" ID="MobileUserDataDetailsComponent"
            HeaderIcon="phone" BoxIcon="phone" DataTextBoxMaxLength="20" />
    </asp:Panel>
    <asp:Panel runat="server" ID="LandlinePanel">
        <OWD:UserDataDetailsComponent runat="server" ID="LandlineUserDataDetailsComponent"
            HeaderIcon="phone" BoxIcon="phone" DataTextBoxMaxLength="20" />
    </asp:Panel>
    <asp:Panel runat="server" ID="FaxPanel">
        <OWD:UserDataDetailsComponent runat="server" ID="FaxUserDataDetailsComponent" HeaderIcon="print"
            BoxIcon="print" DataTextBoxMaxLength="20" />
    </asp:Panel>
    <asp:Panel runat="server" ID="WebsitePanel">
        <OWD:UserDataDetailsComponent runat="server" ID="WebsiteUserDataDetailsComponent"
            HeaderIcon="link" BoxIcon="link" DataTextBoxMaxLength="150" />
    </asp:Panel>
    <asp:Panel runat="server" ID="WorkPanel">
        <OWD:UserWorkDetailsComponent runat="server" ID="WorkDetailsComponent" />
    </asp:Panel>
    <asp:Panel runat="server" ID="AddressPanel">
        <OWD:UserAddressDetailsComponent runat="server" ID="AddressDetailsComponent" />
    </asp:Panel>
    <asp:Panel runat="server" ID="EducationPanel">
        <OWD:UserEducationDetailsComponent runat="server" ID="EducationDetailsComponent" />
    </asp:Panel>
</asp:content>