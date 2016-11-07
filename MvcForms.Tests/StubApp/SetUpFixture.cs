using MvcForms.Tests.StubApp.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.StubApp
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [SetUp]
        public void SetUp()
        {
            StubAppTest.SetUpWebHost();
        }

        [TearDown]
        public void TearDown()
        {
            StubAppTest.TearDownWebHost();
        }
    }
}
