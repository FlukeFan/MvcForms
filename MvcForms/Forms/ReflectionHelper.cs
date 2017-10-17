using System.Linq.Expressions;
using System.Web.Mvc;

namespace MvcForms.Forms
{
    public static class ReflectionHelper
    {
        public static string GetExpressionText(this LambdaExpression propertyLambda)
        {
            if (propertyLambda.Body.NodeType == ExpressionType.Convert)
                propertyLambda = LambdaExpression.Lambda((propertyLambda.Body as UnaryExpression).Operand);

            return ExpressionHelper.GetExpressionText(propertyLambda);
        }
    }
}
