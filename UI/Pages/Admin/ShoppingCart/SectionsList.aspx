<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="SectionsList.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.SectionsList" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Sections List</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/SectionsAdd.aspx">
                <i class="icon-plus-sign"></i> Add Section
                </asp:HyperLink>
            </div>
            <br />
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False" DataKeyNames="SectionID"
                        DataSourceID="EntityDataSource1" AllowPaging="True" AllowSorting="True">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a id="fancybox" href='<%# ResolveClientUrl((string)Eval("ImageURL")) %>'>
                                        <asp:Image runat="server" ImageUrl='<%# Eval("ImageURL") %>' AlternateText='<%#Eval("Title") %>' Height="100px" Width="100px" Style="margin: 5px 0px 5px 0px;" />
                                    </a>
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
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("SectionsManage.aspx?SectionID={0}", Eval("SectionID")) %>'>
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
        DefaultContainerName="ShoppingCartEntities" EnableFlattening="False" EntitySetName="SC_Sections">
    </asp:EntityDataSource>
</asp:content>