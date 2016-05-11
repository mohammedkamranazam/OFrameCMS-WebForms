<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocalizationSwitchComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Others.Nested.LocalizationSwitchComponent" %>

<asp:DropDownList runat="server" ID="LanguagesDropDownList" AutoPostBack="true" ValidationGroup="LanguageChangeValidationGroup" CssClass="LocaleDropDownList" OnSelectedIndexChanged="LanguagesDropDownList_SelectedIndexChanged1">
</asp:DropDownList>