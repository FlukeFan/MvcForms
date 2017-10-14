using System;
using System.Web;
using System.Web.Mvc;
using HtmlTags;

namespace MvcForms
{
    public abstract class Control : IHtmlString
    {
        private static readonly Func<HtmlHelper, HtmlTag, HtmlTag> _defaultHelper = (h, t) => t;

        private HtmlHelper                          _html;
        private Func<HtmlHelper, HtmlTag, HtmlTag>  _tagMutator = _defaultHelper;
        private Lazy<UrlHelper>                     _urlHelper;

        public Control(HtmlHelper html)
        {
            _html = html;
            _urlHelper = new Lazy<UrlHelper>(() => new UrlHelper(_html.ViewContext.RequestContext));
        }

        protected UrlHelper     Url     => _urlHelper.Value;
        protected HtmlHelper    Html    => _html;

        protected abstract HtmlTag CreateTag();

        protected HtmlTag RenderTag()
        {
            var tag = CreateTag();
            tag = Styler.Style(this, tag);
            tag = _tagMutator(_html, tag);
            return tag;
        }

        string IHtmlString.ToHtmlString()
        {
            return RenderTag().ToHtmlString();
        }

        public void SetTagMutator(Func<HtmlHelper, HtmlTag, HtmlTag> tagMutator)
        {
            _tagMutator = tagMutator;
        }

        public ScopedHtmlHelper<TModel> Begin<TModel>()
        {
            var tag = RenderTag().NoClosingTag();
            _html.ViewContext.Writer.Write(tag.ToHtmlString());

            return new ScopedHtmlHelper<TModel>(_html, () =>
            {
                _html.ViewContext.Writer.Write($"</{tag.TagName()}>");
            });
        }
    }
}
