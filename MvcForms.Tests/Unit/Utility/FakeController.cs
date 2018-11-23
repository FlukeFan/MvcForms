using Microsoft.AspNetCore.Mvc;

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
            //var fakeContext = new FakeHttpContext();
            //var routeData = new RouteData();
            //ControllerContext = new ControllerContext(fakeContext, routeData, this);
        }

        //public FakeHttpContext FakeHttpContext => (FakeHttpContext)HttpContext;

        public FakeController SetRawUrl(string rawUrl) { /*FakeHttpContext.FakeRequest.SetRawUrl(rawUrl);*/ return this; }
    }
}
