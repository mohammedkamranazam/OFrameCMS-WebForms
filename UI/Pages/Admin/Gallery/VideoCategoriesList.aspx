<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Themes/Default/Main.Master" CodeBehind="VideoCategoriesList.aspx.cs"
    Inherits="ProjectJKL.UI.Pages.Admin.Gallery.VideoCategoriesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Video Categories List</span>
        </div>
        <div class="Clear">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
            <ContentTemplate>
                <div class="uibutton-toolbar btn-group">
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Gallery/VideoCategoriesAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Video Category
                    </asp:HyperLink>
                    <asp:DropDownList runat="server" AutoPostBack="true" ID="LanguagesDropDownList" />
                </div>
                <div class="content">
                    <OWD:VideoCategoriesSelectComponent runat="server" ID="VideoCategoriesSelectComponent1" EditMode="true" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=GalleryEntities" DefaultContainerName="GalleryEntities"
        EnableFlattening="False" EntitySetName="GY_VideoCategories" AutoGenerateWhereClause="True" EntityTypeFilter="" Select=""
        Where="">
        <WhereParameters>
            <asp:ControlParameter ControlID="LanguagesDropDownList" Name="Locale" PropertyName="SelectedValue" />
        </WhereParameters>
    </asp:EntityDataSource>
</asp:Content>