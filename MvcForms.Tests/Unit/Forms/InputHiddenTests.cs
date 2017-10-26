using FluentAssertions;
using MvcForms.Forms;
using MvcForms.StubApp.Models.Examples;
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
            var model = new FormInputsModel();
            var helper = FakeHtmlHelper.New(model);

            var input = helper.InputHidden(f => f.StringInput1);
            var tag = input.RenderTag();

            tag.TagName().Should().Be("input");
            tag.Attr("type").Should().Be("hidden");
            tag.Attr("name").Should().Be("StringInput1");
        }
    }
}
