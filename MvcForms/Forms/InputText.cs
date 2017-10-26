﻿using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public class InputText : NamedInput
    {
        public InputText(HtmlHelper html, PropertyContext propertyContext) : base(html, propertyContext)
        {
        }

        protected override HtmlTag CreateTag()
        {
            return CreateInputTag()
                .Attr("type", "text");
        }
    }
}
