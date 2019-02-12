using NUnit.Framework;

namespace MvcForms.Tests.SystemTests.Utility
{
    [TestFixture]
    [Category("Slow")]
    public abstract class NoJsTest
    {
        protected BrowserApp App { get; set; }

        [SetUp]
        public void SetUp()
        {
            App = new BrowserApp(JsDisabled());
            App.WriteLine($"\n\nStarting browser test: {TestContext.CurrentContext.Test.FullName}");
        }

        protected abstract bool JsDisabled();
    }
}
