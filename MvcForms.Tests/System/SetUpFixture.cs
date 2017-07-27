using MvcForms.Tests.System.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.System
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [SetUp]
        public void SetUp()
        {
            IisExpress.BeforeTests(TestContext.CurrentContext.TestDirectory, "MvcForms.StubApp", BrowserApp.Port);
            WebDriver.Instance();
        }

        [TearDown]
        public void TearDown()
        {
            WebDriver.Close();
            IisExpress.AfterTests();
        }
    }
}
