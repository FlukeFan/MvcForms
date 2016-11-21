using System.Web.Mvc;

namespace MvcForms.StubApp.Controllers
{
    public static class HomeActions
    {
        public static string Index() { return "~/"; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}