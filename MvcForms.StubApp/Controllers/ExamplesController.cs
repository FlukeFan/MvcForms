using System.Web.Mvc;
using MvcForms.StubApp.Models.Examples;

namespace MvcForms.StubApp.Controllers
{
    public static class ExamplesActions
    {
        public static string Index()    { return "~/Examples"; }
        public static string Buttons()  { return "~/Examples/Buttons"; }
        public static string Inputs()   { return "~/Examples/Inputs"; }
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

        public ActionResult Inputs()
        {
            var model = new InputsModel();
            return View(model);
        }
    }
}