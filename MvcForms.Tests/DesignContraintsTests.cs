using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using FluentAssertions;
using NUnit.Framework;

namespace MvcForms.Tests
{
    [TestFixture]
    public class DesignContraintsTests
    {
        [Test]
        public void DependenciesHaveNotChanged()
        {
            var name = "MvcForms";
            var folder = FindBinConfigFolder(".", name);

            NugetPackage.VerifyDependencies(folder, name, new string[]
            {
                "HtmlTags.AspNetCore:*",
                "Microsoft.AspNetCore.Mvc.Core:*",
            });
        }

        [Test]
        public void ContentIsPackaged()
        {
            var name = "MvcForms";
            var packageFolder = FindBinConfigFolder(".", name);

            var tmpDir = Path.GetFullPath("tmp");
            DeleteFolder(tmpDir);

            Directory.CreateDirectory(tmpDir);

            var commonTargets = new XmlDocument();
            commonTargets.Load(Path.Combine(packageFolder, "../../../Build/common.targets"));
            var version = commonTargets.SelectSingleNode("//*[local-name()='Version']").InnerText;

            var nugetCache = Path.Combine(Environment.GetEnvironmentVariable("UserProfile"), $".nuget/packages/{name}/{version}");
            DeleteFolder(nugetCache);

            Exec.Cmd("dotnet", $"new web", tmpDir);
            Exec.Cmd("dotnet", $"add package MvcForms -v {version} -s {packageFolder}", tmpDir);

            Assert.Ignore("need to verify content files are extracted");

            var contentFiles = NugetPackage.FindContentFiles(packageFolder, name);

            contentFiles.Should().NotBeEmpty("content (script) files should be packaged");

            var nonMvcFormsFiles = contentFiles.Where(f => !f.Contains("/mvcForms")).ToList();

            nonMvcFormsFiles.Should().BeEmpty("only mvcForms files should be packaged (potentially change the build action to None)");
        }

        [Test]
        public void StylerMethodsAreVirtual()
        {
            var stylerTypes = typeof(IStyler).Assembly.GetTypes()
                .Where(t => typeof(IStyler).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract)
                .ToList();

            foreach (var stylerType in stylerTypes)
            {
                foreach (var method in stylerType.GetMethods())
                {
                    if (method.IsPrivate)
                        continue;

                    if (method.DeclaringType == typeof(object))
                        continue;

                    method.IsVirtual.Should().BeTrue("methd {0} on {1} should be virtual to allow clients to override", method.Name, stylerType.FullName);
                }
            }
        }

        private static void DeleteFolder(string path, int retryCount = 3)
        {
            try
            {
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
            }
            catch
            {
                // *sigh* http://stackoverflow.com/questions/329355/cannot-delete-directory-with-directory-deletepath-true#comment11564214_329502

                if (retryCount <= 0)
                    throw;

                Thread.Sleep(3);
                DeleteFolder(path, retryCount - 1);
            }
        }

        private static string FindBinConfigFolder(string searchFolder, string name)
        {
            var config = "Unknown";
            searchFolder = Path.GetFullPath(searchFolder);

            while (!Directory.Exists(ConfigFolder(searchFolder, name, config)))
            {
                var parent = Directory.GetParent(searchFolder).FullName;

                if (parent == searchFolder)
                    throw new Exception($"Could not find ");

                if (Path.GetFileName(parent)?.ToLower() == "bin")
                    config = Path.GetFileName(searchFolder);

                searchFolder = parent;
            }

            return ConfigFolder(searchFolder, name, config);
        }

        private static string ConfigFolder(string searchFolder, string name, string config)
        {
            return Path.Combine(searchFolder, name, "bin", config);
        }
    }
}