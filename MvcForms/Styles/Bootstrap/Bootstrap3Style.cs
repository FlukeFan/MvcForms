using HtmlTags;
using MvcForms.Forms;

namespace MvcForms.Styles.Bootstrap
{
    public class Bootstrap3Style : BootstrapStyle
    {
        public Bootstrap3Style()
        {
            RegisterInterface<IForm>((c, t) => FormStyler((IForm)c, t));
            RegisterInterface<IRenderedFormGroup>((c, t) => FormGroupStyler((IRenderedFormGroup)c, t));
            Register<InputText>((c, t) => InputTextStyler((InputText)c, t));
        }

        public virtual HtmlTag FormStyler(IForm form, HtmlTag tag)
        {
            return tag.AddClasses("form-horizontal");
        }

        public virtual HtmlTag FormGroupStyler(IRenderedFormGroup formGroup, HtmlTag tag)
        {
            formGroup.Container.AddClasses("form-group");
            formGroup.Label.AddClasses("control-label", "col-xs-4");
            formGroup.ControlContainer.AddClasses("col-xs-8");
            return tag;
        }

        public virtual HtmlTag InputTextStyler(InputText inputText, HtmlTag tag)
        {
            return tag.AddClasses("form-control");
        }
    }
}
