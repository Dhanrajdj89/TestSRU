(function ($) {

    "use strict";
    $(window).scroll(function () {
        var sticky = $('.main-header'),
            scroll = $(window).scrollTop();
        if (scroll >= 5) sticky.addClass('fixed-top-header');
        else sticky.removeClass('fixed-top-header');
    });
    
    //Update Header Style + Scroll to Top
    function headerStyle() {
        if ($('.main-header').length) {
            var topHeader = $('.header-top').innerHeight();
            var windowpos = $(window).scrollTop();
            if (windowpos >= topHeader) {
                $('.cmain-header').addClass('fixed-top-header');
                $('.scroll-to-top').fadeIn(300);
            } else {
                $('.cmain-header').removeClass('fixed-top-header');
                $('.scroll-to-top').fadeOut(300);
            }
        }
    }

    //Submenu Dropdown Toggle
    if ($('.main-header li.dropdown .submenu').length) {
        $('.main-header li.dropdown').append('<div class="dropdown-btn"></div>');

        //Dropdown Button
        $('.main-header li.dropdown .dropdown-btn').on('click', function () {
            $(this).prev('.submenu').slideToggle(500);
        });
    }

    //Main Slider
    if ($('.main-slider').length) {

        jQuery('.tp-banner').show().revolution({
            delay: 7500,
            startwidth: 1200,
            startheight: 620,
            hideThumbs: 600,

            thumbWidth: 80,
            thumbHeight: 50,
            thumbAmount: 5,

            navigationType: "bullet",
            navigationArrows: "1",
            navigationStyle: "preview4",

            touchenabled: "on",
            onHoverStop: "off",

            swipe_velocity: 0.7,
            swipe_min_touches: 1,
            swipe_max_touches: 1,
            drag_block_vertical: false,

            parallax: "mouse",
            parallaxBgFreeze: "on",
            parallaxLevels: [7, 4, 3, 2, 5, 4, 3, 2, 1, 0],

            keyboardNavigation: "on",

            navigationHAlign: "center",
            navigationVAlign: "bottom",
            navigationHOffset: 0,
            navigationVOffset: 20,

            soloArrowLeftHalign: "left",
            soloArrowLeftValign: "center",
            soloArrowLeftHOffset: 20,
            soloArrowLeftVOffset: 0,

            soloArrowRightHalign: "right",
            soloArrowRightValign: "center",
            soloArrowRightHOffset: 20,
            soloArrowRightVOffset: 0,

            shadow: 0,
            fullWidth: "on",
            fullScreen: "off",

            spinner: "spinner4",

            stopLoop: "off",
            stopAfterLoops: -1,
            stopAtSlide: -1,

            shuffle: "off",

            autoHeight: "off",
            forceFullWidth: "on",

            hideThumbsOnMobile: "on",
            hideNavDelayOnMobile: 1500,
            hideBulletsOnMobile: "on",
            hideArrowsOnMobile: "on",
            hideThumbsUnderResolution: 0,

            hideSliderAtLimit: 0,
            hideCaptionAtLimit: 0,
            hideAllCaptionAtLilmit: 0,
            startWithSlide: 0,
            videoJsPath: "",
            fullScreenOffsetContainer: ".main-slider"
        });

    }

    //LightBox / Fancybox
    if ($('.lightbox-image').length) {
        $('.lightbox-image').fancybox();
    }

    $('.testimonial_slider').owlCarousel({
        loop: true,
        nav: false,
        margin: 10,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                nav: false
            },
            600: {
                items: 1,
                nav: false
            },
            1000: {
                items: 2,
                nav: false,
                loop: false
            }
        }
    })

    // Scroll to top
    if ($('.scroll-to-top').length) {
        $(".scroll-to-top").on('click', function () {
            // animate
            $('html, body').animate({
                scrollTop: $('html, body').offset().top
            }, 1000);
        });
    }
    
    // Elements Animation
    if ($('.wow').length) {
        var wow = new WOW(
		  {
		      boxClass: 'wow',      // animated element css class (default is wow)
		      animateClass: 'animated', // animation css class (default is animated)
		      offset: 0,          // distance to the element when triggering the animation (default is 0)
		      mobile: true,       // trigger animations on mobile devices (default is true)
		      live: true       // act on asynchronously loaded content (default is true)
		  }
		);
        wow.init();
    }

    /* ==========================================================================
       When document is ready, do
       ========================================================================== */

    $(document).on('ready', function () {
        headerStyle();
    });

    /* ==========================================================================
       When document is Scrollig, do
       ========================================================================== */

    $(window).on('scroll', function () {
        headerStyle();
    });

})(window.jQuery);