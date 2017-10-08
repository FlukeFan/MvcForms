using NUnit.Framework;

namespace MvcForms.Tests.SystemTests.Utility
{
    [TestFixture]
    public abstract class NoJsTest
    {
        protected BrowserApp App { get; set; }

        [SetUp]
        public void SetUp()
        {
            App = new BrowserApp(JsDisabled());

        }

        protected abstract bool JsDisabled();
    }


}
