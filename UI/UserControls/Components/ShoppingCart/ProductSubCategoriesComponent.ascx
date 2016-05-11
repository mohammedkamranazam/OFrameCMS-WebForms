<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductSubCategoriesComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.ShoppingCart.ProductSubCategoriesComponent" %>

<ul class='<%# SubCategoriesULCssClassHiddenField.Value %>' runat="server" id="SubCategoriesUL">
    <asp:Repeater ID="SubCategoriesRepeater" runat="server">
        <ItemTemplate>
            <li class='<%# String.Format("{0} {1}", SubCategoryLICssClassHiddenField.Value , (( OWDARO.Util.DataParser.IntParse((string.IsNullOrWhiteSpace(Request.QueryString["SubCategoryID"])) ? "-1" : Request.QueryString["SubCategoryID"], false) == (int)Eval("SubCategoryID")) ? SubCategoryLISelectedCssClassHiddenField.Value : "")) %>'>
                <asp:HyperLink ID="SubCategoryHyperLink" runat="server" CssClass='<%# SubCategoryAnchorCssClassHiddenField.Value %>' Text='<%#Eval("Title") %>'
                    ToolTip='<%#Eval("Title") %>'
                    NavigateUrl='<%# String.Format("~/Products.aspx?SubCategoryID={0}", Eval("SubCategoryID")) %>' />
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
<asp:HiddenField runat="server" ID="SubCategoriesULCssClassHiddenField" Visible="false" Value="ProductSubCategories" />
<asp:HiddenField runat="server" ID="SubCategoryLICssClassHiddenField" Visible="false" Value="SubCategory" />
<asp:HiddenField runat="server" ID="SubCategoryLISelectedCssClassHiddenField" Visible="false" Value="Selected" />
<asp:HiddenField runat="server" ID="SubCategoryAnchorCssClassHiddenField" Visible="false" Value="SubCategoryTitle" />