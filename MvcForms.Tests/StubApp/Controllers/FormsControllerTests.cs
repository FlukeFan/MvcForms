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
        public void Index_GET_Renders()
        {
            Test(async http =>
            {
                await http.GetAsync(FormsActions.Index());
            });
        }

        [Test]
        public void BootstrapHorizontal_GET_Renders()
        {
            Test(async http =>
            {
                await http.GetAsync(FormsActions.BootstrapHorizontal());
            });
        }

        [Test]
        public void ForModel_GET_RendersForm()
        {
            Test(async http =>
            {
                var response = await http.GetAsync(FormsActions.ForModel());

                var form = response.Form<ForModelPost>();
                form.GetText(m => m.BasicValue).Should().BeNullOrWhiteSpace();
            });
        }

        [Test]
        public void ForModel_GET_RendersValues()
        {
            Test(async http =>
            {
                var response = await http.GetAsync(FormsActions.ForModel("testValue"));

                var form = response.Form<ForModelPost>();
                form.GetText(m => m.BasicValue).Should().Be("testValue");

                response.Text.Should().Contain("BasicValue=testValue");
                response.Text.Should().Contain("{validation=}");
            });
        }

        [Test]
        public void ForModel_POST_RendersValidationErrors()
        {
            Test(async http =>
            {
                var response = await http.GetAsync(FormsActions.ForModel("testValue"))
                    .Form<ForModelPost>().Result
                    .SetText(m => m.BasicValue, "tst")
                    .Submit(r => r.SetExpectedResponse(HttpStatusCode.OK));

                var form = response.Form<ForModelPost>();
                form.GetText(m => m.BasicValue).Should().Be("tst");

                response.Text.Should().Contain("validationErrorMessage");
            });
        }

        [Test]
        public void ForModelUsing_GET_RendersForm()
        {
            Test(async http =>
            {
                var response = await http.GetAsync(FormsActions.ForModelUsing("test"));

                var form = response.Form<ForModelPost>();
                form.GetText(m => m.BasicValue).Should().Be("test");
            });
        }

        [Test]
        public void FormFor_GET_RendersForm()
        {
            Test(async http =>
            {
                var response = await http.GetAsync(FormsActions.FormFor("test"));

                var form = response.Form<FormForPost>();
                form.Method.Should().Be("post");
                form.Action.Should().Be(FormsActions.FormFor("test").PathOnly());
                form.Element.Id.Should().Be("canSetId");

                form.GetText(m => m.Value).Should().Be("test");

                response.Text.Should().Contain("</form>");
            });
        }
    }
}
