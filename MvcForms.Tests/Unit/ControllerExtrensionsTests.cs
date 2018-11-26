using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit
{
    [TestFixture]
    public class ControllerExtrensionsTests
    {
        [Test]
        public void ReturnModal_RedirectsToReturnUrl()
        {
            var controller = FakeController.New()
                .SetRawUrl("http://unit.test?modalReturnUrl=/result");

            var result = controller.ReturnModal() as RedirectResult;

            result.Should().NotBeNull();
            result.Url.Should().Be("/result");
        }

        [Test]
        public void ReturnModal_RedirectsToDefaultUrl_IfModalReturnIsEmpty()
        {
            var controller = FakeController.New()
                .SetRawUrl("http://unit.test?modalReturnMissing=/result");

            var result = controller.ReturnModal("~/default") as RedirectResult;

            result.Should().NotBeNull();
            result.Url.Should().Be("~/default");
        }

        [Test]
        public void ReturnModal_RedirectsToResponseUrl_IfDefaultNotSupplied()
        {
            var requestUrl = "http://unit.test:80/?modalReturnMissing=/result";
            var controller = FakeController.New()
                .SetRawUrl(requestUrl);

            var result = controller.ReturnModal() as RedirectResult;

            result.Should().NotBeNull();
            result.Url.Should().Be(requestUrl);
        }
    }
}
