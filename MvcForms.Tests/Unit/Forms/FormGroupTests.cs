using FluentAssertions;
using MvcForms.Forms;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    public class FormGroupTests
    {
        [Test]
        public void FormGroup()
        {
            var model = new ExamplePostModel();

            var helper = FakeHtmlHelper.New(model);

            var group = helper.LabelledInputText("test label", f => f.String);
            var tags = Render(group);

            tags.Label.Attr("for").Should().Be(tags.Control.Id());
        }

        public IRenderedFormGroup Render<T>(FormGroup<T> group)
            where T : Control
        {
            group.RenderTag();
            return (IRenderedFormGroup)group;
        }
    }
}
