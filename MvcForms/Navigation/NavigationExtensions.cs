using System.Web;
using System.Web.Mvc;

namespace MvcForms.Navigation
{
    public static class NavigationExtensions
    {
        public static LinkButton<TViewModel> LinkButton<TViewModel>(this HtmlHelper<TViewModel> helper, string content, string action)
        {
            return helper.LinkButton(MvcHtmlString.Create(content), action);
        }

        public static LinkButton<TViewModel> LinkButton<TViewModel>(this HtmlHelper<TViewModel> helper, IHtmlString content, string action)
        {
            return new LinkButton<TViewModel>(helper, content, action);
        }

        public static LinkButton<TViewModel> LinkButtonModal<TViewModel>(this HtmlHelper<TViewModel> helper, string content, string action)
        {
            return helper.LinkButtonModal(MvcHtmlString.Create(content), action);
        }

        public static LinkButton<TViewModel> LinkButtonModal<TViewModel>(this HtmlHelper<TViewModel> helper, IHtmlString content, string action)
        {
            return new LinkButton<TViewModel>(helper, content, action).Modal();
        }

        public static LinkButton<TViewModel> LinkButtonCancelModal<TViewModel>(this HtmlHelper<TViewModel> helper, string content = "Cancel")
        {
            return helper.LinkButtonCancelModal(MvcHtmlString.Create(content));
        }

        public static LinkButton<TViewModel> LinkButtonCancelModal<TViewModel>(this HtmlHelper<TViewModel> helper, IHtmlString content)
        {
            return new LinkButton<TViewModel>(helper, content).ModalReturn();
        }
    }
}
