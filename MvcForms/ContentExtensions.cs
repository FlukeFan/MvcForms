using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace MvcForms
{
    public static class ContentExtensions
    {
        public static string ToHtmlString(this IHtmlContent content)
        {
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
