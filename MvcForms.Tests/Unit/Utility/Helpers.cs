﻿using Microsoft.AspNetCore.Html;

namespace MvcForms.Tests.Unit.Utility
{
    public static class Helpers
    {
        public static string ToHtmlString(this HtmlString control)
        {
            return control.ToHtmlString();
        }

        public static FakeHtmlHelper<T> Helper<T>(this T model)
        {
            return FakeHtmlHelper.New(model);
        }
    }
}
