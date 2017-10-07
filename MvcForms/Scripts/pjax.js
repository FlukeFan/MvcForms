﻿
var mfoPjax = {};

(function () {

    mfoPjax.init = init;
    mfoPjax.onError = onError;
    mfoPjax.load = load;
    mfoPjax.addOverlay = addOverlay;
    mfoPjax.removeOverlay = removeOverlay;

    var lastButton = null;

    function init() {

        $(document).on('click', '[data-pjax] a', onNavigate);
        $(document).on('submit', '[data-pjax] form', onSubmitForm);
        $(window).on('popstate', onPopState);
        $(document).on('click', 'input[type=submit], button', onSubmitClicked);

    }

    function onSubmitClicked(e) {

        if (lastButton) {
            lastButton.removeAttr('clicked');
        }

        lastButton = $(e.currentTarget);
        lastButton.attr('clicked', 'true');

    }

    function onNavigate(e) {

        var anchor = $(e.currentTarget);

        if (anchor.attr('data-nopjax')) {
            return;
        }

        var container = anchor.closest('[data-pjax]');
        var url = anchor.attr('href');

        if (!url || url.substr(0, 1) === '#') {
            return;
        }

        if (!container.attr('id')) {
            container.attr('id', 'pjax_' + new Date().valueOf());
        }

        if (url.toLowerCase().substr(0, 7) === 'http://' || url.toLowerCase().substr(0, 8) === 'https://') {
            return; // we don't pjax external links
        }

        var context = {
            anchor: anchor,
            url: url,
            verb: 'GET',
            container: container
        };

        container.trigger("pjax:navigate", context);

        if (context.cancel === true) {
            return;
        }

        if (history.state === null || history.state.containerId !== container.attr('id')) {
            var state = history.state || {};
            state.url = location.pathname + location.search + location.hash;
            state.containerId = container.attr('id');
            history.replaceState(state, null, '');
        }

        mfoPjax.load(context, navigateSuccess);
        e.preventDefault();

    }

    function onSubmitForm(e) {

        var form = $(e.currentTarget);

        var clickedButton = form.find('*[clicked=true]');

        if (clickedButton.attr('data-nopjax')) {
            return;
        }

        var container = form.closest('[data-pjax]');
        var data = form.serialize();

        if (clickedButton.length > 0) {
            data += '&' + clickedButton.attr('name') + '=' + clickedButton.attr('value');
        }

        var context = {
            form: form,
            url: form.attr('action') || location.pathname + location.search + location.hash,
            data: data,
            verb: form.attr('method') || 'POST',
            container: container
        };

        container.trigger("pjax:submitform", context);

        if (context.cancel === true) {
            return;
        }

        mfoPjax.load(context, navigateSuccess);
        e.preventDefault();
    }

    function onPopState(e) {

        var popState = e.originalEvent.state;

        if (popState === null || !popState.containerId) {
            return;
        }

        var container = $('#' + popState.containerId);

        if (container.length === 0) {
            // no container - just refresh the page
            location.reload();
            return;
        }

        var context = {
            verb: 'GET',
            url: popState.url,
            container: container
        };

        mfoPjax.load(context, function (context, data) {
            render(container, data);
        });
    }

    function onError(jqXHR, textStatus, errorThrown, context, callback) {
        // default is to do nothing if there was no response (and return false) and
        // to display the error otherwise (and return true)
        // clients can change pjax.onError to their requirements

        if (!jqXHR.responseText) {
            return false;
        }

        callback(context, jqXHR.responseText, textStatus, jqXHR);
        return true;
    }

    function navigateSuccess(context, data, textStatus, jqXHR) {

        var container = context.container;
        render(container, data);

        if (context.noPushState === true) {
            return;
        }

        var url = stripInternalParams(jqXHR.getResponseHeader('X-PJAX-URL') || context.url);

        if (url !== location.href) {
            history.pushState({ url: url, containerId: context.container.attr('id') }, null, url);
        }
    }

    function stripInternalParams(url) {
        return url.replace(/([?&])(_pjax|_)=[^&]*/g, '');
    }

    function render(container, data) {

        container.html(data);

        var pjaxContent = container.children(':first');
        document.title = pjaxContent.attr('data-title');

    }

    function addOverlay(fadeTime1, fadeTime2) {

        return $('<div class="mfo-overlay"></div>').css({
            'opacity': '0',
            'margin': '0',
            'border': '0',
            'position': 'fixed',
            'top': 0,
            'left': 0,
            'width': '100%',
            'height': '100%',
            'z-index': 10000
        })
            .fadeTo(fadeTime1 || 100, 0)
            .fadeTo(fadeTime2 || 1500, 0.2)
            .appendTo('body');

    }

    function removeOverlay(overlay) {

        overlay.stop(true)
            .fadeTo(50, 0, function () {
                overlay.remove();
            });

    }

    function load(context, callback) {

        callback = callback || navigateSuccess;

        var overlay = mfoPjax.addOverlay()
            .css('cursor', 'wait');

        var headers = {
            'X-PJAX': 'true',
            'X-PJAX-URL': context.url
        };

        $.extend(headers, context.headers);

        $.ajax({
            headers: headers,
            cache: false,
            type: context.verb,
            url: context.url,
            data: context.data,
            timeout: 29000,
            dataType: 'html',
            success: function (data, textStatus, jqXHR) {
                mfoPjax.removeOverlay(overlay);
                callback(context, data, textStatus, jqXHR);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                mfoPjax.removeOverlay(overlay);
                mfoPjax.onError(jqXHR, textStatus, errorThrown, context, callback);
            }
        });

    }

}());
