using HtmlTags;
using MvcForms.Navigation;

namespace MvcForms.Styles.Bootstrap
{
    public class BootstrapStyle : CachingStyler
    {
        public BootstrapStyle()
        {
            Register<LinkButton>((c, t) => LinkButtonStyler((LinkButton)c, t));
        }

        public virtual HtmlTag LinkButtonStyler(LinkButton linkButton, HtmlTag tag)
        {
            return tag.AddClasses("btn", "btn-primary");
        }
    }
}
