using MvcForms.Tests.StubApp.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.StubApp
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            StubAppTest.SetUpWebHost();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            StubAppTest.TearDownWebHost();
        }
    }
}
