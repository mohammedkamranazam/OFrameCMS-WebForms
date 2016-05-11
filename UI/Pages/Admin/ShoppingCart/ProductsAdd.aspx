<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="ProductsAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.ProductsAdd" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add New Product</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ProductsList.aspx">
                <i class="icon-list-ul"></i> Product List
                </asp:HyperLink>
            </div>
            <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="Product Name"
                RequiredErrorMessage="product name is required" MaxLength="100" />
            <OWD:CKEditor runat="server" ID="DescriptionEditor" LabelText="Description" SmallLabelText="detailed description of the product"
                RequiredErrorMessage="description of the product is required" />
            <OWD:TextBoxAdv runat="server" ID="PriceTextBox" LabelText="Price" SmallLabelText="price of the product"
                RequiredErrorMessage="price is required" MaxLength="10" FilterMode="ValidChars"
                FilterType="Custom" ValidChars="1234567890." />
            <OWD:TextBoxAdv runat="server" ID="AvailableQuantityTextBox" LabelText="Available Quantity"
                SmallLabelText="quantity of product available" RequiredErrorMessage="available quantity is required"
                MaxLength="10" FilterMode="ValidChars" FilterType="Custom" ValidChars="1234567890." />
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" ClientIDMode="Static">
                <ContentTemplate>
                    <OWD:CheckBoxAdv runat="server" ID="HasDiscountCheckBox" LabelText="Have Discount"
                        SmallLabelText="if product have any discount" HelpLabelText="check this if product have any discount"
                        AutoPostBack="true" />
                    <asp:Panel runat="server" ID="DiscountFieldsDiv" Visible="false">
                        <OWD:TextBoxAdv runat="server" ID="DiscountPercentageTextBox" LabelText="Discount Percentage"
                            SmallLabelText="percentage of discount given on this product" RequiredErrorMessage="discount percentage is required"
                            MaxLength="3" RangeType="Double" MinValue="0" MaxValue="100" RangeErrorMessage="value between 0 and 100"
                            FilterMode="ValidChars" FilterType="Custom" ValidChars="1234567890." />
                        <OWD:TextBoxAdv runat="server" ID="DiscountAmountTextBox" LabelText="Discount Amount"
                            SmallLabelText="discount given as amount on this product" RequiredErrorMessage="discount amount is required"
                            MaxLength="10" FilterMode="ValidChars" FilterType="Custom" ValidChars="1234567890." />
                    </asp:Panel>
                    <OWD:DropDownListAdv runat="server" ID="SectionsDropDownList" LabelText="Section"
                        SmallLabelText="section to which this product belongs" InitialValue="-1" RequiredFieldErrorMessage="please select a section"
                        AutoPostBack="true" OnChange="return OnChange(this);" />
                    <OWD:DropDownListAdv runat="server" ID="CategoriesDropDownList" LabelText="Category"
                        SmallLabelText="category to which this product belongs" Visible="false" InitialValue="-1"
                        RequiredFieldErrorMessage="please select a category" AutoPostBack="true" OnChange="return OnChange(this);" />
                    <OWD:DropDownListAdv runat="server" ID="SubCategoriesDropDownList" LabelText="Sub Category"
                        SmallLabelText="sub category to which this product belongs" Visible="false" InitialValue="-1"
                        RequiredFieldErrorMessage="please select a sub category" AutoPostBack="true" OnChange="return OnChange(this);" />
                    <OWD:DropDownListAdv runat="server" ID="UnitsDropDownList" LabelText="Unit" SmallLabelText="Unit Of The Product" Visible="false" />
                    <OWD:DropDownListAdv runat="server" ID="ColorsDropDownList" LabelText="Color" SmallLabelText="Color Of The Product" Visible="false" />
                    <OWD:DropDownListAdv runat="server" ID="SizesDropDownList" LabelText="Size" SmallLabelText="Size Of The Product" Visible="false" />
                    <OWD:DropDownListAdv runat="server" ID="ProductTypesDropDownList" LabelText="Product Type" SmallLabelText="Type Of The Product" Visible="false" />
                    <OWD:DropDownListAdv runat="server" ID="ProductModelsDropDownList" LabelText="Product Model" SmallLabelText="Model Of The Product" Visible="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <OWD:DropDownListAdv runat="server" ID="AvailabilityTypesDropDownList" LabelText="Availability Type" SmallLabelText="current status of product availability" />
            <OWD:DropDownListAdv runat="server" ID="BrandsDropDownList" LabelText="Brand" SmallLabelText="brand of the product" />
            <OWD:TextBoxAdv runat="server" ID="ItemNumberTextBox" LabelText="Item Number" SmallLabelText="Item Number Of The Product" MaxLength="20" RequiredErrorMessage="item number is required" />
            <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="Product Visibility"
                HelpLabelText="check this if you want to hide this product from display" />
            <OWD:CheckBoxAdv runat="server" ID="ShowInCartCheckBox" LabelText="Products Gallery Visibility" SmallLabelText="show in the products gallery"
                HelpLabelText="check this if you want to show this product in the products gallery" />
            <OWD:FileUploadAdv runat="server" ID="FileUpload1" LabelText="Image" SmallLabelText="Product Image For Display"
                MaxFileSizeMB="5" RequiredErrorMessage="please select an image for the product" />
            <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true"
                ShowCustom="true" CustomButtonText="Save and Continue To Manage" />
            <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
        </div>
    </div>
</asp:content>