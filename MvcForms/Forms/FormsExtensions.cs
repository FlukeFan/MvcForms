using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MvcForms.Forms
{
    public static class FormsExtensions
    {
        public static ScopedHtmlHelper<TPostModel> ForModelScope<TViewModel, TPostModel>(this HtmlHelper<TViewModel> helper, TPostModel postModel)
        {
            var newHelper = helper.ForModel(postModel);
            return new ScopedHtmlHelper<TPostModel>(newHelper);
        }

        public static HtmlHelper<TPostModel> ForModel<TViewModel, TPostModel>(this HtmlHelper<TViewModel> helper, TPostModel postModel)
        {
            var viewData = new ViewDataDictionary(helper.ViewData);
            viewData.Model = postModel;
            var data = new ViewDataContainer { ViewData = viewData };
            var newHelper = new HtmlHelper<TPostModel>(helper.ViewContext, data);
            return newHelper;
        }

        public static Form<TViewModel> Form<TViewModel>(this HtmlHelper<TViewModel> helper)
        {
            return helper.FormFor(helper.ViewData.Model);
        }

        public static Form<TPostModel> FormFor<TViewModel, TPostModel>(this HtmlHelper<TViewModel> helper, TPostModel postModel)
        {
            var newHelper = helper.ForModel(postModel);
            return new Form<TPostModel>(newHelper);
        }

        public static Button ButtonSubmit<TViewModel>(this HtmlHelper<TViewModel> helper, string content)
        {
            return helper.ButtonSubmit(MvcHtmlString.Create(HttpUtility.HtmlEncode(content)));
        }

        public static Button ButtonSubmit<TViewModel>(this HtmlHelper<TViewModel> helper, IHtmlString content)
        {
            return new Button(helper, "submit", content);
        }

        private class ViewDataContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData { get; set; }
        }

        public static InputText InputText<T>(this HtmlHelper<T> helper, Expression<Func<T, string>> property)
        {
            var propertyContext = FindPropertyContext(helper, property);
            return new InputText(helper, propertyContext);
        }

        public static FormRow<InputText> LabelledInputText<T>(this HtmlHelper<T> helper, string label, Expression<Func<T, string>> property)
        {
            return LabelledControl(helper, label, property, ctx => new InputText(helper, ctx.Property));
        }

        public static PropertyContext FindPropertyContext<T, P>(HtmlHelper<T> helper, Expression<Func<T, P>> property)
        {
            var propertyName = property.GetExpressionText();
            var name = helper.ViewData.TemplateInfo.GetFullHtmlFieldName(propertyName);
            var id = TagBuilder.CreateSanitizedId(name);

            return new PropertyContext
            {
                Name = name,
                Id = id,
            };
        }

        public delegate TControl ControlFactory<TControl>(ControlContext controlContext);

        public static FormRow<TControl> LabelledControl<TModel, TProperty, TControl>(this HtmlHelper<TModel> helper, string labelText, Expression<Func<TModel, TProperty>> property, ControlFactory<TControl> controlFactory)
            where TControl : Control
        {
            var propertyContext = FindPropertyContext(helper, property);

            var controlContext = new ControlContext
            {
                Property = propertyContext,
            };

            var control = controlFactory(controlContext);

            var formRow = new FormRow<TControl>(helper, labelText, control);

            return formRow;
        }
    }
}
