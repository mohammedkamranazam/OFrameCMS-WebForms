<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="ProjectJKL.AboutUs" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <h1 class="PageTitle">
        <asp:Literal runat="server" ID="AboutUsTitleLiteral"></asp:Literal>
    </h1>
    <div>
        <owd:PostEmbedComponent runat="server" ID="PostEmbedComponent1" />
        <div class="Clear"></div>
    </div>
</asp:content>
