using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.TagHelpers.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using MvcForms.StubApp.Utility;

namespace MvcForms.StubApp.Views.Shared
{
    public static class SharedViews
    {
        public const string Master      = "~/Views/Shared/_Master.cshtml";
        public const string PjaxWhole   = "~/Views/Shared/_PjaxWhole.cshtml";
        public const string PjaxPartial = "~/Views/Shared/_PjaxPartial.cshtml";
        public const string PjaxModal   = "~/Views/Shared/_PjaxModal.cshtml";

        public static HtmlString CssFrameworkSelector(RazorPage page)
        {
            var context = page.Context;

            var currentFramework = context.GetCurrentCssFramework();

            var selector = new StringBuilder($"<select id='cssFrameworkSelector'>");

            foreach (CssFramework framework in Enum.GetValues(typeof(CssFramework)))
            {
                var selected = framework == currentFramework ? "selected='selected'" : "";
                var option = $"<option value='{framework}' {selected}>{framework}</option>";
                selector.Append(option);
            }

            selector.Append("</select>");
            return new HtmlString(selector.ToString());
        }

        // from:  https://stackoverflow.com/a/45088852/357728
        public static string AddFileVersionToPath(this HttpContext context, string path)
        {
            IMemoryCache cache = context.RequestServices.GetRequiredService<IMemoryCache>();
            var hostingEnvironment = context.RequestServices.GetRequiredService<IHostingEnvironment>();
            var versionProvider = new FileVersionProvider(hostingEnvironment.WebRootFileProvider, cache, context.Request.Path);
            return versionProvider.AddFileVersionToPath(path);
        }
    }
}