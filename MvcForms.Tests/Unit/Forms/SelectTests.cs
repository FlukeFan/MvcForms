﻿using System.Collections.Generic;
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

        private IDictionary<int, string> _intValues = new Dictionary<int, string>
        {
            { 1, "Value 1" },
            { 2, "Value 2" },
            { 3, "Value 3" },
        };

        private IDictionary<bool, string> _boolValues = new Dictionary<bool, string>
        {
            { false, "No" },
            { true, "Yes" },
        };

        private IDictionary<ExamplePostModel.Values, string> _enumValues = new Dictionary<ExamplePostModel.Values, string>
        {
            { ExamplePostModel.Values.Key1, "Value 1" },
            { ExamplePostModel.Values.Key2, "Value 2" },
            { ExamplePostModel.Values.Key3, "Value 3" },
        };

        public void String()
        {
            var model = new ExamplePostModel { String = "Key2" };

            var tag = model.Helper().Select(f => f.String, _stringValues).Optional("<please select>").RenderTag();

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

            var tag = model.Helper().Select(f => f.String, _stringValues).Optional("<please select>").RenderTag();

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

            var tag = model.Helper().Select(f => f.Strings, _stringValues).RenderTag();

            tag.Attr("multiple").Should().Be("true");

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true, true);
        }

        [Test]
        public void Typed()
        {
            var model = new ExamplePostModel { String = "Key2" };

            var tag = model.Helper().Select(f => f.String, _stringValues).RenderTag();

            var optionTags = tag.Children;
            optionTags.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true, false);
        }

        [Test]
        public void Typed_Optional()
        {
            var model = new ExamplePostModel { NullableEnum = ExamplePostModel.Values.Key2 };

            var options = new Dictionary<ExamplePostModel.Values, string>
            {
                { ExamplePostModel.Values.Key1, "Value 1" },
                { ExamplePostModel.Values.Key2, "Value 2" },
                { ExamplePostModel.Values.Key3, "Value 3" },
            };

            var tag = model.Helper().Select(f => f.NullableEnum, options).Optional("<please select>").RenderTag();

            var optionTags = tag.Children;
            optionTags.Select(o => o.Text()).Should().BeEquivalentTo("<please select>", "Value 1", "Value 2", "Value 3");
            optionTags.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, false, true, false);
        }

        [Test]
        public void Int()
        {
            var model = new ExamplePostModel { Int = 2 };

            var tag = model.Helper().Select(f => f.Int, _intValues).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true, false);
        }

        [Test]
        public void NullableInt()
        {
            var model = new ExamplePostModel { NullableInt = 2 };

            var tag = model.Helper().Select(f => f.NullableInt, _intValues).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true, false);
        }

        [Test]
        public void Bool()
        {
            var model = new ExamplePostModel { Bool = true };

            var tag = model.Helper().Select(f => f.Bool, _boolValues).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true);
        }

        [Test]
        public void NullableBool()
        {
            var model = new ExamplePostModel { NullableBool = true };

            var tag = model.Helper().Select(f => f.NullableBool, _boolValues).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true);
        }

        [Test]
        public void Enum()
        {
            var model = new ExamplePostModel { Enum = ExamplePostModel.Values.Key2 };

            var tag = model.Helper().Select(f => f.Enum, _enumValues).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true, false);
        }

        [Test]
        public void NullableEnum()
        {
            var model = new ExamplePostModel { NullableEnum = ExamplePostModel.Values.Key2 };

            var tag = model.Helper().Select(f => f.NullableEnum, _enumValues).RenderTag();

            var options = tag.Children;
            options.Select(o => o.HasAttr("selected")).Should().BeEquivalentTo(false, true, false);
        }
    }
}
