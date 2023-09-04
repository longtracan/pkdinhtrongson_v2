
(function ($) {
    $.fn.slide = function (options) {
        return this.each(function () {
            var defaults = {
                cur: -1,
                cr: [],
                al: null,
                at: 10 * 1000,
                ar: true,
                img: [],
                nav: []
            };

            var $this = this;
            var slider = $.extend(defaults, options);
            slider.img = $(".slide-runner > a > img", $this);
            slider.nav = $(".slide-nav a", $this);
            init();

            function init() {
                if (!slider.img || !slider.img.length)
                    return false;
                var pos = 0;
                slider.cur = pos;
                on(pos);

                slider.nav.each(function (idx) {
                    $(this).click(function () {
                        slide(idx);
                    });
                });

                for (var i = 0; i < slider.img.length; i++)
                    $(slider.img[i]).css({ left: ((i - pos) * 1000) + "px" });

                window.setTimeout(function () {
                    auto();
                }, slider.at);
            }

            function auto() {
                if (!slider.ar)
                    return false;

                var next = slider.cur + 1;
                if (next >= slider.img.length) next = 0;
                slide(next);
            }

            function slide(pos) {
                if (pos < 0 || pos >= slider.img.length || pos == slider.cur)
                    return;

                window.clearTimeout(slider.al);
                slider.al = window.setTimeout(function () {
                    auto();
                }, slider.at);

                for (var i = 0; i < slider.img.length; i++)
                    $(slider.img[i]).stop().animate({ left: ((i - pos) * 1000) }, 1000, 'swing');

                on(pos);

                slider.cur = pos;
            }

            function on(pos) {
                $('.slide-nav a', $this).removeClass('on');
                $(slider.nav[pos]).addClass('on');
            }
        });
    }

})(jQuery);
