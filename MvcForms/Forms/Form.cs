using System;
using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public class Form<T> : Control<HtmlTag>
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

        public new Form<T> Tag(Func<HtmlTag, HtmlTag> tagMutator)
        {
            Tag((html, tag) => tagMutator(tag));
            return this;
        }

        public new Form<T> Tag(Func<HtmlHelper, HtmlTag, HtmlTag> tagMutator)
        {
            base.Tag(tagMutator);
            return this;
        }

        public ScopedHtmlHelper<T> Begin()
        {
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
