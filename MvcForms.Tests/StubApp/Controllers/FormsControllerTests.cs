using MvcForms.StubApp.Controllers;
using MvcForms.Tests.StubApp.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.StubApp.Controllers
{
    [TestFixture]
    public class FormsControllerTests : StubAppTest
    {
        [Test]
        public void ForModel_Get_RendersForm()
        {
            StubApp.Test(http =>
            {
                http.Get(FormsActions.ForModel());
            });
        }
    }
}
