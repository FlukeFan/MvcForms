using FluentAssertions;
using HtmlTags;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit
{
    [TestFixture]
    public class ControlTests
    {
        public class MyControl : Control
        {
            public MyControl() : base(FakeHtmlHelper.New())
            {
            }

            protected override HtmlTag CreateTag()
            {
                return new HtmlTag("div");
            }
        }

        [Test]
        public void TagMutation()
        {
            var control = new MyControl();

            control.Tag(t => t.Attr("data-test1", "value1"));

            control.RenderTag().Attr("data-test1").Should().Be("value1");

            control.Tag(t => t.Attr("data-test2", "value2"));

            control.RenderTag().Attr("data-test1").Should().BeNullOrWhiteSpace();
            control.RenderTag().Attr("data-test2").Should().Be("value2");

            control.ThenTag(t => t.Attr("data-test3", "value3"));

            control.RenderTag().Attr("data-test1").Should().BeNullOrWhiteSpace();
            control.RenderTag().Attr("data-test2").Should().Be("value2");
            control.RenderTag().Attr("data-test3").Should().Be("value3");
        }
    }
}
