<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategorySettingsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.CategorySettingsComponent" %>
<div class="boxtitle">
    <span class="ico color gear"></span>
    <asp:Literal runat="server" ID="BoxTitleLiteral"></asp:Literal>
</div>
<OWD:CheckBoxAdv runat="server" ID="EducationAUSCheckBox" LabelText="Show Education" HelpLabelText="show/hide education fields" />
<OWD:CheckBoxAdv runat="server" ID="WorkAUSCheckBox" LabelText="Show Work" HelpLabelText="show/hide work fields" />
<OWD:CheckBoxAdv runat="server" ID="AddressAUSCheckBox" LabelText="Show Address" HelpLabelText="show/hide address fields" />
<OWD:CheckBoxAdv runat="server" ID="GenderAUSCheckBox" LabelText="Show Gender" HelpLabelText="show/hide gender fields" />
<OWD:CheckBoxAdv runat="server" ID="FaxAUSCheckBox" LabelText="Show Fax" HelpLabelText="show/hide fax fields" />
<OWD:CheckBoxAdv runat="server" ID="DOBAUSCheckBox" LabelText="Show Date of Birth" HelpLabelText="show/hide date of birth fields" />
<OWD:CheckBoxAdv runat="server" ID="LandlineAUSCheckBox" LabelText="Show Landline" HelpLabelText="show/hide landline fields" />
<OWD:CheckBoxAdv runat="server" ID="MobileAUSCheckBox" LabelText="Show Mobile" HelpLabelText="show/hide mobile fields" />
<OWD:CheckBoxAdv runat="server" ID="WebsiteAUSCheckBox" LabelText="Show Website" HelpLabelText="show/hide website fields" />
<OWD:CheckBoxAdv runat="server" ID="BillingAddressAUSCheckBox" LabelText="Show Billing Address" HelpLabelText="show/hide billing address fields" />
<OWD:CheckBoxAdv runat="server" ID="DeliveryAddressAUSCheckBox" LabelText="Show Delivery Address" HelpLabelText="show/hide delivery address fields" />
<%--<div class="grid1-3">
    <div class="boxtitle">
        <span class="ico color gear"></span>Fields To Show While Managing User Account
    </div>
    <OWD:CheckBoxAdv runat="server" ID="EducationMUSCheckBox" LabelText="Show Education"
        HelpLabelText="show/hide education fields" />
    <OWD:CheckBoxAdv runat="server" ID="WorkMUSCheckBox" LabelText="Show Work"
        HelpLabelText="show/hide work fields" />
    <OWD:CheckBoxAdv runat="server" ID="AddressMUSCheckBox" LabelText="Show Address"
        HelpLabelText="show/hide address fields" />
    <OWD:CheckBoxAdv runat="server" ID="GenderMUSCheckBox" LabelText="Show Gender"
        HelpLabelText="show/hide gender fields" />
    <OWD:CheckBoxAdv runat="server" ID="FaxMUSCheckBox" LabelText="Show Fax" HelpLabelText="show/hide fax fields" />
    <OWD:CheckBoxAdv runat="server" ID="DOBMUSCheckBox" LabelText="Show Date of Birth"
        HelpLabelText="show/hide date of birth fields" />
    <OWD:CheckBoxAdv runat="server" ID="LandlineMUSCheckBox" LabelText="Show Landline"
        HelpLabelText="show/hide landline fields" />
    <OWD:CheckBoxAdv runat="server" ID="MobileMUSCheckBox" LabelText="Show Mobile"
        HelpLabelText="show/hide mobile fields" />
    <OWD:CheckBoxAdv runat="server" ID="WebsiteMUSCheckBox" LabelText="Show Website"
        HelpLabelText="show/hide website fields" />
    <OWD:CheckBoxAdv runat="server" ID="BillingAddressMUSCheckBox" LabelText="Show Billing Address"
        HelpLabelText="show/hide billing address fields" />
    <OWD:CheckBoxAdv runat="server" ID="DeliveryAddressMUSCheckBox" LabelText="Show Delivery Address"
        HelpLabelText="show/hide delivery address fields" />
</div>
<div class="grid1-3">
    <div class="boxtitle">
        <span class="ico color gear"></span>Fields To Show While Listing User Accounts
    </div>
</div>
<div class="Clear"></div>--%>