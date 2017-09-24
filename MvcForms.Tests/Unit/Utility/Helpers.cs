using System.Web;

namespace MvcForms.Tests.Unit.Utility
{
    public static class Helpers
    {
        public static string ToHtmlString(this IHtmlString control)
        {
            return control.ToHtmlString();
        }
    }
}
