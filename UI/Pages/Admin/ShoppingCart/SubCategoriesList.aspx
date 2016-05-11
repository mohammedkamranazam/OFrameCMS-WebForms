<%@ Page Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="SubCategoriesList.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.SubCategoriesList" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Sub Categories List</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/SubCategoriesAdd.aspx">
                        <i class="icon-plus-sign"></i> Add Sub Categories
                        </asp:HyperLink>
                        <asp:DropDownList ID="SectionsDropDownList" runat="server" AutoPostBack="True" DataSourceID="SectionsEntityDataSource" DataTextField="Title"
                            DataValueField="SectionID">
                        </asp:DropDownList>
                        <asp:DropDownList ID="CategoriesDropDownList" runat="server" AutoPostBack="True" DataSourceID="CategoriesEntityDataSource" DataTextField="Title"
                            DataValueField="CategoryID">
                        </asp:DropDownList>
                    </div>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" CssClass="GridView" AutoGenerateColumns="False" DataKeyNames="SubCategoryID"
                        DataSourceID="EntityDataSource1" AllowPaging="true" AllowSorting="true">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%--  <a id="fancybox" href='<%# ResolveClientUrl((string)Eval("ImageURL")) %>'>--%>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImageThumbURL") %>' AlternateText='<%#Eval("Title") %>' Height="100px" Width="100px"
                                        Style="margin: 5px 0px 5px 0px;" />
                                    <%--  </a>--%>
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
                            <asp:TemplateField HeaderText="Category" SortExpression="CategoryID">
                                <ItemTemplate>
                                    <%# Eval("SC_Categories.Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("SubCategoriesManage.aspx?SubCategoryID={0}", Eval("SubCategoryID")) %>'>
                                            <i class="icon-cog"></i> Manage Section
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
        DefaultContainerName="ShoppingCartEntities" EnableFlattening="False" EntitySetName="SC_SubCategories" Include="SC_Categories" Where="it.CategoryID==@CategoryID && it.SC_Categories.SectionID==@SectionID">
        <WhereParameters>
            <asp:ControlParameter ControlID="CategoriesDropDownList" DbType="Int32" Name="CategoryID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="SectionsDropDownList" DbType="Int32" Name="SectionID" PropertyName="SelectedValue" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="CategoriesEntityDataSource" runat="server" ConnectionString="name=ShoppingCartEntities" DefaultContainerName="ShoppingCartEntities"
        EnableFlattening="False" EntitySetName="SC_Categories" Where="it.SectionID==@SectionID">
        <WhereParameters>
            <asp:ControlParameter ControlID="SectionsDropDownList" DbType="Int32" Name="SectionID" PropertyName="SelectedValue" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="SectionsEntityDataSource" runat="server" ConnectionString="name=ShoppingCartEntities" DefaultContainerName="ShoppingCartEntities"
        EnableFlattening="False" EntitySetName="SC_Sections">
    </asp:EntityDataSource>
</asp:content>