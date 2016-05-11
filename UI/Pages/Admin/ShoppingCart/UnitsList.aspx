<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="UnitsList.aspx.cs"
    Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.UnitsList" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Units List</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/UnitsAdd.aspx">
                        <i class="icon-plus-sign"></i> Add Unit
                        </asp:HyperLink>
                        <asp:DropDownList ID="SectionsDropDownList" runat="server" AutoPostBack="True" DataSourceID="SectionsEntityDataSource" DataTextField="Title"
                            DataValueField="SectionID">
                        </asp:DropDownList>
                        <asp:DropDownList ID="CategoriesDropDownList" runat="server" AutoPostBack="True" DataSourceID="CategoriesEntityDataSource" DataTextField="Title"
                            DataValueField="CategoryID">
                        </asp:DropDownList>
                        <asp:DropDownList ID="SubCategoriesDropDownList" runat="server" AutoPostBack="True" DataSourceID="SubCategoriesEntityDataSource" DataTextField="Title"
                            DataValueField="SubCategoryID">
                        </asp:DropDownList>
                    </div>
                    <br />
                    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
                        DataSourceID="EntityDataSource1">
                        <Columns>
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
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <asp:HyperLink runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("UnitsManage.aspx?UnitID={0}", Eval("UnitID")) %>'>
                                            <i class="icon-cog"></i> Manage Unit
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
        DefaultContainerName="ShoppingCartEntities" EnableFlattening="False" EntitySetName="SC_Units" AutoGenerateWhereClause="True">
        <WhereParameters>
            <asp:ControlParameter ControlID="SectionsDropDownList" DbType="Int32" Name="SectionID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="CategoriesDropDownList" DbType="Int32" Name="CategoryID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="SubCategoriesDropDownList" DbType="Int32" Name="SubCategoryID" PropertyName="SelectedValue" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="SectionsEntityDataSource" runat="server" ConnectionString="name=ShoppingCartEntities" DefaultContainerName="ShoppingCartEntities"
        EnableFlattening="False" EntitySetName="SC_Sections">
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="CategoriesEntityDataSource" runat="server" ConnectionString="name=ShoppingCartEntities" DefaultContainerName="ShoppingCartEntities"
        EnableFlattening="False" EntitySetName="SC_Categories" Where="it.SectionID==@SectionID">
        <WhereParameters>
            <asp:ControlParameter ControlID="SectionsDropDownList" DbType="Int32" Name="SectionID" PropertyName="SelectedValue" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="SubCategoriesEntityDataSource" runat="server" ConnectionString="name=ShoppingCartEntities" DefaultContainerName="ShoppingCartEntities"
        EnableFlattening="False" EntitySetName="SC_SubCategories" Where="it.CategoryID==@CategoryID && it.SC_Categories.SectionID==@SectionID">
        <WhereParameters>
            <asp:ControlParameter ControlID="CategoriesDropDownList" DbType="Int32" Name="CategoryID" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="SectionsDropDownList" DbType="Int32" Name="SectionID" PropertyName="SelectedValue" />
        </WhereParameters>
    </asp:EntityDataSource>
</asp:content>