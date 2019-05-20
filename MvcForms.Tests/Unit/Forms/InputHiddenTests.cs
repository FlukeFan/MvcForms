using FluentAssertions;
using MvcForms.Forms;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    public class InputHiddenTests
    {
        [Test]
        public void InputHidden()
        {
            var model = new ExamplePostModel { String = "123" };

            var tag = model.Helper().InputHidden(f => f.String).RenderTag();

            tag.TagName().Should().Be("input");
            tag.Attr("type").Should().Be("hidden");
            tag.Attr("name").Should().Be("String");
            tag.Attr("value").Should().Be("123");
        }

        [Test]
        public void InputHidden_Long()
        {
            var model = new ExamplePostModel { Long = 123 };

            var tag = model.Helper().InputHidden(f => f.Long).RenderTag();

            tag.TagName().Should().Be("input");
            tag.Attr("type").Should().Be("hidden");
            tag.Attr("name").Should().Be("Long");
            tag.Attr("value").Should().Be("123");
        }
    }
}
