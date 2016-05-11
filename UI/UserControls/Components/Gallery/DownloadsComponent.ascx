<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DownloadsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Gallery.DownloadsComponent" %>

<h1 runat="server" id="TitleH1" class="PageTitle">
    <asp:Literal runat="server" ID="TitleLiteral"></asp:Literal>
</h1>
<div class="Toolbar">
    <span>Sort Files By:</span>
    <asp:DropDownList runat="server" ID="SortByDropDownList" AutoPostBack="true" OnSelectedIndexChanged="SortByDropDownList_SelectedIndexChanged" />
    <asp:DropDownList runat="server" ID="OrderByDropDownList" AutoPostBack="true" OnSelectedIndexChanged="OrderByDropDownList_SelectedIndexChanged" />
    <asp:HiddenField runat="server" ID="SortByHiddenField" />
    <asp:HiddenField runat="server" ID="OrderByHiddenField" />
</div>

<asp:Literal runat="server" ID="DownloadsLiteral"></asp:Literal>
<asp:Button runat="server" ID="LoadMoreButton" Text="Load More" OnClick="LoadMoreButton_Click" CssClass="LoadMoreButton" Visible="false" />
<asp:HiddenField runat="server" ID="CurrentCountHiddenField" Value="0" />