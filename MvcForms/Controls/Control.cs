using System;
using System.Web;
using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Controls
{
    public abstract class Control<TModel, TTag> : IHtmlString
        where TTag : HtmlTag
    {
        protected HtmlHelper<TModel>    _html;

        private Action<HtmlHelper<TModel>, TTag>    _tagMutator;

        public Control(HtmlHelper<TModel> html)
        {
            _html = html;
            _tagMutator = null;
        }

        protected abstract TTag CreateTag();

        protected TTag RenderTag()
        {
            var tag = CreateTag();
            _tagMutator?.Invoke(_html, tag);
            return tag;
        }

        string IHtmlString.ToHtmlString()
        {
            return RenderTag().ToHtmlString();
        }

        public Control<TModel, TTag> Tag(Action<TTag> tagMutator)
        {
            return Tag((html, tag) => tagMutator(tag));
        }

        public Control<TModel, TTag> Tag(Action<HtmlHelper<TModel>, TTag> tagMutator)
        {
            _tagMutator = tagMutator;
            return this;
        }

        public ScopedHtmlHelper<TModel> Begin()
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
