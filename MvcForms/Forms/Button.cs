using System.Web;
using HtmlTags;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcForms.Forms
{
    public class Button : Control, IHasButtonStyle
    {
        private string          _type;
        private IHtmlContent    _content;
        private string          _name;
        private string          _value;
        private bool            _noPjax;

        public Button(IHtmlHelper html, string type, IHtmlContent content) : base(html)
        {
            Type(type);
            Content(content);
        }

        public string       Type()                          { return _type; }
        public Button       Type(string type)               { _type = type; return this; }

        public IHtmlContent Content()                       { return _content; }
        public Button       Content(string content)         { return Content(new HtmlString(HttpUtility.HtmlEncode(content))); }
        public Button       Content(IHtmlContent content)   { _content = content; return this; }

        public string       Name()                          { return _name; }
        public Button       Name(string name)               { _name = name; return this; }

        public string       Value()                         { return _value; }
        public Button       Value(string value)             { _value = value; return this; }

        public bool         CausesNoPjax()                  { return _noPjax; }
        public Button       NoPjax(bool noPjax = true)      { _noPjax = noPjax; return this; }

        protected override HtmlTag CreateTag()
        {
            var tag = new HtmlTag("button")
                .Attr("type", _type)
                .AppendHtml(_content.ToHtmlString());

            if (_name != null)
                tag.Attr("name", _name);

            if (_value != null)
                tag.Attr("value", _value);

            if (_noPjax)
                tag.Attr("data-nopjax", "true");

            return tag;
        }
    }
}
