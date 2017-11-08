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
            RegisterInterface<IRenderedFormButtons>((c, t) => FormButtonsStyler((IRenderedFormButtons)c, t));
            RegisterInterface<IRenderedErrorSummary>((c, t) => ErrorSummaryStyler((IRenderedErrorSummary)c, t));
            Register<Input>((c, t) => InputStyler((Input)c, t));
        }

        public virtual HtmlTag FormStyler(IForm form, HtmlTag tag)
        {
            return tag.AddClasses("form-horizontal");
        }

        public virtual HtmlTag FormGroupStyler(IRenderedFormGroup formGroup, HtmlTag tag)
        {
            var groupContext = formGroup.GroupContext;

            formGroup.Container.AddClasses("form-group");
            formGroup.Label.AddClasses("control-label", "col-sm-4");
            formGroup.ControlContainer.AddClasses("col-sm-8");

            if (formGroup.Error != null)
                formGroup.Error.AddClasses("help-block", "col-sm-offset-4", "col-sm-8");

            if (groupContext.HasErrors)
                formGroup.Container.AddClasses("has-error");

            return tag;
        }

        public virtual HtmlTag FormButtonsStyler(IRenderedFormButtons formButtons, HtmlTag tag)
        {
            formButtons.Outer.AddClasses("row");
            formButtons.Inner.AddClasses("col-sm-offset-4", "col-sm-8");
            return tag;
        }

        public virtual HtmlTag ErrorSummaryStyler(IRenderedErrorSummary errorSummary, HtmlTag tag)
        {
            foreach (var errorTag in errorSummary.Errors)
            {
                var text = errorTag.Text();
                errorTag.AddClasses("alert", "alert-warning", "alert-dismissible");
                errorTag.Attr("role", "alert");

                var closeButton = new HtmlTag("button")
                    .Attr("type", "button")
                    .AddClasses("close", "show-js")
                    .Attr("data-dismiss", "alert")
                    .Attr("aria-label", "Close")
                    .Append(new HtmlTag("span")
                        .Text("&times;").Encoded(false)
                        .Attr("aria-hidden", "true"));

                errorTag.Append(closeButton);
            }

            return tag;
        }

        public virtual HtmlTag InputStyler(Input input, HtmlTag tag)
        {
            return input.Type() != "hidden"
                ? tag.AddClasses("form-control")
                : tag;
        }
    }
}
