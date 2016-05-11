<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.ShoppingCart.ProductsComponent" %>

<div class="ProductsSearchBox">
    <asp:TextBox runat="server" ID="SearchTextBox" CssClass="SearchTextField" ValidationGroup="ProductSearchValidationGroup" placeholder="search for products"></asp:TextBox>
    <asp:Button runat="server" ID="SearchButton" CssClass="SearchButton" OnClick="SearchButton_Click" ValidationGroup="ProductSearchValidationGroup" />
    <asp:RequiredFieldValidator runat="server" ControlToValidate="SearchTextBox" Display="Dynamic" SetFocusOnError="true" ValidationGroup="ProductSearchValidationGroup"></asp:RequiredFieldValidator>
</div>
<h1 class="PageTitle">
    <asp:Literal runat="server" ID="TitleLiteral"></asp:Literal>
</h1>
<asp:Panel runat="server" ID="ProductsPanel" CssClass="Products">
    <asp:DataList ID="ProductsRepeater" runat="server" RepeatDirection="Horizontal" ShowFooter="False" ShowHeader="False">
        <ItemTemplate>
            <div class="Product">
                <div class="ProductMouseOver">
                    <asp:HyperLink ID="HyperLink1" CssClass="MoreInfo" runat="server" Text="More Info"
                        NavigateUrl='<%# String.Format("~/Product.aspx?ProductID={0}", Eval("ProductID")) %>' />
                    <asp:HyperLink ID="HyperLink3" CssClass="AddToCart" runat="server" Text="Add To Cart"
                        NavigateUrl='<%# String.Format("~/Cart.aspx?ProductID={0}&InCart=1", Eval("ProductID")) %>' Visible='<%# ( ProjectJKL.BLL.ShoppingCartBLL.ProductsBL.IsOutOfStock((int)Eval("ProductID")) || (bool)Eval("PreOderFlag") ) ? false : true  %>' />
                </div>
                <%# ProjectJKL.BLL.ShoppingCartBLL.ProductsBL.GetHasDiscountText((int)Eval("ProductID")) %>
                <asp:Image ID="Image1" CssClass="SpecialOffer" runat="server" Visible='<%# ( !string.IsNullOrWhiteSpace((string)Eval("SpecialOffer")) ) ? true:false  %>'
                    ImageUrl="~/Themes/Default/Graphics/offer.png" />
                <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("~/Product.aspx?ProductID={0}", Eval("ProductID")) %>'>
                            <img class="ThumbnailImage" id='<%# String.Format("RollOverImage-{0}", Eval("ProductID")) %>' src='<%# ResolveClientUrl(ProjectJKL.BLL.ShoppingCartBLL.ProductImagesBL.GetProductFirstImageThumb((int)Eval("ProductID"))) %>' alt='<%#Eval("Title") %>' />
                </asp:HyperLink>
                <div class="BadgeDiv">
                    <asp:Image ID="Image2" CssClass="HotItem" runat="server" Visible='<%# ProjectJKL.BLL.ShoppingCartBLL.ProductsBL.IsHotItem((double)Eval("SoldOutCount"))  %>'
                        ImageUrl="~/Themes/Default/Graphics/hotitem-badge.png"></asp:Image>
                    <asp:Image ID="Image3" CssClass="NewArrival" runat="server" Visible='<%# ProjectJKL.BLL.ShoppingCartBLL.ProductsBL.IsNewProduct((DateTime)Eval("UploadedOn"))  %>'
                        ImageUrl="~/Themes/Default/Graphics/new-badge.png"></asp:Image>
                    <div class="Clear"></div>
                </div>
                <asp:HyperLink ID="HyperLink2" CssClass="Title" runat="server" Text='<%#Eval("Title") %>' NavigateUrl='<%# String.Format("~/Product.aspx?ProductID={0}", Eval("ProductID")) %>' />
                <div class="Rating" runat="server" visible='<%# ProjectJKL.BLL.ShoppingCartBLL.RatingsBL.ShowRating((int)Eval("ProductID"))  %>'>
                    <asp:Rating ID="Rating1" runat="server" CurrentRating='<%# ProjectJKL.BLL.ShoppingCartBLL.RatingsBL.CurrentRating((int)Eval("ProductID"))  %>'
                        ReadOnly="True" EmptyStarCssClass="EmptyStarRating" FilledStarCssClass="FilledStarRating" StarCssClass="StarRating" WaitingStarCssClass="WaitingStarRating">
                    </asp:Rating>
                    <span class="ReviewsCount"><%# String.Format("({0} Reviews)", ProjectJKL.BLL.ShoppingCartBLL.ReviewsBL.GetReviewsCount((int)Eval("ProductID"))) %></span>
                </div>
                <div class="FewDetails">
                    <div>
                        <%# ProjectJKL.BLL.ShoppingCartBLL.ProductsBL.GetPriceText((int)Eval("ProductID")) %>
                    </div>
                    <div class="Highlights">
                        <OWD:ProductHighlightsComponent runat="server" ProductID='<%#Eval("ProductID") %>' Count="3" />
                    </div>
                    <div class="AvailabilityType" style='<%# String.Format("color:{0};", Eval("SC_AvailabilityTypes.ColorName")) %>'>
                        <%# Eval("SC_AvailabilityTypes.Title") %>
                    </div>
                    <span class="OutOfStock" runat="server" visible='<%# ProjectJKL.BLL.ShoppingCartBLL.ProductsBL.IsOutOfStock((int)Eval("ProductID"))  %>'>Out Of
                        Stock
                    </span>
                </div>
                <OWD:ProductColorsComponent runat="server" ID="ProductColorsComponent1" HideTitle="True" ProductID='<%#Eval("ProductID") %>' />
            </div>
        </ItemTemplate>
    </asp:DataList>
    <div class="Clear"></div>
    <asp:Button runat="server" ID="LoadMoreButton" Text="Load More" OnClick="LoadMoreButton_Click" CssClass="LoadMoreButton" />
    <asp:HiddenField runat="server" ID="CurrentCountHiddenField" Value="20" />
</asp:Panel>
<asp:Panel runat="server" ID="EmptyDataPanel" CssClass="EmptyProducts">
    <asp:Image ID="Image4" runat="server" CssClass="EmptyProductsImage" ImageUrl="~/Themes/Default/Graphics/nothinghere.png" />
</asp:Panel>