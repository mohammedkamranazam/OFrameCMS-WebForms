<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Downloads.aspx.cs" Inherits="ProjectJKL.Downloads" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="Content">
        <div class="SideBarLeft">
            <OWD:DrivesAndFoldersListComponent runat="server" ID="DrivesAndFoldersListComponent1"></OWD:DrivesAndFoldersListComponent>
        </div>
        <div class="ContentMiddle">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <OWD:DownloadsComponent runat="server" ID="DownloadsComponent1"></OWD:DownloadsComponent>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="Clear"></div>
    </div>
</asp:content>