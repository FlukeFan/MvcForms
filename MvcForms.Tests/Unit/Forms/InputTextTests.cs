using System.Web.Mvc;
using FluentAssertions;
using MvcForms.Forms;
using MvcForms.StubApp.Models.Examples;
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
            var model = new FormInputsModel { StringInput1 = "existing value" };

            var tag = model.Helper().InputText(f => f.StringInput1).RenderTag();

            tag.Attr("type").Should().Be("text");
            tag.Attr("name").Should().Be("StringInput1");
            tag.Attr("id").Should().Be("StringInput1");
            tag.Attr("value").Should().Be("existing value");
        }

        [Test]
        public void SanitizedId()
        {
            var helper = FakeHtmlHelper.New(new FormInputsModel());

            var tag = helper.InputText(f => f.InputsArray[1].StringInput2).RenderTag();

            tag.Attr("type").Should().Be("text");
            tag.Attr("name").Should().Be("InputsArray[1].StringInput2");
            tag.Attr("id").Should().Be("InputsArray_1__StringInput2");
        }

        [Test]
        public void AttemptedValue()
        {
            var helper = FakeHtmlHelper.New(new FormInputsModel());
            helper.ViewData.ModelState.Add("StringInput1", new ModelState { Value = new ValueProviderResult("raw value", "attempted value", null) });

            var input = helper.InputText(f => f.StringInput1);

            input.Value().Should().Be("attempted value");
        }

        [Test]
        public void CanLabel()
        {
            var model = new FormInputsModel();

            model.Helper().LabelledInputText("label", m => m.StringInput1).RenderTag().ToHtmlString().Contains("type=\"text\"");
        }
    }
}
