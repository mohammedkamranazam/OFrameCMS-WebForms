<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AudioCategoryComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Gallery.AudioCategoryComponent" %>

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
    <div id="tab-1" class="content CategoryAudiosDiv">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Literal runat="server" ID="AudiosLiteral" />
                <asp:Button runat="server" ID="CategoryAudiosLoadMoreButton" CssClass="LoadMoreButton" OnClick="CategoryAudiosLoadMoreButton_Click" />
                <asp:HiddenField runat="server" ID="CategoryAudiosCurrentCountHiddenField" Value="0" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="CategoryAudiosLoadMoreButton" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="tab-2" class="content CategoryAudioSetsDiv">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Literal runat="server" ID="AudioSetLiteral" />
                <asp:Button runat="server" ID="CategoryAudioSetsLoadMoreButton" CssClass="LoadMoreButton" OnClick="CategoryAudioSetsLoadMoreButton_Click" />
                <asp:HiddenField runat="server" ID="CategoryAudioSetsCurrentCountHiddenField" Value="0" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="CategoryAudioSetsLoadMoreButton" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>