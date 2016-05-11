<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAddressComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.UserAddressComponent" %>

<OWD:TextBoxAdv runat="server" ID="StreetTextBox" AutoCompleteType="HomeStreetAddress" />
<OWD:TextBoxAdv runat="server" ID="CityTextBox" AutoCompleteType="HomeCity" />
<OWD:TextBoxAdv runat="server" ID="ZipCodeTextBox" AutoCompleteType="HomeZipCode" />
<OWD:TextBoxAdv runat="server" ID="StateTextBox" AutoCompleteType="HomeState" />
<OWD:Countries runat="server" ID="CountryDropDownList" />