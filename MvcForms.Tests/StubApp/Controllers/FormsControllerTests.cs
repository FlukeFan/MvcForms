using System.Net;
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
        public void BootstrapHorizontal_GET_Renders()
        {
            StubApp.Test(http =>
            {
                http.Get(FormsActions.BootstrapHorizontal());
            });
        }

        [Test]
        public void ForModel_GET_RendersForm()
        {
            StubApp.Test(http =>
            {
                var response = http.Get(FormsActions.ForModel());

                var form = response.Form<ForModelPost>();
                form.GetText(m => m.BasicValue).Should().BeNullOrWhiteSpace();
            });
        }

        [Test]
        public void ForModel_GET_RendersValues()
        {
            StubApp.Test(http =>
            {
                var response = http.Get(FormsActions.ForModel("testValue"));

                var form = response.Form<ForModelPost>();
                form.GetText(m => m.BasicValue).Should().Be("testValue");

                response.Text.Should().Contain("BasicValue=testValue");
                response.Text.Should().Contain("{validation=}");
            });
        }

        [Test]
        public void ForModel_POST_RendersValidationErrors()
        {
            StubApp.Test(http =>
            {
                var response = http.Get(FormsActions.ForModel("testValue"))
                    .Form<ForModelPost>()
                    .SetText(m => m.BasicValue, "tst")
                    .Submit(http, r => r.SetExpectedResponse(HttpStatusCode.OK));

                var form = response.Form<ForModelPost>();
                form.GetText(m => m.BasicValue).Should().Be("tst");

                response.Text.Should().Contain("validationErrorMessage");
            });
        }

        [Test]
        public void ForModelUsing_GET_RendersForm()
        {
            StubApp.Test(http =>
            {
                var response = http.Get(FormsActions.ForModelUsing("test"));

                var form = response.Form<ForModelPost>();
                form.GetText(m => m.BasicValue).Should().Be("test");
            });
        }

        [Test]
        public void FormFor_GET_RendersForm()
        {
            StubApp.Test(http =>
            {
                var response = http.Get(FormsActions.FormFor("test"));

                var form = response.Form<FormForPost>();
                form.GetText(m => m.Value).Should().Be("test");

                response.Text.Should().Contain("</form>");
            });
        }
    }
}
