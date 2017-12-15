
(function () {

    $(function () {

        mvcForms.init();

        $(document).on('pjax:loaded', loaded);
        loaded($.Event('firstLoad'), { container: $(document.body) });

        $(document).on('change', '#cssFrameworkSelector', onCssFrameworkChanged);

    });

    function loaded(e, context) {

        console.log(e.type + ': ' + context.container[0].outerHTML.substring(0, 100));

    }

    function onCssFrameworkChanged(e) {

        var option = $(e.currentTarget);
        document.cookie = "cssFramework=" + option.val() + ";path=/";
        location.reload(true);

    }

}());
