<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="Generic.aspx.cs" Inherits="ProjectJKL.UI.Pages.Error.Generic" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="ErrorPage">
        <h2>An Unknown Error Occured</h2>
        <p>
            Go Back To
            <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx" Text="Home Page"></asp:HyperLink>
        </p>
    </div>
</asp:content>