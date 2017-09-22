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
    }
}
