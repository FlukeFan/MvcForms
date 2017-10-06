
var mfoDialog = {};

(function () {

    mfoDialog.init = init;
    mfoDialog.showModal = showModal;
    mfoDialog.dialogCount = dialogCount;
    mfoDialog.topDialog = topDialog;
    mfoDialog.closeDialog = closeDialog;

    removeModalHistory();

    var maxHeight;
    var originalOverflow;
    var dialogStack = [];

    function init() {

        $(document).on('click', '[data-dialog]', onClickOpenDialog);
        $(document).on('click', '[data-close-dialog]', onClickCloseDialog);
        $(window).on('popstate', onPopState);
        $(document).on('keyup', onDocKeyup);

        maxHeight = $(window).height() - 20;

    }

    function onDocKeyup(e) {

        if (dialogCount() > 0 && e.keyCode === 27) {
            history.back();
        }

    }

    function onPopState() {

        if (!history.state || history.state.dialogCount === undefined) {
            return;
        }

        var count = history.state.dialogCount;

        if (count < dialogCount()) {
            closeDialog();
        } else if (count > dialogCount()) {
            history.back();
        }

    }

    function onClickOpenDialog(e) {

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

    function onClickCloseDialog(e) {

        history.back();
        e.preventDefault();

    }

    function removeModalHistory() {
        if (history.state && history.state.dialogCount > 0) {
            history.back();
            setTimeout(removeModalHistory, 1);
        }
    }

    function dialogCount() {
        return dialogStack.length;
    }

    function topDialog() {

        if (dialogCount() === 0) {
            return $('body');
        } else {
            return dialogStack[dialogStack.length - 1];
        }
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
            width = '80%';
        }

        dialogStack.push({
            dialog: dialog,
            container: container,
            overlay: overlay
        });

        dialog.width(width);

        if (history.state === null || !history.dialogCount) {
            var state = history.state || {};
            state.dialogCount = count;
            history.replaceState(state, null, '');
        }

        var url = location.pathname + location.search + location.hash;
        history.pushState({ dialogCount: count + 1 }, null, url);

        return dialog;

    }

    function closeDialog() {

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

    }

}());
