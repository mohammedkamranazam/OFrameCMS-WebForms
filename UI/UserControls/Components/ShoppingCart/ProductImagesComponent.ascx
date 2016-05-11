<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductImagesComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.ShoppingCart.ProductImagesComponent" %>

<div class="ProductImages">
    <asp:Image CssClass="BigThumbImage Zoom" runat="server" ID="ProductDetailsImageImage" ClientIDMode="Static" />
    <asp:Repeater runat="server" ID="ImagesRepeater">
        <HeaderTemplate>
            <ul class="SmallThumbImages" id="zoom_gallery">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <asp:HyperLink runat="server" data-image='<%#ResolveClientUrl((string)Eval("ImageThumbURL")) %>' data-zoom-image='<%# ResolveClientUrl((string)Eval("ImageURL"))  %>'>
                    <asp:Image CssClass="SmallThumb" runat="server" ImageUrl='<%#Eval("ImageThumbURL") %>' />
                </asp:HyperLink>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            <div class="Clear"></div>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
    <div class="Clear"></div>
</div>
<script>
    $(".Zoom").elevateZoom({
        gallery: 'zoom_gallery',
        cursor: 'pointer',
        galleryActiveClass: 'active',
        easing: true,
        zoomWindowFadeIn: 700,
        zoomWindowFadeOut: 700,
        lensFadeIn: 700,
        lensFadeOut: 700,
        zoomWindowWidth: 500,
        zoomWindowHeight: 500,
    });

    $(".Zoom").bind("click", function (e) {
        var ez = $('.Zoom').data('elevateZoom');
        $.fancybox(ez.getGalleryList());
        return false;
    });
</script>