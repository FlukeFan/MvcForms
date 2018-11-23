using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcForms
{
    public class ScopedHtmlHelper<T> : IDisposable
    {
        private IHtmlHelper<T>  _html;
        private Action          _onDispose;

        public IHtmlHelper<T> Html { get { return _html; } }

        public ScopedHtmlHelper(IHtmlHelper html) : this(html, null) { }

        public ScopedHtmlHelper(IHtmlHelper html, Action onDispose)
        {
            _html = (IHtmlHelper<T>)html;
            _onDispose = onDispose ?? (() => { });
        }

        public void Dispose()
        {
            _onDispose();
        }
    }
}
