﻿using HtmlTags;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcForms.Forms
{
    public class Input : PropertyControl
    {
        private string          _type;
        private string          _id;
        private string          _name;
        private string          _value;
        private string          _autoComplete;

        public Input(IHtmlHelper html, string type, PropertyContext propertyContext) : base(html, propertyContext)
        {
            Type(type);
            Id(propertyContext.Id);
            Name(propertyContext.Name);
            Value(propertyContext.Value);
        }

        public string   Type()              { return _type; }
        public Input    Type(string type)   { _type = type; return this; }

        public string   Id()                { return _id; }
        public Input    Id(string id)       { _id = id; return this; }

        public string   Name()              { return _name; }
        public Input    Name(string name)   { _name = name; return this; }

        public string   Value()             { return _value; }
        public Input    Value(string value) { _value = value; return this; }

        public string   AutoComplete()                      { return _autoComplete; }
        public Input    AutoComplete(string autoComplete)   { _autoComplete = autoComplete; return this; }
        public Input    AutoCompleteOff()                   { return AutoComplete("off"); }

        protected override HtmlTag CreateTag()
        {
            return new HtmlTag("input")
                .Attr("type", _type)
                .Attr("id", _id)
                .Attr("name", _name)
                .Attr("value", _value)
                .Attr("autocomplete", _autoComplete);
        }
    }
}
