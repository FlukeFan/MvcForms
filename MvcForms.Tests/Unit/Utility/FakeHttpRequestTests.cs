using FluentAssertions;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Utility
{
    [TestFixture]
    public class FakeHttpRequestTests
    {
        [Test]
        public void FullUrl()
        {
            var request = new FakeHttpRequest();
            request.SetRawUrl("http://unit.test:1234/test/url?a=1&B=2");

            request.Scheme.Should().Be("http");
            request.Host.Value.Should().Be("unit.test:1234");
            request.PathBase.Value.Should().Be("");
            request.Path.Value.Should().Be("/test/url");
            request.QueryString.Value.Should().Be("?a=1&B=2");
        }

        [Test]
        public void SparseUrl()
        {
            var request = new FakeHttpRequest();
            request.SetRawUrl("http://unit.test");

            request.Scheme.Should().Be("http");
            request.Host.Value.Should().Be("unit.test:80");
            request.PathBase.Value.Should().Be("");
            request.Path.Value.Should().Be("/");
            request.QueryString.Value.Should().Be("");
        }

        [Test]
        public void QueryCollection()
        {
            var request = new FakeHttpRequest();
            request.SetRawUrl("/url?a=1&B=2&B=3");

            request.Query["a"].Should().BeEquivalentTo("1");
            request.Query["B"].Should().BeEquivalentTo("2", "3");
        }
    }
}
