using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MvcForms.Controls
{
    public class Form<T> : IDisposable
    {
        public HtmlHelper<T>    Html    { get; protected set; }
        public MvcForm          MvcForm { get; protected set; }

        public Form(HtmlHelper<T> html, MvcForm mvcForm)
        {
            Html = html;
            MvcForm = mvcForm;
        }

        public void Dispose()
        {
            MvcForm.Dispose();
        }
    }
}
