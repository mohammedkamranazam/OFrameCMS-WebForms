<%@ Page MasterPageFile="~/Themes/Default/Main.Master" Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="PostsList.aspx.cs"
    Inherits="ProjectJKL.UI.Pages.Admin.Media.PostsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Posts List</span>
        </div>
        <div class="Clear">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
            <ContentTemplate>
                <div class="uibutton-toolbar btn-group">
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Media/PostsAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Post
                    </asp:HyperLink>
                    <asp:DropDownList runat="server" AutoPostBack="true" ID="LanguagesDropDownList" />
                </div>
                <div class="content">
                    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
                        DataSourceID="EntityDataSource1">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# OWDARO.Util.Utilities.GetFancyBoxHTML((int?)Eval("ImageID"), string.Empty, false, "style='width:100px; height:100px; margin:5px;'") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title" SortExpression="Title">
                                <ItemTemplate>
                                    <span class="GridTitleFieldColumn"><%#Eval("Title") %></span>
                                    <span class="GridSubTitleFieldColumn"><%#Eval("SubTitle") %></span>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Added On" SortExpression="AddedOn">
                                <ItemTemplate>
                                    <%# OWDARO.Util.DataParser.GetDateTimeFormattedString((DateTime)Eval("AddedOn")) %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category" SortExpression="PostCategoryID">
                                <ItemTemplate>
                                    <%# Eval("ME_PostCategories.Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("PostsManage.aspx?PostID={0}", Eval("PostID")) %>'>
                                            <i class="icon-cog"></i> Manage
                                        </asp:HyperLink>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridHeaderStyle" />
                        <PagerStyle CssClass="GridPagerStyle" />
                        <RowStyle CssClass="GridRowStyle" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=MediaEntities" DefaultContainerName="MediaEntities"
        EnableFlattening="False" EntitySetName="ME_Posts" Include="ME_PostCategories" AutoGenerateWhereClause="True" EntityTypeFilter="" Select="" Where="">
        <WhereParameters>
            <asp:ControlParameter ControlID="LanguagesDropDownList" Name="Locale" PropertyName="SelectedValue" />
        </WhereParameters>
    </asp:EntityDataSource>
</asp:Content>