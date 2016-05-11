<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ProjectJKL.Search" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <OWD:SearchComponent runat="server"></OWD:SearchComponent>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>