using MvcForms.StubApp.Controllers;
using MvcForms.Tests.SystemTests.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.SystemTests
{
    public class ModalTests_NoJs : ModalTests
    {
        protected override bool DisableJs() { return true; }
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
            App.ShouldNotSeeText("ModalIndex");

            App.Navigate("Cancel");

            App.ShouldSeeText("ModalIndex");
            App.ShouldNotSeeText("Page1");
        }
    }
}
