using FluentAssertions;
using MvcForms.StubApp.Controllers;
using MvcForms.StubApp.Models.Forms;
using MvcForms.Tests.StubApp.Utility;
using MvcTesting.Html;
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
                var response = http.Get(FormsActions.ForModel());

                var form = response.Form<ForModelPost>();

                form.GetText(m => m.BasicValue).Should().BeNullOrWhiteSpace();
            });
        }

        [Test]
        public void ForModel_Get_RendersValues()
        {
            StubApp.Test(http =>
            {
                var response = http.Get(FormsActions.ForModel("testValue"));

                var form = response.Form<ForModelPost>();

                form.GetText(m => m.BasicValue).Should().Be("testValue");
            });
        }
    }
}
