using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeHtmlHelper
    {
        public static FakeHtmlHelper<object> New()
        {
            return New<object>(null);
        }

        public static FakeHtmlHelper<T> New<T>(T model)
        {
            return new FakeHtmlHelper<T>(model);
        }
    }

    public class FakeHtmlHelper<T> : IHtmlHelper<T>, IViewContextAware
    {
        public IModelMetadataProvider   MetadataProvider    { get; protected set; }

        public ViewDataDictionary<T>    ViewData            { get; protected set; }

        public FakeViewContext          FakeViewContext     { get; protected set; }

        public ViewContext              ViewContext         => FakeViewContext;

        public FakeHtmlHelper(T model)
        {
            FakeViewContext = new FakeViewContext();

            var detailsProvider = new DefaultCompositeMetadataDetailsProvider(new List<IMetadataDetailsProvider>());
            MetadataProvider = new DefaultModelMetadataProvider(detailsProvider);
            var viewDataDictionary = new ViewDataDictionary(MetadataProvider, new ModelStateDictionary());
            ViewData = new ViewDataDictionary<T>(viewDataDictionary);
            ViewData.Model = model;
        }

        public FakeHtmlHelper<T> SetRawUrl(string url)
        {
            FakeViewContext.FakeHttpContext.FakeHttpRequest.SetRawUrl(url);
            return this;
        }

        public void Contextualize(ViewContext viewContext)
        {
            FakeViewContext.Contextualize(viewContext);
        }

        #region NotImplemented

        public Html5DateRenderingMode Html5DateRenderingMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string IdAttributeDotReplacement => throw new NotImplementedException();

        public dynamic ViewBag => throw new NotImplementedException();

        ViewDataDictionary IHtmlHelper.ViewData => throw new NotImplementedException();

        public ITempDataDictionary TempData => throw new NotImplementedException();

        public UrlEncoder UrlEncoder => throw new NotImplementedException();

        public IHtmlContent CheckBoxFor(Expression<Func<T, bool>> expression, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent DisplayFor<TResult>(Expression<Func<T, TResult>> expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            throw new NotImplementedException();
        }

        public string DisplayNameFor<TResult>(Expression<Func<T, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public string DisplayNameForInnerType<TModelItem, TResult>(Expression<Func<TModelItem, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public string DisplayTextFor<TResult>(Expression<Func<T, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent DropDownListFor<TResult>(Expression<Func<T, TResult>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent EditorFor<TResult>(Expression<Func<T, TResult>> expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            throw new NotImplementedException();
        }

        public string Encode(object value)
        {
            throw new NotImplementedException();
        }

        public string Encode(string value)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent HiddenFor<TResult>(Expression<Func<T, TResult>> expression, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public string IdFor<TResult>(Expression<Func<T, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent LabelFor<TResult>(Expression<Func<T, TResult>> expression, string labelText, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ListBoxFor<TResult>(Expression<Func<T, TResult>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public string NameFor<TResult>(Expression<Func<T, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent PasswordFor<TResult>(Expression<Func<T, TResult>> expression, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent RadioButtonFor<TResult>(Expression<Func<T, TResult>> expression, object value, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Raw(object value)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Raw(string value)
        {
            return new HtmlString(value);
        }

        public IHtmlContent TextAreaFor<TResult>(Expression<Func<T, TResult>> expression, int rows, int columns, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent TextBoxFor<TResult>(Expression<Func<T, TResult>> expression, string format, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ValidationMessageFor<TResult>(Expression<Func<T, TResult>> expression, string message, object htmlAttributes, string tag)
        {
            throw new NotImplementedException();
        }

        public string ValueFor<TResult>(Expression<Func<T, TResult>> expression, string format)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ActionLink(string linkText, string actionName, string controllerName, string protocol, string hostname, string fragment, object routeValues, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent AntiForgeryToken()
        {
            throw new NotImplementedException();
        }

        public MvcForm BeginForm(string actionName, string controllerName, object routeValues, FormMethod method, bool? antiforgery, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public MvcForm BeginRouteForm(string routeName, object routeValues, FormMethod method, bool? antiforgery, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent CheckBox(string expression, bool? isChecked, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Display(string expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            throw new NotImplementedException();
        }

        public string DisplayName(string expression)
        {
            throw new NotImplementedException();
        }

        public string DisplayText(string expression)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent DropDownList(string expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Editor(string expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            throw new NotImplementedException();
        }

        public void EndForm()
        {
            throw new NotImplementedException();
        }

        public string FormatValue(object value, string format)
        {
            throw new NotImplementedException();
        }

        public string GenerateIdFromName(string fullName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : struct
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetEnumSelectList(Type enumType)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Hidden(string expression, object value, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public string Id(string expression)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Label(string expression, string labelText, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ListBox(string expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public string Name(string expression)
        {
            throw new NotImplementedException();
        }

        public Task<IHtmlContent> PartialAsync(string partialViewName, object model, ViewDataDictionary viewData)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Password(string expression, object value, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent RadioButton(string expression, object value, bool? isChecked, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public Task RenderPartialAsync(string partialViewName, object model, ViewDataDictionary viewData)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent RouteLink(string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent TextArea(string expression, string value, int rows, int columns, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent TextBox(string expression, object value, string format, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ValidationMessage(string expression, string message, object htmlAttributes, string tag)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ValidationSummary(bool excludePropertyErrors, string message, object htmlAttributes, string tag)
        {
            throw new NotImplementedException();
        }

        public string Value(string expression, string format)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
