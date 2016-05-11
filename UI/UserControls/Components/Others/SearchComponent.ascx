<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Others.SearchComponent" %>

<h1 class="PageTitle">Search Results For
    <span>&nbsp;"<asp:Literal runat="server" ID="TitleLiteral" />"
    </span>
</h1>
<div class="SearchBox">
    <asp:TextBox runat="server" ID="SearchTermTextBox" placeholder="Search..." CssClass="SearchTermField" AutoCompleteType="Search" ValidationGroup="GlobalSearchValidationGroup"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="SearchTermTextBox" SetFocusOnError="true" ValidationGroup="GlobalSearchValidationGroup"></asp:RequiredFieldValidator>
    <asp:Button runat="server" ID="SearchButton" CssClass="SearchButton" OnClick="SearchButton_Click" ValidationGroup="GlobalSearchValidationGroup" />
</div>
<ul class="SearchResults">
    <asp:Literal runat="server" ID="SearchLiteral" />
</ul>
<asp:Button runat="server" ID="LoadMoreButton" Text="Load More" OnClick="LoadMoreButton_Click" CssClass="LoadMoreButton" />
<asp:HiddenField runat="server" ID="CurrentCountHiddenField" Value="0" />