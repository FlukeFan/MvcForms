
var mfoDialog = {};

(function () {

    mfoDialog.init = init;
    mfoDialog.showModal = showModal;
    mfoDialog.dialogCount = dialogCount;

    var maxHeight;
    var originalOverflow;
    var dialogStack = [];

    function init() {

        $(document).on('click', '[data-dialog]', openDialog);
        $(document).on('click', '[data-close-dialog]', closeDialog);

        maxHeight = $(window).height() - 20;

    }

    function openDialog(e) {

        var anchor = $(e.currentTarget);
        var url = anchor.attr('href');

        mfoPjax.addOverlay(50, 50);

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

    function dialogCount() {
        return dialogStack.length;
    }

    function showModal(html) {

        var count = dialogCount();

        if (count === 0) {
            // prevent scroll on underlying page
            var body = $('body');
            originalOverflow = body.css('overflow');
            body.css('overflow', 'hidden');
        }

        var overlay = mfoPjax.addOverlay(1, 100)
            .css('z-index', 10000 + count * 1000);

        var container = $('<div></div>').css({
            'margin': '0',
            'border': '0',
            'position': 'fixed',
            'top': 0,
            'left': 0,
            'width': '100%',
            'height': '100%',
            'z-index': 10500 + count * 1000
        })
            .appendTo('body');

        var dialog = $('<div class="mfo-dialog"></div>').css({
            'margin': 'auto',
            'position': 'absolute',
            'left': '0',
            'right': '0',
            'top': '50%',
            'transform': 'translateY(-50%)',
            'max-height': maxHeight + 'px',
            'overflow': 'auto'
        })
            .appendTo(container);

        dialog.html(html);

        var dialogContent = dialog.children(':first');
        var width = dialogContent.attr('data-modal-width');

        if (!width) {
            width = "80%";
        }

        dialogStack.push({
            dialog: dialog,
            container: container,
            overlay: overlay
        });

        dialog.width(width);

        return dialog;

    }

    function closeDialog(e) {

        if (dialogCount() === 0) {
            return;
        }

        var topDialog = dialogStack.pop();

        topDialog.dialog.remove();
        topDialog.container.remove();
        mfoPjax.removeOverlay(topDialog.overlay);

        if (dialogCount() === 0) {
            $('body').css('overflow', originalOverflow);
        }

        e.preventDefault();

    }

}());
