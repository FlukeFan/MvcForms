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
        private bool        _noPjax;

        public LinkButton(HtmlHelper<T> html, IHtmlString content, string action) : base(html)
        {
            Content(content);
            Action(action);
        }

        public IHtmlString Content() { return _content; }
        public LinkButton<T> Content(IHtmlString content)   { _content = content; return this; }
        public LinkButton<T> Content(string content)        { return this.Content(MvcHtmlString.Create(content)); }

        public string Action() { return _action; }
        public LinkButton<T> Action(string action)          { _action = action; return this; }

        public bool GetNoPjax() { return _noPjax; }
        public LinkButton<T> NoPjax(bool noPjax = true)     { _noPjax = noPjax; return this; }

        protected override LinkTag CreateTag()
        {
            var url = Url.Content(_action);
            var tag = new LinkTag(_content.ToHtmlString(), url);

            if (_noPjax)
                tag.Attr("data-nopjax", "true");

            return tag;
        }
    }
}
