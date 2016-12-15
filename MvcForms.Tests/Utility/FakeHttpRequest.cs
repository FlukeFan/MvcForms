using System.Web;

namespace MvcForms.Tests.Utility
{
    public class FakeHttpRequest : HttpRequestBase
    {
        private string _rawUrl = "blah";

        public override string RawUrl { get { return _rawUrl; } }

        public FakeHttpRequest SetRawUrl(string rawUrl) { _rawUrl = rawUrl; return this; }
    }
}
