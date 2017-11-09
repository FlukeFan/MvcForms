
(function () {

    var pjaxOnError = mfoPjax.onError;

    mfoPjax.onError = function (jqXHR, textStatus, errorThrown, context, callback) {
        if (!pjaxOnError(jqXHR, textStatus, errorThrown, context, callback))
            alert("textStatus='" + textStatus + "'\nerrorThrown='" + errorThrown + "'");
    };

    $(function () {

        mvcForms.init();

        $(document).on('pjax:loaded', loaded);
        loaded($.Event('firstLoad'), { container: $(document.body) });

    });

    function loaded(e, context) {

        console.log(e.type + ': ' + context.container[0].outerHTML.substring(0, 100));

    }

}());
