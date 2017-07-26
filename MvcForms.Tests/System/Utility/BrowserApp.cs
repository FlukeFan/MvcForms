using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OpenQA.Selenium;

namespace MvcForms.Tests.System.Utility
{
    public class BrowserApp
    {
        private IWebDriver  _webDriver;
        private bool        _jsDisabled;

        public BrowserApp(bool disableJs) : this(WebDriver.Instance(), disableJs)
        { }

        public BrowserApp(IWebDriver webDriver, bool disableJs)
        {
            _webDriver = webDriver;
            _webDriver.Navigate().GoToUrl("http://localhost:46585/");

            var scriptExecutor = (IJavaScriptExecutor)webDriver;
            scriptExecutor.ExecuteScript("document.cookie = 'disableJs=" + disableJs + "; path=/;'");
            _jsDisabled = disableJs;
        }

        public void GoTo(string action)
        {
            Console.WriteLine("Navigating to '{0}' ", action);
            WaitFor(() =>
            {
                action = action.Replace("~/", "http://localhost:46585/");
                _webDriver.Navigate().GoToUrl(action);
            });
        }

        public object ActiveJQuery()
        {
            if (_jsDisabled)
                return 0L;

            var js = (IJavaScriptExecutor)_webDriver;
            var active = js.ExecuteScript("return jQuery.active + $(':animated').length;");
            return active;
        }

        public void WaitFor(Action action)
        {
            Wait.For(() =>
            {
                action();

                Wait.For(() =>
                {
                    var active = ActiveJQuery();
                    active.Should().NotBeNull();
                    active.GetType().Should().Be(typeof(long));
                    active.Should().Be(0L);
                });
            });
        }

        public T Query<T>(Func<IWebDriver, T> query)
        {
            return query(_webDriver);
        }

        public void ShouldSeeText(string text)
        {
            Console.WriteLine("Verify can see text '{0}'", text);
            WaitFor(() =>
            {
                var body = _webDriver.FindElement(By.TagName("body"));
                body.Text.Should().Contain(text);
            });
        }

        public void ShouldHaveTitleContaining(string text)
        {
            Console.WriteLine("Verify title contains '{0}'", text);
            WaitFor(() =>
            {
                _webDriver.Title.Should().Contain(text);
            });
        }

        public void ShouldHaveUrl(string action)
        {
            Console.WriteLine("Verify url is for action '{0}'", action);
            WaitFor(() =>
            {
                _webDriver.Url.Should().EndWith(action.Replace("~", ""));
            });
        }

        public void Navigate(string linkText)
        {
            Console.WriteLine("Click link '{0}'", linkText);
            WaitFor(() =>
            {
                IList<IWebElement> links = _webDriver.FindElements(By.TagName("a"))
                    .Where(l => l.Text == linkText)
                    .ToList();

                links.Count.Should().BeLessThan(2, "No single link; found: ", string.Join("\n", links.Select(l => l.GetAttribute("outerHTML"))));

                links.Count.Should().BeGreaterThan(0, "No links found with text '{0}'", linkText);

                links.First().Click();
            });
        }

        public void Back()
        {
            Console.WriteLine("Click browser 'back' button");
            _webDriver.Navigate().Back();
        }

        public void Forward()
        {
            Console.WriteLine("Click browser 'forward' button");
            _webDriver.Navigate().Forward();
        }
    }
}
