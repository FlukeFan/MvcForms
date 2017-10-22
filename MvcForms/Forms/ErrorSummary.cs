using System.Collections.Generic;
using System.Web.Mvc;
using HtmlTags;

namespace MvcForms.Forms
{
    public interface IRenderedErrorSummary
    {
        IList<HtmlTag> Errors { get; }
    }

    public class ErrorSummary : Control, IRenderedErrorSummary
    {
        // post-render members
        private IList<HtmlTag> _errorTags = new List<HtmlTag>();

        public ErrorSummary(HtmlHelper html) : base(html)
        {
        }

        IList<HtmlTag> IRenderedErrorSummary.Errors => _errorTags;

        protected override HtmlTag CreateTag()
        {
            var formErrors = new List<KeyedError>();
            var propertyErrors = new List<KeyedError>();

            foreach (var key in Html.ViewData.ModelState.Keys)
            {
                var value = Html.ViewData.ModelState[key];

                foreach (var error in value.Errors)
                {
                    var keyedError = new KeyedError { Key = key, Error = error };

                    if (string.IsNullOrEmpty(key))
                        formErrors.Add(keyedError);
                    else
                        propertyErrors.Add(keyedError);
                }
            }

            var errors = new Errors
            {
                FormErrors = formErrors,
                PropertyErrors = propertyErrors,
            };

            return CreateTag(errors);
        }

        protected virtual HtmlTag CreateTag(Errors errors)
        {
            foreach (var formError in errors.FormErrors)
            {
                var errorTag = new HtmlTag("div").Text(formError.Error.ErrorMessage);
                _errorTags.Add(errorTag);
            }

            return new HtmlTag("div").Append(_errorTags);
        }

        public class Errors
        {
            public IList<KeyedError>    FormErrors;
            public IList<KeyedError>    PropertyErrors;
        }

        public class KeyedError
        {
            public string       Key;
            public ModelError   Error;
        }
    }
}
