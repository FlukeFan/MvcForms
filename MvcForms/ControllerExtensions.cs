using System.Web.Mvc;

namespace MvcForms
{
    public static class ControllerExtensions
    {
        public static ActionResult ReturnModal(this Controller controller, string defaultModalReturnAction = null)
        {
            var modalReturnUrl = controller.Request.QueryString["modalReturnUrl"];
            var redirectUrl = modalReturnUrl ?? defaultModalReturnAction ?? controller.Request.Url.OriginalString;
            return new RedirectResult(redirectUrl);
        }
    }
}
