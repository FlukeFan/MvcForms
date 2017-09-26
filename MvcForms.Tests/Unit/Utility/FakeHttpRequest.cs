using System.Collections.Specialized;
using System.Web;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeHttpRequest : HttpRequestBase
    {
        private string _rawUrl = "http://unit.test/default/rawUrl";

        public override string RawUrl           => _rawUrl;
        public override string ApplicationPath  => "/";

        public FakeHttpRequest SetRawUrl(string rawUrl) { _rawUrl = rawUrl; return this; }

        public override NameValueCollection QueryString
        {
            get
            {
                var queryIndex = _rawUrl.IndexOf('?');

                if (queryIndex < 0)
                    return new NameValueCollection();

                var query = _rawUrl.Substring(queryIndex);

                return HttpUtility.ParseQueryString(query);
            }
        }
    }
}
