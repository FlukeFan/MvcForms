using FluentAssertions;
using MvcForms.StubApp.Controllers;
using MvcForms.Tests.SystemTests.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.SystemTests
{
    public class ModalTests_NoJs : ModalTests
    {
        protected override bool JsDisabled() { return true; }
    }

    public abstract class ModalTests : NoJsTest
    {
        [Test]
        public void Cancel()
        {
            App.GoTo(HomeActions.Testing());
            App.Navigate("Modal");

            App.ShouldSeeText("Modal");

            var setScript = "$('#insidePjaxPartial').text('modified');";
            var getScript = "return $('#insidePjaxPartial').text();";

            if (!JsDisabled())
            {
                App.Exec(setScript);
                App.Exec(getScript).Should().Be("modified");
            }

            App.Navigate("Modal1");

            App.ShouldSeeText("Page1");

            App.Navigate("Cancel");

            App.ShouldSeeText("ModalIndex");
            App.ShouldNotSeeText("Page1");

            if (!JsDisabled())
                App.Exec(getScript).Should().Be("modified", "cancelling dialog should not refresh page");
        }

        [Test]
        public void Ok()
        {
            App.GoTo(ModalActions.Index());
            App.Navigate("Modal1");

            App.Submit("OK");

            App.ShouldSeeText("ModalIndex");
        }

        [Test]
        public void NestedModal()
        {
            App.GoTo(ModalActions.Index());

            App.ShouldSeeText("Modal");

            App.Navigate("Modal1");

            App.ShouldSeeText("Page1");
            App.ShouldSeeText("Count=0");

            App.Navigate("Modal2");

            App.ShouldSeeText("Page2");

            App.Navigate("Cancel");

            if (JsDisabled())
                App.ShouldSeeText("Count=1");
            else
                App.ShouldSeeText("Count=0");

            App.Navigate("Modal2");
            App.Submit("OK");

            App.ShouldSeeText("Count=3");

            App.Navigate("Cancel");

            App.ShouldSeeText("ModalIndex");
        }
    }
}
