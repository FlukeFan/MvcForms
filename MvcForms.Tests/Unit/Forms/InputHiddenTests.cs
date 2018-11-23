using FluentAssertions;
using MvcForms.Forms;
using MvcForms.StubApp.Models.Examples;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    [Ignore("updating to core")]
    public class InputHiddenTests
    {
        [Test]
        public void InputHidden()
        {
            var model = new FormInputsModel { StringInput1 = "123" };

            var tag = model.Helper().InputHidden(f => f.StringInput1).RenderTag();

            tag.TagName().Should().Be("input");
            tag.Attr("type").Should().Be("hidden");
            tag.Attr("name").Should().Be("StringInput1");
            tag.Attr("value").Should().Be("123");
        }

        [Test]
        public void InputHidden_Long()
        {
            var model = new FormInputsModel { Long = 123 };

            var tag = model.Helper().InputHidden(f => f.Long).RenderTag();

            tag.TagName().Should().Be("input");
            tag.Attr("type").Should().Be("hidden");
            tag.Attr("name").Should().Be("Long");
            tag.Attr("value").Should().Be("123");
        }
    }
}
