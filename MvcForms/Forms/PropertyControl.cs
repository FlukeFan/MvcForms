using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcForms.Forms
{
    public interface IPropertyControl : IControl
    {
        PropertyContext PropertyContext { get; }
    }

    public abstract class PropertyControl : Control, IPropertyControl
    {
        public PropertyControl(IHtmlHelper html, PropertyContext propertyContext) : base(html)
        {
            PropertyContext = propertyContext;
        }

        public PropertyContext PropertyContext { get; }
    }
}
