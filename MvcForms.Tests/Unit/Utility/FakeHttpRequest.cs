using System.Web;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeHttpRequest : HttpRequestBase
    {
        private string _rawUrl = "blah";

        public override string RawUrl           => _rawUrl;
        public override string ApplicationPath  => "/";

        public FakeHttpRequest SetRawUrl(string rawUrl) { _rawUrl = rawUrl; return this; }
    }
}
