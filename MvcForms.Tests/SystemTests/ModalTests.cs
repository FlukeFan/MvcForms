using MvcForms.StubApp.Controllers;
using MvcForms.Tests.SystemTests.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.SystemTests
{
    public class ModalTests_NoJs : ModalTests
    {
        protected override bool JsDisabled() { return true; }
    }

    public class ModalTests_Js : ModalTests
    {
        protected override bool JsDisabled() { return false; }
    }

    public abstract class ModalTests : NoJsTest
    {
        [Test]
        public void Cancel()
        {
            App.GoTo(ModalActions.Index());

            App.ShouldSeeText("Modal");

            App.Navigate("Modal1");

            App.ShouldSeeText("Page1");

            App.Navigate("Cancel");

            App.ShouldSeeText("ModalIndex");
            App.ShouldNotSeeText("Page1");
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

            if (!JsDisabled())
                Assert.Ignore("we don't refresh the underlying page on submit (yet)");

            App.ShouldSeeText("Count=3");

            App.Navigate("Cancel");

            App.ShouldSeeText("ModalIndex");
        }
    }
}
