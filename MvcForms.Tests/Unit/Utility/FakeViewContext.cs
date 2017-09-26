using System.Web.Mvc;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeViewContext : ViewContext
    {
        public FakeViewContext()
        {
            HttpContext = new FakeHttpContext();
        }

        public FakeHttpContext FakeHttpContext => (FakeHttpContext)HttpContext;
    }
}
