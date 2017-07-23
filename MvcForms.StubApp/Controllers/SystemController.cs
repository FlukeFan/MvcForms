using System.Web.Mvc;

namespace MvcForms.StubApp.Controllers
{
    public static class SystemActions
    {
        public static string PjaxPage1()    { return "~/System/PjaxPage1"; }
        public static string PjaxPage2()    { return "~/System/PjaxPage2"; }
        public static string PjaxPage3()    { return "~/System/PjaxPage3"; }
        public static string PjaxForm()     { return "~/System/PjaxForm"; }
        public static string PjaxFormDone() { return "~/System/PjaxFormDone"; }
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

        public ActionResult PjaxForm()
        {
            object model = "First load";
            return View(model);
        }

        [HttpPost]
        public ActionResult PjaxForm(string postedValue)
        {
            if (postedValue == "Success")
                return Redirect(SystemActions.PjaxFormDone());
            else
                return View((object)postedValue);
        }

        public ActionResult PjaxFormDone()
        {
            return View();
        }
    }
}