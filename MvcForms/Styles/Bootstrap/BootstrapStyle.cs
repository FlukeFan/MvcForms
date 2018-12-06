using System.Collections.Generic;
using HtmlTags;
using MvcForms.Forms;
using MvcForms.Navigation;
using MvcForms.Styles.Default;

namespace MvcForms.Styles.Bootstrap
{
    public abstract class BootstrapStyle : CachingStyler
    {
        protected IDictionary<DefaultButton, string> DefaultButtonStyles = new Dictionary<DefaultButton, string>
        {
            { DefaultButton.Default,    "btn-default" },
            { DefaultButton.Primary,    "btn-primary" },
            { DefaultButton.Accent,     "btn-warning" },
        };

        protected IDictionary<BootstrapButton, string> BootstrapButtonStyles = new Dictionary<BootstrapButton, string>
        {
            { BootstrapButton.Default,  "btn-default" },
            { BootstrapButton.Primary,  "btn-primary" },
            { BootstrapButton.Success,  "btn-success" },
            { BootstrapButton.Info,     "btn-info" },
            { BootstrapButton.Warning,  "btn-warning" },
            { BootstrapButton.Danger,   "btn-danger" },
        };

        public BootstrapStyle()
        {
            Register<LinkButton>((c, t) => LinkButtonStyler((LinkButton)c, t));
            Register<Button>((c, t) => ButtonStyler((Button)c, t));
        }

        public virtual HtmlTag LinkButtonStyler(LinkButton linkButton, HtmlTag tag)
        {
            return AddButtonStyle(linkButton, tag);
        }

        public virtual HtmlTag ButtonStyler(Button button, HtmlTag tag)
        {
            return AddButtonStyle(button, tag);
        }

        protected virtual HtmlTag AddButtonStyle(IHasButtonStyle button, HtmlTag tag)
        {
            var basicButton = "btn";
            var buttonStyle = DefaultButtonStyles[DefaultButton.Default];
            var controlBag = button.NullableControlBag;

            if (controlBag != null && controlBag.ContainsKey(ButtonStyle.Key))
            {
                var style = controlBag[ButtonStyle.Key];

                if (style is DefaultButton)
                    buttonStyle = DefaultButtonStyles[(DefaultButton)style];

                if (style is BootstrapButton)
                    buttonStyle = BootstrapButtonStyles[(BootstrapButton)style];
            }

            return tag.AddClasses(basicButton, buttonStyle);
        }
    }
}
