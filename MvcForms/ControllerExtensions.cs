using System.Web.Mvc;

namespace MvcForms
{
    public static class ControllerExtensions
    {
        public static ActionResult ReturnModal(this Controller controller, string defaultModalReturnAction = null)
        {
            return controller.Request.IsPjaxModal()
                ? PjaxModal(controller)
                : RedirectModal(controller, defaultModalReturnAction);
        }

        private static ActionResult PjaxModal(Controller controller)
        {
            var script = "<script> mfoDialog.closeDialog(true); </script>";

            return new JavaScriptResult { Script = script };
        }

        private static ActionResult RedirectModal(Controller controller, string defaultModalReturnAction = null)
        {
            var modalReturnUrl = controller.Request.QueryString["modalReturnUrl"];
            var redirectUrl = modalReturnUrl ?? defaultModalReturnAction ?? controller.Request.Url.OriginalString;
            return new RedirectResult(redirectUrl);
        }
    }
}
