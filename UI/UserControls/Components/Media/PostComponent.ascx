<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Media.PostComponent" %>

<div class="PostDetails" runat="server" id="PostDetailsDiv">
    <asp:Literal runat="server" ID="ImageLiteral" />
    <h1 class="PostTitle">
        <asp:Literal runat="server" ID="TitleLiteral" />
    </h1>
    <h2 class="PostSubTitle">
        <asp:Literal runat="server" ID="SubTitleLiteral" />
    </h2>
    <div class="InfoDiv">
        By
        <asp:HyperLink runat="server" ID="AuthorHyperLink" />
        | <span>
            <asp:Literal runat="server" ID="AddedOnLiteral" /></span>| <span>
                <asp:HyperLink runat="server" ID="CategoryHyperLink" /></span>
    </div>
    <div class="PostContent">
        <OWD:PostEmbedComponent runat="server" id="PostEmbedComponent"></OWD:PostEmbedComponent>
        <div class="Clear"></div>
    </div>
    <div class="PostTags">
        Tags:
        <asp:Literal runat="server" ID="TagsLiteral" />
    </div>
</div>
