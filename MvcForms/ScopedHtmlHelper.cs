using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MvcForms
{
    public class ScopedHtmlHelper<T> : IDisposable
    {
        private HtmlHelper<T>   _html;
        private Action          _onDispose;

        public HtmlHelper<T> Html { get { return _html; } }

        public ScopedHtmlHelper(HtmlHelper html) : this(html, null) { }

        public ScopedHtmlHelper(HtmlHelper html, Action onDispose)
        {
            _html = (HtmlHelper<T>)html;
            _onDispose = onDispose ?? (() => { });
        }

        public void Dispose()
        {
            _onDispose();
        }
    }
}
