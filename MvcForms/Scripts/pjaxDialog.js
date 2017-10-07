
var mfoPjaxDialog = {};

(function () {

    mfoPjaxDialog.init = init;

    function init() {

        $(document).on('click', '[data-modal-dialog]', onClickOpenModalDialog);
        $(document).on('pjax:navigate', onPjaxNavigate);

    }

    function onPjaxNavigate(e, context) {

        var closesDialog = context.anchor.attr('data-close-dialog');

        if (closesDialog) {
            context.cancel = true;
        }

    }

    function onClickOpenModalDialog(e) {

        var anchor = $(e.currentTarget);
        var url = anchor.attr('href');

        var context = {
            url: url,
            verb: 'GET',
            headers: { 'X-PJAX-MODAL': 'true' }
        };

        mfoPjax.load(context, onDisplayModalContent);

        e.preventDefault();

    }

    function onDisplayModalContent(context, data) {

        mfoDialog.showModal(data);

    }

}());
