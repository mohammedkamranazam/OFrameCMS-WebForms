<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactUsQueryFormComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Others.ContactUsQueryFormComponent" %>

<OWD:TextBoxAdv ID="NameTextBox" runat="server" />
<OWD:TextBoxAdv ID="EmailTextBox" runat="server" />
<OWD:TextBoxAdv ID="PhoneTextBox" runat="server" />
<OWD:TextBoxAdv ID="CityTextBox" runat="server" />
<OWD:TextBoxAdv ID="StateTextBox" runat="server" />
<OWD:Countries ID="CountryDropDownList" runat="server" InitialValue="-1" SelectedValue="India" />
<OWD:TextBoxAdv ID="MessageTextBox" runat="server" TextMode="MultiLine" />
<OWD:FormToolbar ID="FormToolbar1" runat="server" ShowCustom="true" />
<OWD:StatusMessageJQuery ID="StatusMessage" runat="server" />