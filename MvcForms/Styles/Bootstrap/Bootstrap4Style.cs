using HtmlTags;
using MvcForms.Forms;
using MvcForms.Navigation;
using MvcForms.Styles.Default;

namespace MvcForms.Styles.Bootstrap
{
    public class Bootstrap4Style : Bootstrap3Style
    {
        public Bootstrap4Style()
        {
            DefaultButtonStyles[DefaultButton.Default] = DefaultButtonStyles[DefaultButton.Primary];
            BootstrapButtonStyles[BootstrapButton.Default] = BootstrapButtonStyles[BootstrapButton.Primary];
        }

        public override HtmlTag LinkButtonStyler(LinkButton linkButton, HtmlTag tag)
        {
            return base.LinkButtonStyler(linkButton, tag)
                .Attr("role", "button");
        }

        public override HtmlTag FormGroupStyler(IRenderedFormGroup formGroup, HtmlTag tag)
        {
            var groupContext = formGroup.GroupContext;

            formGroup.Container.AddClasses("form-group", "form-row");
            formGroup.Label.AddClasses("col-form-label", "col-sm-4");
            formGroup.ControlContainer.AddClasses("col-sm-8");

            if (formGroup.Error != null)
            {
                formGroup.Error.TagName("span");
                formGroup.Error.AddClasses("d-block", "invalid-feedback", "offset-sm-4", "col-sm-8");
            }

            if (groupContext.HasErrors)
            {
                formGroup.Container.AddClass("is-invalid");
                formGroup.Control.AddClass("is-invalid");
            }

            return tag;
        }

        public override HtmlTag FormButtonsStyler(IRenderedFormGroupLayout formGroupLayout, HtmlTag tag)
        {
            formGroupLayout.Outer.AddClasses("form-row");
            formGroupLayout.Inner.AddClasses("offset-sm-4", "col-sm-8");
            return tag;
        }
    }
}
