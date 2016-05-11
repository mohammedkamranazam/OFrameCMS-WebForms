<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="AudioCategory.aspx.cs" Inherits="ProjectJKL.AudioCategory"
    Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="Content">
        <div class="SideBarLeft">
            <OWD:AudioCategoriesListComponent runat="server" />
            <OWD:TagCloudComponent runat="server" ID="TagCloudComponent1" />
        </div>
        <div class="ContentMiddle">
            <OWD:AudioCategoryComponent runat="server" />
        </div>
        <div class="Clear"></div>
    </div>
</asp:content>