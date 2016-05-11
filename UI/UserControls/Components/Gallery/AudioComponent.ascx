<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AudioComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Gallery.AudioComponent" %>

<div class="AudioDiv">
    <h1 class="MainTitle" runat="server" id="TitleH1">
        <asp:Literal runat="server" ID="TitleLiteral"></asp:Literal>
    </h1>
    <div class="EmbedAudio">
        <asp:Literal runat="server" ID="AudioLiteral"></asp:Literal>
    </div>
    <div class="Description">
        <div class="Info">
            <span class="TakenOn">
                <strong>Taken On: </strong>
                <asp:Literal runat="server" ID="TakenOnLiteral"></asp:Literal>
            </span>
            <span class="LikesCount">
                <strong>Likes: </strong>
                <asp:Literal runat="server" ID="LikesCountLiteral"></asp:Literal>
            </span>
            <span class="DislikesCount">
                <strong>Dislikes: </strong>
                <asp:Literal runat="server" ID="DislikesCountLiteral"></asp:Literal>
            </span>
            <span class="Share">
                <asp:HyperLink runat="server" ID="AudioDownloadLink" CssClass="btn"><i class="icon-arrow-down"></i>Download</asp:HyperLink>
            </span>
        </div>
        <p runat="server" id="DescriptionParaTag">
            <asp:Literal runat="server" ID="DescriptionLiteral"></asp:Literal>
        </p>
    </div>
    <%= OWDARO.AppConfig.DiscussCode %>
</div>