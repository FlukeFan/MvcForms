using System;
using FluentAssertions;
using MvcForms.StubApp.Controllers;
using MvcForms.Tests.SystemTests.Utility;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MvcForms.Tests.SystemTests.Pjax
{
    public class PjaxTests_Js : PjaxTests
    {
        protected override bool DisableJs() { return false; }

        protected override void VerifyNavState()
        {
            Console.WriteLine($"Verify header is still {_headerTicks}");
            var newHeaderTicks = QueryHeaderTicks();
            newHeaderTicks.Should().Be(_headerTicks);
        }
    }

    public class PjaxTests_NoJs : PjaxTests
    {
        protected override bool DisableJs() { return true; }

        protected override void VerifyNavState()
        {
            Console.WriteLine($"Verify header is not {_headerTicks}");
            var newHeaderTicks = QueryHeaderTicks();
            newHeaderTicks.Should().NotBe(_headerTicks);
        }
    }

    public abstract class PjaxTests : NoJsTest
    {
        protected string _headerTicks;

        private void StoreNavState()
        {
            _headerTicks = QueryHeaderTicks();
        }

        protected string QueryHeaderTicks()
        {
            return App.Query(wd => wd.FindElement(By.Id("header")).Text);
        }

        protected abstract void VerifyNavState();

        [Test]
        public void Navigate()
        {
            App.GoTo(SystemActions.PjaxPage1());
            App.ShouldSeeText("Page 1");
            App.ShouldHaveTitleContaining("Page 1");
            App.ShouldHaveUrl(SystemActions.PjaxPage1());

            StoreNavState();
            App.Navigate("Navigate to 2");

            App.ShouldSeeText("Page 2");
            App.ShouldHaveTitleContaining("Page 2");
            App.ShouldHaveUrl(SystemActions.PjaxPage2());
            VerifyNavState();
        }

        [Test]
        public void BackAndForward()
        {
            App.GoTo(SystemActions.PjaxPage1());
            App.Navigate("Navigate to 2");

            StoreNavState();
            App.Back();

            App.ShouldSeeText("Page 1");
            App.ShouldHaveTitleContaining("Page 1");
            App.ShouldHaveUrl(SystemActions.PjaxPage1());
            VerifyNavState();

            StoreNavState();
            App.Forward();

            App.ShouldSeeText("Page 2");
            App.ShouldHaveTitleContaining("Page 2");
            App.ShouldHaveUrl(SystemActions.PjaxPage2());
            VerifyNavState();
        }

        [Test]
        public void Redirect()
        {
            App.GoTo(SystemActions.PjaxPage2());

            StoreNavState();
            App.Navigate("Navigate Redirect");

            App.ShouldSeeText("Page 1");
            App.ShouldHaveTitleContaining("Page 1");
            App.ShouldHaveUrl(SystemActions.PjaxPage1());
            VerifyNavState();
        }

        [Test]
        public void Error()
        {
            App.GoTo(SystemActions.PjaxPage2());

            App.Navigate("Navigate Error");

            App.ShouldSeeText("DeliberateError");
            App.ShouldHaveUrl(SystemActions.PjaxPageErr());
        }

        [Test]
        public void SubmitForm_Redraw()
        {
            App.GoTo(SystemActions.PjaxForm());

            App.TypeText("postedValue", "posted");

            StoreNavState();
            App.Submit("go");

            App.ShouldSeeText("button=go");
            App.ShouldHaveTitleContaining("Form:posted");
            App.ShouldHaveUrl(SystemActions.PjaxForm());
            VerifyNavState();
        }

        [Test]
        public void SubmitForm_Redirect()
        {
            App.GoTo(SystemActions.PjaxForm());

            App.TypeText("postedValue", "Success");

            StoreNavState();
            App.Submit("go");

            App.ShouldHaveTitleContaining("FormDone");
            App.ShouldHaveUrl(SystemActions.PjaxFormDone());
            VerifyNavState();
        }
    }
}
