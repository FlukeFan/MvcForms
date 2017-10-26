using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public class InputHidden : NamedInput
    {
        public InputHidden(HtmlHelper html, PropertyContext propertyContext) : base(html, propertyContext)
        {
        }

        protected override HtmlTag CreateTag()
        {
            return CreateInputTag()
                .Attr("type", "hidden");
        }
    }
}
