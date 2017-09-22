using System.Web;
using System.Web.Mvc;
using HtmlTags;
using MvcForms.Controls;

namespace MvcForms.Navigation
{
    public class LinkButton<T> : Control<T, LinkTag>
    {
        private IHtmlString _content;
        private string      _action;

        public LinkButton(HtmlHelper<T> html, IHtmlString content, string action) : base(html)
        {
            _content = content;
            _action = action;
        }

        public string Action() { return _action; }

        protected override LinkTag CreateTag()
        {
            var url = Url.Content(_action);
            return new LinkTag(_content.ToHtmlString(), url);
        }
    }
}
