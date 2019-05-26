using System.Collections.Generic;
using HtmlTags;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcForms.Forms
{
    public class Select : PropertyControl
    {
        private string              _id;
        private string              _name;
        private string              _selectedValue;
        private IEnumerable<Option> _options;

        public Select(IHtmlHelper html, IEnumerable<Option> options, PropertyContext propertyContext) : base(html, propertyContext)
        {
            Id(propertyContext.Id);
            Name(propertyContext.Name);
            SelectedValue(propertyContext.Value);
            Options(options);
        }

        public string               Id()                                    { return _id; }
        public Select               Id(string id)                           { _id = id; return this; }

        public string               Name()                                  { return _name; }
        public Select               Name(string name)                       { _name = name; return this; }

        public string               SelectedValue()                         { return _selectedValue; }
        public Select               SelectedValue(string value)             { _selectedValue = value; return this; }

        public IEnumerable<Option>  Options()                               { return _options; }
        public Select               Options(IEnumerable<Option> options)    { _options = options; return this; }

        protected override HtmlTag CreateTag()
        {
            var select = new HtmlTag("select")
                .Attr("id", _id)
                .Attr("name", _name);

            foreach (var option in _options)
            {
                var optionTag = option.CreateTag(_selectedValue);
                select.Append(optionTag);
            }

            return select;
        }
    }
}
