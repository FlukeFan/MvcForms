using MvcForms.StubApp.Controllers;
using NUnit.Framework;

namespace MvcForms.Tests.SystemTests
{
    [Ignore("Updating to core")]
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

            App.ShouldHaveValue("#textInput", "alert_closed: OK");
        }

        [Test]
        public void Confirm()
        {
            App.GoTo(ModalActions.Client());

            App.ClickButton("Confirm");

            App.ShouldSeeText("confirm_title");
            App.ShouldSeeText("confirm_message");

            App.ClickButton("OK");

            App.ShouldHaveValue("#textInput", "confirm_closed: OK");

            App.ClickButton("Confirm");

            App.ClickButton("Cancel");

            App.ShouldHaveValue("#textInput", "confirm_closed: Cancel");
        }
    }
}
