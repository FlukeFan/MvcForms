using System;
using System.IO;
using System.Linq;
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
        [Ignore("updating to core")]
        public void OnlyMvcFormsScriptsArePackaged()
        {
            var folder = @"..\..\..\_output";
            var name = "MvcForms";

            var contentFiles = NugetPackage.FindContentFiles(folder, name);

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