using System.Web;
using FluentAssertions;
using MvcForms.Navigation;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Navigation
{
    [TestFixture]
    public class LinkButtonTests
    {
        [Test]
        public void CanContainHtmlContent()
        {
            var helper = FakeHtmlHelper.New();

            var linkButton = helper.LinkButton("<i>italic</i>", "~/home");

            var html = linkButton.ToHtmlString();
            html.Should().Contain("<i>italic</i>");
        }

        [Test]
        public void Cancel_UsesReturnUrl()
        {
            var helper = FakeHtmlHelper.New()
                .SetRawUrl("test?modalReturnUrl=" + HttpUtility.UrlEncode("http://return.url"));

            var linkButton = helper.LinkButtonCancelModal().DefaultModalReturn("~/default");

            var html = linkButton.ToHtmlString();
            html.Should().Contain("href=\"http://return.url\"");
        }

        [Test]
        public void Cancel_HasHash_When_MissingReturnUrl_And_NoDefaultSupplied()
        {
            var helper = FakeHtmlHelper.New();

            var linkButton = helper.LinkButtonCancelModal();

            var html = linkButton.ToHtmlString();
            html.Should().Contain("href=\"#\"");
        }

        [Test]
        public void Cancel_HasDefaultUrl_When_DefaultSupplied()
        {
            var helper = FakeHtmlHelper.New();

            var linkButton = helper.LinkButtonCancelModal().DefaultModalReturn("~/default");

            var html = linkButton.ToHtmlString();
            html.Should().Contain("href=\"/default\"");
        }
    }
}
