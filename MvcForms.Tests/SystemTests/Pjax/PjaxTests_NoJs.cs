using System;
using FluentAssertions;
using MvcForms.StubApp.Controllers;
using MvcForms.Tests.SystemTests.Utility;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MvcForms.Tests.SystemTests.Pjax
{
    [TestFixture]
    public class PjaxTests_NoJs
    {
        protected string _headerTicks;

        private BrowserApp App { get; set; }

        [SetUp]
        public void SetUp()
        {
            App = NewApp();
        }

        protected virtual BrowserApp NewApp()
        {
            return new BrowserApp(true);
        }

        protected virtual void StoreNavState()
        {
            _headerTicks = QueryHeaderTicks();
        }

        protected virtual void VerifyNavState()
        {
            Console.WriteLine($"Verify header is not {_headerTicks}");
            var newHeaderTicks = QueryHeaderTicks();
            newHeaderTicks.Should().NotBe(_headerTicks);
        }

        protected string QueryHeaderTicks()
        {
            return App.Query(wd => wd.FindElement(By.Id("header")).Text);
        }

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
