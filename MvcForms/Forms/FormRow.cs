using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public interface IRenderedFormRow
    {
        HtmlTag Row     { get; }
        HtmlTag Control { get; }
    }

    public class FormRow<TControl> : Control, IRenderedFormRow
        where TControl : Control
    {

        private string      _labeltext;
        private TControl    _control;

        // POST render members
        private HtmlTag     _rowTag;
        private HtmlTag     _controlTag;

        public FormRow(HtmlHelper html, string labelText, TControl control) : base(html)
        {
            _labeltext = labelText;
            _control = control;
        }

        HtmlTag IRenderedFormRow.Row        => _rowTag;
        HtmlTag IRenderedFormRow.Control    => _controlTag;

        protected override HtmlTag CreateTag()
        {
            var label = new HtmlTag("label").Text(_labeltext);
            _controlTag = _control.RenderTag();

            _rowTag = new HtmlTag("div")
                .Append(label)
                .Append(_controlTag);

            return _rowTag;
        }
    }
}
