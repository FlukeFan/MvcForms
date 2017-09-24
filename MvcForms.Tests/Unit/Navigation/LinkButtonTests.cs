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
            var helper = FakeHtmlHelper.New<object>(null);

            var linkButton = helper.LinkButton("<i>italic</i>", "~/home");

            var html = linkButton.ToHtmlString();
            html.Should().Contain("<i>italic</i>");
        }
    }
}
