<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetailsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.ShoppingCart.ProductDetailsComponent" %>

<div class="ProductDetailsDiv">
    <div class="LeftDiv">
        <OWD:ProductImagesComponent ID="ProductImagesComponent1" runat="server"></OWD:ProductImagesComponent>
        <OWD:ProductIconsComponent ID="ProductIconsComponent1" runat="server"></OWD:ProductIconsComponent>
    </div>
    <div class="RightDiv">
        <div class="BadgeDiv">
            <asp:Image ID="HotItemImage" CssClass="HotItem" runat="server" ImageUrl="~/Themes/Default/Graphics/hotitem-badge.png"></asp:Image>
            <asp:Image ID="NewArrivalImage" CssClass="NewArrival" runat="server" ImageUrl="~/Themes/Default/Graphics/new-badge.png"></asp:Image>
            <div class="Clear"></div>
        </div>
        <h1 class="Title">
            <asp:Literal runat="server" ID="TitleLiteral"></asp:Literal>
        </h1>
        <h3 class="SubTitle">
            <asp:Literal runat="server" ID="SubTitleLiteral"></asp:Literal>
        </h3>
        <div class="Highlights">
            <OWD:ProductHighlightsComponent runat="server" ID="ProductHighlightsComponent1" />
        </div>
        <div class="ColorsDiv">
            <OWD:ProductColorsComponent ID="ProductColorsComponent1" runat="server" />
        </div>
        <div class="SizesDiv">
            <OWD:ProductSizesComponent ID="ProductSizesComponent1" runat="server" />
        </div>
        <div class="StatisticsDiv">
            <span class="Rating">
                <asp:Rating ID="Rating1" runat="server" ReadOnly="True" EmptyStarCssClass="EmptyStarRating" FilledStarCssClass="FilledStarRating"
                    StarCssClass="StarRating" WaitingStarCssClass="WaitingStarRating">
                </asp:Rating>
            </span>
            <span class="Reviews">
                <asp:Literal runat="server" ID="ReviewsCountLiteral"></asp:Literal></span>
            <span class="Likes">Likes:
                <asp:Literal runat="server" ID="LikesLiteral"></asp:Literal>
            </span>
            <span class="Socials"></span>
        </div>
        <div class="DataDiv">
            <div class="LeftDataDiv">
                <div class="PriceDiv">
                    <asp:Literal runat="server" ID="PriceLiteral"></asp:Literal>
                    <div class="PriceDescription" runat="server" id="PriceDescriptionDiv">
                        <asp:Literal runat="server" ID="PriceDescriptionLiteral"></asp:Literal>
                    </div>
                </div>
                <div class="AvailabilityDiv" runat="server" id="AvailabilityDiv">
                    <div class="AvailabilityTypeDiv" runat="server" id="AvailabilityTypeDiv">
                        <asp:Literal runat="server" ID="AvailabilityTypeLiteral"></asp:Literal>
                    </div>
                    <div class="AvailabilityDescriptionDiv" runat="server" id="AvailabilityTypeDescriptionDiv">
                        <asp:Literal runat="server" ID="AvailabilityDescriptionLiteral"></asp:Literal>
                    </div>
                </div>
                <div class="DetailsDiv" runat="server" id="DetailsDiv">
                    <asp:Literal runat="server" ID="DetailsLiteral"></asp:Literal>
                </div>
            </div>
            <div class="RightDataDiv">
                <span id="OutOfStockSpan" class="OutOfStock" runat="server">Out Of Stock</span>
                <span id="InStockSpan" class="InStock" runat="server">In Stock</span>
                <div class="DiscountDiv">
                    <asp:Literal runat="server" ID="DiscountLiteral"></asp:Literal>
                    <div class="SpecialOfferDiv" runat="server" id="SpecialOfferDiv">
                        <span class="SpecialOfferBadge"></span>
                        <asp:Literal runat="server" ID="SpecialOfferLiteral"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="Clear"></div>
        </div>
    </div>
    <div class="Clear"></div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <OWD:ProductsCartPopUpComponent runat="server" ID="ProductsCartPopUpComponent1" ShowShowCartPopUpButton="false" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="DescriptionDiv">
        <asp:Literal runat="server" ID="DescriptionLiteral"></asp:Literal>
        <div class="Clear"></div>
    </div>
    <div class="ReviewsDiv">
        <a name="reviewsBlock"></a>
        <OWD:ProductReviewsComponent runat="server" ID="ReviewsComponent" />
    </div>
</div>