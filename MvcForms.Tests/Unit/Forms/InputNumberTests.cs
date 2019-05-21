using FluentAssertions;
using MvcForms.Forms;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    public class InputNumberTests
    {
        [Test]
        public void InputNumber_Int()
        {
            var model = new ExamplePostModel { Int = 123 };

            var tag = model.Helper().InputNumber(f => f.Int).RenderTag();

            tag.TagName().Should().Be("input");
            tag.Attr("type").Should().Be("number");
            tag.Attr("name").Should().Be("Int");
            tag.Attr("value").Should().Be("123");
        }

        [Test]
        public void InputNumber_NullableInt()
        {
            var model = new ExamplePostModel { NullableInt = 123 };

            var tag = model.Helper().InputNumber(f => f.NullableInt).RenderTag();

            tag.Attr("type").Should().Be("number");
            tag.Attr("name").Should().Be("NullableInt");
            tag.Attr("value").Should().Be("123");
        }

        [Test]
        public void InputNumber_String()
        {
            var model = new ExamplePostModel { String = "123" };

            var tag = model.Helper().InputNumber(f => f.String).RenderTag();

            tag.Attr("type").Should().Be("number");
            tag.Attr("name").Should().Be("String");
            tag.Attr("value").Should().Be("123");
        }
    }
}
