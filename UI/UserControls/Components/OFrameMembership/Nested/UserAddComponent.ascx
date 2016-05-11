<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAddComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.UserAddComponent" %>

<div class="onecolumn">
    <div class="header">
        <span><span class="ico color user_woman "></span>
            <asp:Literal runat="server" ID="HeaderTitleLiteral" Text="New User" /></span>
    </div>
    <div class="Clear">
    </div>
    <div class="content">
        <div class="boxtitle">
            <span class="ico color id "></span>
            <asp:Literal runat="server" ID="BoxTitleLiteral" Text="New User Registration Form" />
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" ID="RegistrationForm">
                    <asp:Panel runat="server" ID="RolesPanel" Visible="false">
                        <OWD:DropDownListAdv runat="server" ID="RolesDropDownList" LabelText="Role" SmallLabelText="select a role for new user" RequiredFieldErrorMessage="select a role"
                            InitialValue="-1" ValidationGroup="UserAddValidationGroup" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="CategoryPanel" Visible="false">
                        <OWD:DropDownListAdv runat="server" ID="CategoryDropDownList" AutoPostBack="true" LabelText="Category" SmallLabelText="select a category for new user" RequiredFieldErrorMessage="select a category"
                            ValidationGroup="UserAddValidationGroup" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="NamePanel">
                        <OWD:TextBoxAdv runat="server" ID="NameTextBox" LabelText="Name" SmallLabelText="name of the new user" MaxLength="100" RequiredErrorMessage="name is required" ValidationGroup="UserAddValidationGroup"
                            AutoCompleteType="DisplayName" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="GenderPanel" Visible="false">
                        <OWD:DropDownListAdv runat="server" ID="GenderDropDown" LabelText="Gender" SmallLabelText="select a gender for new user" RequiredErrorMessage="please select a gender"
                            ValidationGroup="UserAddValidationGroup" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="DateOfBirthPanel" Visible="false">
                        <OWD:TextBoxAdv runat="server" ID="DateOfBirthTextBox" LabelText="Date Of Birth" CalendarDefaultView="Years" MaxLength="20" RequiredErrorMessage="date of birth is required"
                            ValidationGroup="UserAddValidationGroup" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="AddressPanel" Visible="false">
                        <OWD:UserAddressComponent runat="server" ID="HomeAddressComponent" StreetLabelText="Home Street Name"
                            StreetSmallLabelText="complete street address" StreetMaxLength="200" StreetRequiredErrorMessage="street name is required"
                            CityLabelText="Home City" CitySmallLabelText="city of residence" CityMaxLength="50"
                            CityRequiredErrorMessage="city name is required" ZipCodeLabelText="Zip Code"
                            ZipCodeSmallLabelText="zipcode or pincode of the city" ZipCodeMaxLength="10"
                            ZipCodeRequiredErrorMessage="zip code is required" StateLabelText="Home State"
                            StateSmallLabelText="state or province name" StateMaxLength="50" StateRequiredErrorMessage="state is required"
                            CountryLabelText="Home Country" CountrySmallLabelText="country of residence" ValidationGroup="UserAddValidationGroup" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="BillingAddressPanel" Visible="false">
                        <OWD:UserAddressComponent runat="server" ID="BillingAddressComponent" StreetLabelText="Billing Street Name"
                            StreetSmallLabelText="complete street address" StreetMaxLength="200" StreetRequiredErrorMessage="street name is required"
                            CityLabelText="Billing City" CitySmallLabelText="city of residence" CityMaxLength="50"
                            CityRequiredErrorMessage="city name is required" ZipCodeLabelText="Zip Code"
                            ZipCodeSmallLabelText="zipcode or pincode of the city" ZipCodeMaxLength="10"
                            ZipCodeRequiredErrorMessage="zip code is required" StateLabelText="Billing State"
                            StateSmallLabelText="state or province name" StateMaxLength="50" StateRequiredErrorMessage="state is required"
                            CountryLabelText="Billing Country" CountrySmallLabelText="country of residence" ValidationGroup="UserAddValidationGroup" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="DeliveryAddressPanel" Visible="false">
                        <OWD:UserAddressComponent runat="server" ID="DeliveryAddressComponent" StreetLabelText="Delivery Street Name"
                            StreetSmallLabelText="complete street address" StreetMaxLength="200" StreetRequiredErrorMessage="street name is required"
                            CityLabelText="Delivery City" CitySmallLabelText="city of residence" CityMaxLength="50"
                            CityRequiredErrorMessage="city name is required" ZipCodeLabelText="Zip Code"
                            ZipCodeSmallLabelText="zipcode or pincode of the city" ZipCodeMaxLength="10"
                            ZipCodeRequiredErrorMessage="zip code is required" StateLabelText="Delivery State"
                            StateSmallLabelText="state or province name" StateMaxLength="50" StateRequiredErrorMessage="state is required"
                            CountryLabelText="Delivery Country" CountrySmallLabelText="country of residence" ValidationGroup="UserAddValidationGroup" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="EmailPanel">
                        <OWD:TextBoxAdv runat="server" ID="EmailIDTextBox" LabelText="Email" SmallLabelText="primary email id" MaxLength="100" RequiredErrorMessage="email id is required"
                            ValidationErrorMessage="invalid email id" ValidationGroup="UserAddValidationGroup" AutoCompleteType="Email" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="MobilePanel" Visible="false">
                        <OWD:TextBoxAdv runat="server" ID="MobileTextBox" LabelText="Mobile" SmallLabelText="mobile number" MaxLength="15" RequiredErrorMessage="mobile number is required"
                            ValidationGroup="UserAddValidationGroup" AutoCompleteType="Cellular" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="LandlinePanel" Visible="false">
                        <OWD:TextBoxAdv runat="server" ID="LandlineTextBox" LabelText="Landline" SmallLabelText="landline number" MaxLength="15" RequiredErrorMessage="landline number is required"
                            ValidationGroup="UserAddValidationGroup" AutoCompleteType="HomePhone" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="FaxPanel" Visible="false">
                        <OWD:TextBoxAdv runat="server" ID="FaxTextBox" LabelText="Fax" SmallLabelText="fax number" MaxLength="15" RequiredErrorMessage="fax number is required" ValidationGroup="UserAddValidationGroup"
                            AutoCompleteType="HomeFax" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="WebsitePanel" Visible="false">
                        <OWD:TextBoxAdv runat="server" ID="WebsiteTextBox" LabelText="Website" SmallLabelText="website address" MaxLength="100" RequiredErrorMessage="website address is required"
                            ValidationGroup="UserAddValidationGroup" AutoCompleteType="BusinessUrl" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="WorkPanel">
                        <OWD:TextBoxAdv ID="WorkOrganizationTextBox" runat="server" LabelText="Company" SmallLabelText="company or organization where you work" MaxLength="50" RequiredErrorMessage="company name is required"
                            ValidationGroup="UserAddValidationGroup" AutoCompleteType="Company" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="EducationPanel">
                        <OWD:TextBoxAdv runat="server" ID="EducationInstituteTextBox" LabelText="Institute" SmallLabelText="" MaxLength="100" RequiredErrorMessage="institute name is required"
                            ValidationGroup="UserAddValidationGroup" />
                        <OWD:TextBoxAdv runat="server" ID="EducationStreamTextBox" LabelText="Stream" SmallLabelText="" MaxLength="50" RequiredErrorMessage="stream is required" ValidationGroup="UserAddValidationGroup" />
                        <OWD:TextBoxAdv runat="server" ID="EducationStartDateTextBox" LabelText="Start Date" SmallLabelText="" MaxLength="20" ValidationGroup="UserAddValidationGroup" />
                        <OWD:TextBoxAdv runat="server" ID="EducationEndDateTextBox" LabelText="End Date" SmallLabelText="" MaxLength="20" ValidationGroup="UserAddValidationGroup" />
                        <OWD:DropDownListAdv runat="server" ID="EducationQualificationTypeDropDownList" LabelText="Qualification Type" SmallLabelText="add your different addresses based upon type"
                            RequiredFieldErrorMessage="please select the qualification type" InitialValue="-1" ValidationGroup="UserAddValidationGroup" />
                    </asp:Panel>
                    <OWD:TextBoxAdv runat="server" ID="UsernameTextBox" LabelText="Username" SmallLabelText="username required for site login" MaxLength="50" RequiredErrorMessage="username is required"
                        ValidationGroup="UserAddValidationGroup" />
                    <OWD:PasswordConfirmationComponent runat="server" ID="PasswordConfirmationComponent" ValidationGroup="UserAddValidationGroup" />
                    <asp:Panel runat="server" ID="QAPanel">
                        <OWD:TextBoxAdv runat="server" ID="SecurityQuestionTextBox" LabelText="Security Question" SmallLabelText="your own security question for password retrieval" MaxLength="200"
                            RequiredErrorMessage="security question is required" ValidationGroup="UserAddValidationGroup" />
                        <OWD:TextBoxAdv runat="server" ID="SecurityAnswerTextBox" LabelText="Security Answer" SmallLabelText="answer for your security question" MaxLength="200" RequiredErrorMessage="security answer is required"
                            ValidationGroup="UserAddValidationGroup" />
                    </asp:Panel>
                    <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCustom="true" CustomButtonText="Register" ValidationGroup="UserAddValidationGroup" />
                </asp:Panel>
                <OWD:StatusMessageJQuery runat="server" ID="StatusMessageLabel" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>