using System.Web.Mvc;

namespace MvcForms.StubApp.Controllers
{
    public static class ExamplesActions
    {
        public static string Index()    { return "~/Examples"; }
        public static string Buttons()  { return "~/Examples/Buttons"; }
    }

    public class ExamplesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buttons()
        {
            return View();
        }
    }
}