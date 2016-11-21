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
            StubApp.Test(http =>
            {
                http.Get(HomeActions.Index());
            });
        }
    }
}
