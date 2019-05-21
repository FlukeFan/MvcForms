﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace MvcForms.Forms
{
    public static class FormsExtensions
    {
        public static ScopedHtmlHelper<TPostModel> ForModelScope<TViewModel, TPostModel>(this IHtmlHelper<TViewModel> helper, TPostModel postModel)
        {
            var newHelper = helper.ForModel(postModel);
            return new ScopedHtmlHelper<TPostModel>(newHelper);
        }

        public static IHtmlHelper<TPostModel> ForModel<TViewModel, TPostModel>(this IHtmlHelper<TViewModel> helper, TPostModel postModel)
        {
            var viewContext = helper.ViewContext;
            var postModelDataDictionary = new ViewDataDictionary<TPostModel>(viewContext.ViewData, postModel);
            var postModelContext = new ViewContext(viewContext, viewContext.View, postModelDataDictionary, viewContext.Writer);

            var serviceProvider = viewContext.HttpContext.RequestServices;
            var value = serviceProvider.GetRequiredService(typeof(IHtmlHelper<TPostModel>));

            (value as IViewContextAware)?.Contextualize(postModelContext);
            return (IHtmlHelper<TPostModel>)value;
        }

        public static Form<TViewModel> Form<TViewModel>(this IHtmlHelper<TViewModel> helper)
        {
            return helper.FormFor(helper.ViewData.Model);
        }

        public static Form<TPostModel> FormFor<TViewModel, TPostModel>(this IHtmlHelper<TViewModel> helper, TPostModel postModel)
        {
            var newHelper = helper.ForModel(postModel);
            return new Form<TPostModel>(newHelper);
        }

        public static Button ButtonSubmit<TViewModel>(this IHtmlHelper<TViewModel> helper, string content)
        {
            return helper.ButtonSubmit(new HtmlString(HttpUtility.HtmlEncode(content)));
        }

        public static Button ButtonSubmit<TViewModel>(this IHtmlHelper<TViewModel> helper, IHtmlContent content)
        {
            return new Button(helper, "submit", content);
        }

        public static ScopedHtmlHelper<T> FormGroupLayout<T>(this IHtmlHelper<T> helper)
        {
            var formGroupLayout = new FormGroupLayout(helper);
            return formGroupLayout.Begin<T>();
        }

        public static ErrorSummary ErrorSummary<T>(this IHtmlHelper<T> helper)
        {
            return new ErrorSummary(helper);
        }

        #region InputHidden

        public static Input InputHidden<TModel, TValue>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> property)
        {
            var propertyContext = PropertyContext.New<TModel, TValue>(helper, property);
            return new Input(helper, "hidden", propertyContext);
        }

        #endregion

        #region InputText

        public static Input InputText<T>(this IHtmlHelper<T> helper, Expression<Func<T, string>> property)
        {
            var propertyContext = PropertyContext.New(helper, property);
            return new Input(helper, "text", propertyContext);
        }

        #endregion

        #region InputNumber

        public static Input InputNumber<T>(this IHtmlHelper<T> helper, Expression<Func<T, string>> property)
        {
            var propertyContext = PropertyContext.New(helper, property);
            return new Input(helper, "number", propertyContext);
        }

        public static Input InputNumber<T>(this IHtmlHelper<T> helper, Expression<Func<T, int>> property)
        {
            var propertyContext = PropertyContext.New(helper, property);
            return new Input(helper, "number", propertyContext);
        }

        public static Input InputNumber<T>(this IHtmlHelper<T> helper, Expression<Func<T, int?>> property)
        {
            var propertyContext = PropertyContext.New(helper, property);
            return new Input(helper, "number", propertyContext);
        }

        #endregion

        #region Select

        public static FormGroup<Select> LabelledSelect<T>(this IHtmlHelper<T> helper, string label, Expression<Func<T, string>> property, IDictionary<string, string> values)
        {
            return LabelledControl(helper, label, property, ctx => new Select(helper, ctx.Property));
        }

        #endregion

        public delegate TControl ControlFactory<TControl>(GroupContext groupContext);

        public static FormGroup<TControl> FormGroup<TModel, TControl>(this IHtmlHelper<TModel> helper, string labelText, Func<IHtmlHelper<TModel>, TControl> controlFactory)
            where TControl : IPropertyControl
        {
            var control = controlFactory(helper);
            var propertyContext = control.PropertyContext;
            var groupContext = GroupContext.New(helper, propertyContext, labelText);
            var formGroup = new FormGroup<TControl>(helper, groupContext, control);
            return formGroup;
        }

        public static FormGroup<TControl> LabelledControl<TModel, TProperty, TControl>(this IHtmlHelper<TModel> helper, string labelText, Expression<Func<TModel, TProperty>> property, ControlFactory<TControl> controlFactory)
            where TControl : Control
        {
            var propertyContext = PropertyContext.New(helper, property);

            var groupContext = GroupContext.New(helper, propertyContext, labelText);

            var control = controlFactory(groupContext);

            var formGroup = new FormGroup<TControl>(helper, groupContext, control);

            return formGroup;
        }
    }
}
