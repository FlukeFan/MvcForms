using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public class Input : Control
    {
        private PropertyContext _propertyContext;
        private string          _type;
        private string          _id;
        private string          _name;
        private string          _value;

        public Input(HtmlHelper html, PropertyContext propertyContext, string type) : base(html)
        {
            _propertyContext = propertyContext;
            Type(type);
            Id(propertyContext.Id);
            Name(propertyContext.Name);
            Value(propertyContext.Value);
        }

        protected PropertyContext PropertyContext => _propertyContext;

        public string   Type()              { return _type; }
        public Input    Type(string type)   { _type = type; return this; }

        public string   Id()                { return _id; }
        public Input    Id(string id)       { _id = id; return this; }

        public string   Name()              { return _name; }
        public Input    Name(string name)   { _name = name; return this; }

        public string   Value()             { return _value; }
        public Input    Value(string value) { _value = value; return this; }

        protected override HtmlTag CreateTag()
        {
            return new HtmlTag("input")
                .Attr("type", _type)
                .Attr("id", _id)
                .Attr("name", _name)
                .Attr("value", _value);
        }
    }
}
