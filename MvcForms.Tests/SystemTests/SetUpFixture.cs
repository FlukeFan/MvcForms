using System.Threading.Tasks;
using MvcForms.Tests.SystemTests.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.SystemTests
{
    [SetUpFixture]
    [Ignore("updating to core")]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            try
            {
                Task.WaitAll(
                    Task.Run(() => IisExpress.BeforeTests(TestContext.CurrentContext.TestDirectory, "MvcForms.StubApp", BrowserApp.Port)),
                    Task.Run(() => WebDriver.Instance()));
            }
            catch { OneTimeTearDown(); throw; }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Task.WaitAll(
                Task.Run(() => WebDriver.Close()),
                Task.Run(() => IisExpress.AfterTests()));
        }
    }
}
