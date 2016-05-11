<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Event.aspx.cs" Inherits="ProjectJKL.Event" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <OWD:EventDetailsComponent runat="server" ID="EventDetailsComponent1"></OWD:EventDetailsComponent>
    <br />
    <br />
    <%= OWDARO.AppConfig.DiscussCode %>
</asp:content>