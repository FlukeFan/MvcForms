﻿using System.Collections.Generic;
using HtmlTags;

namespace MvcForms.Forms
{
    public abstract class Option
    {
        public static OptionValue Value(string key, string label)
        {
            return new OptionValue(key, label);
        }

        public static OptionGroup Group(string label, IEnumerable<OptionValue> options)
        {
            return new OptionGroup(label, options);
        }

        public abstract HtmlTag CreateTag(string selectedValue);
    }

    public class OptionValue : Option
    {
        public OptionValue(string key, string label)
        {
            Key = key;
            Label = label;
        }

        public string Key   { get; }
        public string Label { get; }

        public override HtmlTag CreateTag(string selectedValue)
        {
            var tag = new HtmlTag("option")
                .Text(Label)
                .Attr("value", Key ?? "");

            if (Key == selectedValue)
                tag.Attr("selected", "selected");

            return tag;
        }
    }

    public class OptionGroup : Option
    {
        public OptionGroup(string label, IEnumerable<OptionValue> options)
        {
            Label = label;
            Options = options;
        }

        public string                   Label   { get; }
        public IEnumerable<OptionValue> Options { get; }

        public override HtmlTag CreateTag(string selectedValue)
        {
            var tag = new HtmlTag("optgroup")
                .Attr("label", Label);

            foreach (var option in Options)
                tag.Append(option.CreateTag(selectedValue));

            return tag;
        }
    }
}
