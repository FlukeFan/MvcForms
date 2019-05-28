using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace MvcForms.Forms
{
    public class PropertyContext
    {
        public static PropertyContext New<T, P>(IHtmlHelper<T> helper, Expression<Func<T, P>> property, bool isList = false)
        {
            var propertyName = ExpressionHelper.GetExpressionText(property);
            var name = helper.ViewData.TemplateInfo.GetFullHtmlFieldName(propertyName);
            var id = TagBuilder.CreateSanitizedId(name, "_");
            var explorer = ExpressionMetadataProvider.FromLambdaExpression(property, helper.ViewData, helper.MetadataProvider);
            var values = RenderValue(helper.ViewData.ModelState, name, explorer, isList);
            var modelState = helper.ViewData.ModelState;
            var propertyModelState = modelState[name];

            return new PropertyContext
            {
                Name        = name,
                Id          = id,
                Values      = values,
                Value       = isList ? null : values.SingleOrDefault(),
                Explorer    = explorer,
                ModelState  = propertyModelState,
            };
        }

        protected PropertyContext() { }

        public string           Id;
        public string           Name;
        public string           Value;
        public string[]         Values;
        public ModelExplorer    Explorer;
        public ModelStateEntry  ModelState;

        public static string[] RenderValue(ModelStateDictionary modelState, string name, ModelExplorer explorer, bool isList)
        {
            return ModelStateValue(modelState, name, isList) ?? ModelValue(explorer, isList);
        }

        public static string[] ModelStateValue(ModelStateDictionary modelState, string name, bool isList)
        {
            if (!modelState.ContainsKey(name))
                return null;

            var value = modelState[name];

            if (value == null)
                return null;

            if (isList)
            {
                var enumerable = value.RawValue as IEnumerable;

                if (enumerable != null && !(enumerable is string))
                    return enumerable.Cast<object>().Select(o => Convert.ToString(o)).ToArray();
            }

            return new string[] { value.AttemptedValue };
        }

        public static string[] ModelValue(ModelExplorer explorer, bool isList)
        {
            if (!isList)
                return new string[] { Convert.ToString(explorer.Model) };
            else
                return ((explorer.Model ?? new string[0]) as IEnumerable).Cast<object>().Select(o => Convert.ToString(o)).ToArray();
        }
    }
}
