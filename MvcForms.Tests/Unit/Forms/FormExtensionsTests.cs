using FluentAssertions;
using MvcForms.Forms;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    public class FormExtensionsTests
    {
        public class TestViewModel
        {
            public TestPostModel Cmd = new TestPostModel();
        }

        public class TestPostModel { }

        [Test]
        public void FormFor()
        {
            var html = FakeHtmlHelper.New(new TestViewModel());

            html.SetRawUrl("http://fake.url");

            var form = html.FormFor(html.ViewData.Model.Cmd);
            form.Action().Should().Be("http://fake.url:80/");
        }
    }
}
