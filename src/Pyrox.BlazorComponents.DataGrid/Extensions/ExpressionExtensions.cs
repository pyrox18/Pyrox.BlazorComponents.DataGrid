using System;
using System.Linq.Expressions;

namespace Pyrox.BlazorComponents.DataGrid.Extensions
{
    internal static class ExpressionExtensions
    {
        // Reference:
        // https://stackoverflow.com/questions/671968/retrieving-property-name-from-lambda-expression
        internal static string GetMemberName<T>(this Expression<T> expression)
        {
            switch (expression.Body)
            {
                case MemberExpression m:
                    return m.Member.Name;
                case UnaryExpression u when u.Operand is MemberExpression m:
                    return m.Member.Name;
                default:
                    throw new NotImplementedException(expression.GetType().ToString());
            }
        }
    }
}
