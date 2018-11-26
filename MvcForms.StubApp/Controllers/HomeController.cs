using Microsoft.AspNetCore.Mvc;

namespace MvcForms.StubApp.Controllers
{
    public static class HomeActions
    {
        public static string Index()    { return "/"; }
        public static string Testing()  { return "/Home/Testing"; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Testing()
        {
            return View();
        }
    }
}