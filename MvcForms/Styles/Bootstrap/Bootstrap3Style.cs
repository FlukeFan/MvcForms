using HtmlTags;
using MvcForms.Forms;

namespace MvcForms.Styles.Bootstrap
{
    public class Bootstrap3Style : BootstrapStyle
    {
        public Bootstrap3Style()
        {
            RegisterInterface<IForm>((c, t) => FormStyler((IForm)c, t));
            RegisterInterface<IRenderedFormGroup>((c, t) => FormRowStyler((IRenderedFormGroup)c, t));
        }

        public virtual HtmlTag FormStyler(IForm form, HtmlTag tag)
        {
            return tag.AddClasses("form-horizontal");
        }

        public virtual HtmlTag FormRowStyler(IRenderedFormGroup formGroup, HtmlTag tag)
        {
            formGroup.Container.AddClasses("form-group");
            return tag;
        }
    }
}
