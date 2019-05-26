using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MvcForms.Forms;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    public class SelectTests
    {
        private IDictionary<string, string> _stringValues = new Dictionary<string, string>
        {
            { "Key1", "Value 1" },
            { "Key2", "Value 2" },
            { "Key3", "Value 3" },
        };

        [Test]
        public void Select_String()
        {
            var model = new ExamplePostModel { String = "Key1" };
            var values = _stringValues.Prepend(KeyValuePair.Create<string, string>(null, "<please select>"));

            var tag = model.Helper().Select(f => f.String, values).RenderTag();

            tag.TagName().Should().Be("select");
            tag.Attr("name").Should().Be("String");

            var options = tag.Children;
            options.Select(o => o.HasAttr("value")).Should().AllBeEquivalentTo(true);
            options.Select(o => o.Attr("value")).Should().BeEquivalentTo("", "Key1", "Key2", "Key3");
            options.Select(o => o.Text()).Should().BeEquivalentTo("<please select>", "Value 1", "Value 2", "Value 3");
        }
    }
}
