<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="FileListAndUpload.aspx.cs" Inherits="ProjectJKL.UI.Pages.Helpers.FileListAndUpload" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Files</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
                <ContentTemplate>
                    <OWD:FilesSelectComponent runat="server" ID="FilesSelectComponent1" EditMode="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>