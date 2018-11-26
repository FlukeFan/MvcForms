using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
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
            var contentRoot = "../../../../MvcForms.StubApp";

            if (!Directory.Exists(contentRoot))
                contentRoot = $"../{contentRoot}";

            var webHost = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(contentRoot)
                .UseStartup<TestStartup>();

            _testServer = webHost.MvcTestingTestServer();
            _httpClient = _testServer.MvcTestingClient();
        }

        public static void TearDownWebHost()
        {
            using (_testServer) { }
        }

        private class TestStartup : Startup
        {
            public TestStartup(IConfiguration configuration) : base(configuration)
            {
            }
        }
    }
}
