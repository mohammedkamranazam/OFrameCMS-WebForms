<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PasswordConfirmationComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.PasswordConfirmationComponent" %>
<div class="section">
    <label>
        New Password<small>strongly typed password for securing your account</small>
    </label>
    <div>
        <asp:TextBox ID="NewPasswordTextBox" CssClass="full required" runat="server" MaxLength="50" TextMode="Password" />
        <span class="f_help">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="enter new password" ControlToValidate="NewPasswordTextBox" Display="Dynamic"
                SetFocusOnError="True" />
            <asp:RegularExpressionValidator ID="PasswordStrengthRegularExpressionValidator" runat="server" ControlToValidate="NewPasswordTextBox" Display="Dynamic" SetFocusOnError="True" />
            <asp:PasswordStrength ID="NewPasswordTextBox_PasswordStrength" runat="server" DisplayPosition="LeftSide"
                TargetControlID="NewPasswordTextBox" StrengthIndicatorType="BarIndicator" BarIndicatorCssClass="bar"
                BarBorderCssClass="barborder" MinimumLowerCaseCharacters="1" MinimumNumericCharacters="1"
                MinimumSymbolCharacters="1" MinimumUpperCaseCharacters="1" PreferredPasswordLength="15"
                RequiresUpperAndLowerCaseCharacters="true" StrengthStyles="style1;style2;style3;style4"
                CalculationWeightings="50;15;15;20">
            </asp:PasswordStrength>
        </span>
    </div>
    <div style="clear: both; height: 0px; padding: 0px; margin: 0px;">
    </div>
</div>
<div class="section">
    <label>
        Confirm password<small>just to confirm your above entered password</small>
    </label>
    <div>
        <asp:TextBox ID="ConfirmPasswordTextBox" CssClass="full required" runat="server" MaxLength="50" TextMode="Password" />
        <span class="f_help">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="confirm new password" ControlToValidate="ConfirmPasswordTextBox" Display="Dynamic"
                SetFocusOnError="True" />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="passwords do not match" ControlToCompare="NewPasswordTextBox" ControlToValidate="ConfirmPasswordTextBox"
                Display="Dynamic" SetFocusOnError="True" />
        </span>
    </div>
    <div style="clear: both; height: 0px; padding: 0px; margin: 0px;">
    </div>
</div>