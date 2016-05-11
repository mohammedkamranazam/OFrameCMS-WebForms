<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Photos.aspx.cs" Inherits="ProjectJKL.Photos" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <OWD:PhotosComponent runat="server" />
            <%= OWDARO.AppConfig.DiscussCode %>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>