using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public abstract class NamedInput : Control
    {
        private PropertyContext _propertyContext;
        private string          _id;
        private string          _name;
        private string          _value;

        public NamedInput(HtmlHelper html, PropertyContext propertyContext) : base(html)
        {
            _propertyContext = propertyContext;
            Id(propertyContext.Id);
            Name(propertyContext.Name);
            Value(propertyContext.Value);
        }

        protected PropertyContext PropertyContext => _propertyContext;

        public string       Id()                { return _id; }
        public NamedInput   Id(string id)       { _id = id; return this; }

        public string       Name()              { return _name; }
        public NamedInput   Name(string name)   { _name = name; return this; }

        public string       Value()             { return _value; }
        public NamedInput   Value(string value) { _value = value; return this; }

        protected HtmlTag CreateInputTag()
        {
            return new HtmlTag("input")
                .Attr("id", _id)
                .Attr("name", _name)
                .Attr("value", _value);
        }
    }
}
