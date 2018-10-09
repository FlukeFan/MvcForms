using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcForms
{
    public class PjaxFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var context = filterContext.HttpContext;
            var request = context.Request;
            var response = context.Response;
            response.Headers.Add("X-PJAX-URL", request.GetEncodedUrl());
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}
