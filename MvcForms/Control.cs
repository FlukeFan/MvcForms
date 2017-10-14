using System;
using System.Web;
using System.Web.Mvc;
using HtmlTags;

namespace MvcForms
{
    public abstract class Control<TTag> : IHtmlString
        where TTag : HtmlTag
    {
        private static readonly Func<HtmlHelper, TTag, TTag> _defaultHelper = (h, t) => t;

        private HtmlHelper                      _html;
        private Func<HtmlHelper, TTag, TTag>    _tagMutator = _defaultHelper;
        private Lazy<UrlHelper>                 _urlHelper;

        public Control(HtmlHelper html)
        {
            _html = html;
            _urlHelper = new Lazy<UrlHelper>(() => new UrlHelper(_html.ViewContext.RequestContext));
        }

        protected UrlHelper     Url     => _urlHelper.Value;
        protected HtmlHelper    Html    => _html;

        protected abstract TTag CreateTag();

        protected TTag RenderTag()
        {
            var tag = CreateTag();
            tag = (TTag)Styler.Style(this, tag);
            tag = _tagMutator(_html, tag);
            return tag;
        }

        string IHtmlString.ToHtmlString()
        {
            return RenderTag().ToHtmlString();
        }

        public Control<TTag> Tag(Func<TTag, TTag> tagMutator)
        {
            return Tag((html, tag) => tagMutator(tag));
        }

        public Control<TTag> Tag(Func<HtmlHelper, TTag, TTag> tagMutator)
        {
            _tagMutator = tagMutator;
            return this;
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
