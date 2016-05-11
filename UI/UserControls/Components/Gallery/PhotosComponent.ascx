<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhotosComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Gallery.PhotosComponent" %>

<h1 runat="server" id="TitleH1" class="PageTitle">
    <asp:Literal runat="server" ID="TitleLiteral"></asp:Literal>
</h1>
<asp:Literal runat="server" ID="PhotosLiteral" />
<asp:Button runat="server" ID="LoadMoreButton" OnClick="LoadMoreButton_Click" CssClass="LoadMoreButton" />
<asp:HiddenField runat="server" ID="CurrentCountHiddenField" Value="0" />