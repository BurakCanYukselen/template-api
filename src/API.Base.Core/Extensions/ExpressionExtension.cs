using System;
using System.Linq.Expressions;
using AutoMapper.Internal;

namespace API.Base.Core.Extensions
{
    public static class ExpressionExtension
    {
        public static ParameterExpression GetParameterExpression<T>(this string parameterName)
        {
            var parameter = Expression.Parameter(typeof(T), parameterName);
            return parameter;
        }
        
        public static Expression<Func<T, bool>> ResetLambdaExpressionParameter<T>(this Expression<Func<T, bool>> replacedExpression, ParameterExpression expressionParameter)
        {
            replacedExpression = Expression.Lambda<Func<T, bool>>(replacedExpression.ReplaceParameters(expressionParameter), expressionParameter);
            return replacedExpression;
        }
        
        public static Expression<Func<T, bool>> ConvertLambdaExpression<T>(this BinaryExpression expression, ParameterExpression expressionParameter)
        {
            var lambdaExpression = Expression.Lambda<Func<T, bool>>(expression, expressionParameter);
            return lambdaExpression;
        }
    }
}