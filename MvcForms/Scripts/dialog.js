
var dialog = {};

(function () {

    dialog.init = init;

    function init() {

        $(document).on('click', '[data-dialog]', openDialog);

    }

    function openDialog() {
        alert('open dialog');
    }

}());
