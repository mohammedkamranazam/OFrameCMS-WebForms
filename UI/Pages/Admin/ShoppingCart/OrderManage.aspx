<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="OrderManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.OrderManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
    <style>
        .ItemNumber {
            display: block;
            line-height: 20px;
        }

        .Color {
            display: block;
            line-height: 20px;
        }

        .Size {
            display: block;
            line-height: 20px;
        }
    </style>
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Edit Order</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="uibutton-toolbar btn-group">
                        <div class="btn-group FloatLeft">
                            <asp:Button runat="server" ID="IsPaidButton" Text="Complete Payment" CssClass="btn btn-info" OnClick="IsPaidButton_Click" />
                            <asp:Button runat="server" ID="IsRefundButton" Text="Refund Amount" CssClass="btn btn-primary" OnClick="IsRefundButton_Click" />
                        </div>
                        <div class="btn-group FloatLeft">
                            <asp:Button runat="server" ID="IsCancelledButton" Text="Cancel Order" CssClass="btn btn-danger" OnClick="IsCancelledButton_Click" />
                            <asp:Button runat="server" ID="IsCompletedButton" Text="Complete Order" CssClass="btn btn-success" OnClick="IsCompletedButton_Click" />
                        </div>
                        <div class="btn-group FloatLeft">
                            <asp:Button runat="server" ID="IsDispatchedButton" Text="Dispatch Order" CssClass="btn" OnClick="IsDispatchedButton_Click" />
                            <asp:Button runat="server" ID="IsReturnedButton" Text="Products Returned" CssClass="btn btn-info" OnClick="IsReturnedButton_Click" />
                        </div>
                        <div class="btn-group FloatLeft">
                            <asp:HyperLink ID="OrderPrintHyperLink" runat="server" CssClass="btn btn-success" Target="_blank">
                            <i class="icon-print"></i> Print
                            </asp:HyperLink>
                        </div>
                        <div class="Clear"></div>
                        <OWD:StatusMessageJQuery runat="server" ID="StatusStatusMessage" />
                    </div>
                    <br />
                    <OWD:LabelAdv runat="server" ID="PaidStatusLabel" LabelText="Amount Paid" SmallLabelText="" />
                    <OWD:LabelAdv runat="server" ID="CancelledStatusLabel" LabelText="Order Cancelled" SmallLabelText="" />
                    <OWD:LabelAdv runat="server" ID="CompletedStatusLabel" LabelText="Order Completed" SmallLabelText="" />
                    <OWD:LabelAdv runat="server" ID="FailedStatusLabel" LabelText="Order Failed" SmallLabelText="" />
                    <OWD:LabelAdv runat="server" ID="RefundStatusLabel" LabelText="Amount Refunded" SmallLabelText="" />
                    <OWD:LabelAdv runat="server" ID="DispatchedStatusLabel" LabelText="Products Dispatched" SmallLabelText="" />
                    <OWD:LabelAdv runat="server" ID="ReturnedStatusLabel" LabelText="Products Returned" SmallLabelText="" />
                    <div class="grid2">
                        <OWD:LabelAdv ID="OrderNumberLabel" runat="server" LabelText="Order Number" SmallLabelText="customer order number"></OWD:LabelAdv>
                        <OWD:LabelAdv ID="DateTimeLabel" runat="server" LabelText="Date Time" SmallLabelText="date and time of order"></OWD:LabelAdv>
                        <OWD:LabelAdv ID="EmailIDLabel" runat="server" LabelText="Email ID" SmallLabelText="order email id" />
                        <OWD:LabelAdv ID="MobileLabel" runat="server" LabelText="Mobile" SmallLabelText="customer mobile number" />
                        <OWD:LabelAdv ID="OrderTotalLabel" runat="server" LabelText="Order Total" SmallLabelText="total order amount" />
                    </div>
                    <div class="grid2">
                        <OWD:LabelAdv ID="ShippingAddressLabel" runat="server" LabelText="Shipping Address" SmallLabelText="customer shipping address"></OWD:LabelAdv>
                        <OWD:LabelAdv ID="BillingAddressLabel" runat="server" LabelText="Billing Address" SmallLabelText="customer billing address"></OWD:LabelAdv>
                    </div>
                    <div class="Clear"></div>
                    <asp:GridView ID="GridView1" runat="server" CssClass="GridView" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" DataKeyNames="OrderDetailID" DataSourceID="EntityDataSource1">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a id="fancybox" href='<%# ResolveClientUrl(ProjectJKL.BLL.ShoppingCartBLL.ProductImagesBL.GetProductFirstImage((int)Eval("ProductID"))) %>'>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# ProjectJKL.BLL.ShoppingCartBLL.ProductImagesBL.GetProductFirstImageThumb((int)Eval("ProductID")) %>'
                                            AlternateText='<%#Eval("SC_Products.Title") %>' Height="100px" Width="100px" Style="margin: 5px 0px 5px 0px;" />
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" Width="20px" />
                                <ItemStyle CssClass="GridItemStyle" Width="20px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="SC_Products.Title">
                                <ItemTemplate>
                                    <span class="GridTitleFieldColumn"><%#Eval("SC_Products.Title") %></span>
                                    <small class="GridSubTitleFieldColumn"><%#Eval("SC_Products.SubTitle") %></small>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="200px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Details" SortExpression="SC_Products.ItemNumber">
                                <ItemTemplate>
                                    <span class="ItemNumber"><strong>Itm. No.: </strong><%#Eval("SC_Products.ItemNumber") %></span>
                                    <%#ProjectJKL.BLL.ShoppingCartBLL.ColorsBL.GetColor((int)Eval("ProductID")) %>
                                    <%#ProjectJKL.BLL.ShoppingCartBLL.SizesBL.GetSize((int)Eval("ProductID")) %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="120px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty." SortExpression="Quantity">
                                <ItemTemplate>
                                    <span class="Text"><%#Eval("Quantity") %></span>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="50px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price" SortExpression="Price">
                                <ItemTemplate>
                                    <span class="Text"><%#String.Format("Rs.{0:0.00}/-", Convert.ToDouble(Eval("Price"))) %></span>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="50px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sub Total">
                                <ItemTemplate>
                                    <%#String.Format("Rs.{0:0.00}/-", Convert.ToDouble(Eval("Quantity")) * Convert.ToDouble(Eval("Price")) ) %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="90px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="CartIDHiddenField" runat="server" Value='<%# Eval("OrderDetailID") %>' />
                                    <asp:Button ID="Button2" CssClass="DeleteButton" runat="server" CausesValidation="false" CommandName="Delete" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="50px" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>--%>
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
        <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=ShoppingCartEntities" DefaultContainerName="ShoppingCartEntities"
            EnableFlattening="False" EntitySetName="SC_OrderDetails" Where="it.OrderNumber==@OrderNumber" Include="SC_Orders, SC_Products">
            <WhereParameters>
                <asp:QueryStringParameter Name="OrderNumber" DbType="String" QueryStringField="OrderNumber" />
            </WhereParameters>
        </asp:EntityDataSource>
    </div>
</asp:content>