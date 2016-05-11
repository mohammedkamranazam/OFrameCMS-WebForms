<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Posts.aspx.cs" Inherits="ProjectJKL.Posts" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="Content">
        <div class="SideBarLeft">
            <OWD:PostCategoriesListComponent runat="server" ID="PostCategoriesListComponent1" />
            <OWD:TagCloudComponent runat="server" ID="TagCloudComponent1" />
        </div>
        <div class="ContentMiddle">
            <OWD:PostsComponent runat="server" ID="PostsComponent1"></OWD:PostsComponent>
        </div>
        <div class="Clear"></div>
    </div>
</asp:content>