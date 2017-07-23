using System.Web.Mvc;
using MvcForms.StubApp.Views.Shared;

namespace MvcForms.StubApp.Views
{
    public abstract class StubPage<T> : WebViewPage<T>
    {
        public void SetLayout(string title)
        {
            ViewBag.Title = title;
            Layout = SharedViews.MasterPjaxWhole;

            var pjax = Request.Headers["X-PJAX"];

            if (string.IsNullOrEmpty(pjax))
                return;

            Layout = SharedViews.MasterPjaxPartial;
        }
    }
}