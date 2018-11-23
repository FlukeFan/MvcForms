using FluentAssertions;
using MvcForms.Forms;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    [Ignore("updating to core")]
    public class ButtonTests
    {
        [Test]
        public void CanContainHtmlContent()
        {
            var helper = FakeHtmlHelper.New<object>(null);

            var button = helper.ButtonSubmit(helper.Raw("<b>bold</b>"));

            var html = button.ToHtmlString();
            html.Should().Contain("<b>bold</b>");
        }

        [Test]
        public void HelperStringsAreEscaped()
        {
            var helper = FakeHtmlHelper.New<object>(null);

            var button = helper.ButtonSubmit("<b>bold</b>");

            var html = button.ToHtmlString();
            html.Should().Contain("&lt;b&gt;");
        }

        [Test]
        public void ControlStringsAreEscaped()
        {
            var helper = FakeHtmlHelper.New<object>(null);

            var button = new Button(helper, "submit", helper.Raw("test"))
                .Content("<b>bold</b>");

            var html = button.ToHtmlString();
            html.Should().Contain("&lt;b&gt;");
        }
    }
}
