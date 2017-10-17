using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public interface IRenderedFormRow
    {
        HtmlTag Row { get; }
    }

    public class FormRow<TControl> : Control, IRenderedFormRow
        where TControl : Control
    {

        private string      _labeltext;
        private TControl    _control;

        // POST render members
        private HtmlTag     _row;

        public FormRow(HtmlHelper html, string labelText, TControl control) : base(html)
        {
            _labeltext = labelText;
            _control = control;
        }

        public HtmlTag Row => _row;

        protected override HtmlTag CreateTag()
        {
            var label = new HtmlTag("label").Text(_labeltext);

            _row = new HtmlTag("div")
                .Append(label)
                .Append(_control.RenderTag());

            return _row;
        }
    }
}
