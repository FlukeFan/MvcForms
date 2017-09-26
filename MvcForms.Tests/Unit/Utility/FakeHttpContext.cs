using System;
using System.Web;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeHttpContext : HttpContextBase
    {
        private FakeHttpRequest _request = new FakeHttpRequest();

        public override HttpRequestBase Request     => _request;
        public FakeHttpRequest          FakeRequest => _request;

        public override object GetService(Type serviceType)
        {
            return null;
        }
    }
}
