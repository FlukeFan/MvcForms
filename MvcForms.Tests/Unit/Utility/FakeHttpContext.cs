using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeHttpContext : HttpContext
    {
        public FakeServiceProvider          FakeServiceProvider { get; protected set; }
        public override IServiceProvider    RequestServices     { get { return FakeServiceProvider; } set { } }

        public FakeHttpRequest              FakeHttpRequest     { get; protected set; }
        public override HttpRequest         Request             { get { return FakeHttpRequest; } }

        public FakeHttpContext()
        {
            FakeServiceProvider = new FakeServiceProvider();
            FakeHttpRequest = new FakeHttpRequest();
        }

        #region NotImplemented

        public override IFeatureCollection Features => throw new NotImplementedException();

        public override HttpResponse Response => throw new NotImplementedException();

        public override ConnectionInfo Connection => throw new NotImplementedException();

        public override WebSocketManager WebSockets => throw new NotImplementedException();

        [Obsolete]
        public override AuthenticationManager Authentication => throw new NotImplementedException();

        public override ClaimsPrincipal User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IDictionary<object, object> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override CancellationToken RequestAborted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string TraceIdentifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ISession Session { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Abort()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
