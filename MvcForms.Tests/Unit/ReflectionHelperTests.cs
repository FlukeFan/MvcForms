using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit
{
    [TestFixture]
    public class ReflectionHelperTests
    {
        [Test]
        public void GenericViewData()
        {
            var helper = FakeHtmlHelper.New(new ReflectionHelperTests());
            helper.ViewData.ModelState.SetModelValue("Test", new ValueProviderResult("rawValue"), "attempted value");

            var genericViewData = helper.ViewData;
            var genericViewBag = helper.ViewBag;

            helper.ViewData.ModelState.Keys.Count().Should().Be(1);

            var untypedHelper = (helper as IHtmlHelper);

            untypedHelper.GenericViewData().ModelState.Keys.Count().Should().Be(1);

            untypedHelper.GenericViewData().Should().BeSameAs(genericViewData);

            var viewBagEquality = (bool)object.ReferenceEquals(untypedHelper.GenericViewBag(), genericViewBag);
            viewBagEquality.Should().BeTrue();
        }

        [Test][Ignore("Investigate if generic methods are still required")]
        public void InvestigateIfGenericHelpersAreStillRequired()
        {
            // if they are still required, then remove the comment from the two methods
        }
    }
}
