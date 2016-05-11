<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserProfileComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.UserProfileComponent" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <OWD:TextBoxAdv runat="server" ID="NameTextBox" RequiredErrorMessage="name is required" LabelText="Name" SmallLabelText="full name of the user" ValidationGroup="UserProfileValidationGroup"
            AutoCompleteType="DisplayName" />
        <asp:Panel runat="server" ID="RolesPanel" Visible="false">
            <OWD:DropDownListAdv ID="RolesDropDownList" runat="server" LabelText="Role" SmallLabelText="role for site access" RequiredErrorMessage="please select a role for site access"
                InitialValue="-1" ValidationGroup="UserProfileValidationGroup" />
        </asp:Panel>
        <asp:Panel runat="server" ID="CategoryPanel" Visible="false">
            <OWD:DropDownListAdv runat="server" ID="CategoryDropDownList" AutoPostBack="true" LabelText="Category" SmallLabelText="category of the user" RequiredFieldErrorMessage="please select a category"
                ValidationGroup="UserProfileValidationGroup" />
        </asp:Panel>
        <asp:Panel runat="server" ID="GenderPanel" Visible="false">
            <OWD:DropDownListAdv runat="server" ID="GenderDropDownList" LabelText="Gender" SmallLabelText="the sex of the user" RequiredErrorMessage="please select a gender"
                ValidationGroup="UserProfileValidationGroup" />
        </asp:Panel>
        <asp:Panel runat="server" ID="DateOfBirthPanel" Visible="false">
            <OWD:TextBoxAdv runat="server" ID="DateOfBirthTextBox" LabelText="Date Of Birth" CalendarDefaultView="Years" RequiredErrorMessage="please enter your date of birth"
                ValidationGroup="UserProfileValidationGroup" />
        </asp:Panel>
        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ValidationGroup="UserProfileValidationGroup" />
        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
    </ContentTemplate>
</asp:UpdatePanel>