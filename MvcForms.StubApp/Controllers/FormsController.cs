using System.Web.Mvc;
using MvcForms.StubApp.Models.Forms;

namespace MvcForms.StubApp.Controllers
{
    public static class FormsActions
    {
        public static string BootstrapHorizontal()              { return "~/Forms/BootstrapHorizontal"; }

        public static string ForModel()                         { return "~/Forms/ForModel"; }
        public static string ForModelUsing(string initialValue) { return $"~/Forms/ForModelUsing/{initialValue}"; }
        public static string ForModel(string initialValue)      { return $"~/Forms/ForModel/{initialValue}"; }

        public static string FormFor(string initialValue)       { return $"~/Forms/FormFor/{initialValue}"; }
    }

    public class FormsController : Controller
    {
        [HttpGet]
        public ActionResult BootstrapHorizontal()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ForModel(string id)
        {
            var model = new ForModelView();

            if (!string.IsNullOrWhiteSpace(id))
                model.Post = new ForModelPost { BasicValue = id };

            return View(model);
        }

        [HttpPost]
        public ActionResult ForModel(ForModelPost input)
        {
            var model = new ForModelView();
            return View(model);
        }

        [HttpGet]
        public ActionResult ForModelUsing(string id)
        {
            var model = new ForModelView();

            if (!string.IsNullOrWhiteSpace(id))
                model.Post = new ForModelPost { BasicValue = id };

            return View(model);
        }

        [HttpGet]
        public ActionResult FormFor(string id)
        {
            var model = new FormForView();
            model.PostCommand = new FormForPost { Value = id };
            return View(model);
        }
    }
}