using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

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

            return new ContentResult { ContentType = "application/javascript", Content = script };
        }

        private static ActionResult RedirectModal(Controller controller, string defaultModalReturnAction = null)
        {
            var modalReturn = controller.Request.Query["modalReturnUrl"];
            var modalReturnUrl = modalReturn.Count > 0 ? modalReturn[0] : null;
            var redirectUrl = modalReturnUrl ?? defaultModalReturnAction ?? controller.Request.GetEncodedUrl();
            return new RedirectResult(redirectUrl);
        }
    }
}
