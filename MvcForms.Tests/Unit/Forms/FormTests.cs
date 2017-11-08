using FluentAssertions;
using MvcForms.Forms;
using MvcForms.StubApp.Models.Examples;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    public class FormTests
    {
        [Test]
        public void AutoComplete_Off()
        {
            var model = new FormInputsModel { StringInput1 = "existing value" };

            var tag = model.Helper().Form()
                .AutoCompleteOff()
                .RenderTag();

            tag.Attr("autocomplete").Should().Be("off");
        }

        [Test]
        public void Form_AutoCompleteDefault()
        {
            var model = new FormInputsModel { StringInput1 = "existing value" };

            var tag = model.Helper().Form().RenderTag();

            tag.ToString().Should().NotContain("autocomplete");
        }
    }
}
