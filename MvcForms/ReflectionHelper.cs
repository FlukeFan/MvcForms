using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MvcForms
{
    public static class ReflectionHelper
    {
        private static IDictionary<Type, PropertyInfo> _cachedViewData = new Dictionary<Type, PropertyInfo>();

        /// <summary> TODO: find out if this is still needed in core (in previous versions of MVC, the reference from the IHtmlHelper was different from the IHtmlHelper&lt;T&gt;) </summary>
        public static ViewDataDictionary GenericViewData(this IHtmlHelper helper)
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

        /// <summary> TODO: find out if this is still needed in core (in previous versions of MVC, the reference from the IHtmlHelper was different from the IHtmlHelper&lt;T&gt;) </summary>
        public static dynamic GenericViewBag(this IHtmlHelper helper)
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
