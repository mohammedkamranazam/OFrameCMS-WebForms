<%@ Page MasterPageFile="~/Themes/Default/Main.Master" Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="FileList.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.FileList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Files List</span>
        </div>
        <div class="Clear">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
            <ContentTemplate>
                <div class="uibutton-toolbar btn-group">
                    <asp:HyperLink runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Gallery/FileAdd.aspx">
                <i class="icon-plus-sign"></i> Add File
                    </asp:HyperLink>
                    <asp:DropDownList runat="server" AutoPostBack="true" ID="LanguagesDropDownList" />
                </div>
                <div class="content">
                    <OWD:FilesSelectComponent runat="server" ID="FilesSelectComponent1" EditMode="True" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>