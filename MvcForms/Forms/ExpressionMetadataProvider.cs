using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MvcForms.Forms
{
    public class ExpressionMetadataProvider
    {
        private static MethodInfo _fromLambdaExpression;

        public static ModelExplorer FromLambdaExpression<TModel, TResult>(
            Expression<Func<TModel, TResult>> expression,
            ViewDataDictionary<TModel> viewData,
            IModelMetadataProvider metadataProvider)
        {
            if (_fromLambdaExpression == null)
            {
                var type = typeof(ModelExpressionProvider).Assembly
                    .GetTypes()
                    .Single(t => t.Name == "ExpressionMetadataProvider");

                _fromLambdaExpression = type.GetMethod("FromLambdaExpression");
            }

            var method = _fromLambdaExpression.MakeGenericMethod(typeof(TModel), typeof(TResult));
            var modelExplorer = method.Invoke(null, new object[] { expression, viewData, metadataProvider });

            return (ModelExplorer)modelExplorer;
        }
    }
}
