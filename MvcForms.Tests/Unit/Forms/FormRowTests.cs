using FluentAssertions;
using MvcForms.Forms;
using MvcForms.StubApp.Models.Examples;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    public class FormRowTests
    {
        [Test]
        public void FormRow()
        {
            var model = new FormInputsModel();

            var helper = FakeHtmlHelper.New(model);

            var formRow = helper.LabelledInputText("test label", f => f.StringInput1);
            var tags = Render(formRow);

            tags.Label.Attr("for").Should().Be(tags.Control.Id());
        }

        public IRenderedFormRow Render<T>(FormRow<T> formRow)
            where T : Control
        {
            formRow.RenderTag();
            return (IRenderedFormRow)formRow;
        }
    }
}
