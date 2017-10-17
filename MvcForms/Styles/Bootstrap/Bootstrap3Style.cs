using HtmlTags;
using MvcForms.Forms;

namespace MvcForms.Styles.Bootstrap
{
    public class Bootstrap3Style : BootstrapStyle
    {
        public Bootstrap3Style()
        {
            RegisterInterface<IForm>((c, t) => FormStyler((IForm)c, t));
            RegisterInterface<IRenderedFormRow>((c, t) => FormRowStyler((IRenderedFormRow)c, t));
        }

        public virtual HtmlTag FormStyler(IForm form, HtmlTag tag)
        {
            return tag.AddClasses("form-horizontal");
        }

        public virtual HtmlTag FormRowStyler(IRenderedFormRow formRow, HtmlTag tag)
        {
            formRow.Row.AddClasses("form-group");
            return tag;
        }
    }
}
