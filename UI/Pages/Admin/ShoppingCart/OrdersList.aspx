<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="OrdersList.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.OrdersList" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
    <style>
        tr {
            display: table-cell;
            float: left;
        }

        .OrderItem {
            float: left;
            border: 1px solid rgba(0,0,0,0.1);
            padding: 5px;
            margin: 5px;
        }

            .OrderItem:hover {
                box-shadow: 0px 0px 5px 1px rgba(0,0,0,0.3);
                border: 1px solid rgba(0,0,0,0.3);
            }

            .OrderItem span.OrderNumber {
                display: block;
                background: #222222;
                line-height: 25px;
                padding: 0px 10px 0px 10px;
                font-family: Verdana;
                font-size: 12px;
                color: white;
            }

            .OrderItem span.Cancelled {
                background: #ff6a00;
            }

            .OrderItem span.Completed {
                background: #2c9009;
            }

            .OrderItem span.Failed {
                background: #a90909;
            }

            .OrderItem span.DateTime {
                display: block;
                line-height: 25px;
                padding: 0px 10px 0px 10px;
                font-family: Verdana;
                font-size: 10px;
                color: black;
            }

            .OrderItem span.OrderTotal {
                display: block;
                line-height: 25px;
                padding: 0px 10px 0px 10px;
                font-family: Verdana;
                font-size: 12px;
                color: black;
            }
    </style>
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Orders List</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="uibutton-toolbar btn-group">
                        <asp:DropDownList runat="server" ID="FilterTypeDropDownList" AutoPostBack="true" OnSelectedIndexChanged="FilterTypeDropDownList_SelectedIndexChanged">
                            <asp:ListItem Text="Pending" />
                            <asp:ListItem Text="Cancelled" />
                            <asp:ListItem Text="Completed" />
                            <asp:ListItem Text="Paid" />
                            <asp:ListItem Text="Failed" />
                            <asp:ListItem Text="Refund" />
                            <asp:ListItem Text="Dispatched" />
                            <asp:ListItem Text="Returned" />
                            <asp:ListItem Text="All" />
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="SearchTermTextBox" /><asp:Button runat="server" ID="SearchButton" Text="Search" OnClick="SearchButton_Click" CssClass="btn btn-warning" />
                    </div>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" PagerStyle-CssClass="GridPagerStyle" AllowPaging="True" AutoGenerateColumns="False" GridLines="None" PageSize="20" ShowHeader="False" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="OrderItem">
                                        <span class='<%# String.Format("OrderNumber {0}", GetOrderItemStyle((string)Eval("OrderNumber"))) %>'><%# Eval("OrderNumber") %></span>
                                        <span class="DateTime"><strong>Date & Time: </strong><%#OWDARO.Util.DataParser.GetDateTimeFormattedString((DateTime)Eval("DateTime")) %></span>
                                        <span class="OrderTotal"><strong>Order Total: </strong><%#string.Format("Rs. {0:0.00}/-", Eval("OrderTotal")) %></span>
                                        <div class="btn-group">
                                            <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("OrderManage.aspx?OrderNumber={0}", Eval("OrderNumber")) %>'>
                                            <i class="icon-cog"></i> Manage
                                            </asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-mini" Target="_blank" NavigateUrl='<%#String.Format("OrderPrint.aspx?OrderNumber={0}", Eval("OrderNumber")) %>'>
                                            <i class="icon-print"></i> Print
                                            </asp:HyperLink>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>