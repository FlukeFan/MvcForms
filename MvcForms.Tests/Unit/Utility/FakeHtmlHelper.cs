using System.Web.Mvc;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeHtmlHelper
    {
        public static FakeHtmlHelper<T> New<T>(T model)
        {
            var viewContext = new FakeViewContext();

            var viewDataDictionary = new ViewDataDictionary<T>(model);
            var viewDataContainer = new ViewDataContainer { ViewData = viewDataDictionary };

            return new FakeHtmlHelper<T>(viewContext, viewDataContainer, model);
        }

        private class ViewDataContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData { get; set; }
        }
    }

    public class FakeHtmlHelper<T> : HtmlHelper<T>
    {
        public T Model { get; protected set; }

        public FakeHtmlHelper(FakeViewContext viewContext, IViewDataContainer viewDataContainer, T model) : base(viewContext, viewDataContainer)
        {
            Model = model;
        }

        public FakeViewContext FakeViewContext { get { return (FakeViewContext)ViewContext; } }
    }
}
