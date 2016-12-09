using System.Web;
using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Controls
{
    public abstract class Control<TModel, TTag> : IHtmlString
        where TTag : HtmlTag
    {
        private HtmlHelper<TModel> _html;

        public Control(HtmlHelper<TModel> html)
        {
            _html = html;
        }

        protected abstract TTag CreateTag();

        protected TTag RenderTag()
        {
            var tag = CreateTag();
            return tag;
        }

        string IHtmlString.ToHtmlString()
        {
            return RenderTag().ToHtmlString();
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
