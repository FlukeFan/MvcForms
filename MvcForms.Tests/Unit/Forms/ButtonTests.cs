using FluentAssertions;
using MvcForms.Forms;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    public class ButtonTests
    {
        [Test]
        public void CanContainHtmlContent()
        {
            var helper = FakeHtmlHelper.New<object>(null);

            var button = helper.ButtonSubmit("<b>bold</b>");

            var html = button.ToHtmlString();
            html.Should().Contain("<b>bold</b>");
        }
    }
}
