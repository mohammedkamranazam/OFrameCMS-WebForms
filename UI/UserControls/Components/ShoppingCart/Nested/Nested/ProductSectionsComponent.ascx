<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductSectionsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.ShoppingCart.ProductSectionsComponent" %>

<ul class='<%: SectionsULCssClassHiddenField.Value %>'>
    <asp:Repeater ID="SectionsRepeater" runat="server">
        <ItemTemplate>
            <li class='<%# String.Format("{0} {1}", SectionLICssClassHiddenField.Value, (( OWDARO.Util.DataParser.IntParse((string.IsNullOrWhiteSpace(Request.QueryString["SectionID"])) ? "-1" : Request.QueryString["SectionID"], false) == (int)Eval("SectionID")) ? SectionLISelectedCssClassHiddenField.Value : "")) %>'>
                <asp:HyperLink runat="server" CssClass='<%#SectionAnchorCssClassHiddenField.Value %>' Text='<%#Eval("Title") %>' ToolTip='<%#Eval("Title") %>'
                    NavigateUrl='<%# String.Format("~/Products.aspx?SectionID={0}", Eval("SectionID")) %>' />
                <OWD:ProductCategoriesComponent ID="ProductCategories1" runat="server" SectionID='<%#Eval("SectionID") %>'
                    CategoryAnchroCssClass='<%# CategoryAnchorCssClassHiddenField.Value %>'
                    CategoriesULCssClass='<%# CategoriesULCssClassHiddenField.Value %>' CategoryLICssClass='<%# CategoryLICssClassHiddenField.Value %>'
                    CategoryLISelectedCssClass='<%# CategoryLISelectedCssClassHiddenField.Value %>'
                    SubCategoryAnchroCssClass='<%# SubCategoryAnchorCssClassHiddenField.Value %>'
                    SubCategoriesULCssClass='<%# SubCategoriesULCssClassHiddenField.Value %>' SubCategoryLICssClass='<%# SubCategoryLICssClassHiddenField.Value %>'
                    SubCategoryLISelectedCssClass='<%# SubCategoryLISelectedCssClassHiddenField.Value %>' CategoriesVisible='<%# OWDARO.Util.DataParser.BoolParse(CategoriesVisibleHiddenField.Value) %>'
                    SubCategoriesVisible='<%# OWDARO.Util.DataParser.BoolParse(SubCategoriesVisibleHiddenField.Value) %>' />
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
<asp:HiddenField runat="server" ID="SectionsULCssClassHiddenField" Visible="false" Value="ProductSections" />
<asp:HiddenField runat="server" ID="SectionLICssClassHiddenField" Visible="false" Value="Section" />
<asp:HiddenField runat="server" ID="SectionLISelectedCssClassHiddenField" Visible="false" Value="Selected" />
<asp:HiddenField runat="server" ID="SectionAnchorCssClassHiddenField" Visible="false" Value="SectionTitle" />

<asp:HiddenField runat="server" ID="CategoriesULCssClassHiddenField" Visible="false" Value="ProductCategories" />
<asp:HiddenField runat="server" ID="CategoryLICssClassHiddenField" Visible="false" Value="Category" />
<asp:HiddenField runat="server" ID="CategoryLISelectedCssClassHiddenField" Visible="false" Value="Selected" />
<asp:HiddenField runat="server" ID="CategoryAnchorCssClassHiddenField" Visible="false" Value="CategoryTitle" />

<asp:HiddenField runat="server" ID="SubCategoriesULCssClassHiddenField" Visible="false" Value="ProductSubCategories" />
<asp:HiddenField runat="server" ID="SubCategoryLICssClassHiddenField" Visible="false" Value="SubCategory" />
<asp:HiddenField runat="server" ID="SubCategoryLISelectedCssClassHiddenField" Visible="false" Value="Selected" />
<asp:HiddenField runat="server" ID="SubCategoryAnchorCssClassHiddenField" Visible="false" Value="SubCategoryTitle" />

<asp:HiddenField runat="server" ID="CategoriesVisibleHiddenField" Visible="false" Value="True" />
<asp:HiddenField runat="server" ID="SubCategoriesVisibleHiddenField" Visible="false" Value="True" />