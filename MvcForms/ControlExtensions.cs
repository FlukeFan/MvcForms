using System;
using System.Web.Mvc;
using HtmlTags;

namespace MvcForms
{
    public static class ControlExtensions
    {
        public static TControl Tag<TControl>(this TControl control, Func<HtmlHelper, HtmlTag, HtmlTag> tagMutator)
            where TControl : Control
        {
            control.SetTagMutator(tagMutator);
            return control;
        }

        public static TControl Tag<TControl>(this TControl control, Func<HtmlTag, HtmlTag> tagMutator)
            where TControl : Control
        {
            return control.Tag((html, tag) => tagMutator(tag));
        }
    }
}
