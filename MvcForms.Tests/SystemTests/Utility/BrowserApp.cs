using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MvcForms.Tests.SystemTests.Utility
{
    public class BrowserApp
    {
        public const int Port = 59505;

        private IWebDriver  _webDriver;
        private bool        _jsDisabled;

        public BrowserApp(bool disableJs) : this(WebDriver.Instance(), disableJs)
        { }

        public BrowserApp(IWebDriver webDriver, bool disableJs)
        {
            _webDriver = webDriver;
            _webDriver.Navigate().GoToUrl($"http://localhost:{Port}/");

            var scriptExecutor = (IJavaScriptExecutor)webDriver;
            scriptExecutor.ExecuteScript("document.cookie = 'disableJs=" + disableJs + "; path=/;'");
            _jsDisabled = disableJs;
        }

        public void GoTo(string action)
        {
            var url = $"http://localhost:{Port}{action}";
            Console.WriteLine("Navigating to '{0}' ", url);
            WaitFor(() =>
            {
                _webDriver.Navigate().GoToUrl(url);
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

        public object Exec(string javascript)
        {
            object returnValue = null;

            WaitFor(() =>
            {
                var scriptExecutor = (IJavaScriptExecutor)_webDriver;
                returnValue = scriptExecutor.ExecuteScript(javascript);
            });

            return returnValue;
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

        public void ShouldNotSeeText(string text)
        {
            Console.WriteLine("Verify can not see text '{0}'", text);
            WaitFor(() =>
            {
                var body = _webDriver.FindElement(By.TagName("body"));
                body.Text.Should().NotContain(text);
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

        public void ShouldHaveValue(string inputSelector, string expectedValue)
        {
            Console.WriteLine($"Verify input {inputSelector} has value '{expectedValue}'");
            WaitFor(() =>
            {
                var input = _webDriver.FindElement(By.CssSelector(inputSelector));
                input.GetAttribute("value").Should().Be(expectedValue);
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
                    .Where(l => l.Enabled && l.GetAttribute("tabIndex") != "-1" && l.Text == linkText)
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

        public void TypeText(string name, string text, bool clearFirst = true)
        {
            TypeText(name, $"input[name='{name}']", text, clearFirst);
        }

        private void TypeText(string name, string selector, string text, bool clearFirst = true)
        {
            Console.WriteLine("Type text '{0}' into '{1}'", text, name);
            WaitFor(() =>
            {
                var inputs = _webDriver.FindElements(By.CssSelector(selector))
                    .Where(i => i.Displayed && i.Enabled)
                    .ToList();

                inputs.Count.Should().Be(1, "should be 1 visible {0}, but found {1}", name, inputs.Count);
                var input = inputs.Single();

                input.Click();

                if (clearFirst)
                    input.Clear();

                input.SendKeys(text);
            });
        }

        public void Submit(string text)
        {
            ClickButton(text, "form input[type=submit], form button");
        }

        public void ClickButton(string text)
        {
            ClickButton(text, "button");
        }

        private void ClickButton(string text, string selector)
        {
            Console.WriteLine("Click button {0}", text);
            WaitFor(() =>
            {
                IList<IWebElement> buttons = _webDriver.FindElements(By.CssSelector(selector))
                    .Where(b => b.Enabled)
                    .ToList();

                if (text != null)
                    buttons = buttons.Where(b => b.Text == text || b.GetAttribute("value") == text).ToList();

                if (buttons.Count > 1)
                    Assert.Fail("No single button; found: {0}", string.Join("\n", buttons.Select(b => b.GetAttribute("outerHTML"))));

                if (buttons.Count < 1)
                    Assert.Fail("No buttons found with text = {0}", text);

                buttons.First().Click();
            });
        }
    }
}
