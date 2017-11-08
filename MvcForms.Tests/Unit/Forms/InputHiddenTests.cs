﻿using FluentAssertions;
using MvcForms.Forms;
using MvcForms.StubApp.Models.Examples;
using MvcForms.Tests.Unit.Utility;
using NUnit.Framework;

namespace MvcForms.Tests.Unit.Forms
{
    [TestFixture]
    public class InputHiddenTests
    {
        [Test]
        public void InputHidden()
        {
            var model = new FormInputsModel { StringInput1 = "123" };
            var helper = FakeHtmlHelper.New(model);

            var input = helper.InputHidden(f => f.StringInput1);
            var tag = input.RenderTag();

            tag.TagName().Should().Be("input");
            tag.Attr("type").Should().Be("hidden");
            tag.Attr("name").Should().Be("StringInput1");
            tag.Attr("value").Should().Be("123");
        }

        [Test]
        public void InputHidden_Long()
        {
            var model = new FormInputsModel { Long = 123 };
            var helper = FakeHtmlHelper.New(model);

            var input = helper.InputHidden(f => f.Long);
            var tag = input.RenderTag();

            tag.TagName().Should().Be("input");
            tag.Attr("type").Should().Be("hidden");
            tag.Attr("name").Should().Be("Long");
            tag.Attr("value").Should().Be("123");
        }
    }
}
