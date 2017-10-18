using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public interface IRenderedFormRow
    {
        HtmlTag Row     { get; }
        HtmlTag Label   { get; }
        HtmlTag Control { get; }
    }

    public class FormRow<TControl> : Control, IRenderedFormRow
        where TControl : Control
    {

        private ControlContext  _controlContext;
        private TControl        _control;

        // POST render members
        private HtmlTag     _rowTag;
        private HtmlTag     _labelTag;
        private HtmlTag     _controlTag;

        public FormRow(HtmlHelper html, ControlContext controlContext, TControl control) : base(html)
        {
            _controlContext = controlContext;
            _control = control;
        }

        HtmlTag IRenderedFormRow.Row        => _rowTag;
        HtmlTag IRenderedFormRow.Label      => _labelTag;
        HtmlTag IRenderedFormRow.Control    => _controlTag;

        protected override HtmlTag CreateTag()
        {
            _labelTag = new HtmlTag("label")
                .Attr("for", _controlContext.Property.Id)
                .Text(_controlContext.LabelText);

            _controlTag = _control.RenderTag();

            _rowTag = new HtmlTag("div")
                .Append(_labelTag)
                .Append(_controlTag);

            return _rowTag;
        }
    }
}
