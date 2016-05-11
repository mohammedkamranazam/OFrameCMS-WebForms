<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductCartGridComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.ShoppingCart.ProductCartGridComponent" %>

<div class="CartGridComponent">
    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
    <asp:Panel runat="server" ID="CartHeaderPanel" CssClass="RowHeader">
        <div class="ImageHeader">
            &nbsp;
        </div>
        <div class="TitleHeader">
            Product
        </div>
        <div class="ItemNumberHeader">
            Details
        </div>
        <div class="QuantityHeader">
            Quantity
        </div>
        <div class="OriginalPriceHeader">
            Original Price
        </div>
        <div class="DiscountPriceHeader">
            Discount Price
        </div>
        <div class="ToolsHeader">
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="CartGridPanel" CssClass="CartGridPanel">
        <asp:GridView ID="GridView1" runat="server" CssClass="CartGridView" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancellingEdit"
            OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" ShowHeader="False"
            BorderStyle="None" GridLines="Horizontal">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="Row">
                            <asp:Image ID="Image1" runat="server" CssClass="ProductImage" ImageUrl='<%# ProjectJKL.BLL.ShoppingCartBLL.ProductImagesBL.GetProductFirstImageThumb((int)Eval("ProductID")) %>' />
                            <div class="ProductTitle">
                                <span><%#Eval("SC_Products.Title") %></span>
                                <small><%#Eval("SC_Products.SubTitle") %></small>
                            </div>
                            <div class="ItemNumber">
                                <span class="ItemNumber"><strong>Itm. No.: </strong><%#Eval("SC_Products.ItemNumber") %></span>
                                <%#ProjectJKL.BLL.ShoppingCartBLL.ColorsBL.GetColor((int)Eval("ProductID")) %>
                                <%#ProjectJKL.BLL.ShoppingCartBLL.SizesBL.GetSize((int)Eval("ProductID")) %>
                            </div>
                            <div class="Quantity">
                                <span class="Text"><%#Eval("Quantity") %></span>
                                <span class="UpdateTools">
                                    <asp:Button ID="EditButton" CssClass="EditButton" runat="server" CausesValidation="false" CommandName="Edit" Visible='<%# !ProjectJKL.BLL.ShoppingCartBLL.CartProcessingBL.IsProductOutOfStock((ProjectJKL.AppCode.DAL.ShoppingCartModel.SC_Products)Eval("SC_Products")) %>' />
                                </span>
                                <%#(ProjectJKL.BLL.ShoppingCartBLL.CartProcessingBL.IsProductOutOfStock((ProjectJKL.AppCode.DAL.ShoppingCartModel.SC_Products)Eval("SC_Products"))) ? "<span style='color:red; font-weight:bold; font-family:verdana; font-size:11px; text-align:center; display:block; line-height:15px;'>Out of Stock</span>": "" %>
                            </div>
                            <div class="OriginalPrice">
                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Quantity")) * Convert.ToDouble(Eval("SC_Products.Price")) ) %>
                            </div>
                            <div class="DiscountPrice">
                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Quantity")) * ProjectJKL.BLL.ShoppingCartBLL.ProductsBL.GetPrice((int)Eval("ProductID")) ) %>
                            </div>
                            <div class="Tools">
                                <asp:HiddenField ID="CartIDHiddenField" runat="server" Value='<%# Eval("CartID") %>' />
                                <asp:Button ID="Button2" CssClass="DeleteButton" runat="server" CausesValidation="false" CommandName="Delete" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="Row">
                            <asp:Image ID="Image2" runat="server" CssClass="ProductImage" ImageUrl='<%# ProjectJKL.BLL.ShoppingCartBLL.ProductImagesBL.GetProductFirstImageThumb((int)Eval("ProductID")) %>' />
                            <div class="ProductTitle">
                                <span><%#Eval("SC_Products.Title") %></span>
                                <small><%#Eval("SC_Products.SubTitle") %></small>
                            </div>
                            <div class="ItemNumber">
                                <span class="ItemNumber"><strong>Itm. No.: </strong><%#Eval("SC_Products.ItemNumber") %></span>
                                <%#ProjectJKL.BLL.ShoppingCartBLL.ColorsBL.GetColor((int)Eval("ProductID")) %>
                                <%#ProjectJKL.BLL.ShoppingCartBLL.SizesBL.GetSize((int)Eval("ProductID")) %>
                            </div>
                            <div class="Quantity">
                                <span class="Field">
                                    <asp:TextBox runat="server" CssClass="ChangeQuantityTextBox" MaxLength="10" ID="EditQuantityTextBox" Text='<%#Eval("Quantity") %>' ValidationGroup="CartUpdateGroup" />
                                </span>
                                <span class="Message">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="Error" runat="server" ControlToValidate="EditQuantityTextBox" SetFocusOnError="true"
                                        Display="Dynamic" ErrorMessage="*" ValidationGroup="CartUpdateGroup" />
                                    <asp:CompareValidator ID="CompareValidator1" CssClass="Error" runat="server" ErrorMessage="quantity not available"
                                        Display="Dynamic" ValueToCompare='<%# Eval("SC_Products.AvailableQuantity") %>' SetFocusOnError="True" Operator="LessThanEqual" ControlToValidate="EditQuantityTextBox"
                                        Type="Double" ValidationGroup="CartUpdateGroup" />
                                    <asp:CompareValidator ID="CompareValidator2" CssClass="Error" runat="server" ErrorMessage='<%# String.Format("minimum allowed quantity: {0}", Eval("SC_Products.MinOQ")) %>'
                                        Display="Dynamic" ValueToCompare='<%# Eval("SC_Products.MinOQ") %>' SetFocusOnError="True" Operator="GreaterThanEqual" ControlToValidate="EditQuantityTextBox"
                                        Type="Double" ValidationGroup="CartUpdateGroup" />
                                    <asp:CompareValidator ID="CompareValidator4" CssClass="Error" runat="server" ErrorMessage='<%# String.Format("maximum allowed quantity: {0}", Eval("SC_Products.MaxOQ")) %>'
                                        Display="Dynamic" ValueToCompare='<%# Eval("SC_Products.MaxOQ") %>' SetFocusOnError="True" Operator="LessThanEqual" ControlToValidate="EditQuantityTextBox"
                                        Type="Double" ValidationGroup="CartUpdateGroup" />
                                    <asp:CompareValidator ID="CompareValidator3" CssClass="Error" runat="server" ErrorMessage="cannot be zero" Display="Dynamic" ValueToCompare="0"
                                        SetFocusOnError="True" Operator="GreaterThanEqual" ControlToValidate="EditQuantityTextBox" Type="Double" ValidationGroup="CartUpdateGroup" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="Error" runat="server" ErrorMessage='<%#OWDARO.Validator.FloatValidationErrorMessage %>'
                                        SetFocusOnError="true" Display="Dynamic" ControlToValidate="EditQuantityTextBox" ValidationGroup="CartUpdateGroup" ValidationExpression='<%# OWDARO.Validator.FloatValidationExpression %>' />
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="EditQuantityTextBox" runat="server" FilterMode="ValidChars" ValidChars="1234567890." />
                                </span>
                                <span class="UpdateTools">
                                    <asp:Button ID="Button3" runat="server" CssClass="UpdateButton" CommandName="Update" ValidationGroup="CartUpdateGroup" />
                                    <asp:Button ID="Button4" runat="server" CssClass="CancelButton" CausesValidation="false" CommandName="Cancel" />
                                    <asp:HiddenField ID="ProductIDHiddenField" runat="server" Value='<%# Eval("ProductID") %>' />
                                </span>
                            </div>
                            <div class="OriginalPrice">
                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Quantity")) * Convert.ToDouble(Eval("SC_Products.Price")) ) %>
                            </div>
                            <div class="DiscountPrice">
                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Quantity")) * ProjectJKL.BLL.ShoppingCartBLL.ProductsBL.GetPrice((int)Eval("ProductID")) ) %>
                            </div>
                            <div class="Tools">
                            </div>
                        </div>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <div class="EmtyCartPlaceHolder">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Products.aspx"></asp:HyperLink>
                </div>
            </EmptyDataTemplate>
        </asp:GridView>
    </asp:Panel>
    <div class="Footer" runat="server" id="SubTotalAmountDiv">
        <asp:Label runat="server" ID="CartSubTotalAmountLabel" CssClass="CartSubTotalAmountLabel"></asp:Label>
        <asp:Label runat="server" ID="CartSubTotalAmountTextLabel" CssClass="CartSubTotalAmountTextLabel"></asp:Label>
    </div>
    <div class="Footer" runat="server" id="DiscountAmountDiv">
        <asp:Label runat="server" ID="CartDiscountAmountLabel" CssClass="CartDiscountAmountLabel"></asp:Label>
        <asp:Label runat="server" ID="CartDiscountAmountTextLabel" CssClass="CartDiscountAmountTextLabel"></asp:Label>
    </div>
    <div class="Footer" runat="server" id="PromoCodeDiscountAmountDiv">
        <asp:Label runat="server" ID="CartPromoCodeDiscountAmountLabel" CssClass="CartPromoCodeDiscountAmountLabel"></asp:Label>
        <asp:Label runat="server" ID="CartPromoCodeDiscountAmountTextLabel" CssClass="CartPromoCodeDiscountAmountTextLabel"></asp:Label>
    </div>
    <div class="Footer" runat="server" id="TotalAmountDiv">
        <asp:Label runat="server" CssClass="CartTotalAmountLabel" ID="CartTotalAmountLabel"></asp:Label>
        <asp:Label runat="server" ID="CartTotalAmountTextLabel" CssClass="CartTotalAmountTextLabel"></asp:Label>
    </div>
    <div class="Footer">
        <asp:HyperLink runat="server" NavigateUrl="~/UI/Pages/Open/ShoppingCart/CheckOut/CheckOutStep1.aspx" CssClass="CheckOutLink" ID="CheckOutLink"
            Text="Check Out"></asp:HyperLink>
        <asp:Button runat="server" ID="CartRefreshButton" CssClass="CartRefreshButton" OnClick="CartRefreshButton_Click" />
    </div>
</div>