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
                await http.GetAsync(ExamplesActions.Index());
            });
        }

        [Test]
        public void Buttons_GET_Renders()
        {
            Test(async http =>
            {
                await http.GetAsync(ExamplesActions.Buttons());
            });
        }

        [Test]
        public void Inputs_GET_Renders()
        {
            Test(async http =>
            {
                await http.GetAsync(ExamplesActions.Inputs());
            });
        }
    }
}
