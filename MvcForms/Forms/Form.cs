using System;
using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public class Form<T> : Control
    {
        private string _action;
        private string _method;

        public Form(HtmlHelper<T> html) : base(html)
        {
            _action = html.ViewContext.HttpContext.Request.RawUrl;
            Method("post");
        }

        public string   Action()                { return _action; }
        public Form<T>  Action(string action)   { _action = action; return this; }

        public string   Method()                { return _method; }
        public Form<T>  Method(string method)   { _method = method; return this; }

        public ScopedHtmlHelper<T> Begin(Action<Form<T>> formMutator = null)
        {
            formMutator?.Invoke(this);
            return Begin<T>();
        }

        protected override HtmlTag CreateTag()
        {
            return new HtmlTag("form")
                .Attr("method", _method)
                .Attr("action", _action);
        }
    }
}
