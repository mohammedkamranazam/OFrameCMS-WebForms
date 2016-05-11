<%@ Page MasterPageFile="~/Themes/Default/Main.Master" Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="EventList.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.EventList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Event List</span>
        </div>
        <div class="Clear">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
            <ContentTemplate>
                <div class="uibutton-toolbar btn-group">
                    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Gallery/EventAdd.aspx">
                <i class="icon-plus-sign"></i> Add Event
                    </asp:HyperLink>
                    <asp:DropDownList runat="server" AutoPostBack="true" ID="LanguagesDropDownList" />
                </div>
                <div class="content">
                    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                        AllowPaging="True" AllowSorting="True" DataSourceID="EntityDataSource1">
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
                            <asp:TemplateField HeaderText="Event Type" SortExpression="EventTypeID">
                                <ItemTemplate>
                                    <%# OWDARO.Settings.EventTypesHelper.Get((int)Eval("EventTypeID")).Title %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Starts On" SortExpression="StartsOn">
                                <ItemTemplate>
                                    <%# OWDARO.Util.DataParser.GetDateTimeFormattedString((DateTime)Eval("StartsOn")) %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ends On" SortExpression="EndsOn">
                                <ItemTemplate>
                                    <%# OWDARO.Util.DataParser.GetDateTimeFormattedString((DateTime?)Eval("EndsOn")) %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location" SortExpression="Location">
                                <ItemTemplate>
                                    <%# Eval("Location") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("EventManage.aspx?EventID={0}", Eval("EventID")) %>'>
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
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=GalleryEntities"
        DefaultContainerName="GalleryEntities" EnableFlattening="False" EntitySetName="GY_Events"
        OrderBy="it.StartsOn DESC" AutoGenerateWhereClause="True" EntityTypeFilter="" Select="" Where="">
        <WhereParameters>
            <asp:ControlParameter ControlID="LanguagesDropDownList" Name="Locale" PropertyName="SelectedValue" />
        </WhereParameters>
    </asp:EntityDataSource>
</asp:Content>