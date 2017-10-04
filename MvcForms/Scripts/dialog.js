
var mfoDialog = {};

(function () {

    mfoDialog.init = init;
    mfoDialog.showModal = showModal;

    function init() {

        $(document).on('click', '[data-dialog]', openDialog);

    }

    function openDialog(e) {

        var anchor = $(e.currentTarget);
        var url = anchor.attr('href');

        mfoPjax.addOverlay('dialog_overlay', 50, 50);

        var container = $('<div class="mfo-dialog"></div>').css({
            'z-index': '10500',
            'position': 'fixed'
        })
            .appendTo('body');

        var context = {
            url: url,
            verb: 'GET',
            container: container
        };

        mfoPjax.load(context);

        e.preventDefault();

    }

    function showModal(html) {

        mfoPjax.addOverlay('dialog_overlay', 50, 50);

        var container = $('<div></div>').css({
            'margin': '0',
            'border': '0',
            'position': 'fixed',
            'top': 0,
            'left': 0,
            'width': '100%',
            'height': '100%',
            'z-index': 10500
        })
            .appendTo('body');

        var dialog = $('<div class="mfo-dialog"></div>').css({
            'margin': 'auto',
            'position': 'absolute',
            'left': '0',
            'right': '0',
            'top': '50%',
            'transform': 'translateY(-50%)',
            'width': '90%',
            'max-height': '500px'
        })
            .appendTo(container)
            .html(html);

        return dialog;

    }

}());
