<%@ Page MasterPageFile="~/Themes/Default/Main.Master" Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="FolderList.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.FolderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Folders List</span>
        </div>
        <div class="Clear">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
            <ContentTemplate>
                <div class="uibutton-toolbar btn-group">
                    <asp:HyperLink runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Gallery/FolderAdd.aspx">
                <i class="icon-plus-sign"></i> Add Folder
                    </asp:HyperLink>
                    <asp:DropDownList runat="server" ID="LanguagesDropDownList" AutoPostBack="true" />
                </div>
                <div class="content">
                    <OWD:FoldersSelectComponent runat="server" ID="FoldersSelectComponent1" EditMode="true" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>