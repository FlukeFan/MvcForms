using System.Collections.Generic;
using MvcForms.Styles.Bootstrap;

namespace MvcForms.StubApp.Utility
{
    public class MultiStyler : IStyler
    {
        private IDictionary<CssFramework, IStyler> _stylers = new Dictionary<CssFramework, IStyler>
        {
            { CssFramework.Bootstrap3,  new Bootstrap3Style() },
            { CssFramework.Bootstrap4,  new Bootstrap4Style() },
        };

        public Styler.ApplyStyle StylerFor(IControl control)
        {
            var framework = control.Html.ViewContext.HttpContext.GetCurrentCssFramework();
            return _stylers[framework].StylerFor(control);
        }
    }
}