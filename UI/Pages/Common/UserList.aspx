<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="OWDARO.UI.Pages.Common.UserList" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color user"></span>Users List</span>
        </div>
        <div class="Clear">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
            <ContentTemplate>
                <div class="uibutton-toolbar btn-group">
                    <asp:Button runat="server" ID="ExportToExcelButton" CssClass="btn btn-primary" Text="Export To Excel" OnClick="ExportToExcelButton_Click" CausesValidation="false" />
                    <asp:DropDownList runat="server" ID="RolesDropDownList" OnSelectedIndexChanged="RolesDropDownList_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:DropDownList runat="server" ID="CategoriesDropDownList" OnSelectedIndexChanged="CategoriesDropDownList_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:TextBox runat="server" ID="SearchTermTextBox" placeholder="search..."></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SearchTermTextBox" Display="Static" SetFocusOnError="true" />
                    <asp:Button runat="server" ID="SearchButton" Text="Search" OnClick="SearchButton_Click" CssClass="btn btn-warning" />
                </div>
                <div class="content">
                    <div class="boxtitle">
                        <span class="ico color list "></span>list of all users
                    </div>
                    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                        AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image runat="server" ImageUrl='<%#Eval("ProfilePic") %>' AlternateText='<%#Eval("Name") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="" />
                                <ControlStyle CssClass="GridControlStyle GridImageStyle" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Username" HeaderText="Username" ReadOnly="True" SortExpression="Username">
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email">
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserRole" HeaderText="Role" SortExpression="UserRole">
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Category" SortExpression="UserCategoryID">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# (Eval("UserCategoryID")==null) ? "None" : Eval("MS_UserCategories.Title") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <span class="wtip"><a title="Manage" href='<%#String.Format("UserManage.aspx?Username={0}", Eval("Username")) %>'>
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Themes/Zice/Graphics/icon/icon_edit.png" />
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:content>