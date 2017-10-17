using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public class InputText : Control
    {
        private string _id;
        private string _name;

        public InputText(HtmlHelper html, PropertyContext propertyContext) : base(html)
        {
            Id(propertyContext.Id);
            Name(propertyContext.Name);
        }

        public string       Id()                { return _id; }
        public InputText    Id(string id)       { _id = id; return this; }

        public string       Name()              { return _name; }
        public InputText    Name(string name)   { _name = name; return this; }

        protected override HtmlTag CreateTag()
        {
            return new HtmlTag("input")
                .Attr("id", _id)
                .Attr("name", _name)
                .Attr("type", "text");
        }
    }
}
