using HtmlTags;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcForms.Forms
{
    public class Select : PropertyControl
    {
        private string          _id;
        private string          _name;
        private string          _value;

        public Select(IHtmlHelper html, PropertyContext propertyContext) : base(html, propertyContext)
        {
            Id(propertyContext.Id);
            Name(propertyContext.Name);
            Value(propertyContext.Value);
        }

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
