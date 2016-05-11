<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="ProjectJKL.Post" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="Content">
        <div class="SideBarLeft">
            <OWD:ChildPostsListComponent runat="server" />
            <OWD:PostCategoriesListComponent runat="server" ID="PostCategoriesListComponent1" />
            <OWD:TagCloudComponent runat="server" ID="TagCloudComponent1" />
        </div>
        <div class="ContentMiddle">
            <OWD:PostComponent runat="server" />
            <OWD:RelatedPostsComponent runat="server" />
            <%= OWDARO.AppConfig.DiscussCode %>
        </div>
        <div class="Clear"></div>
    </div>
</asp:content>