using System.IO;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeViewContext : ViewContext
    {
        public FakeHttpContext  FakeHttpContext { get { return (FakeHttpContext)HttpContext; } }
        public FakeView         FakeView        { get { return (FakeView)View; } }

        public FakeViewContext()
        {
            HttpContext = new FakeHttpContext();
            View = new FakeView();

            RouteData = new RouteData();
            ActionDescriptor = new ActionDescriptor();
            FormContext = new FormContext();
            Writer = new StringWriter();
        }

        internal void Contextualize(ViewContext viewContext)
        {
            HttpContext = viewContext.HttpContext;
            View = viewContext.View;
        }
    }
}
