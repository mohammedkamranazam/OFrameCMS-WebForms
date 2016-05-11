<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="ProjectJKL.Events" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <OWD:EventsComponent ID="EventsComponent1" runat="server"></OWD:EventsComponent>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>