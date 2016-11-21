using System.Web.Mvc;
using MvcForms.StubApp.Models.Forms;

namespace MvcForms.StubApp.Controllers
{
    public static class FormsActions
    {
        public static string ForModel() { return "~/Forms/ForModel"; }
    }

    public class FormsController : Controller
    {
        [HttpGet]
        public ActionResult ForModel()
        {
            var model = new ForModelView();
            return View(model);
        }

        //[HttpPost]
        //public ActionResult ForModel(ForModelPost input)
        //{
        //    var model = new ForModelView { Post = input };
        //    return View(model);
        //}
    }
}