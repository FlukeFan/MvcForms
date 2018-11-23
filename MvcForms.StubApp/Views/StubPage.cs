using Microsoft.AspNetCore.Mvc.Razor;
using MvcForms.StubApp.Views.Shared;

namespace MvcForms.StubApp.Views
{
    public abstract class StubPage<T> : RazorPage<T>
    {
        public virtual void SetLayout(string title)
        {
            ViewBag.Title = title;

            Layout = Context.Request.IsPjaxModal()
                ? SharedViews.PjaxModal
                : SharedViews.Master;
        }
    }
}