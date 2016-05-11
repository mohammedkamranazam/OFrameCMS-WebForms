<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentVideosComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Gallery.RecentVideosComponent" %>

<div class="RecentVideosComponent">
    <div class="Container">
        <h2 runat="server" id="TitleH1" class="PageTitle">
            <asp:Literal runat="server" ID="TitleLiteral" />
        </h2>
        <div class="ItemsCarousel">
            <asp:Literal ID="VideosLiteral" runat="server" />
        </div>
    </div>
</div>