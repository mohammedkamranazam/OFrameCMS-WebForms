<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="MenuList.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.OFrame.MenuList" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Menu List</span>
        </div>
        <div class="Clear">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/OFrame/MenuAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Menu
                        </asp:HyperLink>
                    </div>
                <div class="content">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <OWD:MenuComponent ID="MenuComponent1" runat="server" RootCssClass="ManageHeaderMenu" AllowLinkManagement="True" />
                            <OWD:Locales runat="server" ID="LocaleDropDown" LabelText="Locale" SmallLabelText="select a language" AutoPostBack="true" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="Clear"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:content>