using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeController : Controller
    {
        public static FakeController New()
        {
            return new FakeController();
        }

        public FakeController()
        {
            var httpContext = new FakeHttpContext();
            var routeData = new RouteData();
            var actionDescriptor = new ControllerActionDescriptor();
            var actionContext = new ActionContext(httpContext, routeData, actionDescriptor);
            ControllerContext = new ControllerContext(actionContext);
        }

        public FakeHttpContext FakeHttpContext => (FakeHttpContext)HttpContext;

        public FakeController SetRawUrl(string rawUrl) { FakeHttpContext.FakeHttpRequest.SetRawUrl(rawUrl); return this; }
    }
}
