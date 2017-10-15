using System.Web;
using System.Web.Mvc;

namespace MvcForms.Navigation
{
    public static class NavigationExtensions
    {
        public static LinkButton LinkButton<TViewModel>(this HtmlHelper<TViewModel> helper, string content, string action)
        {
            return helper.LinkButton(MvcHtmlString.Create(HttpUtility.HtmlEncode(content)), action);
        }

        public static LinkButton LinkButton<TViewModel>(this HtmlHelper<TViewModel> helper, IHtmlString content, string action)
        {
            return new LinkButton(helper, content, action);
        }

        public static LinkButton LinkButtonModal<TViewModel>(this HtmlHelper<TViewModel> helper, string content, string action)
        {
            return helper.LinkButtonModal(MvcHtmlString.Create(HttpUtility.HtmlEncode(content)), action);
        }

        public static LinkButton LinkButtonModal<TViewModel>(this HtmlHelper<TViewModel> helper, IHtmlString content, string action)
        {
            return new LinkButton(helper, content, action).Modal();
        }

        public static LinkButton LinkButtonCancelModal<TViewModel>(this HtmlHelper<TViewModel> helper, string content = "Cancel")
        {
            return helper.LinkButtonCancelModal(MvcHtmlString.Create(HttpUtility.HtmlEncode(content)));
        }

        public static LinkButton LinkButtonCancelModal<TViewModel>(this HtmlHelper<TViewModel> helper, IHtmlString content)
        {
            return new LinkButton(helper, content).ModalReturn();
        }
    }
}
