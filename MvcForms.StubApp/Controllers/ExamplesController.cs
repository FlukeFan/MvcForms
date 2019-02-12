using Microsoft.AspNetCore.Mvc;
using MvcForms.StubApp.Models.Examples;

namespace MvcForms.StubApp.Controllers
{
    public static class ExamplesActions
    {
        public static string Index()    { return "/Examples"; }
        public static string Buttons()  { return "/Examples/Buttons"; }
        public static string Inputs()   { return "/Examples/Inputs"; }
        public static string Scroll()   { return "/Examples/Scroll"; }
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

        [HttpGet]
        public ActionResult Inputs()
        {
            ModelState.AddModelError("", "Example error message for the form");
            var model = new InputsModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Inputs(FormInputsModel postModel)
        {
            ModelState.AddModelError("", "Error displayed from form POST");
            var model = new InputsModel { PostModel = postModel };
            return View(model);
        }

        [HttpGet]
        public IActionResult Scroll()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Scroll(object ignored)
        {
            return View();
        }
    }
}