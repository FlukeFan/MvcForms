using System.Web.Mvc;
using MvcForms.StubApp.Models.Forms;

namespace MvcForms.StubApp.Controllers
{
    public static class FormsActions
    {
        public static string ForModel() { return "~/Forms/ForModel"; }
        public static string ForModel(string initialValue) { return $"~/Forms/ForModel/{initialValue}"; }
    }

    public class FormsController : Controller
    {
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
    }
}