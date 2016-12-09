using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Controls
{
    public class Form<T> : Control<T, FormTag>
    {
        private string          _action;

        public Form(HtmlHelper<T> html) : base(html)
        {
            _action = html.ViewContext.HttpContext.Request.RawUrl;
        }

        protected override FormTag CreateTag()
        {
            return new FormTag(_action);
        }
    }
}
