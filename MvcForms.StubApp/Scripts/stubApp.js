
(function () {

    $(function () {

        mvcForms.init();

        $(document).on('pjax:loaded', loaded);
        loaded($.Event('firstLoad'), { container: $(document.body) });

    });

    function loaded(e, context) {

        console.log(e.type + ': ' + context.container[0].outerHTML.substring(0, 100));

    }

}());
