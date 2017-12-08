
var mfoDialog = {};

(function () {

    mfoDialog.init = init;
    mfoDialog.showModal = showModal;
    mfoDialog.dialogCount = dialogCount;
    mfoDialog.topDialog = topDialog;
    mfoDialog.closeDialog = closeDialog;
    mfoDialog.showDialog = showDialog;
    mfoDialog.dialogHtml = dialogHtml;
    mfoDialog.alert = alertDialog;
    mfoDialog.confirm = confirmDialog;

    removeModalHistory();

    var autoWidthStart = 780;
    var dialogBorder = 10;
    var maxHeight;
    var maxWidth;
    var originalOverflow;
    var dialogStack = [];
    var closeResponse;

    function init() {

        $(document).on('click', '[data-close-dialog]', onClickCloseDialog);
        $(window).on('popstate', onPopState);
        $(document).on('keyup', onDocKeyup);
        $(window).on('resize', onWindowResize);

        onWindowResize();

    }

    function onWindowResize() {

        maxHeight = $(window).height() - dialogBorder;
        maxWidth = $(window).width() - dialogBorder;
        resizeDialogs();

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
            removeTopDialog(closeResponse);
        } else if (count > dialogCount()) {
            history.back();
        }

    }

    function onClickCloseDialog(e) {

        var anchor = $(e.currentTarget);
        var response = anchor.attr('data-close-dialog');
        closeResponse = JSON.parse(response);
        history.back();
        e.preventDefault();

    }

    function closeDialog(response) {

        closeResponse = response;
        history.back();

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

    function alertDialog(options) {

        var defaults = {
            buttons: ['OK']
        };

        $.extend(defaults, options);

        showDialog(defaults);

    }

    function confirmDialog(options) {

        var defaults = {
            buttons: ['OK', 'Cancel']
        };

        $.extend(defaults, options);

        showDialog(defaults);

    }

    function showDialog(options) {

        var defaults = {
            width: '200px'
        };

        $.extend(defaults, options);

        var html = mfoDialog.dialogHtml(defaults);

        var dialogInfo = showModal(html, function (response) {
            if (options.onClose) {
                options.onClose(response);
            }
        });

        dialogInfo.dialog.find('.dialogHtml-buttons button').first().focus();

    }

    function dialogHtml(options) {

        var buttons = '<div class="dialogHtml-buttons">';

        if (options.buttons) {
            for (var i in options.buttons) {
                var buttonText = options.buttons[i];
                buttons += '<button type="button" href="#" data-close-dialog="{ &quot;clicked&quot;: &quot;' + buttonText + '&quot; }">' + buttonText + '</button>';
            }
        }

        buttons += '</div>';

        var title = options.title || '';
        var message = options.message || '';

        var html =
            '<div data-modal-width="' + options.width + '" data-title="' + title + '">'
            + '<div><h3>' + options.title + '</h3></div>'
            + '<div style="max-height: ' + (maxHeight - 100) + 'px; overflow: auto;"><p>' + message.replace('\n', '<br/>') + '</p></div>'
            + buttons
            + '</div > ';

        return html;
    }

    function showModal(html, callback) {

        var count = dialogCount();

        if (count === 0) {
            // prevent scroll on underlying page
            var body = $('body');
            originalOverflow = body.css('overflow');
            body.css('overflow', 'hidden');
        }

        var overlay = mfoOverlay.add(1, 100);

        var zIndex = parseInt(overlay.css('z-index')) + 500;

        var container = $('<div></div>').css({
            'margin': '0',
            'border': '0',
            'position': 'fixed',
            'top': 0,
            'left': 0,
            'width': '100%',
            'height': '100%',
            'z-index': zIndex
        })
            .appendTo('body');

        var dialog = $('<div class="mfo-dialog"></div>').css({
            'margin': 'auto',
            'position': 'absolute',
            'left': '0',
            'right': '0',
            'top': '50%',
            'transform': 'translateY(-50%)',
            'overflow': 'auto'
        })
            .appendTo(container);

        dialog.html(html);

        var dialogInfo = {
            dialog: dialog,
            container: container,
            previousTitle: document.title,
            callback: callback,
            overlay: overlay
        };

        dialogStack.push(dialogInfo);

        resizeDialogs();

        var dialogContent = dialog.children(':first');
        var title = dialogContent.attr('data-title');

        if (title) {
            document.title = document.title + ' - ' + title;
        }

        if (history.state === null || !history.dialogCount) {
            var state = history.state || {};
            state.dialogCount = count;
            history.replaceState(state, null, '');
        }

        mfoPjax.onBeforeNonPjaxPushState();
        var url = location.pathname + location.search + location.hash;
        history.pushState({ dialogCount: count + 1 }, null, url);

        return dialogInfo;

    }

    function removeTopDialog(response) {

        if (dialogCount() === 0) {
            return;
        }

        var topDialog = dialogStack.pop();

        topDialog.dialog.remove();
        topDialog.container.remove();
        mfoOverlay.remove(topDialog.overlay);
        document.title = topDialog.previousTitle;

        if (dialogCount() === 0) {
            $('body').css('overflow', originalOverflow);
        }

        if (topDialog.callback) {
            topDialog.callback(response);
        }

    }

    function resizeDialogs() {

        var currentWidth = autoWidthStart + dialogBorder;

        for (var i = 0; i < dialogStack.length; i++) {

            var dialogInfo = dialogStack[i];
            var dialog = dialogInfo.dialog;
            var dialogContent = dialog.children(':first');
            var width = dialogContent.attr('data-modal-width');

            var autoWidth = currentWidth - dialogBorder;

            if (!width) {
                width = autoWidth;
            }

            dialog.width(width);
            dialog.css('max-height', maxHeight + 'px');

            if (dialog.width() > maxWidth) {
                dialog.width(maxWidth);
            }

            currentWidth = dialog.width();

        }

    }

}());
