
var mfoPjaxDialog = {};

(function () {

    mfoPjaxDialog.init = init;

    function init() {

        $(document).on('click', '[data-modal-dialog]', onClickOpenModalDialog);
        $(document).on('pjax:navigate', onPjaxNavigate);
        $(document).on('pjax:submitform', onPjaxSubmitForm);

        mfoPjax.onError = onPjaxError;

    }

    function onPjaxError(jqXHR, textStatus, errorThrown) {

        // change default pjax.onError to use an alert dialog

        var options = {
            title: 'Error: ' + textStatus,
            message: 'errorThrown="' + errorThrown + '"'
        };

        if (textStatus === 'error') {
            options.title = 'Error';
        }

        if (jqXHR.responseText) {
            options.width = '70%';
            options.message = mfoPjax.stripBody(jqXHR.responseText);
        } else {
            if (errorThrown === 'timeout') {
                options.message = 'No response from server.';
            }
        }

        mfoDialog.alert(options);

    }

    function onPjaxNavigate(e, context) {

        var anchor = context.anchor;
        var closesDialog = anchor.attr('data-close-dialog');
        var opensDialog = anchor.attr('data-modal-dialog');

        if (closesDialog || opensDialog) {
            context.cancel = true;
        }

    }

    function onPjaxSubmitForm(e, context) {

        var form = context.form;
        var dialog = form.closest('.mfo-dialog');

        if (dialog.length === 0) {
            return;
        }

        context.headers = { 'X-PJAX-MODAL': 'true' };
        context.noPushState = true;

    }

    function onClickOpenModalDialog(e) {

        var anchor = $(e.currentTarget);
        var pjaxContainer = anchor.closest('[data-pjax]');
        var url = anchor.attr('href');

        var context = {
            url: url,
            verb: 'GET',
            headers: { 'X-PJAX-MODAL': 'true' }
        };

        mfoPjax.load(context, function (context, data) {
            onDisplayModalContent(context, data, pjaxContainer);
        });

        e.preventDefault();

    }

    function onDisplayModalContent(context, data, pjaxContainer) {

        var dialogInfo = mfoDialog.showModal(data, function (response) {
            onModalClosed(response, pjaxContainer);
        });

        var dialog = dialogInfo.dialog;
        dialog.attr('data-pjax', context.url);
        context.container = dialog;

    }

    function onModalClosed(response, pjaxContainer) {

        if (response) {

            if (pjaxContainer.length === 0) {

                location.reload(true);

            } else {

                var context = {
                    container: pjaxContainer
                };

                if (mfoDialog.dialogCount() !== 0) {
                    context.headers = { 'X-PJAX-MODAL': 'true' };
                }

                mfoPjax.reload(context);

            }

        }

    }

}());
