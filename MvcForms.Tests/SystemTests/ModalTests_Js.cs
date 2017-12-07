using MvcForms.StubApp.Controllers;
using NUnit.Framework;

namespace MvcForms.Tests.SystemTests
{
    public class ModalTests_Js : ModalTests
    {
        protected override bool JsDisabled() { return false; }

        [Test]
        public void Alert()
        {
            App.GoTo(ModalActions.Client());

            App.ClickButton("Alert");

            App.ShouldSeeText("alert_title");
            App.ShouldSeeText("alert_message");

            App.ClickButton("OK");

            App.ShouldNotSeeText("alert_title");
        }

        [Test]
        public void Confirm()
        {
            App.GoTo(ModalActions.Client());

            App.ClickButton("Confirm");

            App.ShouldSeeText("confirm_title");
            App.ShouldSeeText("confirm_message");

            App.ClickButton("OK");

            App.ShouldNotSeeText("confirm_title");

            App.ClickButton("Confirm");

            App.ShouldSeeText("confirm_title");

            App.ClickButton("Cancel");

            App.ShouldNotSeeText("confirm_title");
        }
    }
}
