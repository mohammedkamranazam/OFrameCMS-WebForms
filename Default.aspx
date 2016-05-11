<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProjectJKL.Default" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content4" contentplaceholderid="SliderPlaceHolder" runat="server">
    <OWD:RevolutionBanner runat="server"></OWD:RevolutionBanner>
    <OWD:PostEmbedComponent runat="server" ID="HomePageTopBlockPostEmbedComponent" />
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="HomePagePost">
        <OWD:PostEmbedComponent runat="server" ID="HomePagePostEmbedComponent" />
        <div class="Clear"></div>
    </div>
</asp:content>
<asp:content id="Content3" contentplaceholderid="BottomPlaceHolder" runat="server">
    <OWD:RecentVideosComponent runat="server" ID="RecentVideosComponent1" Count="20" />
    <OWD:TestimonialsComponent runat="server"></OWD:TestimonialsComponent>
    <OWD:PostEmbedComponent runat="server" ID="HomePageBottomBlockPostEmbedComponent" />
</asp:content>
