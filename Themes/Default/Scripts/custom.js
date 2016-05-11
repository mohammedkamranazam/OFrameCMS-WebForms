$(document).ready(function () {
    $('#tabs div.content').hide();
    $('#tabs div.content:first').show();
    $('#tabs ul li:first').addClass('active');

    $('#tabs ul li a').click(function () {
        $('#tabs ul li').removeClass('active');
        $(this).parent().addClass('active');
        var currentTab = $(this).attr('href');
        $('#tabs div.content').hide();
        $(currentTab).show();
        return false;
    });

    $('.focuspoint').focusPoint({
        reCalcOnWindowResize: true
    });

    $('.MultiLevelAccordianMenu').slightSubmenu();

    $(".TestimonialCarousel").owlCarousel({
        navigation: true,
        slideSpeed: 300,
        paginationSpeed: 400,
        singleItem: true,
        stopOnHover: true,
        autoHeight: true
    });

    $(".ItemsCarousel").owlCarousel({
        itemsScaleUp: true,
        navigation: true,
        slideSpeed: 300,
        paginationSpeed: 400,
        singleItem: false,
        stopOnHover: true,
        autoHeight: true,
        responsive: true,
        itemsCustom: [
        [0, 1],
        [450, 1],
        [600, 2],
        [700, 2],
        [900, 3],
        [1000, 3],
        [1100, 4],
        [1200, 4],
        [1400, 4],
        [1600, 4]
        ]
    });

    $("#accordian h3").click(function () {
        $("#accordian ul ul").slideUp();

        if (!$(this).next().is(":visible")) {
            $(this).next().slideDown();
        }
    });

    $("a[rel=fancybox]").fancybox({
        openEffect: 'elastic',
        closeEffect: 'fade'
    });

    $(".fancybox").fancybox({
        helpers: {
            title: {
                type: 'float'
            }
        }
    });

    $("#fancybox").fancybox({
        helpers: {
            title: {
                type: 'float'
            }
        }
    });

    tooltip();

    $("div.Rows").hide();

    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.scrollup').fadeIn();
        } else {
            $('.scrollup').fadeOut();
        }
    });

    $('.scrollup').click(function () {
        $("html, body").animate({ scrollTop: 0 }, 600);
        return false;
    });
});

$(function () {
    var element = document.getElementById("_TextTruncateCountHiddenField__");

    if (element !== null) {
        $('.truncate').truncate({
            max_length: parseInt(element.value)
        });
    }
});

$(function () {
    $('#MenuUL').smartmenus({
        subMenusSubOffsetX: 0,
        subMenusSubOffsetY: 0
    });
});

function OnAction(productID, imageURL) {
    $("#RollOverImage-" + productID).attr("src", imageURL);
}

function ToggleRow(id) {
    $("div.Row" + id).toggle("slow");
};

function Close() {
    $(".alertMessage").hide("slow");
}

this.tooltip = function () {
    xOffset = 0;
    yOffset = 0;

    $(".tooltip").hover(function (e) {
        this.t = this.title;
        this.title = "";
        $("body").append("<p id='TooltipStyle'>" + this.t + "</p>");
        $("#TooltipStyle")
            .css("top", (e.pageY - xOffset) + "px")
            .css("left", (e.pageX + yOffset) + "px")
            .fadeToggle(500);
    },
    function () {
        this.title = this.t;
        $("#TooltipStyle").remove();
    });
    $(".tooltip").mousemove(function (e) {
        $("#TooltipStyle")
            .css("top", (e.pageY - xOffset) + "px")
            .css("left", (e.pageX + yOffset) + "px");
    });
};

$(function () {
    $('.tip a ').tipsy({ gravity: 's', live: true });
    $('.ntip a ').tipsy({ gravity: 'n', live: true });
    $('.wtip a ').tipsy({ gravity: 'w', live: true });
    $('.etip a,.Base').tipsy({ gravity: 'e', live: true });
    $('.netip a ').tipsy({ gravity: 'ne', live: true });
    $('.nwtip a , .setting').tipsy({ gravity: 'nw', live: true });
    $('.swtip a,.iconmenu li a ').tipsy({ gravity: 'sw', live: true });
    $('.setip a ').tipsy({ gravity: 'se', live: true });
    $('.wtip input').tipsy({ trigger: 'focus', gravity: 'w', live: true });
    $('.etip input').tipsy({ trigger: 'focus', gravity: 'e', live: true });
    $('.iconBox, div.logout').tipsy({ gravity: 'ne', live: true });

    $(".takeWebcam").click(function () {
        $(".webcam").show('blind');
        return false;
    });
    $("#closeButton").click(function () {
        $(".webcam").hide('blind');
        return false;
    });

    function togglePane() {
        var visible = $(' .buttonPane:visible:first');
        var hidden = $(' .buttonPane:hidden:first');
        visible.fadeOut('fast', function () {
            hidden.show();
        });
    }
});