using System.Net;
using System.Web.Mvc;
using FluentAssertions;
using MvcForms.StubApp.Controllers;
using MvcForms.Tests.StubApp.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.StubApp.Controllers
{
    [TestFixture]
    public class SystemControllerTests : StubAppTest
    {
        [Test]
        public void PjaxPage1_GET_Renders()
        {
            StubApp.Test(http =>
            {
                http.Get(SystemActions.PjaxPage1());
            });
        }

        [Test]
        public void PjaxPage2_GET_Renders()
        {
            StubApp.Test(http =>
            {
                http.Get(SystemActions.PjaxPage2());
            });
        }

        [Test]
        public void PjaxPage3_GET_Redirects()
        {
            StubApp.Test(http =>
            {
                var response = http.Get(SystemActions.PjaxPage3(), r => r.SetExpectedResponse(HttpStatusCode.Redirect));
                response.ActionResultOf<RedirectResult>().Url.Should().Be(SystemActions.PjaxPage1());
            });
        }
    }
}
