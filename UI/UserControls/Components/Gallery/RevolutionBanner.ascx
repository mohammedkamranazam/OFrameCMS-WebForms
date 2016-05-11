<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RevolutionBanner.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Gallery.RevolutionBanner" %>

<asp:Panel runat="server" ID="PanelContainer">
    <div class="bannercontainer">
        <div class="banner">
            <ul>
                <asp:Literal runat="server" ID="SlideLiteral" />
            </ul>
            <div runat="server" id="TimerDiv"></div>
        </div>
    </div>

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="delay" Value="9000" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="startwidth" Value="960" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="startheight" Value="500" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="autoHeight" Value="on" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="parallaxDisableOnMobile" Value="on" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="fullScreenAlignForce" Value="on" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="fullScreenOffset" Value="0px" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="onHoverStop" Value="on" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="thumbWidth" Value="100" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="thumbHeight" Value="50" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="thumbAmount" Value="3" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThumbsOnMobile" Value="off" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideBulletsOnMobile" Value="off" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideArrowsOnMobile" Value="off" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThumbsUnderResoluition" Value="0" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThumbs" Value="0" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="navigationType" Value="bullet" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="navigationArrows" Value="solo" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="navigationStyle" Value="square" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="navigationHAlign" Value="center" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="navigationVAlign" Value="bottom" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="navigationHOffset" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="navigationVOffset" Value="0" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="soloArrowLeftHalign" Value="left" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="soloArrowLeftValign" Value="center" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="soloArrowLeftHOffset" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="soloArrowLeftVOffset" Value="0" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="soloArrowRightHalign" Value="right" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="soloArrowRightValign" Value="center" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="soloArrowRightHOffset" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="soloArrowRightVOffset" Value="0" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="touchenabled" Value="on" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="stopAtSlide" Value="-1" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="stopAfterLoops" Value="-1" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideCaptionAtLimit" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideAllCaptionAtLilmit" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSliderAtLimit" Value="0" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="dottedOverlay" Value="none" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="fullWidth" Value="off" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="forceFullWidth" Value="off" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="fullScreen" Value="off" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="fullScreenOffsetContainer" Value="#MenuUL" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="shadow" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="videoJsPath" Value="videojs/" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="lazyLoad" Value="on" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="shuffle" Value="off" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="spinner" Value="spinner0" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="minFullScreenHeight" Value="0" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="keyboardNavigation" Value="on" />

    <script type="text/javascript">

        jQuery(document).ready(function () {
            jQuery('.banner').revolution({

                simplifyAll: "off",

                nextSlideOnWindowFocus: "off",

                hideTimerBar: "off",

                hideNavDelayOnMobile: 1500,

                swipe_velocity: 0.7,
                swipe_min_touches: 1,
                swipe_max_touches: 1,
                drag_block_vertical: false,

                parallax: "mouse",
                parallaxBgFreeze: "on",
                parallaxLevels: [7, 4, 3, 2, 5, 4, 3, 2, 1, 0],

                delay: parseInt(document.getElementById("delay").value),
                startwidth: parseInt(document.getElementById("startwidth").value),
                startheight: parseInt(document.getElementById("startheight").value),
                autoHeight: document.getElementById("autoHeight").value,
                fullScreenAlignForce: document.getElementById("fullScreenAlignForce").value,

                parallaxDisableOnMobile: document.getElementById("parallaxDisableOnMobile").value,

                fullScreenOffset: document.getElementById("fullScreenOffset").value,

                onHoverStop: document.getElementById("onHoverStop").value,

                thumbWidth: parseInt(document.getElementById("thumbWidth").value),
                thumbHeight: parseInt(document.getElementById("thumbHeight").value),
                thumbAmount: parseInt(document.getElementById("thumbAmount").value),

                hideThumbsOnMobile: document.getElementById("hideThumbsOnMobile").value,
                hideBulletsOnMobile: document.getElementById("hideBulletsOnMobile").value,
                hideArrowsOnMobile: document.getElementById("hideArrowsOnMobile").value,
                hideThumbsUnderResoluition: parseInt(document.getElementById("hideThumbsUnderResoluition").value),

                hideThumbs: document.getElementById("hideThumbs").value,

                navigationType: document.getElementById("navigationType").value,
                navigationArrows: document.getElementById("navigationArrows").value,
                navigationStyle: document.getElementById("navigationStyle").value,

                navigationHAlign: document.getElementById("navigationHAlign").value,
                navigationVAlign: document.getElementById("navigationVAlign").value,
                navigationHOffset: parseInt(document.getElementById("navigationHOffset").value),
                navigationVOffset: parseInt(document.getElementById("navigationVOffset").value),

                soloArrowLeftHalign: document.getElementById("soloArrowLeftHalign").value,
                soloArrowLeftValign: document.getElementById("soloArrowLeftValign").value,
                soloArrowLeftHOffset: parseInt(document.getElementById("soloArrowLeftHOffset").value),
                soloArrowLeftVOffset: parseInt(document.getElementById("soloArrowLeftVOffset").value),

                soloArrowRightHalign: document.getElementById("soloArrowRightHalign").value,
                soloArrowRightValign: document.getElementById("soloArrowRightValign").value,
                soloArrowRightHOffset: parseInt(document.getElementById("soloArrowRightHOffset").value),
                soloArrowRightVOffset: parseInt(document.getElementById("soloArrowRightVOffset").value),

                touchenabled: document.getElementById("touchenabled").value,

                stopAtSlide: parseInt(document.getElementById("stopAtSlide").value),
                stopAfterLoops: parseInt(document.getElementById("stopAfterLoops").value),
                hideCaptionAtLimit: parseInt(document.getElementById("hideCaptionAtLimit").value),
                hideAllCaptionAtLilmit: parseInt(document.getElementById("hideAllCaptionAtLilmit").value),
                hideSliderAtLimit: parseInt(document.getElementById("hideSliderAtLimit").value),

                dottedOverlay: document.getElementById("dottedOverlay").value,

                fullWidth: document.getElementById("fullWidth").value,
                forceFullWidth: document.getElementById("forceFullWidth").value,
                fullScreen: document.getElementById("fullScreen").value,
                fullScreenOffsetContainer: document.getElementById("fullScreenOffsetContainer").value,
                shadow: parseInt(document.getElementById("shadow").value),
                videoJsPath: document.getElementById("videoJsPath").value,

                lazyLoad: document.getElementById("lazyLoad").value,

                shuffle: document.getElementById("shuffle").value,

                spinner: document.getElementById("spinner").value,

                minFullScreenHeight: parseInt(document.getElementById("minFullScreenHeight").value),

                keyboardNavigation: document.getElementById("keyboardNavigation").value
            });

        });
    </script>
</asp:Panel>