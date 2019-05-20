using HtmlTags;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcForms.Forms
{
    public class Select : Control
    {
        private PropertyContext _propertyContext;
        private string          _id;
        private string          _name;
        private string          _value;

        public Select(IHtmlHelper html, PropertyContext propertyContext) : base(html)
        {
            _propertyContext = propertyContext;
            Id(propertyContext.Id);
            Name(propertyContext.Name);
            Value(propertyContext.Value);
        }

        protected PropertyContext PropertyContext => _propertyContext;

        public string   Id()                { return _id; }
        public Select   Id(string id)       { _id = id; return this; }

        public string   Name()              { return _name; }
        public Select   Name(string name)   { _name = name; return this; }

        public string   Value()             { return _value; }
        public Select   Value(string value) { _value = value; return this; }

        protected override HtmlTag CreateTag()
        {
            var select = new HtmlTag("select")
                .Attr("id", _id)
                .Attr("name", _name)
                .Attr("value", _value);

            return select;
        }
    }
}
