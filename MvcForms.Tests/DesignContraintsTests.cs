using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace MvcForms.Tests
{
    [TestFixture]
    public class DesignContraintsTests
    {
        [Test]
        [Ignore("updating to core")]
        public void DependenciesHaveNotChanged()
        {
            var folder = @"..\..\..\_output";
            var name = "MvcForms";

            NugetPackage.VerifyDependencies(folder, name, new string[]
            {
                "jQuery:1.9.0",
                "HtmlTags:*",
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
    }
}
