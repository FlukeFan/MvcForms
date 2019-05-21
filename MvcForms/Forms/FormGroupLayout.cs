using HtmlTags;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcForms.Forms
{
    public interface IRenderedFormGroupLayout
    {
        HtmlTag Outer { get; }
        HtmlTag Inner { get; }
    }

    public class FormGroupLayout : Control, IRenderedFormGroupLayout
    {
        // post-render members
        private HtmlTag _outer;
        private HtmlTag _inner;

        public FormGroupLayout(IHtmlHelper html) : base(html)
        {
        }

        HtmlTag IRenderedFormGroupLayout.Outer  => _outer;
        HtmlTag IRenderedFormGroupLayout.Inner  => _inner;

        protected override HtmlTag CreateTag()
        {
            _outer = new HtmlTag("div");
            _inner = new HtmlTag("div");

            return _outer.Append(_inner);
        }

        public override ScopedHtmlHelper<TModel> Begin<TModel>()
        {
            RenderTag();
            _outer.NoClosingTag();
            _inner.NoClosingTag();

            var writer = Html.ViewContext.Writer;
            writer.Write(_outer.ToHtmlString());

            return new ScopedHtmlHelper<TModel>(Html, () =>
            {
                writer.Write($"</{_inner.TagName()}>");
                writer.Write($"</{_outer.TagName()}>");
            });
        }
    }
}
