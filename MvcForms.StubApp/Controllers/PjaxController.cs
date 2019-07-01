using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace MvcForms.StubApp.Controllers
{
    public static class PjaxActions
    {
        public static string Index()    { return "/Pjax/Index"; }
        public static string Page1()    { return "/Pjax/Page1"; }
        public static string Page2()    { return "/Pjax/Page2"; }
        public static string Page3()    { return "/Pjax/Page3"; }
        public static string Page4()    { return "/Pjax/Page4"; }
        public static string Timeout()  { return "/Pjax/Timeout"; }
        public static string PageErr()  { return "/Pjax/PageErr"; }
        public static string Form()     { return "/Pjax/Form"; }
        public static string FormDone() { return "/Pjax/FormDone"; }
        public static string FormGet()  { return "/Pjax/FormGet"; }
    }

    public class PjaxController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Page1()
        {
            return View();
        }

        public ActionResult Page2()
        {
            return View();
        }

        public ActionResult Page3()
        {
            return Redirect(PjaxActions.Page1());
        }

        public ActionResult Page4()
        {
            return new ChallengeResult();
        }

        public ActionResult Timeout()
        {
            Thread.Sleep(45000);
            throw new Exception("Should not see this error client-side when using pjax");
        }

        public ActionResult PageErr()
        {
            throw new Exception("DeliberateError");
        }

        public ActionResult Form()
        {
            object model = "First load";
            return View(model);
        }

        [HttpPost]
        public ActionResult Form(string postedValue)
        {
            if (!string.IsNullOrWhiteSpace(Request.Form["redirect"]))
                return Redirect(PjaxActions.Form());

            if (postedValue == "Success")
                return Redirect(PjaxActions.FormDone());

            return View((object)postedValue);
        }

        public ActionResult FormDone()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FormGet(string value)
        {
            return View("FormGet", value);
        }
    }
}