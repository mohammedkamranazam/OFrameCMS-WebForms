<%@ Page Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="UserCategoryList.aspx.cs" Inherits="OWDARO.UI.Pages.SuperAdmin.UserCategoryList" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color group "></span>User Categories </span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="boxtitle">
                <span class="ico color list "></span>list of all user categories
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="UserCategoriesGridView" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                        AllowPaging="True" AllowSorting="True" DataSourceID="EntityDataSource1">
                        <Columns>
                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" ReadOnly="True">
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True"
                                SortExpression="Description">
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <span class="wtip"><a title="Manage" href='<%#String.Format("UserCategoryManage.aspx?UserCategoryID={0}", Eval("UserCategoryID")) %>'>
                                        <asp:Image runat="server" ImageUrl="~/Themes/Zice/Graphics/icon/icon_edit.png" />
                                    </a></span>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridHeaderStyle" />
                        <RowStyle CssClass="GridRowStyle" />
                        <PagerStyle CssClass="GridPagerStyle" />
                    </asp:GridView>
                    <br />
                    <br />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=MembershipEntities"
        DefaultContainerName="MembershipEntities" EnableFlattening="False" EntitySetName="MS_UserCategories">
    </asp:EntityDataSource>
</asp:content>