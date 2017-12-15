using System;
using System.Web;

namespace MvcForms.StubApp.Utility
{
    public static class CssFrameworkExtensions
    {
        public static CssFramework GetCurrentCssFramework(this HttpContextBase context)
        {
            var contextFramework = context.Items["CssFramework"];

            if (contextFramework != null)
                return (CssFramework)contextFramework;

            var currentFramework = CssFramework.Bootstrap3;

            var frameworkCookie = context.Request.Cookies["cssFramework"];

            if (frameworkCookie != null)
                currentFramework = (CssFramework)Enum.Parse(typeof(CssFramework), frameworkCookie.Value);

            context.Items["CssFramework"] = currentFramework;
            return currentFramework;
        }
    }
}