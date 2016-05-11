<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="VideoCategories.aspx.cs" Inherits="ProjectJKL.VideoCategories" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="Content">
        <div class="SideBarLeft">
            <OWD:VideoCategoriesListComponent runat="server" />
            <OWD:TagCloudComponent runat="server" ID="TagCloudComponent1" />
        </div>
        <div class="ContentMiddle">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <OWD:VideoCategoriesComponent runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="Clear"></div>
    </div>
</asp:content>