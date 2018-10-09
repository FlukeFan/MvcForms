using System;
using HtmlTags;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MvcForms.Forms
{
    public interface IForm
    {
    }

    public class Form<T> : Control, IForm
    {
        private string  _action;
        private string  _method;
        private string  _autoComplete;

        public Form(HtmlHelper<T> html) : base(html)
        {
            _action = html.ViewContext.HttpContext.Request.GetEncodedUrl();
            Method("post");
        }

        public string   Action()                            { return _action; }
        public Form<T>  Action(string action)               { _action = action; return this; }

        public string   Method()                            { return _method; }
        public Form<T>  Method(string method)               { _method = method; return this; }

        public string   AutoComplete()                      { return _autoComplete; }
        public Form<T>  AutoComplete(string autoComplete)   { _autoComplete = autoComplete; return this; }
        public Form<T>  AutoCompleteOff()                   { return AutoComplete("off"); }

        public ScopedHtmlHelper<T> Begin(Action<Form<T>> formMutator = null)
        {
            formMutator?.Invoke(this);
            return Begin<T>();
        }

        protected override HtmlTag CreateTag()
        {
            return new HtmlTag("form")
                .Attr("method", _method)
                .Attr("action", _action)
                .Attr("autocomplete", _autoComplete);
        }
    }
}
