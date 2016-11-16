using System.Web.Mvc;

namespace MvcForms.StubApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("Hello world");
        }
    }
}