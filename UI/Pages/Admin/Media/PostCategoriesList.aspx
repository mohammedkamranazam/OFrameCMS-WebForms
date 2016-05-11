<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/UI/Pages/MasterPages/Admin_Zice.master" AutoEventWireup="true" CodeBehind="PostCategoriesList.aspx.cs"
    Inherits="ProjectJKL.UI.Pages.Admin.Media.PostCategoriesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Post Categories List</span>
        </div>
        <div class="Clear">
        </div>
        <asp:updatepanel id="UpdatePanel1" runat="server" clientidmode="Static">
            <ContentTemplate>
                <div class="uibutton-toolbar btn-group">
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Media/PostCategoriesAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Post Category
                    </asp:HyperLink>
                    <asp:DropDownList runat="server" AutoPostBack="true" ID="LanguagesDropDownList" />
                </div>
                <div class="content">
                     <OWD:PostCategoriesSelectComponent runat="server" ID="PostCategoriesSelectComponent1" EditMode="true" />
                </div>
            </ContentTemplate>
        </asp:updatepanel>
    </div>
</asp:Content>