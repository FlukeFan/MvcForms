using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace MvcForms
{
    public static class ReflectionHelper
    {
        private static IDictionary<Type, PropertyInfo> _cachedViewData = new Dictionary<Type, PropertyInfo>();
        //private static IDictionary<Type, PropertyInfo> _cachedViewData = new Dictionary<Type, PropertyInfo>();

        public static string GetExpressionText(this LambdaExpression propertyLambda)
        {
            if (propertyLambda.Body.NodeType == ExpressionType.Convert)
                propertyLambda = LambdaExpression.Lambda((propertyLambda.Body as UnaryExpression).Operand);

            return ExpressionHelper.GetExpressionText(propertyLambda);
        }

        public static ViewDataDictionary GenericViewData(this HtmlHelper helper)
        {
            var helperType = helper.GetType();

            if (!_cachedViewData.ContainsKey(helperType))
                lock(_cachedViewData)
                    if(!_cachedViewData.ContainsKey(helperType))
                    {
                        var viewDataProperty = FindFirstProperty(helperType, "ViewData");
                        _cachedViewData.Add(helperType, viewDataProperty);
                    }

            var viewData = _cachedViewData[helperType].GetValue(helper, null);
            return (ViewDataDictionary)viewData;
        }

        public static dynamic GenericViewBag(this HtmlHelper helper)
        {
            var helperType = helper.GetType();
            var viewBagProperty = FindFirstProperty(helperType, "ViewBag");
            var viewBag = viewBagProperty.GetValue(helper, null);
            return viewBag;
        }

        public static PropertyInfo FindFirstProperty(Type type, string propertyName)
        {
            PropertyInfo property = null;

            do
            {
                property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                type = type.BaseType;
            }
            while (type != null && property == null);

            return property;
        }
    }
}
