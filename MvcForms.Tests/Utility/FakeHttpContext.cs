using System.Web;

namespace MvcForms.Tests.Utility
{
    public class FakeHttpContext : HttpContextBase
    {
        private FakeHttpRequest _request = new FakeHttpRequest();

        public override HttpRequestBase Request { get { return _request; } }

        public FakeHttpRequest FakeRequest { get { return _request; } }
    }
}
