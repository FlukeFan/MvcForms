using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using MvcForms.StubApp;
using MvcTesting.AspNetCore;
using NUnit.Framework;

namespace MvcForms.Tests.StubApp.Utility
{
    [TestFixture]
    public abstract class StubAppTest
    {
        private static TestServer           _testServer;
        private static SimulatedHttpClient  _httpClient;

        protected SimulatedHttpClient HttpClient()
        {
            return _httpClient;
        }

        protected void Test(Func<SimulatedHttpClient, Task> action)
        {
            action(HttpClient()).Wait();
        }

        public static void SetUpWebHost()
        {
            var webHost = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            _testServer = webHost.MvcTestingTestServer();
            _httpClient = _testServer.MvcTestingClient();
        }

        public static void TearDownWebHost()
        {
            using (_testServer) { }
        }
    }
}
