
var mfoDialog = {};

(function () {

    mfoDialog.init = init;

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

}());
