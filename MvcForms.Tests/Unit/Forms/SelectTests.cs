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
        private IEnumerable<Option> _stringOptions = new []
        {
            Option.Value("Key1", "Value 1"),
            Option.Value("Key2", "Value 2"),
            Option.Value("Key3", "Value 3"),
        };

        private IEnumerable<Option> _intOptions = new[]
        {
            Option.Value("1", "Value 1"),
            Option.Value("2", "Value 2"),
            Option.Value("3", "Value 3"),
        };

        private IEnumerable<Option> _boolOptions = new[]
        {
            Option.Value("False", "No"),
            Option.Value("True", "Yes"),
        };

        public void String()
        {
            var model = new ExamplePostModel { String = "Key2" };
            var values = _stringOptions.Optional("<please select>");

            var tag = model.Helper().Select(f => f.String, values).RenderTag();

            tag.TagName().Should().Be("select");
            tag.Attr("name").Should().Be("String");
            tag.Attr("id").Should().Be("String");

            var options = tag.Children;
            options.Select(o => o.HasAttr("value")).Should().AllBeEquivalentTo(true, "all key values should be set");
            options.Select(o => o.Attr("value")).Should().BeEquivalentTo("", "Key1", "Key2", "Key3");
            options.Select(o => o.Text()).Should().BeEquivalentTo("<please select>", "Value 1", "Value 2", "Value 3");

            options.Select(o => o.HasAttr("selected")).Count(s => s == true).Should().Be(1, "current value should be selected");
            options.Select(o => o.Attr("selected")).Should().BeEquivalentTo("", "", "selected", "");
        }

        [Test]
        public void String_NullValue()
        {
            var model = new ExamplePostModel { String = null };
            var values = _stringOptions.Optional("<please select>");

            var tag = model.Helper().Select(f => f.String, values).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Count(s => s == true).Should().Be(0, "nothin should be selected");
        }

        [Test]
        public void SanitizedId()
        {
            var helper = FakeHtmlHelper.New(new ExamplePostModel());

            var tag = helper.Select(f => f.InputsArray[1].String2, new Option[0]).RenderTag();

            tag.Attr("name").Should().Be("InputsArray[1].String2");
            tag.Attr("id").Should().Be("InputsArray_1__String2");
        }

        [Test]
        public void OptGroups()
        {
            var model = new ExamplePostModel { String = "Key3" };

            var values = new List<Option>
            {
                Option.Value(null, "<please select>"),
                Option.Group("Primary", new []
                {
                    Option.Value("Key1", "value 1"),
                    Option.Value("Key2", "value 2"),
                }),
                Option.Group("Secondary", new []
                {
                    Option.Value("Key3", "value 3"),
                    Option.Value("Key4", "value 4"),
                }),
            };

            var tag = model.Helper().Select(f => f.String, values).RenderTag();

            var options = tag.Children;
            options.Select(o => o.TagName()).Should().BeEquivalentTo("option", "optgroup", "optgroup");

            {
                var primayGroup = options[1];
                primayGroup.Attr("label").Should().Be("Primary");
                primayGroup.Children.Select(t => t.Attr("value")).Should().BeEquivalentTo("Key1", "Key2");
            }
            {
                var secondaryGroup = options[2];
                secondaryGroup.Attr("label").Should().Be("Secondary");
                secondaryGroup.Children.Select(t => t.Attr("value")).Should().BeEquivalentTo("Key3", "Key4");
            }
        }

        [Test]
        public void Size()
        {
            var model = new ExamplePostModel();

            var tag = model.Helper().Select(f => f.String, new Option[0]).Size(3).RenderTag();

            tag.Attr("size").Should().Be("3");
        }

        [Test]
        public void Multiple()
        {
            var model = new ExamplePostModel { Strings = new[] { "Key2", "Key3" } };

            var tag = model.Helper().Select(f => f.Strings, _stringOptions).RenderTag();

            tag.Attr("multiple").Should().Be("true");

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true, true);
        }

        [Test]
        public void Int()
        {
            var model = new ExamplePostModel { Int = 2 };

            var tag = model.Helper().Select(f => f.Int, _intOptions).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true, false);
        }

        [Test]
        public void NullableInt()
        {
            var model = new ExamplePostModel { NullableInt = 2 };

            var tag = model.Helper().Select(f => f.NullableInt, _intOptions).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true, false);
        }

        [Test]
        public void Bool()
        {
            var model = new ExamplePostModel { Bool = true };

            var tag = model.Helper().Select(f => f.Bool, _boolOptions).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true);
        }

        [Test]
        public void NullableBool()
        {
            var model = new ExamplePostModel { NullableBool = true };

            var tag = model.Helper().Select(f => f.NullableBool, _boolOptions).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true);
        }

        [Test]
        public void Enum()
        {
            var model = new ExamplePostModel { Enum = ExamplePostModel.Values.Key2 };

            var tag = model.Helper().Select(f => f.Enum, _stringOptions).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true, false);
        }

        [Test]
        public void NullableEnum()
        {
            var model = new ExamplePostModel { NullableEnum = ExamplePostModel.Values.Key2 };

            var tag = model.Helper().Select(f => f.NullableEnum, _stringOptions).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true, false);
        }
    }
}
