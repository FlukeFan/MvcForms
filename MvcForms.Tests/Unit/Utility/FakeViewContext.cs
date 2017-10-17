using System.Web.Mvc;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeViewContext : ViewContext
    {
        public FakeViewContext(ViewDataDictionary viewData)
        {
            HttpContext = new FakeHttpContext();
            ViewData = viewData;
        }

        public FakeHttpContext FakeHttpContext => (FakeHttpContext)HttpContext;
    }
}
