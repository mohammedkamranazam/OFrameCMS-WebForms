<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="CategoriesList.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.CategoriesList" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Categories List</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/CategoriesAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Category
                        </asp:HyperLink>
                        <asp:DropDownList runat="server" ID="SectionsDropDownList" AutoPostBack="True" DataSourceID="SectionsEntityDataSource" DataTextField="Title" DataValueField="SectionID" AppendDataBoundItems="true">
                            <asp:ListItem Text="All" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" CssClass="GridView" AutoGenerateColumns="False" DataKeyNames="CategoryID"
                        DataSourceID="EntityDataSource1" AllowPaging="true" AllowSorting="true">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%-- <a id="fancybox" href='<%# ResolveClientUrl((string)Eval("ImageURL")) %>'>--%>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImageURL") %>' AlternateText='<%#Eval("Title") %>' Height="100px" Width="100px" Style="margin: 5px 0px 5px 0px;" />
                                    <%--</a>--%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title" SortExpression="Title">
                                <ItemTemplate>
                                    <%# Eval("Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                <ItemTemplate>
                                    <%# Eval("Description") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hidden" SortExpression="Hide">
                                <ItemTemplate>
                                    <%# ((bool)Eval("Hide"))? "Yes":"No" %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section" SortExpression="SectionID">
                                <ItemTemplate>
                                    <%# Eval("SC_Sections.Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("CategoriesManage.aspx?CategoryID={0}", Eval("CategoryID")) %>'>
                                            <i class="icon-cog"></i> Manage Category
                                        </asp:HyperLink>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridHeaderStyle" />
                        <PagerStyle CssClass="GridPagerStyle" />
                        <RowStyle CssClass="GridRowStyle" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=ShoppingCartEntities"
        DefaultContainerName="ShoppingCartEntities" EnableFlattening="False" EntitySetName="SC_Categories" AutoGenerateWhereClause="True" EntityTypeFilter="" Select="" Where="" Include="SC_Sections">
        <WhereParameters>
            <asp:ControlParameter ControlID="SectionsDropDownList" DbType="Int32" Name="SectionID" PropertyName="SelectedValue" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="SectionsEntityDataSource" runat="server" ConnectionString="name=ShoppingCartEntities" DefaultContainerName="ShoppingCartEntities" EnableFlattening="False" EntitySetName="SC_Sections">
    </asp:EntityDataSource>
</asp:content>