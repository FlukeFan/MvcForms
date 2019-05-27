
var mfoOverlay = {};

(function () {

    mfoOverlay.add = add;
    mfoOverlay.remove = remove;

    function add(fadeTime1, fadeTime2) {

        var zIndex = maxZIndex() + 500;

        var overlay = $('<div class="mfo-overlay"></div>').css({
            'opacity': '0',
            'margin': '0',
            'border': '0',
            'position': 'fixed',
            'top': 0,
            'left': 0,
            'width': '100%',
            'height': '100%',
            'z-index': zIndex
        })
            .fadeTo(fadeTime1 || 100, 0)
            .fadeTo(fadeTime2 || 1500, 0.2)
            .appendTo('body');

        var undisableActions = [];

        $('input, select, textarea, button, a, area').each(function () {

            var el = $(this);
            var disabled = el.prop('disabled');
            var tabIndex = el.prop('tabIndex');

            undisableActions.push(function () {

                if (disabled !== undefined) {
                    el.prop('disabled', disabled);
                }

                if (tabIndex) {
                    el.prop('tabindex', tabIndex);
                } else {
                    el.removeProp('tabindex');
                }

                el.removeClass('mfo-overlay-disabled');
            });

            if (disabled === false) {
                el.prop('disabled', true);
            }

            el.prop('tabindex', -1);
            el.addClass('mfo-overlay-disabled');

        });

        overlay.undisableActions = undisableActions;
        return overlay;

    }

    function remove(overlay) {

        for (var i = 0; i < overlay.undisableActions.length; i++) {
            overlay.undisableActions[i]();
        }

        overlay.stop(true)
            .fadeTo(50, 0, function () {
                overlay.remove();
            });

    }

    function maxZIndex() {

        // https://stackoverflow.com/a/1118216/357728
        return Math.max.apply(null,
            $.map($('body *'), function (e) {
                if ($(e).css('position') !== 'static') {
                    return parseInt($(e).css('z-index')) || 1;
                }
            }).concat(500));

    }

}());
