
var mvfDialog = {};

(function () {

    mvfDialog.init = init;

    function init() {

        $(document).on('click', '[data-dialog]', openDialog);

    }

    function openDialog(e) {

        var anchor = $(e.currentTarget);
        var url = anchor.attr('href');

        mvfPjax.removeOverlay('dialog_overlay');
        var overlay = mvfPjax.addOverlay('dialog_overlay', 50, 50);

        var container = $('<div></div>').css({
            'z-index': '10500',
            'background': 'white',
            'position': 'fixed'
        });

        $(document.body).append(container);

        var context = {
            url: url,
            verb: "GET",
            container: container
        };

        mvfPjax.load(context);

        e.preventDefault();

    }

}());
