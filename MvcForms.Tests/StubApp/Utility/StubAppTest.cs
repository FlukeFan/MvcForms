using MvcTesting.Hosting;
using NUnit.Framework;

namespace MvcForms.Tests.StubApp.Utility
{
    [TestFixture]
    public abstract class StubAppTest
    {
        protected static AspNetTestHost StubApp { get; private set; }

        public static void SetUpWebHost()
        {
            StubApp = AspNetTestHost.For(@"..\..\..\MvcForms.StubApp", typeof(TestHostStartup));
        }

        public static void TearDownWebHost()
        {
            using (StubApp) { }
        }

        private class TestHostStartup : AppDomainProxy
        {
            public TestHostStartup()
            {
            }
        }
    }
}
