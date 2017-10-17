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
            var helper = FakeHtmlHelper.New<FormInputsModel>(null);

            var input = helper.LabelledInputText("test", f => f.StringInput1);
            input.RenderTag();
            var tag = (input as IRenderedFormRow).Control;

            tag.Attr("type").Should().Be("text");
        }
    }
}
