using System.Threading.Tasks;
using MvcForms.Tests.SystemTests.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.SystemTests
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [SetUp]
        public void SetUp()
        {
            try
            {
                Task.WaitAll(
                    Task.Run(() => IisExpress.BeforeTests(TestContext.CurrentContext.TestDirectory, "MvcForms.StubApp", BrowserApp.Port)),
                    Task.Run(() => WebDriver.Instance()));
            }
            catch
            {
                TearDown();
                throw;
            }
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
