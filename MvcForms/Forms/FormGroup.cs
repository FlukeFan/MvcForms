using System.Linq;
using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public interface IFormGroup
    {
        GroupContext GroupContext { get; }
    }

    public interface IRenderedFormGroup : IFormGroup
    {
        HtmlTag Container           { get; }
        HtmlTag Label               { get; }
        HtmlTag ControlContainer    { get; }
        HtmlTag Control             { get; }
        HtmlTag Error               { get; }
    }

    public class FormGroup<TControl> : Control, IRenderedFormGroup
        where TControl : Control
    {
        private GroupContext    _groupContext;
        private TControl        _control;

        // post-render members
        private HtmlTag     _containerTag;
        private HtmlTag     _labelTag;
        private HtmlTag     _controlContainerTag;
        private HtmlTag     _controlTag;
        private HtmlTag     _errorTag;

        public FormGroup(HtmlHelper html, GroupContext groupContext, TControl control) : base(html)
        {
            _groupContext = groupContext;
            _control = control;
        }

        public GroupContext GroupContext => _groupContext;

        HtmlTag IRenderedFormGroup.Container        => _containerTag;
        HtmlTag IRenderedFormGroup.Label            => _labelTag;
        HtmlTag IRenderedFormGroup.ControlContainer => _controlContainerTag;
        HtmlTag IRenderedFormGroup.Control          => _controlTag;
        HtmlTag IRenderedFormGroup.Error            => _errorTag;

        protected override HtmlTag CreateTag()
        {
            var property = _groupContext.Property;

            _labelTag = new HtmlTag("label")
                .Attr("for", property.Id)
                .Text(_groupContext.LabelText);

            _controlTag = _control.RenderTag();
            _controlContainerTag = new HtmlTag("div").Append(_controlTag);

            _containerTag = new HtmlTag("div")
                .Append(_labelTag)
                .Append(_controlContainerTag);

            if (_groupContext.HasErrors)
            {
                var messages = property.ModelState.Errors.Select(e => e.ErrorMessage);
                var message = string.Join(" ", messages);
                _errorTag = new HtmlTag("div").Text(message);
                _containerTag.Append(_errorTag);
            }

            return _containerTag;
        }
    }
}
