<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelatedPostsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Media.RelatedPostsComponent" %>

<h2 runat="server" id="TitleH1" class="PageTitle">
    <asp:Literal runat="server" ID="TitleLiteral" />
</h2>
<div class="ItemsCarousel">
    <asp:Repeater runat="server" ID="Repeater1">
        <ItemTemplate>
            <div class="RelatedPostsCarousel">
                <asp:HyperLink runat="server" NavigateUrl='<%#String.Format("~/Post.aspx?PostID={0}", Eval("PostID")) %>'>
                                <h5 class="Title">
                                <%#Eval("Title")%>
                                </h5>
                                <asp:Image runat="server" ImageUrl='<%# OWDARO.Util.Utilities.GetImageThumbURL((int?)Eval("ImageID")) %>' />
                </asp:HyperLink>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>