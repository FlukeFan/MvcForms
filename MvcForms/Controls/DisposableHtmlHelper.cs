using System;
using System.Web.Mvc;

namespace MvcForms.Controls
{
    public class DisposableHtmlHelper<T> : HtmlHelper<T>, IDisposable
    {
        public DisposableHtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer) : base(viewContext, viewDataContainer)
        {
        }

        public void Dispose()
        {
            // nothing to dispose of here
            // IDisposable is only to allow nicer syntax inside using statement
        }
    }
}
