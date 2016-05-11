<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="ProductsManage.aspx.cs"
    Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.ProductsManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Edit Product</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ProductsList.aspx">
                <i class="icon-list-ul"></i> Product List
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/ProductsAdd.aspx">
                <i class="icon-plus-sign"></i> Add Product
                </asp:HyperLink>
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" ClientIDMode="Static">
                <ContentTemplate>
                    <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="Product Name"
                        RequiredErrorMessage="product name is required" MaxLength="100" />
                    <OWD:TextBoxAdv runat="server" ID="SubTitleTextBox" LabelText="Sub Title" SmallLabelText="Sub name of the product"
                        MaxLength="100" />
                    <OWD:CKEditor runat="server" ID="DescriptionEditor" LabelText="Description" SmallLabelText="detailed description of the product"
                        RequiredErrorMessage="description of the product is required" />
                    <OWD:TextBoxAdv runat="server" ID="PriceTextBox" LabelText="Price" SmallLabelText="price of the product"
                        RequiredErrorMessage="price is required" MaxLength="10" FilterMode="ValidChars"
                        FilterType="Custom" ValidChars="1234567890." />
                    <OWD:TextBoxAdv runat="server" ID="PriceDescriptionTextBox" LabelText="Price Description" SmallLabelText="Description Of The Price" TextMode="MultiLine"
                        MaxLength="250" />
                    <OWD:TextBoxAdv runat="server" ID="AvailableQuantityTextBox" LabelText="Available Quantity"
                        SmallLabelText="quantity of product available" RequiredErrorMessage="available quantity is required"
                        MaxLength="10" FilterMode="ValidChars" FilterType="Custom" ValidChars="1234567890." />
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
                        SmallLabelText="category to which this product belongs" InitialValue="-1"
                        RequiredFieldErrorMessage="please select a category" AutoPostBack="true" OnChange="return OnChange(this);" />
                    <OWD:DropDownListAdv runat="server" ID="SubCategoriesDropDownList" LabelText="Sub Category"
                        SmallLabelText="sub category to which this product belongs" InitialValue="-1"
                        RequiredFieldErrorMessage="please select a sub category" AutoPostBack="true" OnChange="return OnChange(this);" />
                    <OWD:DropDownListAdv runat="server" ID="UnitsDropDownList" LabelText="Unit" SmallLabelText="Unit Of The Product" />
                    <OWD:DropDownListAdv runat="server" ID="ColorsDropDownList" LabelText="Color" SmallLabelText="Color Of The Product" />
                    <OWD:DropDownListAdv runat="server" ID="SizesDropDownList" LabelText="Size" SmallLabelText="Size Of The Product" />
                    <OWD:DropDownListAdv runat="server" ID="ProductTypesDropDownList" LabelText="Product Type" SmallLabelText="Type Of The Product" />
                    <OWD:DropDownListAdv runat="server" ID="ProductModelsDropDownList" LabelText="Product Model" SmallLabelText="Model Of The Product" />
                    <OWD:DropDownListAdv runat="server" ID="AvailabilityTypesDropDownList" LabelText="Availability Type" SmallLabelText="current status of product availability" />
                    <OWD:DropDownListAdv runat="server" ID="BrandsDropDownList" LabelText="Brand" SmallLabelText="brand of the product" />
                    <OWD:TextBoxAdv runat="server" ID="ItemNumberTextBox" LabelText="Item Number" SmallLabelText="Item Number Of The Product" MaxLength="20" RequiredErrorMessage="item number is required" />
                    <OWD:TextBoxAdv runat="server" ID="ModelTextBox" LabelText="Model" SmallLabelText="Model Make or Model Number of the product" MaxLength="20" />
                    <OWD:TextBoxAdv runat="server" ID="ManufacturerTextBox" LabelText="Manufacturer" SmallLabelText="Product Manufacturer Name" MaxLength="50" />
                    <OWD:TextBoxAdv runat="server" ID="MinOQTextBox" LabelText="Minimum Order Quantity (MinOQ)" SmallLabelText="Minimum Allowed Quantity Of Product To Purchase"
                        MaxLength="10" />
                    <OWD:TextBoxAdv runat="server" ID="MaxOQTextBox" LabelText="Maximum Order Quantity (MaxOQ)" SmallLabelText="Maximum Allowed Quantity Of Product To Purchase"
                        MaxLength="10" />
                    <OWD:TextBoxAdv runat="server" ID="TagsTextBox" LabelText="Tags" SmallLabelText="comma separated tags" MaxLength="250" FilterMode="InvalidChars" InvalidChars=" " />
                    <OWD:TextBoxAdv runat="server" ID="SpecialOfferTextBox" LabelText="Special Offer" SmallLabelText="special offer of the product" TextMode="MultiLine"
                        MaxLength="250" />
                    <OWD:TextBoxAdv runat="server" ID="DetailsTextBox" LabelText="Details" SmallLabelText="short detail of anything related to the product" TextMode="MultiLine"
                        MaxLength="250" />
                    <OWD:CheckBoxAdv runat="server" ID="PreOrderFlagCheckBox" LabelText="Pre Order Flag" SmallLabelText="flag to allow pre order of the product"
                        HelpLabelText="check this if users will be allowed to pre order this product" />
                    <OWD:TextBoxAdv runat="server" ID="PreOrderDescriptionTextBox" LabelText="Pre Order Description" SmallLabelText="details of the pre order for this product"
                        MaxLength="250" TextMode="MultiLine" />
                    <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="Product Visibility"
                        HelpLabelText="check this if you want to hide this product from display" />
                    <OWD:CheckBoxAdv runat="server" ID="ShowInCartCheckBox" LabelText="Products Gallery Visibility" SmallLabelText="show in the products gallery"
                        HelpLabelText="check this if you want to show this product in the products gallery" />
                    <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="grid2">
        <div class="onecolumn">
            <div class="header">
                <span><span class="ico color window"></span>Product Images</span>
            </div>
            <div class="Clear">
            </div>
            <div class="content">
                <div class="center">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                        <ContentTemplate>
                            <asp:Image ID="CoverImage" runat="server" CssClass="ManageImage" Width="300px" Height="300px" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <OWD:TextBoxAdv runat="server" ID="AlternateTextTextBox" LabelText="Alternate Text" SmallLabelText="alternate text for the image" RequiredErrorMessage="alternate text for image is required"
                    ValidationGroup="ImageUploadValidationGroup" />
                <OWD:FileUploadAdv runat="server" ID="FileUpload1" LabelText="Image" SmallLabelText="image for the product" MaxFileSizeMB="5" RequiredErrorMessage="please select an image to upload"
                    ValidationGroup="ImageUploadValidationGroup" FieldWidth="full" />
                <OWD:FormToolbar runat="server" ID="ImageUploadFormToolBar" ShowSave="true" ValidationGroup="ImageUploadValidationGroup" />
                <OWD:StatusMessageJQuery runat="server" ID="StatusMessage2" />
                <div class="GridSection">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductImageID" DataSourceID="EntityDataSource1"
                                OnRowCommand="ProductImagesGridView_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" rel="fancybox" NavigateUrl='<%#Eval("ImageURL") %>'
                                                ToolTip='<%#String.Format("{0}", Eval("AlternateText")) %>'>
                                                <asp:Image ImageUrl='<%#Eval("ImageThumbURL") %>' runat="server" Height="100px" Width="100px" Style="margin: 5px 0px 5px 0px;" />
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeaderStyle" />
                                        <ItemStyle CssClass="GridItemStyle" />
                                        <ControlStyle CssClass="GridControlStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alternate Text" SortExpression="AlternateText">
                                        <ItemTemplate>
                                            <%#Eval("AlternateText") %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeaderStyle" />
                                        <ItemStyle CssClass="GridItemStyle" />
                                        <ControlStyle CssClass="GridControlStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ImageAlign="Middle" ImageUrl="~/Themes/Zice/Graphics/icon/icon_delete.png"
                                                runat="server" CommandName="Cancel" CommandArgument='<%#Eval("ProductImageID") %>' Text="Delete" ToolTip="Delete" CausesValidation="False"
                                                OnClientClick="return confirm('Are you sure you want to delete this image?');" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeaderStyle" />
                                        <ItemStyle CssClass="GridItemStyle" />
                                        <ControlStyle CssClass="GridControlStyle" />
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
        </div>
    </div>
    <div class="grid2">
        <div class="onecolumn">
            <div class="header">
                <span><span class="ico color window"></span>Product Icons</span>
            </div>
            <div class="Clear">
            </div>
            <div class="content">
            </div>
        </div>
    </div>
    <br class="Clear" />
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Product Highlights</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
        </div>
    </div>
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Related Products</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="GridView" DataKeyNames="ProductID"
                        DataSourceID="EntityDataSource2" AllowPaging="true" AllowSorting="true">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# ProjectJKL.BLL.ShoppingCartBLL.ProductImagesBL.GetProductFirstImageThumb((int)Eval("ProductID")) %>'
                                        AlternateText='<%#Eval("Title") %>' Height="100px" Width="100px" Style="margin: 5px 0px 5px 0px;" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title" SortExpression="Title">
                                <ItemTemplate>
                                    <%#Eval("Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" Width="200px" />
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
                            <%--  <asp:TemplateField HeaderText="Available Quantity" SortExpression="AvailableQuantity">
                                <ItemTemplate>
                                    <%#Eval("AvailableQuantity") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Availablity" SortExpression="AvailabilityTypeID">
                                <ItemTemplate>
                                    <%#Eval("SC_AvailabilityTypes.Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section" SortExpression="SectionID">
                                <ItemTemplate>
                                    <%#Eval("SC_Sections.Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category" SortExpression="CategoryID">
                                <ItemTemplate>
                                    <%#Eval("SC_Categories.Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sub Category" SortExpression="SubCategoryID">
                                <ItemTemplate>
                                    <%#Eval("SC_SubCategories.Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("ProductsManage.aspx?ProductID={0}", Eval("ProductID")) %>'>
                                            <i class="icon-cog"></i> Manage Product
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
    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=ShoppingCartEntities" DefaultContainerName="ShoppingCartEntities"
        EnableFlattening="False" EntitySetName="SC_ProductImages" Where="it.ProductID==@ProductID">
        <WhereParameters>
            <asp:QueryStringParameter Name="ProductID" DbType="Int32" QueryStringField="ProductID" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="EntityDataSource2" runat="server" ConnectionString="name=ShoppingCartEntities"
        DefaultContainerName="ShoppingCartEntities" EnableFlattening="False" EntitySetName="SC_Products" Include="SC_AvailabilityTypes, SC_Categories, SC_Subcategories, SC_Sections"
        OrderBy="it.UploadedOn Desc" Where="it.SectionID==@SectionID && it.CategoryID==@CategoryID && it.SubCategoryID==@SubCategoryID && it.ItemNumber==@ItemNumber && it.ProductID!=@ProductID"
        EntityTypeFilter="" Select="">
        <WhereParameters>
            <asp:ControlParameter ControlID="SectionIDHiddenField" DbType="Int32" Name="SectionID" PropertyName="Value" />
            <asp:ControlParameter ControlID="CategoryIDHiddenField" DbType="Int32" Name="CategoryID" PropertyName="Value" />
            <asp:ControlParameter ControlID="SubCategoryIDHiddenField" DbType="Int32" Name="SubCategoryID" PropertyName="Value" />
            <asp:ControlParameter ControlID="ItemNumberHiddenField" DbType="String" Name="ItemNumber" PropertyName="Value" />
            <asp:QueryStringParameter Name="ProductID" DbType="Int32" QueryStringField="ProductID" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:HiddenField runat="server" ID="ItemNumberHiddenField" />
    <asp:HiddenField runat="server" ID="SectionIDHiddenField" />
    <asp:HiddenField runat="server" ID="CategoryIDHiddenField" />
    <asp:HiddenField runat="server" ID="SubCategoryIDHiddenField" />
</asp:content>