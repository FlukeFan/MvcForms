using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeHtmlHelper
    {
        public static FakeHtmlHelper<object> New()
        {
            return New<object>(null);
        }

        public static FakeHtmlHelper<T> New<T>(T model)
        {
            //var viewDataDictionary = new ViewDataDictionary<T>(model);
            //var viewContext = new FakeViewContext(viewDataDictionary);
            //var viewDataContainer = new ViewDataContainer { ViewData = viewDataDictionary };

            return new FakeHtmlHelper<T>(null, null, model);
        }

        private class ViewDataContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData { get; set; }
        }
    }

    public interface IViewDataContainer
    {

    }

    public class FakeHtmlHelper<T> : HtmlHelper<T>
    {
        public T Model { get; protected set; }

        public FakeHtmlHelper(object viewContext, IViewDataContainer viewDataContainer, T model) : base(null, null, null, null, null, null, null)
        {
            Model = model;
        }

        //public FakeViewContext FakeViewContext => (FakeViewContext)ViewContext;

        public FakeHtmlHelper<T> SetRawUrl(string rawUrl) { /*FakeViewContext.FakeHttpContext.FakeRequest.SetRawUrl(rawUrl);*/ return this; }
    }
}
