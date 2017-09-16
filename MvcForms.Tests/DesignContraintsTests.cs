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
            var folder = @"..\..\..\_output";
            var name = "MvcForms";

            NugetPackage.VerifyDependencies(folder, name, new string[]
            {
                "jQuery:1.9.0",
                "HtmlTags:*",
            });
        }

        [Test]
        public void OnlyMvcFormsScriptsArePackaged()
        {
            var folder = @"..\..\..\_output";
            var name = "MvcForms";

            var contentFiles = NugetPackage.FindContentFiles(folder, name);

            contentFiles.Should().NotBeEmpty("content (script) files should be packaged");

            var nonMvcFormsFiles = contentFiles.Where(f => !f.Contains("/mvcForms")).ToList();

            nonMvcFormsFiles.Should().BeEmpty("only mvcForms files should be packaged (potentially change the build action to None)");
        }
    }
}
