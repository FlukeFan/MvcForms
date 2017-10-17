﻿using System.Web.Mvc;

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
            var viewDataDictionary = new ViewDataDictionary<T>(model);
            var viewContext = new FakeViewContext(viewDataDictionary);
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

        public FakeViewContext FakeViewContext => (FakeViewContext)ViewContext;

        public FakeHtmlHelper<T> SetRawUrl(string rawUrl) { FakeViewContext.FakeHttpContext.FakeRequest.SetRawUrl(rawUrl); return this; }
    }
}
