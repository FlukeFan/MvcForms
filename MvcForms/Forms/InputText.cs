using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public class InputText : Control
    {
        private PropertyContext _propertyContext;
        private string          _id;
        private string          _name;
        private string          _value;

        public InputText(HtmlHelper html, PropertyContext propertyContext) : base(html)
        {
            _propertyContext = propertyContext;
            Id(propertyContext.Id);
            Name(propertyContext.Name);
            Value(propertyContext.Value);
        }

        public string       Id()                { return _id; }
        public InputText    Id(string id)       { _id = id; return this; }

        public string       Name()              { return _name; }
        public InputText    Name(string name)   { _name = name; return this; }

        public string       Value()             { return _value; }
        public InputText    Value(string value) { _value = value; return this; }

        protected override HtmlTag CreateTag()
        {
            return new HtmlTag("input")
                .Attr("id", _id)
                .Attr("name", _name)
                .Attr("type", "text")
                .Attr("value", _value);
        }
    }
}
