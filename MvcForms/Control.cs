﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using HtmlTags;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace MvcForms
{
    public interface IControl
    {
        IHtmlHelper Html    { get; }
        IUrlHelper  Url     { get; }

        HtmlTag RenderTag();
    }

    public abstract class Control : IControl, IHtmlContent
    {
        private static readonly TagMutator _defaultMutator = (h, t) => t;

        private IHtmlHelper                 _html;
        private TagMutator                  _tagMutator = _defaultMutator;
        private Lazy<IUrlHelper>            _urlHelper;
        private bool                        _noStyle;
        private IDictionary<string, object> _controlBag;

        public Control(IHtmlHelper html)
        {
            _html = html;
            _urlHelper = new Lazy<IUrlHelper>(() => new UrlHelper(_html.ViewContext));
        }

        public IHtmlHelper  Html    => _html;
        public IUrlHelper   Url     => _urlHelper.Value;

        protected abstract HtmlTag CreateTag();

        public HtmlTag RenderTag()
        {
            var tag = CreateTag();

            if (!_noStyle)
                tag = Styler.Style(this, tag);

            tag = _tagMutator(_html, tag);
            return tag;
        }

        void IHtmlContent.WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            RenderTag().WriteTo(writer, encoder);
        }

        public IDictionary<string, object> ControlBag
        {
            get
            {
                if (_controlBag == null)
                    _controlBag = new Dictionary<string, object>();

                return _controlBag;
            }
        }

        public IDictionary<string, object> NullableControlBag => _controlBag;

        public delegate HtmlTag TagMutator(IHtmlHelper helper, HtmlTag tag);

        public Control Tag(Func<TagMutator, IHtmlHelper, HtmlTag, HtmlTag> tagMutator)
        {
            var existingMutator = _tagMutator;
            _tagMutator = (html, tag) => tagMutator(existingMutator, html, tag);
            return this;
        }

        public Control Tag(Func<HtmlTag, HtmlTag> tagMutator)
        {
            return Tag((mutator, html, tag) =>
            {
                tagMutator(tag);
                return tag;
            });
        }

        public Control NoStyle(bool noStyle = true)
        {
            _noStyle = noStyle;
            return this;
        }

        public virtual ScopedHtmlHelper<TModel> Begin<TModel>()
        {
            var tag = RenderTag().NoClosingTag();
            _html.ViewContext.Writer.Write(tag.ToHtmlString());

            return new ScopedHtmlHelper<TModel>(_html, () =>
            {
                _html.ViewContext.Writer.Write($"</{tag.TagName()}>");
            });
        }
    }
}
