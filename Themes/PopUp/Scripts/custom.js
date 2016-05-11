$(document).ready(function () {
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
});

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