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
            var helper = FakeHtmlHelper.New<FormInputsModel>(new FormInputsModel());

            var input = helper.InputText(f => f.StringInput1);
            var tag = input.RenderTag();

            tag.Attr("type").Should().Be("text");
            tag.Attr("name").Should().Be("StringInput1");
            tag.Attr("id").Should().Be("StringInput1");
        }

        [Test]
        public void SanitizedId()
        {
            var helper = FakeHtmlHelper.New<FormInputsModel>(new FormInputsModel());

            var input = helper.InputText(f => f.InputsArray[1].StringInput2);
            var tag = input.RenderTag();

            tag.Attr("type").Should().Be("text");
            tag.Attr("name").Should().Be("InputsArray[1].StringInput2");
            tag.Attr("id").Should().Be("InputsArray_1__StringInput2");
        }
    }
}
