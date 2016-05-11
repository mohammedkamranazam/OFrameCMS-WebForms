function ReInitialize() {
    $(document).ready(function () {
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

        initMenus();

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

    function initMenus() {
        $('ul#main_menu ul').hide();
        $('ul#main_menu li ').hover(function () {
            var parent = $(this).parents('ul').attr('id');
            var parents = $(this).find('ul');
            $('#' + parent + ' ul:visible').hide();
            $(parents).show();
            $('#' + parent + ' ul:visible li:first').append('<div class="arr"><span></span></div>');
            $('#' + parent + ' ul:visible ').live({
                mouseleave: function () {
                    $(this).hide();
                }
            }
            );
        });
    }

    $(function () {
        LResize();
        $(window).resize(function () { LResize(); });
        $(window).scroll(function () { scrollmenu(); });

        $('#Zice_Admin_text_search').bind('keyup change', function (ev) {
            var searchTerm = $(this).val();
            $('.text-search-data').removeHighlight();
            if (searchTerm) {
                $('.text-search-data').highlight(searchTerm);
            }
        });

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

    var mybrowser = navigator.userAgent;

    if (mybrowser.indexOf('MSIE') > 0) {
        $(function () {
            $('.formEl_b fieldset').css('padding-top', '0');
            $('div.section label small').css('font-size', '10px');
            $('div.section  div .select_box').css({ 'margin-left': '-5px' });
            $('.uibutton').css({ 'padding-top': '6px' });
            $('.uibutton.icon:before').css({ 'top': '1px' });
        });
    }

    if (mybrowser.indexOf('Firefox') > 0) {
        $(function () {
            $('.formEl_b fieldset  legend').css('margin-bottom', '0px');
        });
    }

    if (mybrowser.indexOf('Presto') > 0) {
        $('select').css('padding-top', '8px');
    }

    if (mybrowser.indexOf('Chrome') > 0) {
        $(function () {
            $('div.tab_content  ul.uibutton-group').css('margin-top', '-40px');
            $('div.section  div .select_box').css({ 'margin-top': '0px', 'margin-left': '-2px' });
            $('select').css('padding', '6px');
        });
    }

    if (mybrowser.indexOf('Safari') > 0) { }

    function scrollmenu() {
        if ($(window).scrollTop() >= 1) {
            $("#header ").css("z-index", "999");
        } else {
            $("#header ").css("z-index", "999");
        }
    }

    function LResize() {
        scrollmenu();

        $("#shadowhead").show();

        if ($(window).width() <= 1200) {
            $('body').removeClass('dashborad').addClass('nobg');
            $('#shadowhead').css({ position: "fixed" });
            $(' .column_left, .column_right ,.grid2,.grid3,.grid1').css({ width: "100%", float: "none", padding: "0", marginBottom: "20px" });
            $('#left_menu,#load_menu').css({ left: "0px" }); $('#content').css({ marginLeft: "70px" });
            $('#main_menu').removeClass('main_menu').addClass('iconmenu');
            $('#main_menu li').each(function () {
                var title = $(this).find('b').text();
                $(this).find('a').attr('title', title);
            });
            $('#main_menu li a').find('b').hide();
            $('#main_menu li ').find('ul').hide();
        }

        if ($(window).width() > 1200) {
            $('#main_menu').removeClass('iconmenu ').addClass('main_menu');
            $('#main_menu li a').find('b').show();
            $('.column_left,.column_right,.grid2').css({ width: "49%", float: "left" });
            $('.column_left').css({ 'padding-right': "1%" });
            $('.column_right').css({ 'padding-left': "1%" });
            $('.grid1').css({ width: "24%", float: "left" }); $('.grid3').css({ width: "74%", float: "left" });
            $('#left_menu,#load_menu').css({ left: "0px" });
            $('#content').css({ marginLeft: "240px" });
            $('body').removeClass('nobg').addClass('dashborad');
            $('#shadowhead').css({ position: "absolute" });
        } else {
            $(' .column_left, .column_right ,.grid2,.grid3,.grid1').css({ width: "100%", float: "none", padding: "0", marginBottom: "20px" });
        }
    }
}