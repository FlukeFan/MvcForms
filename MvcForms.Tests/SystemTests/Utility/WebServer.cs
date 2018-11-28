using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MvcForms.Tests.StubApp.Utility;

namespace MvcForms.Tests.SystemTests.Utility
{
    public static class WebServer
    {
        private static IWebHost _webHost;

        public static void BeforeTests(string childSearchFolder, string siteFolder, int port)
        {
            var webPath = WebPath(childSearchFolder, siteFolder);

            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(webPath)
                .UseKestrel()
                .UseStartup<StubAppTest.TestStartup>();

            _webHost = webHostBuilder.Start($"http://localhost:{port}");
        }

        public static void AfterTests()
        {
            using (_webHost) { }
        }

        private static string WebPath(string searchFolder, string siteFolder)
        {
            var searches = new List<string>();
            var dir = searchFolder;
            var path = Path.GetFullPath(Path.Combine(dir, siteFolder));

            while (!File.Exists(Path.Combine(path, $"{siteFolder}.csproj")))
            {
                if (searches.Contains(path))
                    throw new Exception($"Could not find {siteFolder} starting from {searchFolder} - searched {string.Join(", ", searches)}");

                searches.Add(path);
                dir = Path.Combine(dir, "..");
                path = Path.GetFullPath(Path.Combine(dir, siteFolder));
            }

            return path;
        }
    }
}
