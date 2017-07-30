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

            NugetDependency.VerifyDependencies(folder, name, new string[]
            {
                "jQuery:1.9.0",
                "HtmlTags:*",
            });
        }
    }
}
