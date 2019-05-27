using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcForms.Forms;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    public class InputTextTests
    {
        [Test]
        public void InputText()
        {
            var model = new ExamplePostModel { String = "existing value" };

            var tag = model.Helper().InputText(f => f.String).RenderTag();

            tag.Attr("type").Should().Be("text");
            tag.Attr("name").Should().Be("String");
            tag.Attr("id").Should().Be("String");
            tag.Attr("value").Should().Be("existing value");
        }

        [Test]
        public void SanitizedId()
        {
            var helper = FakeHtmlHelper.New(new ExamplePostModel());

            var tag = helper.InputText(f => f.InputsArray[1].String2).RenderTag();

            tag.Attr("type").Should().Be("text");
            tag.Attr("name").Should().Be("InputsArray[1].String2");
            tag.Attr("id").Should().Be("InputsArray_1__String2");
        }

        [Test]
        public void AttemptedValue()
        {
            var helper = FakeHtmlHelper.New(new ExamplePostModel());
            helper.ViewData.ModelState.SetModelValue("String2", new ValueProviderResult("raw value"), "attempted value");

            var input = helper.InputText(f => f.String2);

            input.Value().Should().Be("attempted value");
        }

        [Test]
        public void AutoComplete_Off()
        {
            var model = new ExamplePostModel();

            var tag = model.Helper().InputText(f => f.String).AutoCompleteOff().RenderTag();

            tag.Attr("autocomplete").Should().Be("off");
        }

        [Test]
        public void Form_AutoCompleteDefault()
        {
            var model = new ExamplePostModel();

            var tag = model.Helper().InputText(f => f.String).RenderTag();

            tag.ToString().Should().NotContain("autocomplete");
        }

        [Test]
        public void NullValue()
        {
            var model = new ExamplePostModel();

            var tag = model.Helper().InputText(f => f.String).RenderTag();

            tag.Value().Should().Be("");
        }
    }
}
