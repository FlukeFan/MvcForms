
var mfoScroll = {};

(function () {

    var positions = {};

    mfoScroll.init = init;

    function init() {

        $(document).on('pjax:submitform', onBeforeSubmit);
        $(document).on('pjax:loaded', onLoaded);

    }

    function onBeforeSubmit(e, context) {

        positions = {};
        context.container.find('[data-keep-scroll]').each(function () {

            var el = $(this);
            positions[el.attr('data-keep-scroll')] = el.scrollTop();

        });

    }

    function onLoaded(e, context) {

        context.container.find('[data-keep-scroll]').each(function () {

            var el = $(this);
            var id = el.attr('data-keep-scroll');

            if (positions[id]) {
                el.scrollTop(positions[id]);
            }

        });

    }

}());
