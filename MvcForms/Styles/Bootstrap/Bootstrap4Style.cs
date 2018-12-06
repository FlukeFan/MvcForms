using HtmlTags;
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
    }
}
