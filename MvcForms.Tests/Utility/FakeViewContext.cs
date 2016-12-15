using System.Web.Mvc;

namespace MvcForms.Tests.Utility
{
    public class FakeViewContext : ViewContext
    {
        public FakeViewContext()
        {
            HttpContext = new FakeHttpContext();
        }

        public FakeHttpContext FakeHttpContext { get { return (FakeHttpContext)HttpContext; } }
    }
}
