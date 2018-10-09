using Microsoft.AspNetCore.Http;

namespace MvcForms
{
    public static class RequestExtensions
    {
        public static bool IsPjax(this HttpRequest request)
        {
            return !string.IsNullOrEmpty(request.Headers["X-PJAX"]);
        }

        public static bool IsPjaxModal(this HttpRequest request)
        {
            return !string.IsNullOrEmpty(request.Headers["X-PJAX-MODAL"]);
        }
    }
}
