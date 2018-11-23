﻿using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace MvcForms.Forms
{
    public class PropertyContext
    {
        public static PropertyContext New<T, P>(IHtmlHelper<T> helper, Expression<Func<T, P>> property)
        {
            var propertyName = ExpressionHelper.GetExpressionText(property);
            var name = helper.ViewData.TemplateInfo.GetFullHtmlFieldName(propertyName);
            var id = TagBuilder.CreateSanitizedId(name, "_");
            var metadata = (ModelMetadata)null;//.FromLambdaExpression(property, helper.ViewData); todo - fix for core
            var value = RenderValue(helper.ViewData.ModelState, name, metadata);
            var modelState = helper.ViewData.ModelState;
            var propertyModelState = modelState[name];

            return new PropertyContext
            {
                Name        = name,
                Id          = id,
                Value       = value,
                Metadata    = metadata,
                ModelState  = propertyModelState,
            };
        }

        protected PropertyContext() { }

        public string           Id;
        public string           Name;
        public string           Value;
        public ModelMetadata    Metadata;
        public ModelStateEntry  ModelState;

        public static string RenderValue(ModelStateDictionary modelState, string name, ModelMetadata metadata)
        {
            return ModelStateValue(modelState, name) ?? ModelValue(metadata);
        }

        public static string ModelStateValue(ModelStateDictionary modelState, string name)
        {
            if (!modelState.ContainsKey(name))
                return null;

            var value = modelState[name];

            if (value == null)
                return null;

            return value.AttemptedValue;
        }

        public static string ModelValue(ModelMetadata metadata)
        {
            return Convert.ToString("todo - no idea what this was doing before");// metadata.Model);
        }
    }
}
