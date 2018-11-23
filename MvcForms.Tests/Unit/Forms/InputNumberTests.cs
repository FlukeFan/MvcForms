using FluentAssertions;
using MvcForms.Forms;
using MvcForms.StubApp.Models.Examples;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    [Ignore("updating to core")]
    public class InputNumberTests
    {
        [Test]
        public void InputNumber_Int()
        {
            var model = new FormInputsModel { Int = 123 };

            var tag = model.Helper().InputNumber(f => f.Int).RenderTag();

            tag.TagName().Should().Be("input");
            tag.Attr("type").Should().Be("number");
            tag.Attr("name").Should().Be("Int");
            tag.Attr("value").Should().Be("123");
        }

        [Test]
        public void InputNumber_NullableInt()
        {
            var model = new FormInputsModel { NullableInt = 123 };

            var tag = model.Helper().InputNumber(f => f.NullableInt).RenderTag();

            tag.Attr("type").Should().Be("number");
            tag.Attr("name").Should().Be("NullableInt");
            tag.Attr("value").Should().Be("123");
        }

        [Test]
        public void InputNumber_String()
        {
            var model = new FormInputsModel { StringInput1 = "123" };

            var tag = model.Helper().InputNumber(f => f.StringInput1).RenderTag();

            tag.Attr("type").Should().Be("number");
            tag.Attr("name").Should().Be("StringInput1");
            tag.Attr("value").Should().Be("123");
        }

        [Test]
        public void Labelled()
        {
            var model = new FormInputsModel();

            model.Helper().LabelledInputNumber("label", m => m.Int).RenderTag().ToHtmlString().Contains("type=\"number\"");
            model.Helper().LabelledInputNumber("label", m => m.NullableInt).RenderTag().ToHtmlString().Contains("type=\"number\"");
            model.Helper().LabelledInputNumber("label", m => m.StringInput1).RenderTag().ToHtmlString().Contains("type=\"number\"");
        }
    }
}
