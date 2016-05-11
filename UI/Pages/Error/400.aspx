<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="400.aspx.cs" Inherits="ProjectJKL.UI.Pages.Error._400" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="ErrorPage">
        <h2>400</h2>
        <p>
            Go Back To
            <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx" Text="Home Page"></asp:HyperLink>
        </p>
    </div>
</asp:content>