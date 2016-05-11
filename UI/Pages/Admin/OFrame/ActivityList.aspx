<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="ActivityList.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.OFrame.ActivityList" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Errors List</span>
        </div>
        <div class="Clear">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="content">
                    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" DataSourceID="EntityDataSource1">
                        <Columns>
                            <asp:TemplateField HeaderText="Date & Time" SortExpression="ActivityOn">
                                <ItemTemplate>
                                    <%# Eval("ActivityOn") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" SortExpression="ActivityType">
                                <ItemTemplate>
                                    <%# Eval("ActivityType") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("ActivityManage.aspx?ActivityLogID={0}", Eval("ActivityLogID")) %>'>
                                            <i class="icon-cog"></i>Manage
                                        </asp:HyperLink>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridHeaderStyle" />
                        <PagerStyle CssClass="GridPagerStyle" />
                        <RowStyle CssClass="GridRowStyle" />
                    </asp:GridView>
                    <div class="Clear"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:EntityDataSource ID="EntityDataSource1" runat="server"  ConnectionString="name=OWDAROEntities" DefaultContainerName="OWDAROEntities" EnableFlattening="False" EntitySetName="OW_ActivityLogs" OrderBy="it.ActivityOn DESC">
    </asp:EntityDataSource>
</asp:content>