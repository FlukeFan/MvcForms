using System.Collections.Generic;
using HtmlTags;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcForms.Forms
{
    public class Select : PropertyControl
    {
        private string              _id;
        private string              _name;
        private string[]            _selectedValues;
        private IEnumerable<Option> _options;
        private int?                _size;
        private bool                _multiple;

        public Select(IHtmlHelper html, IEnumerable<Option> options, PropertyContext propertyContext) : base(html, propertyContext)
        {
            Id(propertyContext.Id);
            Name(propertyContext.Name);
            SelectedValue(propertyContext.Values);
            Options(options);
        }

        public string               Id()                                    { return _id; }
        public Select               Id(string id)                           { _id = id; return this; }

        public string               Name()                                  { return _name; }
        public Select               Name(string name)                       { _name = name; return this; }

        public string[]             SelectedValues()                        { return _selectedValues; }
        public Select               SelectedValue(string[] values)          { _selectedValues = values; return this; }

        public IEnumerable<Option>  Options()                               { return _options; }
        public Select               Options(IEnumerable<Option> options)    { _options = options; return this; }

        public int?                 Size()                                  { return _size; }
        public Select               Size(int? size)                         { _size = size; return this; }

        public bool                 Multiple()                              { return _multiple; }
        public Select               Multiple(bool multiple)                 { _multiple = multiple; return this; }

        public Select               Optional(string text)
        {
            _options = _options.Optional(text);
            return this;
        }

        protected override HtmlTag CreateTag()
        {
            var select = new HtmlTag("select")
                .Attr("id", _id)
                .Attr("name", _name)
                .Attr("size", _size);

            if (_multiple)
                select.Attr("multiple", "true");

            foreach (var option in _options)
            {
                var optionTag = option.CreateTag(_selectedValues);
                select.Append(optionTag);
            }

            return select;
        }
    }
}
