using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public class Form<T> : Control<T, HtmlTag>
    {
        private string _action;
        private string _method = "post";

        public Form(HtmlHelper<T> html) : base(html)
        {
            _action = html.ViewContext.HttpContext.Request.RawUrl;
        }

        public string Action() { return _action; }
        public Form<T> Action(string action) { _action = action; return this; }

        public string Method() { return _method; }
        public Form<T> Method(string method) { _method = method; return this; }

        protected override HtmlTag CreateTag()
        {
            return new HtmlTag("form")
                .Attr("method", _method)
                .Attr("action", _action);
        }
    }
}
