using System;
using FluentAssertions;
using MvcForms.Tests.System.Utility;

namespace MvcForms.Tests.System.Pjax
{
    public class PjaxTests_Js : PjaxTests_NoJs
    {
        protected override BrowserApp NewApp()
        {
            return new BrowserApp(false);
        }

        protected override void VerifyNavState()
        {
            Console.WriteLine($"Verify header is still {_headerTicks}");
            var newHeaderTicks = QueryHeaderTicks();
            newHeaderTicks.Should().Be(_headerTicks);
        }
    }
}
