<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserWorkComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.UserWorkComponent" %>

<OWD:TextBoxAdv ID="OrganizationTextBox" runat="server" MaxLength="50" AutoCompleteType="Company" />
<OWD:TextBoxAdv ID="PositionTextBox" runat="server" MaxLength="50" AutoCompleteType="JobTitle" />
<OWD:TextBoxAdv ID="CityTextBox" runat="server" MaxLength="50" AutoCompleteType="BusinessCity" />
<OWD:Countries runat="server" ID="CountryDropDownList" />
<OWD:TextBoxAdv ID="DescriptionTextBox" runat="server" MaxLength="200" />
<OWD:TextBoxAdv ID="StartDateTextBox" runat="server" CalendarDefaultView="Years" MaxLength="20" />
<div id="EndDateDiv">
    <OWD:TextBoxAdv ID="EndDateTextBox" runat="server" CalendarDefaultView="Years" MaxLength="20" />
</div>
<OWD:CheckBoxAdv ID="WorkHereCheckBox" runat="server" OnClick="CheckedChanged(this)" />
<script type="text/javascript">
    function CheckedChanged(element) {
        if (element.checked) {
            document.getElementById("EndDateDiv").style.display = "none";
        }
        else {
            document.getElementById("EndDateDiv").style.display = "block";
        }
    }
</script>