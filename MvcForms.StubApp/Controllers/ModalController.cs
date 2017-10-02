﻿using System.Web.Mvc;

namespace MvcForms.StubApp.Controllers
{
    public static class ModalActions
    {
        public static string Index() { return "~/Modal/Index"; }
        public static string Page1() { return "~/Modal/Page1"; }
        public static string Page2() { return "~/Modal/Page2"; }
    }

    public class ModalController : Controller
    {
        [HttpGet]
        public ActionResult Index()
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
            return View();
        }

        [HttpPost]
        public ActionResult Page1(object ignored)
        {
            return this.ReturnModal();
        }
    }
}