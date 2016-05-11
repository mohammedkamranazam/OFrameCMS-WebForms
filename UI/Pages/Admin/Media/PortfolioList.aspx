<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="PortfolioList.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Media.PortfolioList" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Portfolios List</span>
        </div>
        <div class="Clear">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
            <ContentTemplate>
                <div class="content">
                    <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Media/PortfolioAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Portfolio
                        </asp:HyperLink>
                    </div>
                    <br />
                    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" DataSourceID="EntityDataSource1">
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
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" SortExpression="Date">
                                <ItemTemplate>
                                    <%# OWDARO.Util.DataParser.GetDateFormattedString((DateTime?)Eval("Date")) %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client" SortExpression="ClientID">
                                <ItemTemplate>
                                    <%# Eval("ME_Clients.Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("PortfolioManage.aspx?PortfolioID={0}", Eval("PortfolioID")) %>'>
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
                    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=MediaEntities" DefaultContainerName="MediaEntities"
                        EnableFlattening="False" EntitySetName="ME_Portfolios" AutoGenerateWhereClause="true" Include="ME_Clients">
                    </asp:EntityDataSource>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:content>