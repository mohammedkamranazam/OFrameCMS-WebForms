<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoCategoryComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Gallery.VideoCategoryComponent" %>

<h1 runat="server" id="TitleH1" class="PageTitle">
    <asp:Literal runat="server" ID="TitleLiteral"></asp:Literal>
</h1>
<div id="tabs">
    <ul>
        <li><a href="#tab-1">
            <asp:Literal runat="server" ID="Tab1Literal" /></a></li>
        <li><a href="#tab-2">
            <asp:Literal runat="server" ID="Tab2Literal" /></a></li>
    </ul>
    <div id="tab-1" class="content CategoryVideosDiv">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Literal runat="server" ID="VideosLiteral" />
                <asp:Button runat="server" ID="CategoryVideosLoadMoreButton" CssClass="LoadMoreButton" OnClick="CategoryVideosLoadMoreButton_Click" />
                <asp:HiddenField runat="server" ID="CategoryVideosCurrentCountHiddenField" Value="0" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="CategoryVideosLoadMoreButton" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="tab-2" class="content CategoryVideoSetsDiv">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Literal runat="server" ID="VideoSetLiteral" />
                <asp:Button runat="server" ID="CategoryVideoSetsLoadMoreButton" CssClass="LoadMoreButton" OnClick="CategoryVideoSetsLoadMoreButton_Click" />
                <asp:HiddenField runat="server" ID="CategoryVideoSetsCurrentCountHiddenField" Value="0" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="CategoryVideoSetsLoadMoreButton" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>