using System.Threading.Tasks;
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
            Task.WaitAll(
                Task.Run(() => IisExpress.BeforeTests(TestContext.CurrentContext.TestDirectory, "MvcForms.StubApp", BrowserApp.Port)),
                Task.Run(() => WebDriver.Instance()));
        }

        [TearDown]
        public void TearDown()
        {
            Task.WaitAll(
                Task.Run(() => WebDriver.Close()),
                Task.Run(() => IisExpress.AfterTests()));
        }
    }
}
