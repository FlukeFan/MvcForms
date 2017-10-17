using System;
using System.Collections.Generic;
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
        private bool                                _noStyle;
        private IDictionary<string, object>         _controlBag;

        public Control(HtmlHelper html)
        {
            _html = html;
            _urlHelper = new Lazy<UrlHelper>(() => new UrlHelper(_html.ViewContext.RequestContext));
        }

        protected UrlHelper     Url     => _urlHelper.Value;
        protected HtmlHelper    Html    => _html;

        protected abstract HtmlTag CreateTag();

        public HtmlTag RenderTag()
        {
            var tag = CreateTag();

            if (!_noStyle)
                tag = Styler.Style(this, tag);

            tag = _tagMutator(_html, tag);
            return tag;
        }

        string IHtmlString.ToHtmlString()
        {
            return RenderTag().ToHtmlString();
        }

        public IDictionary<string, object> ControlBag
        {
            get
            {
                if (_controlBag == null)
                    _controlBag = new Dictionary<string, object>();

                return _controlBag;
            }
        }

        public IDictionary<string, object> NullableControlBag => _controlBag;

        public Control Tag(Func<HtmlHelper, HtmlTag, HtmlTag> tagMutator)
        {
            _tagMutator = tagMutator;
            return this;
        }

        public Control Tag(Func<HtmlTag, HtmlTag> tagMutator)
        {
            return Tag((html, tag) => tagMutator(tag));
        }

        public Control NoStyle(bool noStyle = true)
        {
            _noStyle = noStyle;
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
