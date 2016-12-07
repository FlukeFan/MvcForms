using System;
using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Controls
{
    public class Form<T> : IDisposable
    {
        public HtmlHelper<T>    Html    { get; protected set; }
        public FormTag          FormTag { get; protected set; }

        public Form(HtmlHelper<T> html)
        {
            Html = html;
            FormTag = new FormTag(html.ViewContext.HttpContext.Request.RawUrl);

            html.ViewContext.Writer.Write(FormTag.NoClosingTag().ToString());
        }

        public void Dispose()
        {
            Html.ViewContext.Writer.Write($"</{FormTag.TagName()}>");
        }
    }
}
