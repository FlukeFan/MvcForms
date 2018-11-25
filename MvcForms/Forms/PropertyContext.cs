using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
            var explorer = ExpressionMetadataProvider.FromLambdaExpression(property, helper.ViewData, helper.MetadataProvider);
            var value = RenderValue(helper.ViewData.ModelState, name, explorer);
            var modelState = helper.ViewData.ModelState;
            var propertyModelState = modelState[name];

            return new PropertyContext
            {
                Name        = name,
                Id          = id,
                Value       = value,
                Explorer    = explorer,
                ModelState  = propertyModelState,
            };
        }

        protected PropertyContext() { }

        public string           Id;
        public string           Name;
        public string           Value;
        public ModelExplorer    Explorer;
        public ModelStateEntry  ModelState;

        public static string RenderValue(ModelStateDictionary modelState, string name, ModelExplorer explorer)
        {
            return ModelStateValue(modelState, name) ?? ModelValue(explorer);
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

        public static string ModelValue(ModelExplorer explorer)
        {
            return Convert.ToString(explorer.Model);
        }
    }
}
