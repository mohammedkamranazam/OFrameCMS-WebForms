<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="ActivityManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.OFrame.ActivityManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Activity Log</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                <asp:HyperLink runat="server" CssClass="btn btn-primary" NavigateUrl="~/UI/Pages/Admin/OFrame/ActivityList.aspx">
                            <i class="icon-arrow-left"></i> Go Back To Activity List
                </asp:HyperLink>
            </div>
        <div class="content">
            <div style="font-family: verdana; font-size: 13px; line-height: 25px; padding: 10px;">
                <asp:Literal runat="server" ID="ActivityMessageLiteral"></asp:Literal>
                <div class="Clear"></div>
            </div>
            <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
        </div>
    </div>
</asp:content>