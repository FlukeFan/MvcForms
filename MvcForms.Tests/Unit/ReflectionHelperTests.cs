using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit
{
    [TestFixture]
    [Ignore("updating to core")]
    public class ReflectionHelperTests
    {
        [Test]
        public void GenericViewData()
        {
            var helper = FakeHtmlHelper.New(new ReflectionHelperTests());
            //helper.ViewData.ModelState.Add("Test", new ModelState { Value = new ValueProviderResult("rawValue", "attemptValue", null) });

            var genericViewData = helper.ViewData;
            var genericViewBag = helper.ViewBag;

            helper.ViewData.ModelState.Keys.Count().Should().Be(1);

            var untypedHelper = (helper as IHtmlHelper);

            untypedHelper.GenericViewData().ModelState.Keys.Count().Should().Be(1);

            untypedHelper.GenericViewData().Should().BeSameAs(genericViewData);

            var viewBagEquality = (bool)object.ReferenceEquals(untypedHelper.GenericViewBag(), genericViewBag);
            viewBagEquality.Should().BeTrue();
        }
    }
}
