using System.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MvcForms.Navigation
{
    public static class NavigationExtensions
    {
        public static LinkButton LinkButton<TViewModel>(this HtmlHelper<TViewModel> helper, string content, string action)
        {
            return helper.LinkButton(new HtmlString(HttpUtility.HtmlEncode(content)), action);
        }

        public static LinkButton LinkButton<TViewModel>(this HtmlHelper<TViewModel> helper, IHtmlContent content, string action)
        {
            return new LinkButton(helper, content, action);
        }

        public static LinkButton LinkButtonModal<TViewModel>(this HtmlHelper<TViewModel> helper, string content, string action)
        {
            return helper.LinkButtonModal(new HtmlString(HttpUtility.HtmlEncode(content)), action);
        }

        public static LinkButton LinkButtonModal<TViewModel>(this HtmlHelper<TViewModel> helper, IHtmlContent content, string action)
        {
            return new LinkButton(helper, content, action).Modal();
        }

        public static LinkButton LinkButtonCancelModal<TViewModel>(this HtmlHelper<TViewModel> helper, string content = "Cancel")
        {
            return helper.LinkButtonCancelModal(new HtmlString(HttpUtility.HtmlEncode(content)));
        }

        public static LinkButton LinkButtonCancelModal<TViewModel>(this HtmlHelper<TViewModel> helper, IHtmlContent content)
        {
            return new LinkButton(helper, content).ModalReturn();
        }
    }
}
