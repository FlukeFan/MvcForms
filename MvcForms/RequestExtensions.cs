using System.Web;

namespace MvcForms
{
    public static class RequestExtensions
    {
        public static bool IsPjax(this HttpRequestBase request)
        {
            return !string.IsNullOrEmpty(request.Headers["X-PJAX"]);
        }
    }
}
