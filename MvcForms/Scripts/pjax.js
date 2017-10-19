﻿
var mfoPjax = {};

(function () {

    mfoPjax.init = init;
    mfoPjax.onError = onError;
    mfoPjax.load = load;
    mfoPjax.reload = reload;
    mfoPjax.onBeforeNonPjaxPushState = onBeforeNonPjaxPushState;

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

        if (history.state === null || history.state.containerId !== container.attr('id') || !history.state.navigatedFromPjax) {
            var state = history.state || {};
            state.url = location.pathname + location.search + location.hash;
            state.containerId = container.attr('id');
            state.navigatedFromPjax = true;
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
            var name = clickedButton.attr('name');
            if (name) {
                data += '&' + name + '=' + (clickedButton.attr('value') || "");
            }
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

    function onBeforeNonPjaxPushState() {

        var state = history.state;

        if (state && state.navigatedFromPjax) {

            delete state.navigatedFromPjax;
            history.replaceState(state, '', null);

        }

    }

    function onPopState(e) {

        var popState = e.originalEvent.state;

        if (popState === null || !popState.containerId || !popState.navigatedFromPjax) {
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
            render(context, data);
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

        render(context, data);

        if (context.noPushState === true) {
            return;
        }

        var url = stripInternalParams(jqXHR.getResponseHeader('X-PJAX-URL') || context.url);

        if (url !== location.href) {
            history.pushState({ url: url, containerId: context.container.attr('id'), navigatedFromPjax: true }, null, url);
        }

    }

    function stripInternalParams(url) {
        return url.replace(/([?&])(_pjax|_)=[^&]*/g, '');
    }

    function render(context, data) {

        var first100 = data.substr(0, 100);

        if (first100.indexOf('<html') >= 0 || first100.indexOf('<HTML') >= 0) {

            // loading a full HTML page into the PJAX body is an error, so
            // attempt to scrape out the <body> part of the page and
            // disable the scripts
            var bodyStart = Math.max(data.indexOf('<body'), data.indexOf('<BODY'));

            if (bodyStart >= 0) {
                data = data.substr(bodyStart + 5);
                var bodyStartClose = data.indexOf('>');

                if (bodyStartClose >= 0) {
                    data = data.substr(bodyStartClose + 1);
                }
            }

            var bodyEnd = Math.max(data.indexOf('body>'), data.indexOf('BODY>'));

            if (bodyEnd >= 0) {
                data = data.substr(0, bodyEnd);
                var bodyEndOpen = data.lastIndexOf('</');

                if (bodyEndOpen >= 0) {
                    data = data.substr(0, bodyEndOpen);
                }
            }

            data = data.replace(/<script/g, 'script_tag_disabled_pjax_error: ');
            data = data.replace(/<SCRIPT/g, 'SCRIPT_TAG_DISABLED_PJAX_ERROR: ');

            data = '<div data-title="Error - Attempt to load non-PJAX page into container">'
                + '<h1 style="background:white; color: red">Error - Attempt to load non-PJAX page into container</h1>'
                + '<div>' + data + '</div></div>';

        }

        var container = context.container;
        container.html(data);

        if (context.noPushState !== true) {
            var pjaxContent = container.children(':first');
            var title = pjaxContent.attr('data-title');
            document.title = title;
        }

    }

    function load(context, callback) {

        callback = callback || navigateSuccess;

        var overlay = mfoOverlay.add()
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
                mfoOverlay.remove(overlay);
                callback(context, data, textStatus, jqXHR);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                mfoOverlay.remove(overlay);
                mfoPjax.onError(jqXHR, textStatus, errorThrown, context, callback);
            }
        });

    }

    function reload(context) {

        var url = context.container.attr('data-pjax');

        if (url === 'true') {
            url = location.pathname + location.search + location.hash;
        }

        context.url = url;

        context.verb = 'GET';
        context.noPushState = true;

        load(context);

    }

}());
