using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeHttpRequest : HttpRequest
    {
        private Uri _uri;
        private HostString _hostString;
        private QueryString _queryString;
        private PathString _pathBase;
        private PathString _path;
        private IQueryCollection _query;
        private IHeaderDictionary _headers;

        public FakeHttpRequest()
        {
            SetRawUrl("http://unit.test/default/rawUrl");
        }

        public override string              Scheme      { get => _uri.Scheme;       set => throw new NotImplementedException(); }
        public override HostString          Host        { get => _hostString;       set => throw new NotImplementedException(); }
        public override PathString          PathBase    { get => _pathBase;         set => throw new NotImplementedException(); }
        public override PathString          Path        { get => _path;             set => throw new NotImplementedException(); }
        public override QueryString         QueryString { get => _queryString;      set => throw new NotImplementedException(); }
        public override IQueryCollection    Query       { get => _query;            set => throw new NotImplementedException(); }

        public override IHeaderDictionary   Headers     { get => _headers; }

        public void SetRawUrl(string rawUrl)
        {
            if (rawUrl?.ToLower()?.StartsWith("http") == false)
            {
                rawUrl = rawUrl?.StartsWith("/") == true
                    ? $"http://unit.test{rawUrl}"
                    : $"http://unit.test/{rawUrl}";
            }

            _uri = new Uri(rawUrl);
            _hostString = new HostString(_uri.Host, _uri.Port);
            _queryString = new QueryString(_uri.Query);
            _pathBase = new PathString("");
            _path = _uri.PathAndQuery.Substring(0, _uri.PathAndQuery.Length - _queryString.Value.Length);

            var parsedQuery = QueryHelpers.ParseQuery(_queryString.Value);
            _query = new QueryCollection(parsedQuery);

            _headers = new HeaderDictionary();
        }

        #region not implemented

        public override HttpContext HttpContext => throw new NotImplementedException();

        public override string Method { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override bool IsHttps { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string Protocol { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

       public override IRequestCookieCollection Cookies { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override long? ContentLength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string ContentType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override Stream Body { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override bool HasFormContentType => throw new NotImplementedException();

        public override IFormCollection Form { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
