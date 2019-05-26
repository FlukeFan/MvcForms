using System;
using System.Collections.Generic;
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
            return new OptionGroup();
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
        public override HtmlTag CreateTag(string selectedValue)
        {
            throw new NotImplementedException();
        }
    }
}
