<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="VideoCategory.aspx.cs" Inherits="ProjectJKL.VideoCategory" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="Content">
        <div class="SideBarLeft">
           <OWD:VideoCategoriesListComponent runat="server" />
            <OWD:TagCloudComponent runat="server" ID="TagCloudComponent1" />
        </div>
        <div class="ContentMiddle">
            <OWD:VideoCategoryComponent runat="server" />
        </div>
        <div class="Clear"></div>
    </div>
</asp:content>