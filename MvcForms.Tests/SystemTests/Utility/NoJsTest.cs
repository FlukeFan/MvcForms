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
            App = new BrowserApp(DisableJs());

        }

        protected abstract bool DisableJs();
    }


}
