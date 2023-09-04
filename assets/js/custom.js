jQuery(function ($) {
    'use strict'; jQuery('.mean-menu').meanmenu({ meanScreenWidth: '991' }); $(window).on('scroll', function () { if ($(this).scrollTop() > 150) { $('.main-nav').addClass('menu-shrink'); } else { $('.main-nav').removeClass('menu-shrink'); } }); new WOW().init(); $('.testimonials-slider').owlCarousel({ loop: true, margin: 30, singleItem: true, nav: false, dots: true, smartSpeed: 1000, autoplay: false, autoplayTimeout: 4000, autoplayHoverPause: true, responsive: { 0: { items: 1, }, 768: { items: 2, }, 992: { items: 2, } } }); $('.partner-slider').owlCarousel({ loop: true, margin: 30, singleItem: true, nav: false, dots: false, smartSpeed: 1000, autoplay: false, autoplayTimeout: 4000, autoplayHoverPause: true, responsive: { 0: { items: 1, }, 768: { items: 2, }, 992: { items: 4, } } }); $('.department-slider').owlCarousel({ loop: true, margin: 30, singleItem: true, nav: false, dots: false, smartSpeed: 1000, autoplay: false, autoplayTimeout: 4000, autoplayHoverPause: true, responsive: { 0: { items: 4, }, 768: { items: 4, }, 992: { items: 5, } } }); $('.doctors-area .button').on('click', function () { $('.doctors-area .social').toggle('1000'); }); $('.doctors-area .button-two').on('click', function () { $('.doctors-area .social-two').toggle('1000'); }); $('.doctors-area .button-three').on('click', function () { $('.doctors-area .social-three').toggle('1000'); }); $('.doctors-area .button-four').on('click', function () { $('.doctors-area .social-four').toggle('1000'); }); $('.doctors-area .button-five').on('click', function () { $('.doctors-area .social-five').toggle('1000'); }); $('.doctors-area .button-six').on('click', function () { $('.doctors-area .social-six').toggle('1000'); }); $('select').niceSelect(); $('.modal a').not('.dropdown-toggle').on('click', function () { $('.modal').modal('hide'); }); $(function () { $(window).on('scroll', function () { var scrolled = $(window).scrollTop(); if (scrolled > 500) $('.go-top').addClass('active'); if (scrolled < 500) $('.go-top').removeClass('active'); }); $('.go-top').on('click', function () { $('html, body').animate({ scrollTop: '0' }, 500); }); }); jQuery(window).on('load', function () { jQuery('.loader').fadeOut(500); }); $('.banner-slider').owlCarousel({ items: 1, loop: true, margin: 30, singleItem: true, nav: false, dots: true, smartSpeed: 1000, autoplay: false, autoplayTimeout: 4000, autoplayHoverPause: true, }); $('.odometer').appear(function (e) { var odo = $('.odometer'); odo.each(function () { var countNumber = $(this).attr('data-count'); $(this).html(countNumber); }); }); $('.popup-gallery').magnificPopup({ delegate: '.popup', type: 'image', tLoading: 'Loading image #%curr%...', mainClass: 'mfp-img-mobile', gallery: { enabled: true, navigateByImgClick: true, preload: [0, 1] }, }); $('.slider-for').slick({ slidesToShow: 1, slidesToScroll: 1, arrows: false, fade: true, autoplay: false, autoplaySpeed: 4000, asNavFor: '.slider-nav' }); $('.slider-nav').slick({ slidesToShow: 3, slidesToScroll: 1, asNavFor: '.slider-for', arrows: false, dots: false, focusOnSelect: true, responsive: [{ breakpoint: 992, settings: { slidesToShow: 4, } }, { breakpoint: 768, settings: { slidesToShow: 1, } }, { breakpoint: 320, settings: { slidesToShow: 1, } }] }); jQuery('#datetimepicker').datetimepicker({ i18n: { de: { months: ['Januar', 'Februar', 'MÃ¤rz', 'April', 'Mai', 'Juni', 'Juli', 'August', 'September', 'Oktober', 'November', 'Dezember',], dayOfWeek: ["So.", "Mo", "Di", "Mi", "Do", "Fr", "Sa.",] } }, timepicker: false, format: 'd.m.Y' });

    $('.popup-youtube').magnificPopup({
        type: 'iframe',
        iframe: extendMagnificIframe()
    });

    function extendMagnificIframe() {

        var $start = 0;
        var $iframe = {
            markup: '<div class="mfp-iframe-scaler">' +
                '<div class="mfp-close"></div>' +
                '<iframe class="mfp-iframe" frameborder="0" allowfullscreen></iframe>' +
                '</div>' +
                '<div class="mfp-bottom-bar">' +
                '<div class="mfp-title"></div>' +
                '</div>',
            patterns: {
                youtube: {
                    index: 'youtu',
                    id: function (url) {

                        var m = url.match(/^.*(?:youtu.be\/|v\/|e\/|u\/\w+\/|embed\/|v=)([^#\&\?]*).*/);
                        if (!m || !m[1]) return null;

                        if (url.indexOf('t=') != - 1) {

                            var $split = url.split('t=');
                            var hms = $split[1].replace('h', ':').replace('m', ':').replace('s', '');
                            var a = hms.split(':');

                            if (a.length == 1) {

                                $start = a[0];

                            } else if (a.length == 2) {

                                $start = (+a[0]) * 60 + (+a[1]);

                            } else if (a.length == 3) {

                                $start = (+a[0]) * 60 * 60 + (+a[1]) * 60 + (+a[2]);

                            }
                        }

                        var suffix = '?autoplay=1';

                        if ($start > 0) {

                            suffix = '?start=' + $start + '&autoplay=1';
                        }

                        return m[1] + suffix;
                    },
                    src: '//www.youtube.com/embed/%id%'
                },
                vimeo: {
                    index: 'vimeo.com/',
                    id: function (url) {
                        var m = url.match(/(https?:\/\/)?(www.)?(player.)?vimeo.com\/([a-z]*\/)*([0-9]{6,11})[?]?.*/);
                        if (!m || !m[5]) return null;
                        return m[5];
                    },
                    src: '//player.vimeo.com/video/%id%?autoplay=1'
                }
            }
        };

        return $iframe;

    }


    $('.accordion > li:eq(0) .faq-head').addClass('active').next().slideDown(); $('.accordion .faq-head').on('click', function (j) {
        var dropDown = $(this).closest('li').find('.faq-content'); $(this).closest('.accordion').find('.faq-content').not(dropDown).slideUp(300); if ($(this).hasClass('active')) { $(this).removeClass('active'); } else { $(this).closest('.accordion').find('.faq-head.active').removeClass('active'); $(this).addClass('active'); }
dropDown.stop(false,true).slideToggle(300);j.preventDefault();});$('.minus').on('click',function(){var $input=$(this).parent().find('input');var count=parseInt($input.val())-1;count=count<1?1:count;$input.val(count);$input.change();return false;});$('.plus').on('click',function(){var $input=$(this).parent().find('input');$input.val(parseInt($input.val())+1);$input.change();return false;});$(".newsletter-form").validator().on("submit",function(event){if(event.isDefaultPrevented()){formErrorSub();submitMSGSub(false,"Please enter your email correctly.");}else{event.preventDefault();}});function callbackFunction(resp){if(resp.result==="success"){formSuccessSub();}
else{formErrorSub();}}
function formSuccessSub(){$(".newsletter-form")[0].reset();submitMSGSub(true,"Thank you for subscribing!");setTimeout(function(){$("#validator-newsletter").addClass('hide');},4000)}
function formErrorSub(){$(".newsletter-form").addClass("animated shake");setTimeout(function(){$(".newsletter-form").removeClass("animated shake");},1000)}
function submitMSGSub(valid,msg){if(valid){var msgClasses="validation-success";}else{var msgClasses="validation-danger";}
$("#validator-newsletter").removeClass().addClass(msgClasses).text(msg);}
$(".newsletter-form").ajaxChimp({url:"https://envytheme.us20.list-manage.com/subscribe/post?u=60e1ffe2e8a68ce1204cd39a5&amp;id=42d6d188d9",callback:callbackFunction});}(jQuery));