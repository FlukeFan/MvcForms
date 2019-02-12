using System;
using FluentAssertions;
using MvcForms.StubApp.Controllers;
using MvcForms.Tests.SystemTests.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.SystemTests
{
    public class ExamplesTests : NoJsTest
    {
        protected override bool JsDisabled() { return false; }

        [Test]
        public void ScrollIsMaintained()
        {
            App.GoTo(ExamplesActions.Scroll());

            Func<long> getScrollPosition = () => (long)App.Exec("return $('#scrollDiv').scrollTop();");

            getScrollPosition().Should().Be(0L);

            App.Exec("$('#postSubmit')[0].scrollIntoView()");

            getScrollPosition().Should().BeGreaterThan(0L);

            App.Submit("Submit");

            getScrollPosition().Should().BeGreaterThan(0L);
        }
    }
}
