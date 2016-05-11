<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductCategoriesComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.ShoppingCart.ProductCategoriesComponent" %>

<ul class='<%# CategoriesULCssClassHiddenField.Value %>' runat="server" id="CategoriesUL">
    <asp:Repeater ID="CategoriesRepeater" runat="server">
        <ItemTemplate>
            <li class='<%# String.Format("{0} {1}", CategoryLICssClassHiddenField.Value, (( OWDARO.Util.DataParser.IntParse((string.IsNullOrWhiteSpace(Request.QueryString["CategoryID"])) ? "-1" : Request.QueryString["CategoryID"], false) == (int)Eval("CategoryID")) ? CategoryLISelectedCssClassHiddenField.Value : "")) %>'>
                <asp:HyperLink runat="server" CssClass='<%# CategoryAnchorCssClassHiddenField.Value %>' Text='<%#Eval("Title") %>' ToolTip='<%#Eval("Title") %>'
                    NavigateUrl='<%# String.Format("~/Products.aspx?CategoryID={0}", Eval("CategoryID")) %>' />
                <OWD:ProductSubCategoriesComponent ID="ProductSubCategories1" runat="server" CategoryID='<%#Eval("CategoryID") %>' SubCategoryAnchroCssClass='<%# SubCategoryAnchorCssClassHiddenField.Value %>'
                    SubCategoriesULCssClass='<%# SubCategoriesULCssClassHiddenField.Value %>' SubCategoryLICssClass='<%# SubCategoryLICssClassHiddenField.Value %>'
                    SubCategoryLISelectedCssClass='<%# SubCategoryLISelectedCssClassHiddenField.Value %>' SubCategoriesVisible='<%# OWDARO.Util.DataParser.BoolParse(SubCategoriesVisibleHiddenField.Value)  %>' />
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
<asp:HiddenField runat="server" ID="CategoriesULCssClassHiddenField" Visible="false" Value="ProductCategories" />
<asp:HiddenField runat="server" ID="CategoryLICssClassHiddenField" Visible="false" Value="Category" />
<asp:HiddenField runat="server" ID="CategoryLISelectedCssClassHiddenField" Visible="false" Value="Selected" />
<asp:HiddenField runat="server" ID="CategoryAnchorCssClassHiddenField" Visible="false" Value="CategoryTitle" />

<asp:HiddenField runat="server" ID="SubCategoriesULCssClassHiddenField" Visible="false" Value="ProductSubCategories" />
<asp:HiddenField runat="server" ID="SubCategoryLICssClassHiddenField" Visible="false" Value="SubCategory" />
<asp:HiddenField runat="server" ID="SubCategoryLISelectedCssClassHiddenField" Visible="false" Value="Selected" />
<asp:HiddenField runat="server" ID="SubCategoryAnchorCssClassHiddenField" Visible="false" Value="SubCategoryTitle" />

<asp:HiddenField runat="server" ID="SubCategoriesVisibleHiddenField" Visible="false" Value="True" />