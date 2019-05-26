using System.Collections.Generic;
using HtmlTags;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcForms.Forms
{
    public class Select : PropertyControl
    {
        private string          _id;
        private string          _name;
        private string          _value;
        private IEnumerable<KeyValuePair<string, string>> _options;

        public Select(IHtmlHelper html, IEnumerable<KeyValuePair<string, string>> options, PropertyContext propertyContext) : base(html, propertyContext)
        {
            Id(propertyContext.Id);
            Name(propertyContext.Name);
            Value(propertyContext.Value);
            Options(options);
        }

        public string   Id()                { return _id; }
        public Select   Id(string id)       { _id = id; return this; }

        public string   Name()              { return _name; }
        public Select   Name(string name)   { _name = name; return this; }

        public string   Value()             { return _value; }
        public Select   Value(string value) { _value = value; return this; }

        public IEnumerable<KeyValuePair<string, string>>    Options()                                                   { return _options; }
        public Select                                       Options(IEnumerable<KeyValuePair<string, string>> options)  { _options = options; return this; }

        protected override HtmlTag CreateTag()
        {
            var select = new HtmlTag("select")
                .Attr("id", _id)
                .Attr("name", _name)
                .Attr("value", _value);

            foreach (var option in _options)
            {
                var optionTag = new HtmlTag("option")
                    .Text(option.Value)
                    .Attr("value", option.Key ?? "");

                select.Append(optionTag);
            }

            return select;
        }
    }
}
