using System.Web.Mvc;

namespace MvcForms.StubApp.Controllers
{
    public static class SystemActions
    {
        public static string PjaxPage1()    { return "~/System/PjaxPage1"; }
        public static string PjaxPage2()    { return "~/System/PjaxPage2"; }
        public static string PjaxPage3()    { return "~/System/PjaxPage3"; }
    }

    public class SystemController : Controller
    {
        public ActionResult PjaxPage1()
        {
            return View();
        }

        public ActionResult PjaxPage2()
        {
            return View();
        }

        public ActionResult PjaxPage3()
        {
            return Redirect(SystemActions.PjaxPage1());
        }
    }
}