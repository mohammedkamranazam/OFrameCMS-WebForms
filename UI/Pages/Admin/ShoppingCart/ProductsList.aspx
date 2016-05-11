<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="ProductsList.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.ProductsList" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Products List</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ProductsAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Product
                        </asp:HyperLink>
                        <asp:DropDownList ID="SectionsDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SectionsDropDownList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="CategoriesDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CategoriesDropDownList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="SubCategoriesDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SubCategoriesDropDownList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="SearchTermTextBox" placeholder="search..."></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="SearchTermTextBox" Display="Static" SetFocusOnError="true" />
                        <asp:Button runat="server" ID="SearchButton" Text="Search"
                            OnClick="SearchButton_Click" CssClass="btn btn-warning" />
                    </div>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="GridView" DataKeyNames="ProductID"
                        AllowPaging="true" AllowSorting="true" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" Width="20px" />
                                <ItemStyle CssClass="GridItemStyle" Width="20px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a id="fancybox" href='<%# ResolveClientUrl(ProjectJKL.BLL.ShoppingCartBLL.ProductImagesBL.GetProductFirstImage((int)Eval("ProductID"))) %>'>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# ProjectJKL.BLL.ShoppingCartBLL.ProductImagesBL.GetProductFirstImageThumb((int)Eval("ProductID")) %>'
                                            AlternateText='<%#Eval("Title") %>' Height="100px" Width="100px" Style="margin: 5px 0px 5px 0px;" />
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" Width="100px" />
                                <ItemStyle CssClass="GridItemStyle" Width="100px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title" SortExpression="Title">
                                <ItemTemplate>
                                    <span class="GridTitleFieldColumn"><%#Eval("Title") %></span>
                                    <span class="GridSubTitleFieldColumn"><%#Eval("SubTitle") %></span>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="200px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Number" SortExpression="ItemNumber">
                                <ItemTemplate>
                                    <%#Eval("ItemNumber") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="100px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price" SortExpression="Price">
                                <ItemTemplate>
                                    <%# string.Format("Rs.{0}", Eval("Price")) %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty" SortExpression="AvailableQuantity">
                                <ItemTemplate>
                                    <%#Eval("AvailableQuantity") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="50px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Color" SortExpression="ColorID">
                                <ItemTemplate>
                                    <%#Eval("SC_Colors.Title") %><br />
                                    <asp:Image runat="server" ImageUrl='<%# Eval("SC_Colors.ImageURL") %>' Width="40px" Height="40px" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="100px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size" SortExpression="SizeID">
                                <ItemTemplate>
                                    <%#Eval("SC_Sizes.Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="50px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand" SortExpression="BrandID">
                                <ItemTemplate>
                                    <%#Eval("SC_Brands.Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="100px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Visibility" SortExpression="Hide">
                                <ItemTemplate>
                                    Hidden: <%# ((bool)Eval("Hide"))? "Yes":"No" %><br />
                                    Show In Cart: <%# ((bool)Eval("ShowInCart"))? "Yes":"No" %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="120px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("ProductsManage.aspx?ProductID={0}", Eval("ProductID")) %>'>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>