using System.Web.Mvc;
using MvcForms.StubApp.Views.Shared;

namespace MvcForms.StubApp.Views
{
    public abstract class StubPage<T> : WebViewPage<T>
    {
        public virtual void SetLayout(string title)
        {
            ViewBag.Title = title;

            Layout = Request.IsPjaxModal()
                ? SharedViews.PjaxModal
                : SharedViews.Master;
        }
    }
}