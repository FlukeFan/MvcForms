
var mvfDialog = {};

(function () {

    mvfDialog.init = init;

    function init() {

        $(document).on('click', '[data-dialog]', openDialog);

    }

    function openDialog(e) {

        e.preventDefault();
        alert('open dialog');

    }

}());
