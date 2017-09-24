using System.Web;
using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public class Button<T> : Control<T, HtmlTag>
    {
        private string      _type;
        private IHtmlString _content;
        private string      _name;
        private string      _value;
        private bool        _noPjax;

        public Button(HtmlHelper<T> html, string type, IHtmlString content) : base(html)
        {
            Type(type);
            Content(content);
        }

        public string Type()                            { return _type; }
        public Button<T> Type(string type)              { _type = type; return this; }

        public IHtmlString Content()                    { return _content; }
        public Button<T> Content(string content)        { return Content(MvcHtmlString.Create(content)); }
        public Button<T> Content(IHtmlString content)   { _content = content; return this; }

        public string Name()                            { return _name; }
        public Button<T> Name(string name)              { _name = name; return this; }

        public string Value()                           { return _value; }
        public Button<T> Value(string value)            { _value = value; return this; }

        public bool CausesNoPjax()                      { return _noPjax; }
        public Button<T> NoPjax(bool noPjax = true)     { _noPjax = noPjax; return this; }

        protected override HtmlTag CreateTag()
        {
            var tag = new HtmlTag("button")
                .Attr("type", _type)
                .Text(_content.ToHtmlString()).Encoded(false);

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
