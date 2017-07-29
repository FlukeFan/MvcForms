using System.Web.Mvc;

namespace MvcForms
{
    public class PjaxFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var context = filterContext.HttpContext;
            var request = context.Request;
            var response = context.Response;
            response.AddHeader("X-PJAX-URL", request.Url.ToString());
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}
