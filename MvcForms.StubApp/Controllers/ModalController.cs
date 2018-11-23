using Microsoft.AspNetCore.Mvc;

namespace MvcForms.StubApp.Controllers
{
    public static class ModalActions
    {
        public static string Index() { return "~/Modal/Index"; }
        public static string Client() { return "~/Modal/Client"; }
        public static string Page1() { return "~/Modal/Page1"; }
        public static string Page2() { return "~/Modal/Page2"; }
    }

    public class ModalController : Controller
    {
        public static int Count;

        [HttpGet]
        public ActionResult Index()
        {
            Count = 0;
            return View();
        }

        [HttpGet]
        public ActionResult Client()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Page1()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Page2()
        {
            Count++;
            return View();
        }

        [HttpPost]
        public ActionResult Page1(object ignored)
        {
            return this.ReturnModal();
        }

        [HttpPost]
        public ActionResult Page2(object ignored)
        {
            Count++;
            return this.ReturnModal();
        }
    }
}
