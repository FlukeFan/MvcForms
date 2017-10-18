using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public interface IRenderedFormGroup
    {
        HtmlTag Container   { get; }
        HtmlTag Label       { get; }
        HtmlTag Control     { get; }
    }

    public class FormGroup<TControl> : Control, IRenderedFormGroup
        where TControl : Control
    {

        private GroupContext    _groupContext;
        private TControl        _control;

        // POST render members
        private HtmlTag     _containerTag;
        private HtmlTag     _labelTag;
        private HtmlTag     _controlTag;

        public FormGroup(HtmlHelper html, GroupContext groupContext, TControl control) : base(html)
        {
            _groupContext = groupContext;
            _control = control;
        }

        HtmlTag IRenderedFormGroup.Container    => _containerTag;
        HtmlTag IRenderedFormGroup.Label        => _labelTag;
        HtmlTag IRenderedFormGroup.Control      => _controlTag;

        protected override HtmlTag CreateTag()
        {
            _labelTag = new HtmlTag("label")
                .Attr("for", _groupContext.Property.Id)
                .Text(_groupContext.LabelText);

            _controlTag = _control.RenderTag();

            _containerTag = new HtmlTag("div")
                .Append(_labelTag)
                .Append(_controlTag);

            return _containerTag;
        }
    }
}
