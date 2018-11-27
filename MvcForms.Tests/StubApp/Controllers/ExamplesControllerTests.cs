using System.Net;
using FluentAssertions;
using MvcForms.StubApp.Controllers;
using MvcForms.Tests.StubApp.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.StubApp.Controllers
{
    [TestFixture]
    public class ExamplesControllerTests : StubAppTest
    {
        [Test]
        public void Index_GET_Renders()
        {
            Test(async http =>
            {
                var response = await http.GetAsync(ExamplesActions.Index());

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            });
        }

        [Test]
        public void Buttons_GET_Renders()
        {
            Test(async http =>
            {
                var response = await http.GetAsync(ExamplesActions.Buttons());

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            });
        }

        [Test]
        public void Inputs_GET_Renders()
        {
            Test(async http =>
            {
                var response = await http.GetAsync(ExamplesActions.Inputs());

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            });
        }
    }
}
