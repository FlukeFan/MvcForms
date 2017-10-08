
var mfoOverlay = {};

(function () {

    mfoOverlay.add = add;
    mfoOverlay.remove = remove;

    function add(fadeTime1, fadeTime2) {

        var overlay = $('<div class="mfo-overlay"></div>').css({
            'opacity': '0',
            'margin': '0',
            'border': '0',
            'position': 'fixed',
            'top': 0,
            'left': 0,
            'width': '100%',
            'height': '100%',
            'z-index': 10000
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
                el.prop('disabled', disabled);
                el.prop('tabindex', tabIndex);
            });
            el.prop('disabled', true);
            el.prop('tabindex', -1);

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

}());
