using System.Net;
using FluentAssertions;
using MvcForms.StubApp.Controllers;
using MvcForms.Tests.StubApp.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.StubApp.Controllers
{
    [TestFixture]
    public class HomeControllerTests : StubAppTest
    {
        [Test]
        public void Index_GET_Renders()
        {
            Test(async http =>
            {
                var response = await http.GetAsync(HomeActions.Index());

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            });
        }
    }
}
