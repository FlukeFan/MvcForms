﻿using System.Web;
using FluentAssertions;
using MvcForms.StubApp.Controllers;
using MvcForms.Tests.SystemTests.Utility;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MvcForms.Tests.SystemTests
{
    public class PjaxTests_Js : PjaxTests
    {
        protected override bool JsDisabled() { return false; }

        protected override void VerifyNavState()
        {
            VerifyNavRemained();
        }

        [Test]
        public void CanRunInlineScripts()
        {
            App.GoTo(PjaxActions.Page1());
            App.Navigate("Navigate to 2");

            App.Navigate("Click");

            App.ShouldSeeText("Hidden=false");
        }
    }

    public class PjaxTests_NoJs : PjaxTests
    {
        protected override bool JsDisabled() { return true; }

        protected override void VerifyNavState()
        {
            VerifyNavChanged();
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

        protected void VerifyNavChanged()
        {
            App.WriteLine($"Verify header is not {_headerTicks}");
            var newHeaderTicks = QueryHeaderTicks();
            newHeaderTicks.Should().NotBe(_headerTicks);
        }

        protected void VerifyNavRemained()
        {
            App.WriteLine($"Verify header is still {_headerTicks}");
            var newHeaderTicks = QueryHeaderTicks();
            newHeaderTicks.Should().Be(_headerTicks);
        }

        protected abstract void VerifyNavState();

        [Test]
        public void Navigate()
        {
            App.GoTo(PjaxActions.Index());
            App.Navigate("Page1");
            App.ShouldSeeText("Page 1");
            App.ShouldHaveTitleContaining("Page 1");
            App.ShouldHaveUrl(PjaxActions.Page1());

            StoreNavState();
            App.Navigate("Navigate to 2");

            App.ShouldSeeText("Page 2");
            App.ShouldHaveTitleContaining("Page 2");
            App.ShouldHaveUrl(PjaxActions.Page2());
            VerifyNavState();
        }

        [Test]
        public void BackAndForward()
        {
            App.GoTo(PjaxActions.Page1());
            App.Navigate("Navigate to 2");

            StoreNavState();
            App.Back();

            App.ShouldSeeText("Page 1");
            App.ShouldHaveTitleContaining("Page 1");
            App.ShouldHaveUrl(PjaxActions.Page1());
            VerifyNavState();

            StoreNavState();
            App.Forward();

            App.ShouldSeeText("Page 2");
            App.ShouldHaveTitleContaining("Page 2");
            App.ShouldHaveUrl(PjaxActions.Page2());
            VerifyNavState();
        }

        [Test]
        public void Redirect()
        {
            App.GoTo(PjaxActions.Page2());

            StoreNavState();
            App.Navigate("Navigate Redirect");

            App.ShouldSeeText("Page 1");
            App.ShouldHaveTitleContaining("Page 1");
            App.ShouldHaveUrl(PjaxActions.Page1());
            VerifyNavState();
        }

        [Test]
        public void Challenge()
        {
            App.GoTo(PjaxActions.Page2());

            StoreNavState();
            App.Navigate("Navigate Challenge");

            App.ShouldSeeText("Page 1");
            App.ShouldHaveTitleContaining("Page 1");
            App.ShouldContainUrl(PjaxActions.Page1() + $"?ReturnUrl={HttpUtility.UrlEncode(PjaxActions.Page4())}");
            VerifyNavState();
        }

        [Test]
        public void Navigate_NoPjax()
        {
            App.GoTo(PjaxActions.Page2());

            StoreNavState();
            App.Navigate("Navigate to 2");

            VerifyNavChanged();
        }

        [Test]
        public void Error()
        {
            App.GoTo(PjaxActions.Page2());

            App.Navigate("Navigate Error");

            App.ShouldSeeText("DeliberateError");

            if (JsDisabled())
            {
                // browser will have nagivated, and user can press back to get back to the previous page
                App.ShouldHaveUrl(PjaxActions.PageErr());
            }
            else
            {
                // error will have been caught by JS, displayed in a modal alert dialog, and the user can press back to close the dialog
                App.ShouldHaveUrl(PjaxActions.Page2());
            }
        }

        [Test]
        public void SubmitForm_Redraw()
        {
            App.GoTo(PjaxActions.Index());
            App.Navigate("Form");

            App.TypeText("postedValue", "posted");

            StoreNavState();
            App.Submit("go");

            App.ShouldSeeText("button=go");
            App.ShouldHaveTitleContaining("Form:posted");
            App.ShouldHaveUrl(PjaxActions.Form());
            VerifyNavState();
        }

        [Test]
        public void SubmitForm_Redirect()
        {
            App.GoTo(PjaxActions.Form());

            App.TypeText("postedValue", "Success");

            StoreNavState();
            App.Submit("go");

            App.ShouldHaveTitleContaining("FormDone");
            App.ShouldHaveUrl(PjaxActions.FormDone());
            VerifyNavState();

            App.Back();

            App.ShouldSeeText("enter");
        }

        [Test]
        public void SubmitForm_NoPjax()
        {
            App.GoTo(PjaxActions.Form());

            StoreNavState();
            App.Submit("redirect");

            VerifyNavChanged();
        }

        [Test]
        public void SubmitForm_Get()
        {
            App.GoTo(PjaxActions.FormGet());

            StoreNavState();
            App.TypeText("value", "123");
            App.Submit("go");

            App.ShouldSeeText("value=123");

            VerifyNavState();
        }
    }
}
