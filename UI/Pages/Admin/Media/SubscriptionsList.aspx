<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionsList.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Media.SubscriptionsList" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Subscribers List</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="uibutton-toolbar btn-group">
                <asp:Button runat="server" ID="ExportToExcelButton" CssClass="btn btn-warning" Text="Export To Excel" OnClick="ExportToExcelButton_Click" CausesValidation="false" />
            </div>
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
                        DataSourceID="EntityDataSource1" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Email" SortExpression="Email">
                                <ItemTemplate>
                                    <%# Eval("Email") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                <ItemTemplate>
                                    <%# Eval("Name") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile" SortExpression="Mobile">
                                <ItemTemplate>
                                    <%# Eval("Mobile") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Subscribed" SortExpression="IsSubscribed">
                                <ItemTemplate>
                                    <%# ((bool)Eval("IsSubscribed")) ? "Yes" : "No" %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <%--<asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("PostCategoriesManage.aspx?PostCategoryID={0}", Eval("PostCategoryID")) %>'>
                                            <i class="icon-cog"></i> Manage
                                        </asp:HyperLink>--%>
                                        <asp:Button runat="server" ID="RowCommandButton" CssClass="btn btn-mini" Text="Delete" CommandName='<%# Eval("Email") %>' CausesValidation="false" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridHeaderStyle" />
                        <PagerStyle CssClass="GridPagerStyle" />
                        <RowStyle CssClass="GridRowStyle" />
                    </asp:GridView>
                    <br />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=MediaEntities" DefaultContainerName="MediaEntities"
        EnableFlattening="False" EntitySetName="ME_Subscriptions">
    </asp:EntityDataSource>
</asp:content>