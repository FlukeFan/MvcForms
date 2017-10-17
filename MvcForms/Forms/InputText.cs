using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public class InputText : Control
    {
        public InputText(HtmlHelper html) : base(html)
        {
        }

        protected override HtmlTag CreateTag()
        {
            return new HtmlTag("input")
                .Attr("type", "text");
        }
    }
}
