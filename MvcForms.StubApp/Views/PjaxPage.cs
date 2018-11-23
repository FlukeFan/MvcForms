using MvcForms.StubApp.Views.Shared;

namespace MvcForms.StubApp.Views
{
    public abstract class PjaxPage<T> : StubPage<T>
    {
        public override void SetLayout(string title)
        {
            base.SetLayout(title);

            Layout = Context.Request.IsPjaxModal()
                ? SharedViews.PjaxModal
                : Context.Request.IsPjax()
                    ? SharedViews.PjaxPartial
                    : SharedViews.PjaxWhole;
        }
    }
}