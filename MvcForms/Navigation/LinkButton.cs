﻿using System.Web;
using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Navigation
{
    public class LinkButton : Control<HtmlTag>
    {
        private IHtmlString _content;
        private string      _action;
        private bool        _noPjax;
        private bool        _modal;
        private bool        _modalReturn;
        private string      _defaultModalReturn;

        public LinkButton(HtmlHelper html, IHtmlString content, string action = "#") : base(html)
        {
            Content(content);
            Action(action);
        }

        public IHtmlString  Content()                               { return _content; }
        public LinkButton   Content(string content)                 { return Content(MvcHtmlString.Create(content)); }
        public LinkButton   Content(IHtmlString content)            { _content = content; return this; }

        public string       Action()                                { return _action; }
        public LinkButton   Action(string action)                   { _action = action; return this; }

        public bool         CausesNoPjax()                          { return _noPjax; }
        public LinkButton   NoPjax(bool noPjax = true)              { _noPjax = noPjax; return this; }

        public bool         IsModal()                               { return _modal; }
        public LinkButton   Modal(bool modal = true)                { _modal = modal; return this; }

        public bool         IsModalReturn()                         { return _modalReturn; }
        public LinkButton   ModalReturn(bool modalReturn = true)    { _modalReturn = modalReturn; return this; }

        public string       DefaultModalReturn()                    { return _defaultModalReturn; }
        public LinkButton   DefaultModalReturn(string action)       { _defaultModalReturn = action; return this; }

        protected override HtmlTag CreateTag()
        {
            var request = Html.ViewContext.HttpContext.Request;
            var url = Url.Content(_action);

            if (_modalReturn)
            {
                url = request.QueryString["modalReturnUrl"];

                if (string.IsNullOrWhiteSpace(url))
                    url = Url.Content(_defaultModalReturn ?? "#");
            }

            if (_modal)
            {
                var returnUrl = request.Url.PathAndQuery;
                url += "?modalReturnUrl=" + HttpUtility.UrlEncode(returnUrl);
            }

            var tag = new HtmlTag("a")
                .Attr("href", url)
                .Text(_content.ToHtmlString()).Encoded(false);

            if (_modalReturn)
                tag.Attr("data-close-dialog", "false");

            if (_modal)
                tag.Attr("data-modal-dialog", "true");

            if (_noPjax)
                tag.Attr("data-nopjax", "true");

            return tag;
        }
    }
}
