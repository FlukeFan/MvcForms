using System.Net;
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
            TestForAllStylers(async http =>
            {
                await http.GetAsync(ExamplesActions.Index());
            });
        }

        [Test]
        public void Buttons_GET_Renders()
        {
            TestForAllStylers(async http =>
            {
                await http.GetAsync(ExamplesActions.Buttons());
            });
        }

        [Test]
        public void Inputs_GET_Renders()
        {
            TestForAllStylers(async http =>
            {
                await http.GetAsync(ExamplesActions.Inputs());
            });
        }

        [Test]
        public void Scroll_POST()
        {
            Test(async http =>
            {
                var response = await http.GetAsync(ExamplesActions.Scroll());
                await response.Form<object>().Submit(r => r.SetExpectedResponse(HttpStatusCode.OK));
            });
        }
    }
}
