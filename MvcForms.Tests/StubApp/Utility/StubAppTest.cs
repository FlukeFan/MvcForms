using System;
using System.Threading.Tasks;
using MvcTesting.AspNetCore;
using NUnit.Framework;

namespace MvcForms.Tests.StubApp.Utility
{
    [TestFixture]
    public abstract class StubAppTest
    {
        protected SimulatedHttpClient HttpClient()
        {
            return null;
        }

        protected void Test(Func<SimulatedHttpClient, Task> action)
        {
            action(HttpClient()).Wait();
        }

        public static void SetUpWebHost()
        {
            //StubApp = AspNetTestHost.For(@"..\..\..\MvcForms.StubApp", typeof(TestHostStartup));
        }

        public static void TearDownWebHost()
        {
            //using (StubApp) { }
        }

        //private class TestHostStartup : AppDomainProxy
        //{
        //    public TestHostStartup()
        //    {
        //    }
        //}
    }
}
