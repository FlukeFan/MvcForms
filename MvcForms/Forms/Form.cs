using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public class Form<T> : Control<T, FormTag>
    {
        private string _action;

        public Form(HtmlHelper<T> html) : base(html)
        {
            _action = html.ViewContext.HttpContext.Request.RawUrl;
        }

        public string Action() { return _action; }
        public Form<T> Action(string action) { _action = action;  return this; }

        protected override FormTag CreateTag()
        {
            return new FormTag(_action);
        }
    }
}
