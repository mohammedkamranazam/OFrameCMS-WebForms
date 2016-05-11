<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelatedVideosComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Gallery.Nested.RelatedVideosComponent" %>

<div class="RelatedVideosDiv">
    <h2 class="PageTitle" runat="server" id="TitleH1">
        <asp:Literal runat="server" ID="TitleLiteral" />
    </h2>
    <asp:Literal ID="VideosLiteral" runat="server" />
    <asp:Button runat="server" ID="LoadMoreButton" OnClick="LoadMoreButton_Click" CssClass="LoadMoreButton" />
    <asp:HiddenField runat="server" ID="CurrentCountHiddenField" Value="0" />
</div>