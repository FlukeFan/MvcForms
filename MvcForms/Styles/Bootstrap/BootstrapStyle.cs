using HtmlTags;
using MvcForms.Forms;
using MvcForms.Navigation;

namespace MvcForms.Styles.Bootstrap
{
    public class BootstrapStyle : CachingStyler
    {
        public BootstrapStyle()
        {
            Register<LinkButton>((c, t) => LinkButtonStyler((LinkButton)c, t));
            Register<Button>((c, t) => ButtonStyler((Button)c, t));
        }

        public virtual HtmlTag LinkButtonStyler(LinkButton linkButton, HtmlTag tag)
        {
            return tag.AddClasses("btn", "btn-default");
        }

        public virtual HtmlTag ButtonStyler(Button linkButton, HtmlTag tag)
        {
            return tag.AddClasses("btn", "btn-default");
        }
    }
}
