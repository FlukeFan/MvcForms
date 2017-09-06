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
        public void OnlyMinifiedScriptsArePackaged()
        {
            var folder = @"..\..\..\_output";
            var name = "MvcForms";

            var content = NugetPackage.FindContentFiles(folder, name);

            var nonMinifiedFiles = content.Where(f => !f.Contains(".min.")).ToList();

            nonMinifiedFiles.Should().BeEmpty("only minified files should be packaged (potentially change the build action to None)");
        }
    }
}
