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

            var group = helper.FormGroup("test label", fg => fg.InputText(f => f.String));
            var tags = Render(group);

            tags.Label.Attr("for").Should().Be(tags.Control.Id());
        }

        [Test]
        public void MutateControl()
        {
            var model = new ExamplePostModel();

            var group = model.Helper().FormGroup("test label", fg => fg.InputText(f => f.String)).Control(tb => tb.Value("override"));
            var tag = Render(group);

            tag.Control.Value().Should().Be("override");
        }

        public IRenderedFormGroup Render<T>(FormGroup<T> group)
            where T : Control
        {
            group.RenderTag();
            return (IRenderedFormGroup)group;
        }
    }
}
